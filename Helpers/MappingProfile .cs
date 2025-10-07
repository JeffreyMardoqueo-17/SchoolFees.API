using AutoMapper;
using SchoolFees.API.Helpers;

namespace SchoolFees.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Esto registra la conversiom generica de Result<TSource> -> Result<TDestination>
            CreateMap(typeof(Result<>), typeof(Result<>))
                .ConvertUsing(typeof(ResultConverter<,>));
        }
    }
}
