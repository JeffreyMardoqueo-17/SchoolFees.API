using SchoolFees.EN.Exceptions;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Rules
{
    public static class RolRules
    {
        public static void ValidarCreacion(Rol rol)
        {
            if (rol == null)
                throw new BusinessException("El rol es obligatorio.");

            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new BusinessException("El nombre del rol es obligatorio.");

            if (rol.Nombre.Length < 3)
                throw new BusinessException("El nombre del rol es demasiado corto.");
        }

        public static void ValidarModificacion(Rol rol)
        {
            if (rol == null || rol.Id <= 0)
                throw new BusinessException("Rol inválido.");
        }

        public static void ValidarDesactivacion(Rol rol)
        {
            if (!rol.Estado)
                throw new BusinessException("No se puede desactivar un rol del sistema.");
        }
    }
}
