//notaciones

using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.Models
{
    public class Turno
    {

        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El maximo de caracteres es de 100")]
        public string? Name { get; set; }
    }
}
