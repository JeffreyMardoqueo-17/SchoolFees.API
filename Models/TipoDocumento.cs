
using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.Models
{
    public class TipoDocumento
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage ="El maximo de caracteres permitidos es de: 50")]
        public string Name { get; set; }
    }
}
