using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public interface IRepository<T> where T : IComparable<T>, IEquatable<T>
    {
        void Add(T item);
        void Remove(T item);
        IEnumerable<T> GetAllItems();
        IEnumerable<T> GetElementsByTag(string tag);
        IEnumerable<T> Sort(Func<T, object> keySelector);
    }
}
