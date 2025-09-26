using SchoolFees.API.Models;

namespace SchoolFees.API.Services.TypeInstitution
{
    public interface ITipeInstitution
    {
        Task<IEnumerable<TipoInstitucion>> GetAllTipoInsititucion();
        Task<TipoInstitucion> GetByIdTipoInstitucion(int id);
        //metodo para eliminar
        Task<bool> DeleteTipoInstitucion(int id);

    }
}
