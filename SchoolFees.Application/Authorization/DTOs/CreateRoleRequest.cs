using System.ComponentModel.DataAnnotations;

namespace SchoolFees.Application.Authorization.DTOs;

public sealed class CreateRoleRequest
{
    [Required, MinLength(3)]
    public string Name { get; init; } = default!;

    public string? Description { get; init; }

    /// <summary>Códigos de permisos a vincular al crear (opcional).</summary>
    public IList<string>? PermissionCodes { get; init; }
}
