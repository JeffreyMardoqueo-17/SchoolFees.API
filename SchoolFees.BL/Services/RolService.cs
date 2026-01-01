using SchoolFees.BL.Interfaces;
using SchoolFees.BL.Rules;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.Exceptions;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<IEnumerable<Rol>> GetAllRolesAsync()
        {
            var roles = await _rolRepository.GetAllRolesAsync();
            if(roles == null || !roles.Any())
                throw new BusinessException("No hay roles disponibles.");
            return roles;
            // return await _rolRepository.GetAllRolesAsync();
        }

        public async Task<Rol> GetRoleByIdAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("Id inválido.");

            var rol = await _rolRepository.GetRoleByIdAsync(id);
            if (rol == null)
                throw new BusinessException("El rol no existe.");

            return rol;
        }

        public async Task<Rol> CreateRoleAsync(Rol rol)
        {
            RolRules.ValidarCreacion(rol);

            rol.Estado = true;

            return await _rolRepository.CreateRoleAsync(rol);
        }

        public async Task UpdateRoleAsync(Rol rol)
        {
            RolRules.ValidarModificacion(rol);

            var actual = await _rolRepository.GetRoleByIdAsync(rol.Id);
            if (actual == null)
                throw new BusinessException("El rol no existe.");

            actual.Nombre = rol.Nombre;

            await _rolRepository.UpdateRoleAsync(actual);
        }

        public async Task DeleteRoleAsync(int id)
        {
            var rol = await GetRoleByIdAsync(id);

            RolRules.ValidarDesactivacion(rol);

            rol.Estado = false;

            await _rolRepository.UpdateRoleAsync(rol);
        }
    }
}
