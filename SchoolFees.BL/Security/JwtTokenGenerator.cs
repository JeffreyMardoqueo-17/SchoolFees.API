using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Security
{
    public class JwtTokenGenerator
    {
        //  Esta clase solo genera tokens.
        //  No sabe de HTTP.
        //  No sabe de Controllers.
        //  No sabe de Middleware.
        ///
        /// <summary>
        /// 🔑 ¿Qué hace esta clase?

        //Firma un JWT seguro
        // Incluye:
        // Id del admin
        // Correo
        // Nombre
        // Roles

        // Usa la clave secreta que solo el servidor conoce
        /// </summary>
        /// 
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Administrador admin)
        {
            if (admin == null)
                throw new ArgumentNullException(nameof(admin));

            // 🔐 Leer configuración
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiresMinutes = int.Parse(
                _configuration["Jwt:ExpiresMinutes"]!
            );

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!)
            );

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            // 🎯 Claims (la identidad del usuario)
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, admin.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, admin.Correo),
                new Claim("nombres", admin.Nombres),
                new Claim("apellidos", admin.Apellidos)
            };

            // 🎭 Roles (si existen)
            if (admin.Roles != null)
            {
                foreach (var rol in admin.Roles)
                {
                    claims.Add(new Claim(
                        ClaimTypes.Role,
                        rol.Rol.Nombre
                    ));
                }
            }

            // ⏱️ Token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
