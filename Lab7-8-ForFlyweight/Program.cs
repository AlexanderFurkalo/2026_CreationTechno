using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7_8_ForFlyweight
{
    class BookFlyweight
    {
        public string Title { get; }
        public string Author { get; }

        public BookFlyweight(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public void Display(int id, string shelf)
        {
            Console.WriteLine($"Book ID: {id}, Title: {Title}, Author: {Author}, Shelf: {shelf}");
        }
    }

    class BookFactory
    {
        private Dictionary<string, BookFlyweight> books = new Dictionary<string, BookFlyweight>();

        public BookFlyweight GetBook(string title, string author)
        {
            string key = title + "_" + author;

            if (!books.ContainsKey(key))
            {
                Console.WriteLine("Creating new book flyweight...");
                books[key] = new BookFlyweight(title, author);
            }

            return books[key];
        }
    }
    class Book
    {
        private int id;
        private string shelf;
        private BookFlyweight flyweight;

        public Book(int id, string shelf, BookFlyweight flyweight)
        {
            this.id = id;
            this.shelf = shelf;
            this.flyweight = flyweight;
        }

        public void Display()
        {
            flyweight.Display(id, shelf);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BookFactory factory = new BookFactory();

            List<Book> library = new List<Book>();

            library.Add(new Book(1, "A1", factory.GetBook("1984", "George Orwell")));
            library.Add(new Book(2, "A2", factory.GetBook("Flowers for Algernon", "Daniel Keyes")));
            library.Add(new Book(3, "B1", factory.GetBook("The Colour Out of Space", "Howard Phillips Lovecraft")));
            library.Add(new Book(4, "B2", factory.GetBook("1984", "George Orwell")));
            library.Add(new Book(5, "B3", factory.GetBook("1984", "George Orwell")));

            foreach (var book in library)
            {
                book.Display();
            }

            Console.ReadLine();
        }
    }
}
