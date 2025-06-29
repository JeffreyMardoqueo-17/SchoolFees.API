using System.ComponentModel.DataAnnotations;

namespace SchoolFees.Application.Authorization.DTOs;

/// <summary>Asigna una lista de permisos a un rol.</summary>
public sealed class AssignPermissionsRequest
{
    [Required] public Guid RoleId { get; init; }

    /// <summary>Lista de IDs (o códigos, según tu implementación) de permisos a agregar.</summary>
    [Required, MinLength(1)]
    public IList<Guid> PermissionIds { get; init; } = new List<Guid>();
}
