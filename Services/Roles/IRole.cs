using SchoolFees.API.Models;
using SchoolFees.API.Helpers; // Para Result<T> y PagedResult<T>

namespace SchoolFees.API.Services.Roles
{
    public interface IRole
    {
        // Task<List<Role>> GetAllRoleAsync();
        Task<Result<IEnumerable<Role>>> GetAllRoleAsync();
        Task<Result<Role>> GetRoleByIdAsync(int id);

        //los demas metodos a implementar
        Task<Result<Role>> CreateRoleAsync(Role role);
        Task<Result<Role>> UpdateRoleAsync(Role role);
        Task<Result<bool>> DeleteRoleAsync(Role role);

        // Servicio de roles (frontend/backend)
        Task<PagedResult<Role>> GetRolesPagedAsync(
            int pageNumber,
            int pageSize,
            string? orderBy = "Id",
            string? orderDirection = "asc"); ///============> AQUI ESTO ES NUEBO NI SE SI FUNCIONE LA VEERDAD
    }
}
