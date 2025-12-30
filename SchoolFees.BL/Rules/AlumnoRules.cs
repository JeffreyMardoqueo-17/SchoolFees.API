
using SchoolFees.EN.Exceptions;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Rules
{
    public static class AlumnoRules
    {
        public static void PuedeSerCreado(Alumno alumno)
        {
            if (alumno == null)
                throw new BusinessException("Alumno requerido.");

            if (string.IsNullOrWhiteSpace(alumno.Nombres))
                throw new BusinessException("Los nombres son obligatorios.");

            if (string.IsNullOrWhiteSpace(alumno.Apellidos))
                throw new BusinessException("Los apellidos son obligatorios.");

            if (alumno.FechaNacimiento == null)
                throw new BusinessException("La fecha de nacimiento es obligatoria.");
        }

        public static void PuedeSerAsignadoAGrupo(int alumnoId, int grupoId)
        {
            if (alumnoId <= 0)
                throw new BusinessException("Alumno inválido.");

            if (grupoId <= 0)
                throw new BusinessException("Grupo inválido.");
        }

        public static void PuedeSerActualizado(Alumno alumno)
        {
            if (!alumno.Estado)
                throw new BusinessException("No se puede modificar un alumno inactivo.");
        }
    }
}
