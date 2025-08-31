using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolFees.API.Models
{
    public class Institucion
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // GUID único e irrepetible

        [Required, StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(250, ErrorMessage = "La dirección no puede superar los 250 caracteres")]
        public string Address { get; set; } = string.Empty;

        [Phone, StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
        public string Phone { get; set; }  = string.Empty;// Ejemplo: +503 0000-0000

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = string.Empty;

        // Relación con TipoInstitucion
        [Required]
        public int IdTipoInstitucion { get; set; }

        [ForeignKey(nameof(IdTipoInstitucion))]
        public TipoInstitucion TipoInstitucion { get; set; }
    }
}
