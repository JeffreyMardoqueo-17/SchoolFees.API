using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SchoolFees.API.DataBase;
using SchoolFees.API.Models;

namespace SchoolFees.API.Services.Roles
{
    public class RoleService: IRole
    {
        private readonly AplicationDBContext _context;
        public RoleService(AplicationDBContext context)
        {
            _context = context;
        }
        //primer metodo para obtener todos 
        public async Task<List<Role>> GetAllRoleAsync()
        {
            return await _context.Role
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id debe ser mayor que 0.", nameof(id));

            var role = await _context.Role
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
                throw new KeyNotFoundException($"No se encontró el rol con ID {id}.");

            return role;
        }

    }
}
