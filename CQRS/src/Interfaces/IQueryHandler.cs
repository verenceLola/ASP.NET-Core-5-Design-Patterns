namespace CQRS.Interfaces
{
    public interface IQueryHandler<TQuery, TReturn> where TQuery : IQuery<TReturn>
    {
        TReturn Handle(TQuery query);
    }
}
