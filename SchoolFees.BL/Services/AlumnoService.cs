using SchoolFees.BL.Exceptions;
using SchoolFees.BL.Interfaces;
using SchoolFees.BL.Rules;
using SchoolFees.BL.Validators;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly AlumnoBusinessValidator _validator;

        public AlumnoService(
            IAlumnoRepository alumnoRepository,
            IGradoRepository gradoRepository)
        {
            _alumnoRepository = alumnoRepository;
            _validator = new AlumnoBusinessValidator(gradoRepository);
        }

        public async Task<Alumno> CrearAsync(Alumno alumno)
        {
            if (alumno == null)
                throw new BusinessException("Alumno requerido.");

            // Estado inicial
            alumno.Estado = true;

            // Reglas puras
            AlumnoRules.PuedeSerCreado(alumno);

            // Validaciones dependientes de BD
            await _validator.ValidarCreacionAsync(alumno);

            return await _alumnoRepository.CreateAsync(alumno);
        }

        public async Task<IEnumerable<Alumno>> ObtenerActivosAsync()
        {
            return await _alumnoRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Alumno>> ObtenerInactivosAsync()
        {
            return await _alumnoRepository.GetInactivosAsync();
        }

        public async Task<Alumno?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("Id inválido.");

            return await _alumnoRepository.GetByIdAsync(id);
        }

        public async Task ActualizarAsync(Alumno alumno)
        {
            if (alumno == null)
                throw new BusinessException("Alumno requerido.");

            AlumnoRules.PuedeSerActualizado(alumno);

            await _alumnoRepository.UpdateAsync(alumno);
        }

        public async Task EliminarAsync(int id)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(id);
            if (alumno == null)
                throw new BusinessException("Alumno no encontrado.");

            await _alumnoRepository.DeleteAsync(id);
        }
    }
}
