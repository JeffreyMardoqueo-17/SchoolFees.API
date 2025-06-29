using System.ComponentModel.DataAnnotations;

namespace SchoolFees.Application.Authorization.DTOs;

/// <summary>Quita permisos específicos de un rol.</summary>
public sealed class RemovePermissionsRequest
{
    [Required] public Guid RoleId { get; init; }

    [Required, MinLength(1)]
    public IList<Guid> PermissionIds { get; init; } = new List<Guid>();
}
