using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.API.DTOs.Institucion
{
    public record class InstitucionReadDto(
         Guid Id,
         string Name,
         string Address,
         string Phone,
         string Email,
         int IdTipoInstitucion,
         string TipoInstitucionName
     );
}