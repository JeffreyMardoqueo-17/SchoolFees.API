using SchoolFees.BL.Exceptions;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Validators
{
    public class AlumnoBusinessValidator
    {
        private readonly IGradoRepository _gradoRepository;

        public AlumnoBusinessValidator(IGradoRepository gradoRepository)
        {
            _gradoRepository = gradoRepository;
        }

        public async Task ValidarCreacionAsync(Alumno alumno)
        {
            var grado = await _gradoRepository.GetByIdGradoAsync(alumno.GradoId);
            if (grado == null)
                throw new BusinessException("El grado asignado no existe.");
        }
    }
}
