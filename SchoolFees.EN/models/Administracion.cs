using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SchoolFees.EN.models
{
    public class Administrador
    {
        public int Id { get; set; }

        // Identidad
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;

        // Autenticación
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public DateTime? UltimoLogin { get; set; }
        public int IntentosFallidos { get; set; }
        public DateTime? BloqueadoHasta { get; set; }

        // Auditoría
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? CreadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? ModificadoPor { get; set; }
        public string? UltimaIP { get; set; }

        // Navegación
        public ICollection<AdministradorRol> Roles { get; set; } 
            = new List<AdministradorRol>();
    }
}
