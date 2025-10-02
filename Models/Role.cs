
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolFees.API.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El maximo de caracteres permmitidos es de 50")]
        public string? Name { get; set; }
        
          // relacion con TipoInstitucion
        // [Required]
        public Guid? IdInstitucion { get; set; }

        [ForeignKey(nameof(IdInstitucion))]
        public Institucion? Institucion { get; set; } // aqui por alguna razon tengo un problema al hacer create
    }
}
