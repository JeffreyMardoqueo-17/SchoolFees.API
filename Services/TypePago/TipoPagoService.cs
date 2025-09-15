using Microsoft.EntityFrameworkCore;
using SchoolFees.API.DataBase;
using SchoolFees.API.Models;

namespace SchoolFees.API.Services.TypePago
{
    public class TipoPagoService : ITipoPago
    {
        private readonly AplicationDBContext _context;
        private readonly ILogger<TipoPagoService> _logger;

        public TipoPagoService(AplicationDBContext aplicationDB, ILogger<TipoPagoService> logger)
        {
            _context = aplicationDB;
            _logger = logger;
        }

        // Obtiene todos los tipos de pago de una institución específica
        public async Task<IEnumerable<TipoPago>> GetAllTipoPagoAsinc(Guid idInstitucion)
        {
            return await _context.TipoPago
                                 .Where(tp => tp.IdInstitucion == idInstitucion)
                                 .ToListAsync();
        }

        // Obtiene un tipo de pago por Id y verifica que pertenezca a la institución
        public async Task<TipoPago> GetByIdTipoPagoAsync(int id, Guid idInstitucion)
        {
            var tipoPago = await _context.TipoPago
                                         .FirstOrDefaultAsync(tp => tp.Id == id && tp.IdInstitucion == idInstitucion);

            if (tipoPago == null)
                throw new KeyNotFoundException($"No se encontró ningún tipo de pago con id {id} para esta institución.");

            return tipoPago;
        }

        // Crea un tipo de pago para la institución indicada
        public async Task CreateTipoPagoAsync(TipoPago tipoPago, Guid idInstitucion)
        {
            if (tipoPago == null)
                throw new ArgumentNullException(nameof(tipoPago), "No se puede crear un objeto nulo.");

            if (string.IsNullOrWhiteSpace(tipoPago.Name))
                throw new ArgumentException("El nombre del tipo de pago es obligatorio.", nameof(tipoPago));

            tipoPago.IdInstitucion = idInstitucion;

            await _context.TipoPago.AddAsync(tipoPago);
            await _context.SaveChangesAsync();
        }

        // Actualiza un tipo de pago de la institución indicada
        public async Task UpdateTipoPagoAsync(TipoPago tipoPago, Guid idInstitucion)
        {
            try
            {
                if (tipoPago == null)
                    throw new ArgumentNullException(nameof(tipoPago), "No se puede actualizar un objeto nulo.");

                if (tipoPago.Id <= 0)
                    throw new ArgumentException("Id inválido, debe ser mayor a 0.", nameof(tipoPago.Id));

                if (string.IsNullOrWhiteSpace(tipoPago.Name))
                    throw new ArgumentException("El nombre del tipo de pago es obligatorio.", nameof(tipoPago.Name));

                var tipoPagoExiste = await GetByIdTipoPagoAsync(tipoPago.Id, idInstitucion);

                // Actualizamos la entidad trackeada
                tipoPagoExiste.Name = tipoPago.Name;

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                _logger?.LogWarning(ex, "Objeto nulo recibido en UpdateTipoPagoAsync.");
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger?.LogWarning(ex, "Argumento inválido en UpdateTipoPagoAsync: {Mensaje}", ex.Message);
                throw;
            }
            catch (KeyNotFoundException ex)
            {
                _logger?.LogWarning(ex, "TipoPago no encontrado en UpdateTipoPagoAsync: {Mensaje}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error inesperado actualizando TipoPago.");
                throw new ApplicationException("Error inesperado actualizando TipoPago, contacte al administrador.", ex);
            }
        }

        // Elimina un tipo de pago de la institución indicada
        public async Task DeleteTipoPago(int id, Guid idInstitucion)
        {
            var tipoPagoExiste = await GetByIdTipoPagoAsync(id, idInstitucion);

            _context.TipoPago.Remove(tipoPagoExiste);
            await _context.SaveChangesAsync();
        }
    }
}
