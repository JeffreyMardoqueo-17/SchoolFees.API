using SchoolFees.BL.Interfaces;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.Exceptions;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Services
{
    public class AdministradorRolService : IAdministradorRolService
    {
        private readonly IAdministradorRolRepository _administradorRolRepository;

        public AdministradorRolService(
            IAdministradorRolRepository administradorRolRepository)
        {
            _administradorRolRepository = administradorRolRepository;
        }

        public async Task AsignarRolesAsync(
            int IdAdministrador,
            IEnumerable<int> rolesIds)
        {
            var relaciones = rolesIds.Select(IdRol =>
                new AdministradorRol
                {
                    IdAdministrador = IdAdministrador,
                    IdRol = IdRol
                });

            await _administradorRolRepository.AddRangeAsync(relaciones);
        }
        public async Task<AdministradorRol?> GetByIdAsync(int IdAdministrador, int IdRol)
        {
            var result = await _administradorRolRepository.GetByIdAsync(IdAdministrador, IdRol);
            if(result == null)
                throw new BusinessException("La relacion Administrador-Rol no existe");
            return result;
        }
    }
}
