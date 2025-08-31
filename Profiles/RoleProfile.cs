using AutoMapper;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs.Change;
using SchoolFees.API.DTOs.Roles;

namespace SchoolFees.API.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            //lectura
            CreateMap<Role, RoleReadDto>()
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name ?? string.Empty));
        }
    }
}
