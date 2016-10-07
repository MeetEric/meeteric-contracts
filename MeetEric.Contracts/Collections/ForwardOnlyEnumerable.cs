namespace MeetEric.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ForwardOnlyEnumerable<T> : IEnumerable<T>, IEnumerator<T>
    {
        private readonly IEnumerator<T> _orignal;

        public ForwardOnlyEnumerable(IEnumerable<T> original)
        {
            _orignal = original.GetEnumerator();
        }

        public T Current
        {
            get
            {
                return _orignal.Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return _orignal.Current;
            }
        }

        public void Dispose()
        {
        }

        public void Close()
        {
            _orignal.Dispose();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            return _orignal.MoveNext();
        }

        public void Reset()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
