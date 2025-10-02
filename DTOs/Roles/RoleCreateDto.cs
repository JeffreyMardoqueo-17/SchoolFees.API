using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.DTOs.Roles
{
    public class RoleCreateDto
    {
        [Required(ErrorMessage = "El Nombre es Requerido")]
        [StringLength(50, ErrorMessage = "El máximo de caracteres permitidos es de 50")]
        public string Name { get; set; } = string.Empty;

        // null = rol global, valor = rol ligado a una institución
        public Guid? IdInstitucion { get; set; }
    }
}
