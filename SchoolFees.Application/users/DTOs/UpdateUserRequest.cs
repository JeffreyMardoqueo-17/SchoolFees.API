using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Application.users.DTOs
{
    public record UpdateUserRequest
    (
        [property: Required(ErrorMessage = "EL ID del usuario es obligatorio.")] Guid Id,
        [property: StringLength(40)] string? Name,
        [property: StringLength(40)] string? LastName,
        [property: Phone] string? PhoneNumber,
        Guid? RoleId,
        bool? IsActive
    );
}