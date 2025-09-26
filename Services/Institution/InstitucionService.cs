using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolFees.API.DataBase;
using SchoolFees.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolFees.API.Helpers;

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
        public async Task<Result<IEnumerable<Institucion>>> GetAllInstitucionesAsync()
        {
            var instituciones = await _context.Institucion
                                              .AsNoTracking()
                                              .Include(i => i.TipoInstitucion)
                                              .ToListAsync();

            if (!instituciones.Any())
                return Result<IEnumerable<Institucion>>.Fail("No se encontraron instituciones");

            return Result<IEnumerable<Institucion>>.Ok(instituciones);
        }

        // Obtener una institución por Id
        public async Task<Result<Institucion>> GetInstitucionByIdAsync(Guid id)
        {
            var institucion = await _context.Institucion
                                            .AsNoTracking()
                                            .Include(i => i.TipoInstitucion)
                                            .FirstOrDefaultAsync(i => i.Id == id);

            if (institucion == null)
                return Result<Institucion>.Fail($"No se encontró la institución con Id {id}");

            return Result<Institucion>.Ok(institucion);
        }


        // Crear institución
        public async Task<Result<Institucion>> CreateInstitucionAsync(Institucion institucion)
        {
            // Validación de nulos
            if (institucion == null)
                return Result<Institucion>.Fail("El objeto institución es nulo");

            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(institucion.Name))
                return Result<Institucion>.Fail("El nombre es requerido");

            if (string.IsNullOrWhiteSpace(institucion.Email))
                return Result<Institucion>.Fail("El correo es requerido");

            if (string.IsNullOrWhiteSpace(institucion.Phone))
                return Result<Institucion>.Fail("El teléfono es requerido");

            // Normalizar TELEFONO
            institucion.Phone = institucion.Phone.Replace(" ", "")
                                                    .Replace("-", "")
                                                    .Replace("(", "")
                                                    .Replace(")", "");

            if (!institucion.Phone.StartsWith("503"))
                institucion.Phone = "503" + institucion.Phone;

            // Validar duplicados
            var existe = await _context.Institucion.AnyAsync(i =>
                i.Name == institucion.Name ||
                i.Phone == institucion.Phone ||
                i.Email == institucion.Email);

            if (existe)
                return Result<Institucion>.Fail("Ya existe una institución con el mismo nombre, correo o teléfono");

            // Validar que el IdTipoInstitucion exista antes de guardar (evita FK exception)
            var tipoExiste = await _context.TipoInstitucion.AnyAsync(t => t.Id == institucion.IdTipoInstitucion);
            if (!tipoExiste)
                return Result<Institucion>.Fail("El tipo de institución no es válido");

            // Guardar
            await _context.Institucion.AddAsync(institucion);
            var changes = await _context.SaveChangesAsync();

            if (changes == 0)
                return Result<Institucion>.Fail("No se pudo guardar la institución");

            return Result<Institucion>.Ok(institucion);
        }


        // Actualizar institución
        public async Task<Result<Institucion>> UpdateInstitucionAsync(Institucion institucion)
        {
            if (institucion == null)
                return Result<Institucion>.Fail("El objeto institucion es nulo");

            var result = await GetInstitucionByIdAsync(institucion.Id);

            if (!result.Success)
                return Result<Institucion>.Fail(result.Message!);

            var existing = result.Data!;

            existing.Name = institucion.Name;
            existing.Address = institucion.Address;
            existing.Phone = institucion.Phone;
            existing.Email = institucion.Email;
            existing.IdTipoInstitucion = institucion.IdTipoInstitucion;

            await _context.SaveChangesAsync();

            return Result<Institucion>.Ok(existing);
        }


        // Eliminar institución
        public async Task<Result<Institucion>> DeleteInstitucionAsync(Guid id)
        {
            // Obtener el resultado
            var result = await GetInstitucionByIdAsync(id);

            if (!result.Success)
                return Result<Institucion>.Fail(result.Message!);

            var existing = result.Data!; // Extraemos la entidad real

            _context.Institucion.Remove(existing);
            await _context.SaveChangesAsync();

            return Result<Institucion>.Ok(existing);
        }

    }
}
