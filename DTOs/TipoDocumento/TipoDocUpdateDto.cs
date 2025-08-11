using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.DTOs.TipoDocumento
{
    public record TipoDocUpdateDto
    (
        [property: Required(ErrorMessage ="El Id es requerido")]
        int Id,
        //son las mismas cosas que tengo en el modelo
        [property: Required(ErrorMessage = "El nombre es necesario")]
        [property: StringLength(100, ErrorMessage = "El maximo de caracteres es de 100")]
        string Name
    );
}
