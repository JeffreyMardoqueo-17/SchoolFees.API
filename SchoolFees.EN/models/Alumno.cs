using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.EN.models
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = String.Empty;
        public string Apellidos { get; set; } = String.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string CodigoAlumno { get; set; } = String.Empty;
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}