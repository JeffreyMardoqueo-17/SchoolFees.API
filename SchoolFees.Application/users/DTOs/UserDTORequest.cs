using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Application.users.DTOs
{
    /// <summary>
    /// Datos necesarios para registrar un nuevo usuario.
    /// </summary>
    public record CreateUserRequest(
      [property: Required, StringLength(40)] string Name,
      [property: Required, StringLength(40)] string LastName,
      [property: Required, EmailAddress] string Email,
      [property: Phone] string PhoneNumber,
      [property: Required, MinLength(8)] string Password,
      Guid RoleId,
      Guid InstitutionId);
}