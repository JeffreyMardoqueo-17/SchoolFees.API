namespace SchoolFees.Application.Authorization.DTOs;

public sealed class RolePermissionDto
{
    public Guid RoleId { get; init; }
    public Guid PermissionId { get; init; }
}
