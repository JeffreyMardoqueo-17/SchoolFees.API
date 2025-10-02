using AutoMapper;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs.Roles;

namespace SchoolFees.API.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            // Crear (del DTO al modelo)
            CreateMap<RoleCreateDto, Role>();

            // Actualizar (del DTO al modelo)
            CreateMap<RoleUpdateDto, Role>();

            // Lectura (del modelo al DTO)
            CreateMap<Role, RoleReadDto>()
                .ForMember(dest => dest.InstitucionName, 
                           opt => opt.MapFrom(src => src.Institucion != null ? src.Institucion.Name : null));
        }
    }
}
