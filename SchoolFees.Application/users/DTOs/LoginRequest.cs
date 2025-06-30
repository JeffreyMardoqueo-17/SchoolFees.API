using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Application.users.DTOs
{
    /// <summary>
    /// Solicitud de inicio de sesión con correo y contraseña.
    /// </summary>
    public record LoginRequest(
        [property: Required, EmailAddress] string Email,
        [property: Required] string Password
        );

    /// <summary>
    /// Solicitud de login mediante autenticación de Google (OAuth2).
    /// </summary>
    public record GoogleLoginRequest(
        [property: Required] string IdToken,
        string Provider = "Google");
}