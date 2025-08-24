
using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El maximo de caracteres permmitidos es de 50")]
        public string? Nombre { get; set; }
    }
}
