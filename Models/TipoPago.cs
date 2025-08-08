using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.Models
{
    public class TipoPago
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El maximo de caracteres es de 100")]
        public string? Name { get; set; }
    }
}
