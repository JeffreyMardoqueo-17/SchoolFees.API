using SchoolFees.API.Models;

namespace SchoolFees.API.Services.Change
{
    public interface ITurno
    {
        Task<IEnumerable<Turno>> GetAllTurnoAsync();
        Task<Turno> GetByIdTurnoAsync(int id);
    }
}
