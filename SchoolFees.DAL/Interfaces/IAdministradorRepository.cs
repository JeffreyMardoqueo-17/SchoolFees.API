using SchoolFees.EN.models;
using SchoolFees.EN.Models;

namespace SchoolFees.DAL.Interfaces
{
    public interface IAdministradorRepository
    {
        // CRUD básico
        Task<IEnumerable<Administrador>> GetAllAsync();
        Task<Administrador?> GetByIdAsync(int id);
        Task<Administrador?> GetByCorreoAsync(string correo);
        Task CreateAsync(Administrador administrador);
        Task UpdateAsync(Administrador administrador);

        // Estados
        Task DesactivarAsync(int id);
        Task ActivarAsync(int id);

        // Seguridad / soporte a autenticación
        Task UpdatePasswordAsync(int id, string passwordHash, string passwordSalt);
        Task UpdateLoginInfoAsync(int id, DateTime ultimoLogin, string ip);
        Task IncrementarIntentosFallidosAsync(int id);
        Task ResetIntentosFallidosAsync(int id);
        Task BloquearAsync(int id, DateTime hasta);

        // Consultas de negocio
        Task<int> ContarAdministradoresActivosAsync();
    }
}
