using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public interface IRepository<T> where T : IComparable<Book>, IEquatable<T>
    {
        List<T> LoadBooks();
        void Save(IEnumerable<Book> books);
    }
}
