using System.ComponentModel.DataAnnotations;

namespace SchoolFees.Application.Authorization.DTOs;

public sealed class UpdateRoleRequest
{
    [Required] public Guid Id { get; init; }

    [Required, MinLength(3)]
    public string Name { get; init; } = default!;

    public string? Description { get; init; }
    public IList<string>? PermissionCodes { get; init; }
}
