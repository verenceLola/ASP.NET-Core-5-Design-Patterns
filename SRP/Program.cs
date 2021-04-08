using System;
using SRP.Refcatored;

namespace SRP
{
    class Program
    {
        static Interfaces.IBookReader reader = new BookStore();
        static Interfaces.IBookWriter writer = new BookStore();
        private static readonly BookPresenter _bookPresenter = new BookPresenter();
        static void Main(string[] args)
        {
            while (true)
            {
                DisplayChoices();
                var choice = ReadUserChoice();
                ExecuteUserChoice(choice);
            }
        }
        private static void ExecuteUserChoice(string choice)
        {

            switch (choice)
            {
                case "1":
                    FetchAndDisplayBook();
                    break;
                case "2":
                    FailToFetchBook();
                    break;
                case "3":
                    BookDoesNotExist();
                    break;
                case "4":
                    CreateOutOfOrderBook();
                    break;
                case "5":
                    DisplayTheBookSomewhereElse();
                    break;
                case "6":
                    CreateBook();
                    break;
                case "7":
                    ListAllBooks();
                    break;
                default:
                    System.Environment.Exit(0);
                    break;
            }
        }
        private static string ReadUserChoice() => Console.ReadLine();
        private static void DisplayChoices()
        {
            Console.WriteLine();
            Console.WriteLine("Choices:");
            Console.WriteLine("1: Fetch and display book id 1");
            Console.WriteLine("2: Feail to fetch a book");
            Console.WriteLine("3: Book does not exist");
            Console.WriteLine("4: Create an out of order book");
            Console.WriteLine("5: Display a book somewhere else");
            Console.WriteLine("6: Create a book");
            Console.WriteLine("7: List all books");
            Console.WriteLine("0: Exit");

            Console.WriteLine();
        }
        private static void FetchAndDisplayBook()
        {
            var book = reader.Find(1);
            _bookPresenter.Display(book);
        }
        private static void FailToFetchBook()
        {
            // var book = _bookStore.Load(1);
            // _bookPresenter.Display(book);
        }
        private static void BookDoesNotExist()
        {
            var book = reader.Find(999);
            if (book == null)
            {
                throw new Exception($"Book {book.Id} does not exist");
            }
        }
        private static void CreateOutOfOrderBook()
        {
            var book = new SRP.Refcatored.Book
            {
                Id = 4,
                Title = "Some out of order book"
            };
            writer.Create(book);
            _bookPresenter.Display(book);
        }
        private static void DisplayTheBookSomewhereElse()
        {
            Console.WriteLine("Oops! Can't do that, the Display method only write to the \"Console\".");
        }
        public static void CreateBook()
        {
            Console.Clear();
            Console.WriteLine("Please enter the book title: ");

            var title = Console.ReadLine();
            var book = new Refcatored.Book { Title = title };
            writer.Create(book);
        }
        public static void ListAllBooks()
        {
            foreach (var book in reader.Books)
            {
                _bookPresenter.Display(book);
            }
        }
    }
}
