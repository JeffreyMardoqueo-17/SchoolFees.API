using AutoMapper;
using SchoolFees.API.Helpers;
namespace SchoolFees.API.Profiles
{
    public class ResultConverter<TSource, TDestination> : ITypeConverter<Result<TSource>, Result<TDestination>>
    {
        public Result<TDestination> Convert(Result<TSource> source, Result<TDestination> destination, ResolutionContext context)
        {
            return new Result<TDestination>
            {
                Success = source.Success,
                Message = source.Message,
                Data = context.Mapper.Map<TDestination>(source.Data)
            };
        }
    }
}
