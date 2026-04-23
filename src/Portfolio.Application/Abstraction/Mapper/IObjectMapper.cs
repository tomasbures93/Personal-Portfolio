namespace Portfolio.Application.Abstraction.Mapper;

public interface IObjectMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
}
