
using SchoolFees.API.Models;

namespace SchoolFees.API.Services.Institution
{
    public interface IInstitucion
    {
        Task<IEnumerable<Institucion>> GetAllInstitucionesAsync();
        Task<Institucion> GetInstitucionByIdAsync(Guid id);
        Task CreateInstitucionAsync(Institucion institucion);
        Task UpdateInstitucionAsync(Institucion institucion);
        Task DeleteInstitucionAsync(Guid id);
    }
}