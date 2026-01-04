
using SchoolFees.EN.models;

public interface IAdministradorRolService
{
    Task AsignarRolesAsync(int administradorId, IEnumerable<int> rolesIds);
    Task <AdministradorRol?> GetByIdAsync(int IdAdministrador, int IdRol);
}
