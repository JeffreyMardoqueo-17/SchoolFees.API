using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SchoolFees.API.DataBase;
using SchoolFees.API.Helpers;
using SchoolFees.API.Models;

namespace SchoolFees.API.Services.Roles
{
    public class RoleService : IRole
    {
        private readonly AplicationDBContext _context;
        public RoleService(AplicationDBContext context)
        {
            _context = context;
        }
        //primer metodo para obtener todos 
        public async Task<Result<IEnumerable<Role>>> GetAllRoleAsync()
        {
            var roles = await _context.Role
                .AsNoTracking()
                .Include(r => r.Institucion) // Incluir la entidad relacionada Institucion
                .ToListAsync();

            if (!roles.Any())
                return Result<IEnumerable<Role>>.Fail("No se encontraron roles");

            return Result<IEnumerable<Role>>.Ok(roles);
        }

        public async Task<Result<Role>> GetRoleByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id debe ser mayor que 0.", nameof(id));

            var role = await _context.Role
                .AsNoTracking()
                .Include(r => r.Institucion) // Incluir la entidad relacionada Institucion
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
                return Result<Role>.Fail($"No se encontró el rol con ID {id}.");

            return Result<Role>.Ok(role);
        }
        public async Task<Result<Role>> CreateRoleAsync(Role role)
        {
            if (role == null)
                return Result<Role>.Fail("El objeto rol es nulo");

            if (string.IsNullOrWhiteSpace(role.Name))
                return Result<Role>.Fail("El nombre es requerido");

            // Validar si la institución existe si IdInstitucion no es null
            if (role.IdInstitucion.HasValue)
            {
                var institucionExists = await _context.Institucion
                    .AnyAsync(i => i.Id == role.IdInstitucion.Value);

                if (!institucionExists)
                    return Result<Role>.Fail($"No se encontró la institución con Id {role.IdInstitucion.Value}");
            }

            // 🔍 Validar duplicado de nombre de rol (solo dentro de la misma institución o global)
            bool exists = await _context.Role.AnyAsync(r =>
                r.Name.ToLower() == role.Name.ToLower().Trim() &&
                (
                    (r.IdInstitucion == null && role.IdInstitucion == null) || // ambos globales
                    (r.IdInstitucion != null && role.IdInstitucion != null && r.IdInstitucion == role.IdInstitucion) // misma institución
                )
            );

            if (exists)
                return Result<Role>.Fail($"Ya existe un rol con el nombre '{role.Name}' en esta institución o globalmente.");

            // Crear el nuevo rol
            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            return Result<Role>.Ok(role);
        }
       
        public async Task<Result<Role>> UpdateRoleAsync(Role role)
        {
            if (role == null)
                return Result<Role>.Fail("El objeto rol es nulo");

            // Usamos GetRoleByIdAsync para obtener el existente
            var existingResult = await GetRoleByIdAsync(role.Id);
            var existingRole = existingResult.Data; // <-- usamos Data, no Value

            if (!existingResult.Success || existingRole == null)
                return Result<Role>.Fail(existingResult.Message ?? $"No se encontró el rol con Id {role.Id}");

            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(role.Name))
                return Result<Role>.Fail("El nombre es requerido");

            // Validar si el IdInstitucion es válido si no es null
            if (role.IdInstitucion.HasValue)
            {
                var institucionExists = await _context.Institucion.AnyAsync(i => i.Id == role.IdInstitucion.Value);
                if (!institucionExists)
                    return Result<Role>.Fail($"No se encontró la institución con Id {role.IdInstitucion.Value}");
            }

            // Actualizar los campos
            existingRole.Name = role.Name;
            existingRole.IdInstitucion = role.IdInstitucion;

            _context.Role.Update(existingRole);
            await _context.SaveChangesAsync();

            return Result<Role>.Ok(existingRole);
        }
        public async Task<Result<bool>> DeleteRoleAsync(Role role)
        {
            if (role == null)
                return Result<bool>.Fail("El objeto rol es nulo");

            // udo GetRoleByIdAsync para obtener el existente
            var existingResult = await GetRoleByIdAsync(role.Id);
            var existingRole = existingResult.Data; // 

            if (!existingResult.Success || existingRole == null)
                return Result<bool>.Fail(existingResult.Message ?? $"No se encontró el rol con Id {role.Id}");

            _context.Role.Remove(existingRole);
            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
