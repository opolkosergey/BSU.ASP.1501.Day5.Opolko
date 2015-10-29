using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class FileBookRepository : IRepository<Book>
    {
        private string Path { get; }

        public FileBookRepository(string path)
        {
            Path = path;
            File.Create(path);
        }

        public void Add(Book item)
        {
            using (var writer = new BinaryWriter(File.Open(Path, FileMode.Append)))
            {
                writer.Write(item.Id);
                writer.Write(item.Author);
                writer.Write(item.Title);
                writer.Write(item.Price);
                writer.Write(item.Pages);
            }     
        }
        
        public void Remove(Book item)
        {
            var books = GetAllItems().ToList();
            var isDeleted = books.Remove(item);
            if (isDeleted)
            {
                if (books.Count == 0)
                {
                    File.Delete(Path);
                    File.Create(Path);
                }
                else
                {
                    foreach (var book in books)
                        Add(book);
                }
            }
        }

        public IEnumerable<Book> GetAllItems()
        {
            var books = new List<Book>();
            using (var reader = new BinaryReader(File.Open(Path, FileMode.Open)))
            {   
                while (reader.PeekChar() > -1)
                {
                    int id = reader.ReadInt32();
                    string author = reader.ReadString();
                    string title = reader.ReadString();
                    double price = reader.ReadDouble();
                    int pages = reader.ReadInt32();
                    books.Add(new Book(author, title, price, pages));
                }
            }
            return books;
        }

        public IEnumerable<Book> GetElementsByTag(string tag)
        {
            return GetAllItems().ToList().Where(b => b.Author.Contains(tag));
        }

        public IEnumerable<Book> Sort(Func<Book, object> keySelector)
        {
            return GetAllItems().OrderBy(keySelector);
        }
    }
}
