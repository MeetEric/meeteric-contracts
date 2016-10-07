namespace MeetEric.Exceptions
{
    using System;

    public static class Throw
    {
        public static void IfArgumentNull(string name, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void IfNullOrEmpty(string argumentName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void IfDefault<T>(string name, T value)
            where T : struct
        {
            if (value.Equals(default(T)))
            {
                throw new ArgumentException("Default value is not supported", name);
            }
        }

        public static void IfOutOfRange(string name, bool inRange)
        {
            if (!inRange)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }

        public static void IfFalse(string name, bool result, string message)
        {
            if (!result)
            {
                throw new ArgumentException(name, message);
            }
        }
    }
}
