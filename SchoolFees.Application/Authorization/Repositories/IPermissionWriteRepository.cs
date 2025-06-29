using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.Application.Authorization.DTOs;

namespace SchoolFees.Application.Authorization.Repositories
{
    public interface IPermissionWriteRepository
    {
        /// <summary>
        /// Crea un nuevo permiso.
        /// /// </summary>
        /// <param name="permission">Permiso a crear.</param>
        /// <returns>Tarea asíncrona.</returns>
        public Task CreateAsync(CreatePermissionRequest permissionRequest);
        /// <summary>
        /// Actualiza un permiso existente.
        /// </summary>
        /// <param name="permission">Permiso a actualizar.</param>
        /// <returns>Tarea asíncrona.</returns>
        public Task UpdateAsync(UpdatePermissionRequest permissionRequest);
        /// <summary>
        /// Elimina un permiso por su ID.
        /// </summary>
        /// <param name="id">ID del permiso a eliminar.</param>
        /// <returns>Tarea asíncrona.</returns>
        public Task DeleteAsync(Guid id);
        /// <summary>
        /// Elimina varios permisos por sus IDs.
        /// </summary>
        /// <param name="ids">Lista de IDs de permisos a eliminar.</param>
        /// <returns>Tarea asíncrona.</returns>
        public Task DeleteAsync(IList<Guid> ids);
    }
}