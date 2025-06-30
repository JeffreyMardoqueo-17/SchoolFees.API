
namespace SchoolFees.Application.users.DTOs
{
    public record UserResponse(
        Guid Id,
        string Name,
        string LastName,
        string Email,
        string PhoneNumber,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        Guid RoleId,
        Guid InstitutionId,
        bool IsActive,
        bool IsDeleted
    );

}