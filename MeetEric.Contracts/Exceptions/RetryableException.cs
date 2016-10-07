namespace MeetEric.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    public class RetryableException : BaseMeetEricException
    {
        public RetryableException()
        {
        }

        public RetryableException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
