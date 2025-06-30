using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Application.users.DTOs
{

    /// <summary>
    /// Datos necesarios para cambiar la contraseña de un usuario autenticado.
    /// </summary>
    public record ChangePasswordRequest(
        [property: Required] Guid UserId,
        [property: Required] string CurrentPassword,
        [property: Required, MinLength(8)] string NewPassword);
}