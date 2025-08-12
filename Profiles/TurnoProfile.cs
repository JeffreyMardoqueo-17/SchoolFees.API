using AutoMapper;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs.Change;

namespace SchoolFees.API.Profiles
{
    public class TurnoProfile : Profile
    {
        public TurnoProfile()
        {
            //lectura
            CreateMap<Turno, TurnoReadDto>()
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name ?? string.Empty));
        }
    }
}
