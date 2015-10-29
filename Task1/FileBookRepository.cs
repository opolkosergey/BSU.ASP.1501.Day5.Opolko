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
        }

        public void Add(Book item)
        {
            var file = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);
            var writer = new BinaryWriter(file);
            var pos = writer.Seek(0, SeekOrigin.End);
            file.Position = pos;

            writer.Write(item.Id);
            writer.Write(item.Author);
            writer.Write(item.Title);
            writer.Write(item.Price);
            writer.Write(item.Pages);

            writer.Dispose();
            file.Close();
        }

        public void Remove(Book item)
        {
            var books = GetAllItems().ToList();
            var isDeleted = books.Remove(item);
            if (isDeleted)
            {
                File.Delete(Path);
                foreach (var book in books)
                    Add(book);

            }
        }

        public IEnumerable<Book> GetAllItems()
        {
            var books = new List<Book>();

            var sourceFile = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read);
            var reader = new BinaryReader(sourceFile);
            while (reader.PeekChar() > -1)
            {
                int id = reader.ReadInt32();
                string author = reader.ReadString();
                string title = reader.ReadString();
                double price = reader.ReadDouble();
                int pages = reader.ReadInt32();
                books.Add(new Book(author, title, price, pages));
            }
            reader.Dispose();
            sourceFile.Close();
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
