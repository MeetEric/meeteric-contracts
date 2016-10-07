namespace MeetEric.Security
{
    using System;
    using System.ComponentModel;
    using MeetEric.Serialization;

    public interface IIdentifier : IEquatable<IIdentifier>, IComparable<IIdentifier>
    {
        string Moniker { get; }
    }
}