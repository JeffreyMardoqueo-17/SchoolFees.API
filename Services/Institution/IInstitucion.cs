using SchoolFees.API.Models;
using SchoolFees.API.Helpers; // Para Result<T>

namespace SchoolFees.API.Services.Institution
{
    public interface IInstitucion
    {
        Task<Result<IEnumerable<Institucion>>> GetAllInstitucionesAsync();
        Task<Result<Institucion>> GetInstitucionByIdAsync(Guid id);
        Task<Result<Institucion>> CreateInstitucionAsync(Institucion institucion);
        Task<Result<Institucion>> UpdateInstitucionAsync(Institucion institucion);
        Task<Result<Institucion>> DeleteInstitucionAsync(Guid id);
    }
}
