using AutoMapper;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs.TipoPago;
using SchoolFees.API.DTOs.TipoDocumento;

namespace SchoolFees.API.Profiles
{
    public class TipoDocProfile: Profile
    {
        public TipoDocProfile()
        {
            //lectura
            CreateMap<TipoDocumento, TipoDocReadDto>()
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name ?? string.Empty));

            //creacion
            CreateMap<TipoDocumento, TipoDocCreate>();

            //actualizacion
            CreateMap<TipoDocumento, TipoDocUpdateDto>();
        }
    }
}
