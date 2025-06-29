using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Application.Authorization.DTOs
{

    /// <summary>
    /// Información de un rol que se envía al cliente.
    /// </summary>
    public sealed class RoleDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string? Description { get; init; }

        /// <summary>Permisos asociados (códigos y descripciones).</summary>
        public IList<PermissionDto> Permissions { get; init; } = new List<PermissionDto>();
    }
}