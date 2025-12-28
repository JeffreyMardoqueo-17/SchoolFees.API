using SchoolFees.EN.models;

namespace SchoolFees.BL.Rules
{
    public static class AlumnoRules
    {
        public static void PuedeSerCreado(Alumno alumno)
        {
            if (alumno.GradoId <= 0)
                throw new BusinessException("El alumno debe pertenecer a un grado.");
        }

        public static void PuedeSerActualizado(Alumno alumno)
        {
            if (!alumno.Estado)
                throw new BusinessException("No se puede modificar un alumno inactivo.");
        }
    }
}
