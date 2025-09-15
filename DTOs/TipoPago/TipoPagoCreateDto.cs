using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.DTOs.TipoPago
{
    public record TipoPagoCreateDto
    (
        [property: Required(ErrorMessage = "El nombre es necesario")]
        [property: StringLength(100, ErrorMessage = "El máximo de caracteres es de 100")]
        string Name,

        [property: Required(ErrorMessage = "El Id de la institución es necesario")]
        Guid IdInstitucion
    );
}
