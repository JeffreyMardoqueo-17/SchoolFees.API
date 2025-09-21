using AutoMapper;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs.Institucion;
using SchoolFees.API.DTOs.TipoInsititution;

namespace SchoolFees.API.Profiles
{
    public class InstitucionProfile : Profile
    {
        public InstitucionProfile()
        {
            // Lectura
            CreateMap<Institucion, InstitucionReadDto>();

            // Relación
            CreateMap<TipoInstitucion, TipoInstitucionReadDto>();

            // Creación
            CreateMap<InstitucionCreateDto, Institucion>();

            // Actualización
            CreateMap<InstitucionUpdateDto, Institucion>();
        }
    }
}
