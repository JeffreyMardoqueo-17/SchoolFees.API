using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.Application.Authorization.DTOs;

namespace SchoolFees.Application.Authorization.Repositories
{
    public interface IRoleWriteRepository
    {
        public Task CreateAsync(CreateRoleRequest roleRequest); //crear un nuevo rol
        public Task UpdateAsync(UpdateRoleRequest roleRequest); //actualizar un rol existente
        public Task AddPermissionsAsync(Guid roleId, IList<Guid> permissionIds); //agregar permisos a un rol
        public Task RemovePermissionsAsync(Guid roleId, IList<Guid> permissionIds); //quitar permisos de un rol
        public Task DeleteAsync(Guid id); //eliminar un rol por su id   

    }
}