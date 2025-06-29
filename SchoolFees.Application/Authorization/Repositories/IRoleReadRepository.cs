using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.Application.Authorization.DTOs;

namespace SchoolFees.Application.Authorization.Repositories
{
    public interface IRoleReadRepository
    {
        /// <summary>
        /// Obtiene todos los roles.
        /// </summary>
        Task<IList<RoleDto>> GetAllAsync();

        /// <summary>
        /// Obtiene un rol por su ID.
        /// </summary>
        Task<RoleDto?> GetByIdAsync(Guid id);

        /// <summary>
        /// Verifica si un rol existe por su nombre.
        /// </summary>
        Task<bool> ExistsByNameAsync(string name);

        /// <summary>
        /// Verifica si un rol existe por su ID.
        /// </summary>
        Task<bool> ExistsByIdAsync(Guid id);
    }
}