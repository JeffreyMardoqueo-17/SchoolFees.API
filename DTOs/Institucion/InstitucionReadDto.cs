using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.API.DTOs.Insititution
{
    public record class InstitucionReadDto(
        int Id,
        string Name,
        string Description
    );
}