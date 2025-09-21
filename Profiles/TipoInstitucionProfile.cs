using AutoMapper;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs.TipoInstitucion;

namespace SchoolFees.API.Profiles
{
    public class TipoInstitucionProfile : Profile
    {
        public TipoInstitucionProfile()
        {
            // Lectura → Entidad a ReadDto
            CreateMap<TipoInstitucion, TipoInstitucionReadDto>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name ?? string.Empty))
                .ForCtorParam("Description", opt => opt.MapFrom(src => src.Description ?? string.Empty));

            // Creación → CreateDto a Entidad
            CreateMap<TipoInstitucionCreateDto, TipoInstitucion>();

            // Actualización → UpdateDto a Entidad
            CreateMap<TipoInstitucionUpdateDto, TipoInstitucion>();
        }
    }
}
