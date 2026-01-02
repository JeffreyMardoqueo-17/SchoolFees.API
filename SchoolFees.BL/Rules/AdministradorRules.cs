using System;
using SchoolFees.EN.Exceptions;

namespace SchoolFees.BL.Rules
{
    public static class AdministradorRules
    {
        // ============================
        // 🔒 CONSTANTES DEL SISTEMA
        // ============================

        public const int MAX_ADMINS_POR_ROL = 2;
        public const int MAX_INTENTOS_FALLIDOS = 5;
        public static readonly TimeSpan TIEMPO_BLOQUEO = TimeSpan.FromMinutes(15);

        // ============================
        // 🧱 A. ESTRUCTURA
        // ============================

        public static void ValidarAdministradorActivo(bool activo)
        {
            if (!activo)
                throw new BusinessException("El administrador no está activo.");
        }

        public static void ValidarRolAsignado(bool tieneRoles)
        {
            if (!tieneRoles)
                throw new BusinessException(
                    "El administrador debe tener al menos un rol asignado.");
        }

        public static void ValidarCupoRol(int adminsActivosEnRol)
        {
            if (adminsActivosEnRol >= MAX_ADMINS_POR_ROL)
                throw new BusinessException(
                    "El cupo para este rol ya está completo.");
        }

        public static void ValidarUltimoSuperAdmin(int superAdminsActivos)
        {
            if (superAdminsActivos <= 1)
                throw new BusinessException(
                    "El sistema no puede quedar sin un SUPER ADMIN activo.");
        }

        // ============================
        // 👤 B. CREACIÓN
        // ============================

        public static void ValidarCreacionAdministrador(
            bool creadorEsSuperAdmin,
            bool correoExiste)
        {
            if (!creadorEsSuperAdmin)
                throw new BusinessException(
                    "Solo un SUPER ADMIN activo puede crear administradores.");

            if (correoExiste)
                throw new BusinessException(
                    "El correo electrónico ya está registrado en el sistema.");
        }

        public static void ValidarPasswordCreacion(
            string passwordHash,
            string salt)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new BusinessException("La contraseña debe estar hasheada.");

            if (string.IsNullOrWhiteSpace(salt))
                throw new BusinessException("El salt es obligatorio.");

            if (passwordHash.Length < 20)
                throw new BusinessException("Formato de contraseña inválido.");
        }

        // ============================
        // 🔐 C. AUTENTICACIÓN
        // ============================

        public static void ValidarLogin(
            bool activo,
            DateTime? bloqueadoHasta)
        {
            if (!activo)
                throw new BusinessException("Credenciales inválidas.");

            if (bloqueadoHasta.HasValue &&
                bloqueadoHasta.Value > DateTime.UtcNow)
                throw new BusinessException("Credenciales inválidas.");
        }

        public static bool DebeBloquearse(int intentosFallidos)
            => intentosFallidos >= MAX_INTENTOS_FALLIDOS;

        public static DateTime CalcularBloqueo()
            => DateTime.UtcNow.Add(TIEMPO_BLOQUEO);

        // ============================
        // 🔁 D. CAMBIO DE CONTRASEÑA
        // ============================

        public static void ValidarCambioPassword(bool passwordActualCorrecto)
        {
            if (!passwordActualCorrecto)
                throw new BusinessException(
                    "La contraseña actual es incorrecta.");
        }

        // ============================
        // 🚫 E. DESACTIVACIÓN / ROLES
        // ============================

        public static void ValidarDesactivacionSuperAdmin(
            int superAdminsActivos)
        {
            ValidarUltimoSuperAdmin(superAdminsActivos);
        }

        public static void ValidarAsignacionRol(bool adminActivo)
        {
            if (!adminActivo)
                throw new BusinessException(
                    "No se pueden asignar roles a un administrador inactivo.");
        }
    }
}
