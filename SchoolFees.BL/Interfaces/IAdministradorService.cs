using SchoolFees.EN.models;
using SchoolFees.EN.Models;

namespace SchoolFees.BL.Interfaces
{
    public interface IAdministradorService
    {
        // ---------------- Gestión ----------------

        Task<IEnumerable<Administrador>> GetAllAsync();
        Task<Administrador> GetByIdAsync(int id);

        Task CreateAsync(
            Administrador administrador,
            IEnumerable<int> rolesIds,
            int creadoPor);

        // ---------------- Autenticación ----------------

        Task<Administrador> LoginAsync(string correo, string password, string ip);
        // Task CambiarPasswordAsync(int id, string passwordActual, string nuevoPassword);
        // Task ActualizarAsync(
        //     Administrador administrador,
        //     int modificadoPor);

        // Task ActivarAsync(int id, int modificadoPor);
        // Task DesactivarAsync(int id, int modificadoPor);

        // // ---------------- Roles ----------------

        // Task AsignarRolAsync(int administradorId, int rolId, int asignadoPor);
        // Task QuitarRolAsync(int administradorId, int rolId, int modificadoPor);

        // // ---------------- Seguridad ----------------

        // Task BloquearAsync(int id, int minutos);
        // Task ResetearIntentosFallidosAsync(int id);
    }

}
