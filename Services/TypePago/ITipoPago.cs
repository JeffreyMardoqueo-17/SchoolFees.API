using SchoolFees.API.Models;

public interface ITipoPago
{
    Task<IEnumerable<TipoPago>> GetAllTipoPagoAsinc();
    Task<TipoPago> GetByIdTipoPagoAsync(int id);
    Task CreateTipoPagoAsync(TipoPago tipoPago);
    Task UpdateTipoPagoAsync(TipoPago tipoPago);
    Task DeleteTipoPago(int id);
}
