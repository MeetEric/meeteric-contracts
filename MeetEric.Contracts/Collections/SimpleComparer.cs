namespace MeetEric.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SimpleComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, int> _getHashCode;

        public SimpleComparer(Func<T, int> getHashCode)
        {
            _getHashCode = getHashCode;
        }

        public bool Equals(T x, T y)
        {
            return GetHashCode(x) == GetHashCode(y);
        }

        public int GetHashCode(T obj)
        {
            return _getHashCode(obj);
        }
    }
}
