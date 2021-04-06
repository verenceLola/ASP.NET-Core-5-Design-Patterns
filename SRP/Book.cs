using System;
using System.Collections.Generic;
using System.Linq;

namespace SRP
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        private static int _lastId = 0;
        public static List<Book> Books { get; }
        public static int NextId => ++_lastId;
        static Book()
        {
            Books = new List<Book>{
            new Book{
                Id = NextId,
                Title = "Some cool computer book"
            }
        };
        }
        public Book(int? id = null)
        {
            Id = id ?? default(int);
        }
        public void Save()
        {
            if (Books.Any(x => x.Id == Id))
            {
                var index = Books.FindIndex(x => x.Id == Id);
                Books[index] = this;
            }
            else
            {
                Books.Add(this);
            }
        }
        public void Load()
        {
            if (Id == default(int))
            {
                throw new Exception("You must set the Id to the Book Id you want to load.");
            }
            var book = Books.FirstOrDefault(x => x.Id == Id);
            if (book == null)
            {
                throw new Exception("This book does not exist");
            }
            Id = book.Id;
            Title = book.Title;
        }
        public void Display()
        {
            Console.WriteLine($"Book: {Title} ({Id})");
        }
    }

}
