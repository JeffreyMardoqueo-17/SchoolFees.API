using SchoolFees.API.Models;

namespace SchoolFees.API.Services.TypeDocumento
{
    public interface ITipoDocumento
    {
        Task<IEnumerable<TipoDocumento>> GetAllTipoDocumentoAsinc();
        Task<TipoDocumento> GetByIdTipoDocumento(int id);
        Task CreateTipoDocumentoAsync(TipoDocumento tipoDocumento);
        Task UpdateTipoDocumentoAsync(TipoDocumento tipoDocumento);
        Task DeleteTipoDocumentoAsync(int id);
    }
}
