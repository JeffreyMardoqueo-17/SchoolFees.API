using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolFees.API.DataBase;
using SchoolFees.API.Models;
using SchoolFees.API.Services.Change;

namespace SchoolFees.API.Services.Change
{
    public class TurnoService : ITurno
    {
        private readonly AplicationDBContext _context;
        private readonly ILogger<TurnoService> _logger;
        public TurnoService(AplicationDBContext contexto, ILogger<TurnoService> logger)
        {
            _context = contexto;
            _logger = logger;
        }
        public async Task<IEnumerable<Turno>> GetAllTurnoAsync()
        {
            return await _context.Tuno.ToListAsync();
        }
        public async Task<Turno> GetByIdTurnoAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El Id debe de ser mayor a 0", nameof(id));
                var turno = await _context.Tuno.FindAsync(id);
                if (turno == null)
                    throw new KeyNotFoundException($"No se encontro ningun turno con el id: {id}");
                return turno;
            }
            catch (ArgumentException ex)
            {
                _logger?.LogWarning(ex, "Argumento inválido en GetByIdTurno: {Mensaje}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error inesperado creando Turno.");
                throw new ApplicationException("Error inesperado creando Turno, contacte al administrador.", ex);
            }
        }
    }
}
