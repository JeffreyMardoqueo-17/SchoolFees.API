namespace SchoolFees.Application.Authorization.DTOs;

public sealed class PermissionDto
{
    public Guid Id { get; init; }
    public string Code { get; init; } = default!;
    public string? Description { get; init; }
}
