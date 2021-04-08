namespace SRP.Interfaces
{
    public interface IBookWriter
    {
        void Create(Refcatored.Book book);
        void Replace(Refcatored.Book book);
        void Remove(Refcatored.Book book);
    }
}
