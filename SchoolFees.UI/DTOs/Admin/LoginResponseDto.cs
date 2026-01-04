using System;
using System.Collections.Generic;

namespace SchoolFees.UI.DTOs.Admin
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public DateTime UltimoLogin { get; set; }

        //  JWT
        public string Token { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
