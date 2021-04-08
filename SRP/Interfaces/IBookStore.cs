using System.Collections.Generic;


namespace SRP.Interfaces
{
    public interface IBookStore
    {
        IEnumerable<Refcatored.Book> Books { get; }
        Refcatored.Book Find(int bokId);
        void Create(Refcatored.Book book);
        void Replace(Refcatored.Book book);
        void Remove(Refcatored.Book book);
    }
}
