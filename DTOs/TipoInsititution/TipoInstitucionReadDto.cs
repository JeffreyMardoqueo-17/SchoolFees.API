using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.API.DTOs.TipoInsititution
{
    public record class TipoInstitucionReadDto(
        int Id,
        string Name,
        string Description
    );
}