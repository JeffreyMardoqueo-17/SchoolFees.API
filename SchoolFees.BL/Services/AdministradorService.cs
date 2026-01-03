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
        public async Task CrearAsync( Administrador administrador,IEnumerable<int> rolesIds,int creadoPor)
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
            // tu repository aún NO lo soporta

            // 5️⃣ Persistencia
            await _administradorRepository.CreateAsync(administrador);
        }

    }
}