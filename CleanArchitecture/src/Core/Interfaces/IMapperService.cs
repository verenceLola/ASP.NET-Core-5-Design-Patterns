namespace Core.Interfaces
{
    public interface IMapperService
    {
        TDestination Map<TSource, TDestination>(TSource entity);
    }
}
