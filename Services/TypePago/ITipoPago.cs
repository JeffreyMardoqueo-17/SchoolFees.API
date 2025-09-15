using SchoolFees.API.Models;

public interface ITipoPago
{
    // Obtiene todos los tipos de pago de una institución específica
    Task<IEnumerable<TipoPago>> GetAllTipoPagoAsinc(Guid idInstitucion);

    // Obtiene un tipo de pago por Id y verifica que pertenezca a la institución
    Task<TipoPago> GetByIdTipoPagoAsync(int id, Guid idInstitucion);

    // Crea un tipo de pago para la institución indicada
    Task CreateTipoPagoAsync(TipoPago tipoPago, Guid idInstitucion);

    // Actualiza un tipo de pago de la institución indicada
    Task UpdateTipoPagoAsync(TipoPago tipoPago, Guid idInstitucion);

    // Elimina un tipo de pago de la institución indicada
    Task DeleteTipoPago(int id, Guid idInstitucion);
}
