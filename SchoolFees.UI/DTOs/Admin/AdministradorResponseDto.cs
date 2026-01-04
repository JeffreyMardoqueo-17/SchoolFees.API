using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.UI.DTOs.Admin
{
    public class AdministradorResponseDto
    {
        public int Id { get; set; }

        // Identidad
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;

        // Estado
        public bool Estado { get; set; }

        // Auditoría
        public DateTime FechaCreacion { get; set; }
        public int? CreadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? ModificadoPor { get; set; }
    }
}