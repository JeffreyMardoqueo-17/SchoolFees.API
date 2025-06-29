using System.ComponentModel.DataAnnotations;

namespace SchoolFees.Application.Authorization.DTOs;

public sealed class UpdatePermissionRequest
{
    [Required] public Guid Id { get; init; }

    [Required, MinLength(3)]
    public string Description { get; init; } = default!;
}

