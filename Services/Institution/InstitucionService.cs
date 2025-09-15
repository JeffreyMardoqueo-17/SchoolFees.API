using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolFees.API.DataBase;
using SchoolFees.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolFees.API.Services.Institution
{
    public class InstitucionService : IInstitucion
    {
        private readonly AplicationDBContext _context;
        private readonly ILogger<InstitucionService> _logger;

        public InstitucionService(AplicationDBContext context, ILogger<InstitucionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Obtener todas las instituciones
        public async Task<IEnumerable<Institucion>> GetAllInstitucionesAsync()
        {
            return await _context.Institucion
                                .AsNoTracking()
                                .Include(i => i.TipoInstitucion)
                                .ToListAsync();
        }

        // Obtener una institución por Id
        public async Task<Institucion> GetInstitucionByIdAsync(Guid id)
        {
            var institucion = await _context.Institucion
                                            .AsNoTracking()
                                            .Include(i => i.TipoInstitucion)
                                            .FirstOrDefaultAsync(i => i.Id == id);

            if (institucion == null)
                throw new KeyNotFoundException($"No se encontró la institución con Id {id}");

            return institucion;
        }

        // Crear institución
        public async Task CreateInstitucionAsync(Institucion institucion)
        {
            if (institucion == null)
                throw new ArgumentNullException(nameof(institucion));

            await _context.Institucion.AddAsync(institucion);
            await _context.SaveChangesAsync();
        }

        // Actualizar institución
        public async Task UpdateInstitucionAsync(Institucion institucion)
        {
            if (institucion == null)
                throw new ArgumentNullException(nameof(institucion));

            var existing = await GetInstitucionByIdAsync(institucion.Id);

            existing.Name = institucion.Name;
            existing.Address = institucion.Address;
            existing.Phone = institucion.Phone;
            existing.Email = institucion.Email;
            existing.IdTipoInstitucion = institucion.IdTipoInstitucion;

            await _context.SaveChangesAsync();
        }

        // Eliminar institución
        public async Task DeleteInstitucionAsync(Guid id)
        {
            var institucion = await GetInstitucionByIdAsync(id);
            _context.Institucion.Remove(institucion);
            await _context.SaveChangesAsync();
        }
    }
}
