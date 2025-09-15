using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.API.DTOs.Institucion
{
    public record class InstitucionUpdateDto(
        [property: Required(ErrorMessage = "El Id es obligatorio")]
        Guid Id,

        [property: Required(ErrorMessage = "El nombre es obligatorio")]
        [property: StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
        string Name,

        [property: Required(ErrorMessage = "La dirección es obligatoria")]
        [property: StringLength(250, ErrorMessage = "La dirección no puede superar los 250 caracteres")]
        string Address,

        [property: Phone(ErrorMessage = "El teléfono no es válido")]
        [property: StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
        string Phone,

        [property: Required(ErrorMessage = "El email es obligatorio")]
        [property: EmailAddress(ErrorMessage = "El email no es válido")]
        [property: StringLength(100, ErrorMessage = "El email no puede superar los 100 caracteres")]
        string Email,

        [property: Required(ErrorMessage = "El tipo de institución es obligatorio")]
        int IdTipoInstitucion
    );
}