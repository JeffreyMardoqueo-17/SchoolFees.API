using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolFees.DAL.Context;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.models;

namespace SchoolFees.DAL.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly SchoolFeesDbContext _context;
        public RolRepository(SchoolFeesDbContext context)
        {
            _context = context; 
        }

        public async Task <IEnumerable<Rol>> GetAllRolesAsync()
        {
            var roles = await _context.Rol 
                .AsNoTracking()
                .ToListAsync();
            return roles;
        }

        public async Task<Rol> GetRoleByIdAsync(int id)
        {
            var rol = await _context.Rol
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            return rol ;
        }

        // Task<Rol> CreateRoleAsync(Rol rol);
        public async Task<Rol> CreateRoleAsync(Rol rol)
        {
            _context.Rol.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }
        //  Task UpdateRoleAsync(Rol rol);
        public async Task UpdateRoleAsync(Rol rol)
        {
            _context.Rol.Update(rol);
            await _context.SaveChangesAsync();
        }
        // Task DeleteRoleAsync(int id);
        public async Task DeleteRoleAsync(int id)
        {
            var rol = await GetRoleByIdAsync(id);
            if (rol == null) return;

            rol.Estado = false; // Soft delete
            _context.Rol.Update(rol);
            await _context.SaveChangesAsync();
        }
    }
}