using System.Collections.Generic;

namespace SRP.Interfaces
{
    public interface IBookReader
    {
        IEnumerable<Refcatored.Book> Books { get; }
        Refcatored.Book Find(int bookId);
    }
}
