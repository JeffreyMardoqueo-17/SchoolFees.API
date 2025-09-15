using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.DTOs.TipoPago
{
    public record TipoPagoUpdateDto
    (
        [property: Required(ErrorMessage = "El Id es obligatorio")]
        int Id,

        [property: Required(ErrorMessage = "El nombre es obligatorio")]
        [property: StringLength(100, ErrorMessage = "El máximo de caracteres es de 100")]
        string Name

        // opcional: solo si quieres permitir cambiar la institución
        //[property: Required(ErrorMessage = "El Id de la institución es obligatorio")]
        //Guid IdInstitucion
    );
}
