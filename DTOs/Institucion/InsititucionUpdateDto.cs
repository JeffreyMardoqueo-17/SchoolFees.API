using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.DTOs.Institucion
{
    public class InstitucionUpdateDto
    {
        [Required(ErrorMessage = "El Id es obligatorio")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(250, ErrorMessage = "La dirección no puede superar los 250 caracteres")]
        public string Address { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El teléfono no es válido")]
        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        [StringLength(100, ErrorMessage = "El email no puede superar los 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de institución es obligatorio")]
        public int IdTipoInstitucion { get; set; }
    }
}
