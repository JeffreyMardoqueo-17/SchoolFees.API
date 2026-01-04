using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.BL.Interfaces;
using SchoolFees.BL.Rules;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.Exceptions;
using SchoolFees.EN.models;
using SchoolFees.BL.Security;
namespace SchoolFees.BL.Services
{
    public class AdministradorService : IAdministradorService
    {
        private readonly IAdministradorRepository _administradorRepository;
        public AdministradorService(IAdministradorRepository administradorRepository)
        {
            _administradorRepository = administradorRepository;
        }
        public async Task<IEnumerable<Administrador>> GetAllAsync()
        {
            return await _administradorRepository.GetAllAsync();
        }
        public async Task<Administrador> GetByIdAsync(int id)
        {
            var administrador = await _administradorRepository.GetByIdAsync(id);
            if (administrador == null)
            {
                throw new BusinessException("Administrador no encontrado.");
            }
            return administrador;
        }
        public async Task CreateAsync(Administrador administrador, IEnumerable<int> rolesIds, int creadoPor)
        {
            // 1 Validaciones básicas
            if (administrador == null)
                throw new BusinessException("Administrador requerido.");

            if (rolesIds == null || !rolesIds.Any())
                throw new BusinessException("Debe asignarse al menos un rol.");

            // 2 Validar correo único
            var existente = await _administradorRepository
                .GetByCorreoAsync(administrador.Correo);

            if (existente != null)
                throw new BusinessException(
                    "El correo electrónico ya está registrado en el sistema.");

            // 3 Hash de contraseña (Argon2)
            var (hash, salt) = PasswordHasher
                .HashPassword(administrador.PasswordHash);

            AdministradorRules.ValidarPasswordCreacion(hash, salt);

            administrador.PasswordHash = hash;
            administrador.PasswordSalt = salt;

            // 4️⃣ Estado inicial obligatorio
            administrador.Estado = true;
            administrador.IntentosFallidos = 0;
            administrador.BloqueadoHasta = null;
            administrador.FechaCreacion = DateTime.UtcNow;
            administrador.CreadoPor = creadoPor;

            // ⚠️ NOTA IMPORTANTE:
            // Aquí NO se asignan roles porque
            // el repository aún NO lo soporta

            // 5️⃣ Persistencia
            await _administradorRepository.CreateAsync(administrador);
        }

        public async Task<Administrador> LoginAsync(string correo, string password, string ip)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password))
                throw new BusinessException("Credenciales inválidas.");

            var admin = await _administradorRepository
                .GetByCorreoAsync(correo);

            // ❗ NUNCA se dice si el correo existe
            if (admin == null)
                throw new BusinessException("Credenciales inválidas.");

            if (!admin.Estado)
                throw new BusinessException("Usuario deshabilitado.");

            // ⛔ Bloqueo por intentos
            if (admin.BloqueadoHasta.HasValue &&
                admin.BloqueadoHasta > DateTime.UtcNow)
            {
                throw new BusinessException(
                    $"Cuenta bloqueada hasta {admin.BloqueadoHasta:HH:mm}."
                );
            }

            // 🔐 Verificación de contraseña
            bool passwordOk = PasswordHasher.VerifyPassword(
                password,
                admin.PasswordHash,
                admin.PasswordSalt
            );

            if (!passwordOk)
            {
                admin.IntentosFallidos++;

                // 🔥 Política: 5 intentos → bloqueo 15 min
                if (admin.IntentosFallidos >= 5)
                {
                    admin.BloqueadoHasta = DateTime.UtcNow.AddMinutes(15);
                    admin.IntentosFallidos = 0;
                }

                await _administradorRepository.UpdateAsync(admin);

                throw new BusinessException("Credenciales inválidas.");
            }

            // ✅ LOGIN CORRECTO
            admin.IntentosFallidos = 0;
            admin.BloqueadoHasta = null;
            admin.UltimoLogin = DateTime.UtcNow;
            admin.UltimaIP = ip;

            await _administradorRepository.UpdateAsync(admin);

            return admin;
        }


    }
}