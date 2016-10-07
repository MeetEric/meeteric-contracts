namespace MeetEric.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    public class BackOffException : Exception
    {
        public BackOffException()
        {
        }

        public BackOffException(TimeSpan delay)
        {
            Delay = delay;
        }

        public BackOffException(string message)
            : base(message)
        {
        }

        public BackOffException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public BackOffException(TimeSpan delay, Exception inner)
            : this(delay)
        {
        }

        public TimeSpan Delay { get; }
    }
}
