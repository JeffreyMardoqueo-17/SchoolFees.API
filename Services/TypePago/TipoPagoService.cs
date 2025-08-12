

using Microsoft.EntityFrameworkCore;
using SchoolFees.API.DataBase;
using SchoolFees.API.Models;

namespace SchoolFees.API.Services.TypePago
{
    public class TipoPagoService : ITipoPago
    {
        private readonly AplicationDBContext _context;
        private readonly ILogger<TipoPagoService> _logger;

        public TipoPagoService( AplicationDBContext aplicationDB, ILogger<TipoPagoService> logger)
        {
            _context = aplicationDB;
            _logger = logger;
        }

        public async Task<IEnumerable<TipoPago>> GetAllTipoPagoAsinc()
        {
            return await _context.TipoPago.ToListAsync();
        }
        public async Task<TipoPago> GetByIdTipoPagoAsync(int id)
        {
            var tipoPago = await _context.TipoPago.FindAsync(id);
            if (tipoPago == null)
                throw new KeyNotFoundException($"No se encontro ningun tipo de pago con este id {id}");
            return tipoPago;
        }

        public async Task CreateTipoPagoAsync(TipoPago tipoPago)
        {
            if (tipoPago == null)
                throw new ArgumentNullException(nameof(tipoPago), "No se puede crear un nullo");
            if (string.IsNullOrWhiteSpace(tipoPago.Name))
                throw new ArgumentException("El nombre del tipo de PAGO es obligatorio.", nameof(tipoPago));
            await _context.TipoPago.AddAsync(tipoPago);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTipoPago(int id)
        {
            var tipoPagoexiste = await GetByIdTipoPagoAsync(id);
            if (tipoPagoexiste == null)
                throw new KeyNotFoundException("El tipo de pago no existe, no hay que eliminar");
             _context.TipoPago.Remove(tipoPagoexiste);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateTipoPagoAsync(TipoPago tipoPago)
        {
            try
            {
                if (tipoPago == null)
                    throw new ArgumentNullException(nameof(tipoPago), "No se puede crear un objeto nulo");

                if (tipoPago.Id <= 0)
                    throw new ArgumentException("Id inválido, el Id debe ser mayor a 0", nameof(tipoPago.Id));

                if (string.IsNullOrWhiteSpace(tipoPago.Name))
                    throw new ArgumentException("El nombre del documento es obligatorio", nameof(tipoPago.Name));

                var tipoPagExiste = await GetByIdTipoPagoAsync(tipoPago.Id);

                // Actualizamos la entidad trackeada
                tipoPagExiste.Name = tipoPago.Name;

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                _logger?.LogWarning(ex, "Objeto nulo recibido en UpdateTipoPagoAsync.");
                // Se puede lanzar una excepción especifica o simplemente dejar que el controlador devuelva 400
                throw new ArgumentNullException(nameof(tipoPago), "El objeto tipoPago no puede ser nulo.");
            }

            catch (ArgumentException ex)
            {
                _logger?.LogWarning(ex, "Argumento inválido en UpdateTipoPagoAsync: {Mensaje}", ex.Message);
                throw new ArgumentException(ex.Message, ex.ParamName);
            }

            catch (KeyNotFoundException ex)
            {
                _logger?.LogWarning(ex, "TipoPago no encontrado en UpdateTipoPagoAsync: {Mensaje}", ex.Message);
                throw new KeyNotFoundException(ex.Message);
            }

            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error inesperado actualizando TipoPago.");
                throw new ApplicationException("Error inesperado actualizando TipoPago, contacte al administrador.", ex);
            }

        }

    }
}
