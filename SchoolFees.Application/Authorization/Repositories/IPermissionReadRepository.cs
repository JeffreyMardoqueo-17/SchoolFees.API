using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.Application.Authorization.DTOs;

namespace SchoolFees.Application.Authorization.Repositories
{
    public interface IPermissionReadRepository
    {
        /// <summary>
        /// Obtiene todos los permisos.
        /// /// </summary>
        /// <returns>Lista de permisos.</returns>
        public Task<IList<PermissionDto>> GetAllAsync();
        /// <summary>
        /// Obtiene un permiso por su ID.   
        /// /// </summary>
        /// <param name="id">ID del permiso.</param>
        /// <returns>Permiso encontrado o null si no existe.</returns>
        public Task<PermissionDto?> GetByIdAsync(Guid id);
        /// <summary>
        /// Verifica si un permiso existe por su nombre.
        /// </summary>
        /// <param name="name">Nombre del permiso.</param>
        /// <returns>True si existe, false en caso contrario.</returns>
        public Task<bool> ExistsByNameAsync(string name);
    }
}