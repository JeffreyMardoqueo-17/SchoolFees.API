using System;
using SchoolFees.API.DTOs.TipoInstitucion;
namespace SchoolFees.API.DTOs.Institucion
{
    public class InstitucionReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // 🔹 Aquí traemos el nombre del tipo
        public string TipoInstitucionName { get; set; } = string.Empty;
    }
}
