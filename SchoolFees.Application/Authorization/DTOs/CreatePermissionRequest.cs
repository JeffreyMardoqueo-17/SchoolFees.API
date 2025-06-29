using System.ComponentModel.DataAnnotations;

namespace SchoolFees.Application.Authorization.DTOs;

public sealed class CreatePermissionRequest
{
    [Required, MinLength(3)]
    public string Code { get; init; } = default!;

    [Required, MinLength(3)]
    public string Description { get; init; } = default!;
}
