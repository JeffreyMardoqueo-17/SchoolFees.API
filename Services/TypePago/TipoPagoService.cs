

using Microsoft.EntityFrameworkCore;
using SchoolFees.API.DataBase;
using SchoolFees.API.Models;

namespace SchoolFees.API.Services.TypePago
{
    public class TipoPagoService : ITipoPago
    {
        private readonly AplicationDBContext _context;
        public TipoPagoService( AplicationDBContext aplicationDB)
        {
            _context = aplicationDB;
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
            var TipoPagExiste = await GetByIdTipoPagoAsync(tipoPago.Id);
            if (TipoPagExiste == null)
                throw new KeyNotFoundException("El tipo de pago no existe, no hay que editar");

            //si es que existe, edito uno por uno para no actualizar todo 
            TipoPagExiste.Name = tipoPago.Name;
            await _context.SaveChangesAsync();
        }
    }
}
