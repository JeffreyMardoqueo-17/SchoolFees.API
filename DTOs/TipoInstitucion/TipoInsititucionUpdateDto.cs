using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.API.DTOs.TipoInstitucion
{
    public record class TipoInstitucionUpdateDto(
        [property: Required(ErrorMessage = "El nombre es requerido")]
        [property: StringLength(100, ErrorMessage = "El máximo de caracteres permitido es de 100")]
        string Name,

        [property: Required(ErrorMessage = "La descripción es requerida")]
        [property: StringLength(200, ErrorMessage = "El máximo de caracteres permitidos es de 200")]
        string Description
    );
}