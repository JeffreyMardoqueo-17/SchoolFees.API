using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolFees.API.Models
{
    public class TipoPago
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El maximo de caracteres es de 100")]
        public string? Name { get; set; }
        // Relación con Institucion
        [Required]
        public Guid IdInstitucion { get; set; }
        
        [ForeignKey(nameof(IdInstitucion))]
        public Institucion Institucion { get; set; } = new Institucion();
    }
}
