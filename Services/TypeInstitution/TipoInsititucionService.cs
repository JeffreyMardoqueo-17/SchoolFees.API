using Microsoft.EntityFrameworkCore;
using SchoolFees.API.DataBase;
using SchoolFees.API.Models;
using SchoolFees.API.Services.TypePago;

namespace SchoolFees.API.Services.TypeInstitution
{
    public class TipoInsititucionService : ITipeInstitution
    {
        private readonly AplicationDBContext _context;
        private readonly ILogger<TipoPagoService> _logger;

        public TipoInsititucionService(AplicationDBContext context, ILogger<TipoPagoService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<TipoInstitucion>> GetAllTipoInsititucion()
        {
            return await _context.TipoInstitucion
                .AsNoTracking() //para que no aga seguimiento
                .ToListAsync(); //enlista los tipos de instituciones
        }
        public async Task<TipoInstitucion> GetByIdTipoInstitucion(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id debe de ser mayor a cero. ", nameof(id));

            var tipoInstitucion = await _context.TipoInstitucion
                .AsNoTracking()
                .FirstOrDefaultAsync(ti => ti.Id == id);
            if (tipoInstitucion == null)
                throw new KeyNotFoundException($"No se encontro ningun tipo de isnititucion con el id {id}");

            return tipoInstitucion;
        }
    }
}
