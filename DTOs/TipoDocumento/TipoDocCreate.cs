using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.DTOs.TipoDocumento
{
   public record class TipoDocCreate(
        [property: Required(ErrorMessage = "El Nombre es requerido")]
        [property: StringLength(100,ErrorMessage = "El maximo de caracteres permitidos es de 50")]
        string Name
        );
}
