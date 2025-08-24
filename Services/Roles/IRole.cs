using SchoolFees.API.Models;

namespace SchoolFees.API.Services.Roles
{
    public interface IRole
    {
        Task<List<Role>> GetAllRoleAsync();
        Task<Role> GetRoleByIdAsync(int id);
    }
}
