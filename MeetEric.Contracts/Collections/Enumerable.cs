namespace MeetEric.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// An abstract enumerable specifically for handling DataSourceRow's that are
    /// currently read-in by either File inputs or SQL databases.
    ///
    /// The goal is to have a common framework for the DataSourceReaders to read-in
    /// data and return them as an enumerable that can then be iterated-thru without
    /// requiring the data to be pre-loaded into memory
    ///
    /// The current design expectations are for all inheriting classes, only these
    /// methods should require their source-specific implementations:
    /// Dispose(), MoveNext(), Reset()
    /// </summary>
    public abstract class Enumerable<T> : IEnumerable<T>, IEnumerator<T>
    {
        public T Current
        {
            get; protected set;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public abstract void Dispose();

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public abstract bool MoveNext();

        public void Reset()
        {
            throw new NotSupportedException("Enumerable is forward-only.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
