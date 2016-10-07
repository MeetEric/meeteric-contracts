namespace MeetEric.Security
{
    using System;
    using System.Runtime.Serialization;

    public abstract class BaseIdentifier : IIdentifier
    {
        public abstract string Moniker { get; protected set; }

        public abstract int CompareTo(IIdentifier other);

        public abstract bool Equals(IIdentifier other);

        public override string ToString()
        {
            return Moniker;
        }
    }
}