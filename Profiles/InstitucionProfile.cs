using AutoMapper;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs.Institucion;
using SchoolFees.API.DTOs.TipoInstitucion;

namespace SchoolFees.API.Profiles
{
    public class InstitucionProfile : Profile
    {
        public InstitucionProfile()
        {
            // Lectura con relación
            CreateMap<Institucion, InstitucionReadDto>()
                .ForMember(dest => dest.TipoInstitucionName,
                           opt => opt.MapFrom(src => src.TipoInstitucion != null ? src.TipoInstitucion.Name : null));

            // Relación aparte
            CreateMap<TipoInstitucion, TipoInstitucionReadDto>();

            // Creación
            CreateMap<InstitucionCreateDto, Institucion>();

            // Actualización
            CreateMap<InstitucionUpdateDto, Institucion>();
        }
    }
}
