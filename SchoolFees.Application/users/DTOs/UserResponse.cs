using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Application.users.DTOs
{

    /// <summary>
    /// Proyección de usuario que enviamos al frontend.
    /// </summary>
    public record UserResponse(
        Guid Id,
        string Name,
        string LastName,
        string Email,
        string PhoneNumber,
        Guid RoleId,
        string RoleName,
        Guid InstitutionId,
        string InstitutionName,
        DateTime CreatedAt,
        bool IsActive);
}