using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.DTOs.Roles
{
    public class RoleUpdateDto
    {
        [Required(ErrorMessage = "El Id es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es necesario")]
        [StringLength(50, ErrorMessage = "El máximo de caracteres permitidos es de 50")]
        public string Name { get; set; } = string.Empty;

        // Si decides permitir actualizar la institución asociada:
        public Guid? IdInstitucion { get; set; }
    }
}
