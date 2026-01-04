using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.UI.DTOs.Admin
{
    public class LoginRequestDto
    {
        public string Correo { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}