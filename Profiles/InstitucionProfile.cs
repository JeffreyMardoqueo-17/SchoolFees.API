using AutoMapper;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs.Institucion;

namespace SchoolFees.API.Profiles
{
    public class InstitucionProfile : Profile
    {
        public InstitucionProfile()
        {
            // Lectura: Institucion -> InstitucionReadDto
            CreateMap<Institucion, InstitucionReadDto>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name ?? string.Empty))
                .ForCtorParam("Address", opt => opt.MapFrom(src => src.Address ?? string.Empty))
                .ForCtorParam("Phone", opt => opt.MapFrom(src => src.Phone ?? string.Empty))
                .ForCtorParam("Email", opt => opt.MapFrom(src => src.Email ?? string.Empty))
                .ForCtorParam("IdTipoInstitucion", opt => opt.MapFrom(src => src.IdTipoInstitucion))
                .ForCtorParam("TipoInstitucionName", opt => opt.MapFrom(src => src.TipoInstitucion.Name ?? string.Empty));

            // Creación: InstitucionCreateDto -> Institucion
            CreateMap<InstitucionCreateDto, Institucion>();

            // Actualización: InstitucionUpdateDto -> Institucion
            CreateMap<InstitucionUpdateDto, Institucion>();
        }
    }
}
