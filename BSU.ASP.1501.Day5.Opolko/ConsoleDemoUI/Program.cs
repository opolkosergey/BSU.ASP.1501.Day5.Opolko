using System;
using Task1;

namespace ConsoleDemoUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookService = new BookService(new FileBookRepository("default"));

            bookService.AddBook(new Book("J. Richter", "C# via", 500.0, 800));
            bookService.AddBook(new Book("D. Samal", "Sifo VMSIS", 350.0 , 85));
            bookService.AddBook(new Book("A. Pushkin", "E.Onegin", 110.0, 150));
            bookService.AddBook(new Book("L. Tolstoi", "Voina i mir", 650.0, 1000));
            bookService.AddBook(new Book("S. Perro", "Kot v sapogah", 60.0, 50));

            foreach (var book in bookService.Books)
            Console.WriteLine($"{book}");

            bookService.DeleteBook(new Book("L. Tolstoi", "Voina i mir", 650.0, 1000));
            bookService.DeleteBook(new Book("S. Perro", "Kot v sapogah", 60.0, 50));

            foreach (var book in bookService.Books)
                Console.WriteLine($"{book}");

            bookService.Sort(c => c.Price);

            foreach (var book in bookService.Books)
                Console.WriteLine($"{book}");

            var selectedBooksByTag = bookService.FindBooksByTags("S");

            foreach (var book in selectedBooksByTag)
                Console.WriteLine($"{book}");
            Console.ReadKey();
        }
    }
}
