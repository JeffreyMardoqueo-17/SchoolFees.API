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
        private readonly IUnitOfWork _unitOfWork;
    
        // private readonly IAlumnoGrupoRepository _alumnoGrupoRepository;

        public AlumnoService(
            IAlumnoRepository alumnoRepository,
            IGradoRepository gradoRepository, IUnitOfWork unitOfWork)
        {
            _alumnoRepository = alumnoRepository;
            _validator = new AlumnoBusinessValidator(gradoRepository, alumnoRepository);
            _unitOfWork = unitOfWork;
        }
public async Task<Alumno> PostAsync(Alumno alumno, int grupoId, int adminId)
{
    if (alumno == null)
        throw new BusinessException("Alumno requerido.");

    // 1️⃣ Estado inicial
    alumno.Estado = false;
    alumno.CreadoPor = adminId;
    alumno.FechaCreacion = DateTime.UtcNow;

    // 2️⃣ Reglas de dominio
    AlumnoRules.PuedeSerCreado(alumno);

    // 3️⃣ Validaciones en BD
    await _validator.ValidarCreacionAsync(alumno);

    // 🔒 AQUÍ DEBE IR UNA TRANSACCIÓN
    using var transaction = await _unitOfWork.BeginTransactionAsync();

    try
    {
        // 4️⃣ Crear alumno
        var alumnoCreado = await _alumnoRepository.CreateAsync(alumno);

        // 5️⃣ Generar código
        alumnoCreado.CodigoAlumno =
            AlumnoCodeGenerator.Generar(alumnoCreado, alumnoCreado.Id);

        // 6️⃣ Activar alumno
        alumnoCreado.Estado = true;
        alumnoCreado.ModificadoPor = adminId;
        alumnoCreado.FechaModificacion = DateTime.UtcNow;

        // 7️⃣ Guardar cambios
        await _alumnoRepository.UpdateAsync(alumnoCreado);

        // 8️⃣ Validar grupo
        var grupo = await _grupoRepository.GetByIdGrupoAsync(grupoId);
        if (grupo == null || !grupo.Estado)
            throw new BusinessException("Grupo inválido o inactivo.");

        // 9️⃣ Asignación (DOMINIO)
        var asignacion = new AlumnoGrupo(alumnoCreado.Id, grupo.Id);

        // 🔟 Persistir asignación
        await _alumnoGrupoRepository.CreateAsync(asignacion);

        await transaction.CommitAsync();

        return alumnoCreado;
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
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
