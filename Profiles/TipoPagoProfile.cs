using AutoMapper;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs;
using SchoolFees.API.DTOs.TipoPago;

namespace SchoolFees.API.Profiles
{
    public class TipoPagoProfile : Profile
    {
        public TipoPagoProfile() {
            // configuracion el mapeo de la entidad TipoPago hacia los dtos

            // Entidad -> DTO (lectura)
            CreateMap<TipoPago, TipoPagoReadDto>()
                // ForCtorParam: indica cómo mapear un parámetro específico del constructor del DTO
                // "Name" es el nombre del parametro del constructor en TipoPagoReadDto
                // opt => opt.MapFrom(...) indica la fuente para ese parámetro
                // src => src.Name ?? string.Empty: si src.Name es null, usa cadena vacía para evitar nulls
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name ?? string.Empty));
            // DTO Crear -> Entidad
            CreateMap<TipoPago, TipoPagoCreateDto>();

            //DTO Update -> Entidad
            CreateMap<TipoPago, TipoPagoUpdateDto>();

        }
    }
}
