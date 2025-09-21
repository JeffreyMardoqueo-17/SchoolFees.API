using System;
using SchoolFees.API.DTOs.TipoInsititution;

namespace SchoolFees.API.DTOs.Institucion
{
    public record class InstitucionReadDto(
         Guid Id,
         string Name,
         string Address,
         string Phone,
         string Email,
         int IdTipoInstitucion,
         TipoInstitucionReadDto TipoInstitucion
     );
}
