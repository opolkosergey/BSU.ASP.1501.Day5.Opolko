using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public sealed class BookService
    {
        public IRepository<Book> Repository { get; }
        public IEnumerable<Book> Books => Repository.GetAllItems();
        public BookService(IRepository<Book> repository)
        {
            Repository = repository;
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (Books.Contains(book))
                throw new ArgumentException("Element contains in repository" + nameof(book));
            Repository.Add(book);
        }

        public void DeleteBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (!Repository.GetAllItems().Contains(book))
                throw new ArgumentException("Element not contains in repository" + nameof(book));
            Repository.Remove(book);
        }

        public IEnumerable<Book> Sort(Func<Book, object> keySelector)
        {
            return Repository.Sort(keySelector);
        }

        public IEnumerable<Book> FindBooksByTags(string tag)
        {
            return Repository.GetElementsByTag(tag);
        }
    }
}
