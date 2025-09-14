using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.Models
{
    public class TipoInstitucion
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100, ErrorMessage = "El máximo de caracteres permitido es de 100")]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(200, ErrorMessage = "El máximo de caracteres permitidos es de 200")]
        public string Description { get; set; } = string.Empty;
    }
}
