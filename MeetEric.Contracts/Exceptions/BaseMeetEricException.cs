namespace MeetEric.Exceptions
{
    using System;

    public abstract class BaseMeetEricException : Exception
    {
        public BaseMeetEricException()
            : base()
        {
        }

        public BaseMeetEricException(string message)
            : base(message)
        {
        }

        public BaseMeetEricException(string message, Exception innerException)
        {
            
        }
    }
}
