using SchoolFees.EN.Exceptions;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Validators
{
    /// <summary>
    /// Validador de reglas de negocio que dependen del estado del sistema
    /// o de información persistida en base de datos.
    /// </summary>
    public class AlumnoBusinessValidator
    {
        private readonly IGradoRepository _gradoRepository;
        private readonly IAlumnoRepository _alumnoRepository;

        public AlumnoBusinessValidator(
            IGradoRepository gradoRepository,
            IAlumnoRepository alumnoRepository)
        {
            _gradoRepository = gradoRepository;
            _alumnoRepository = alumnoRepository;
        }

        /// <summary>
        /// Valida las reglas necesarias para crear un alumno.
        /// </summary>
        public async Task ValidarCreacionAsync(Alumno alumno)
        {
            // // 🔹 El grado debe existir
            // var grado = await _gradoRepository.GetByIdGradoAsync(alumno.GradoId);
            // if (grado == null)
            //     throw new BusinessException("El grado asignado no existe.");

            // 🔹 Fecha de nacimiento válida
            if (alumno.FechaNacimiento == default)
                throw new BusinessException("La fecha de nacimiento es obligatoria.");

            if (alumno.FechaNacimiento > DateTime.Today)
                throw new BusinessException("La fecha de nacimiento no puede ser futura.");

            // 🔹 No permitir alumnos duplicados (ejemplo básico)
            var existe = await _alumnoRepository
                .ExistePosibleDuplicadoAsync(
                    alumno.Nombres,
                    alumno.Apellidos,
                    alumno.FechaNacimiento);

            if (existe)
                throw new BusinessException(
                    "Ya existe un alumno registrado con los mismos datos.");
        }

        /// <summary>
        /// Valida si un alumno puede ser activado.
        /// </summary>
        public void ValidarActivacion(Alumno alumno)
        {
            if (string.IsNullOrWhiteSpace(alumno.CodigoAlumno))
                throw new BusinessException(
                    "No se puede activar un alumno sin código institucional.");
        }
    }
}
