using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.EN.models;

namespace SchoolFees.DAL.Interfaces
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> GetAllRolesAsync();
        Task<Rol> GetRoleByIdAsync(int id);
        Task<Rol> CreateRoleAsync(Rol rol);
        Task UpdateRoleAsync(Rol rol);
        Task DeleteRoleAsync(int id);
        

    }
}