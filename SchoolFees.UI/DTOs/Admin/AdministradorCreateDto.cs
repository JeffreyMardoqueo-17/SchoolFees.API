using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.UI.DTOs.Admin
{
    public class AdministradorCreateDto
    {
        // Identidad
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;

        // Autenticación (PLANO, solo aquí)
        public string Password { get; set; } = null!;

        // Roles
        public IEnumerable<int> RolesIds { get; set; } = new List<int>();

        // Auditoría
        public int? CreadoPor { get; set; }
    }
}

// {
//   "nombres": "Jeffrey Mardoqueo",
//   "apellidos": "Jimenez Santos",
//   "correo": "jeffreymardoqueo260@gmail.com",
//   "password": "",
//   "rolesIds": [1],
//   "creadoPor": null
// }
