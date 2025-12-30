using SchoolFees.BL.Codes;
using SchoolFees.EN.Exceptions;
using SchoolFees.BL.Interfaces;
using SchoolFees.BL.Rules;
using SchoolFees.BL.Validators;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.Models;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly AlumnoBusinessValidator _validator;
        private readonly IAlumnoGrupoRepository _alumnoGrupoRepository;
        private readonly IGrupoRepository _grupoRepository;
        // private readonly IAlumnoGrupoRepository _alumnoGrupoRepository;

        public AlumnoService(
            IAlumnoRepository alumnoRepository,
            IGradoRepository gradoRepository)
        {
            _alumnoRepository = alumnoRepository;
            _validator = new AlumnoBusinessValidator(gradoRepository, alumnoRepository);
        }

        public async Task<Alumno> PostAsync(Alumno alumno, int grupoId)
        {
            if (alumno == null)
                throw new BusinessException("Alumno requerido.");

            // 1️⃣ Alumno inicia INACTIVO
            alumno.Estado = false;

            // 2️⃣ Reglas puras de dominio
            AlumnoRules.PuedeSerCreado(alumno);

            // 3️⃣ Validaciones con BD
            await _validator.ValidarCreacionAsync(alumno);

            // 4️⃣ Crear alumno
            var alumnoCreado = await _alumnoRepository.CreateAsync(alumno);

            // 5️⃣ Generar código
            alumnoCreado.CodigoAlumno =
                AlumnoCodeGenerator.Generar(alumnoCreado, alumnoCreado.Id);

            // 6️⃣ Activar alumno
            alumnoCreado.Estado = true;

            // 7️⃣ Persistir cambios del alumno
            await _alumnoRepository.UpdateAsync(alumnoCreado);

            // 8️⃣ Validar grupo
            var grupo = await _grupoRepository.GetByIdGrupoAsync(grupoId);
            if (grupo == null)
                throw new BusinessException("El grupo no existe.");

            if (!grupo.Estado)
                throw new BusinessException("El grupo está inactivo.");

            // 9️⃣ Crear asignación automática (DOMINIO)
            var asignacion = new AlumnoGrupo(alumnoCreado.Id, grupo.Id);

            // 🔟 Persistir asignación
            await _alumnoGrupoRepository.CreateAsync(asignacion);

            return alumnoCreado;
        }

        public async Task<IEnumerable<Alumno>> GetAllAsync()
        {
            return await _alumnoRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Alumno>> GetInactivosAsync()
        {
            return await _alumnoRepository.GetInactivosAsync();
        }

        public async Task<Alumno?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("Id inválido.");

            return await _alumnoRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Alumno alumno)
        {
            if (alumno == null)
                throw new BusinessException("Alumno requerido.");

            AlumnoRules.PuedeSerActualizado(alumno);

            await _alumnoRepository.UpdateAsync(alumno);
        }

        public async Task DeleteAsync(int id)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(id);
            if (alumno == null)
                throw new BusinessException("Alumno no encontrado.");

            await _alumnoRepository.DeleteAsync(id);
        }
    }
}
