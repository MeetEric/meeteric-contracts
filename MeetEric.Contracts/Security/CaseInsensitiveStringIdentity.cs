namespace MeetEric.Security
{
    using System;
    using System.Runtime.Serialization;
    using System.Text.RegularExpressions;
    using MeetEric.Exceptions;

    public class CaseInsensitiveStringIdentity : BaseIdentifier, INormalizedIdentifier, IEquatable<CaseInsensitiveStringIdentity>
    {
        private static readonly Regex RemoveWhiteSpace;
        private static readonly StringComparer Comparer;

        static CaseInsensitiveStringIdentity()
        {
            RemoveWhiteSpace = new Regex(@"\s+");
            Comparer = StringComparer.OrdinalIgnoreCase;
        }

        public CaseInsensitiveStringIdentity()
            : this(Guid.NewGuid().ToString("N").Substring(0, 10))
        {
        }

        public CaseInsensitiveStringIdentity(string id)
        {
            Throw.IfNullOrEmpty(nameof(id), id);
            Moniker = RemoveWhiteSpace.Replace(id, string.Empty);
        }

        public override string Moniker { get; protected set; }

        IIdentifier INormalizedIdentifier.Normalized
        {
            get
            {
                return new CaseInsensitiveStringIdentity(this.Moniker.ToLower());
            }
        }

        public override bool Equals(IIdentifier other)
        {
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IIdentifier);
        }

        public override int GetHashCode()
        {
            return Comparer.GetHashCode(Moniker);
        }

        public override int CompareTo(IIdentifier other)
        {
            var x = other as CaseInsensitiveStringIdentity;
            return Comparer.Compare(Moniker, x?.Moniker);
        }

        bool IEquatable<CaseInsensitiveStringIdentity>.Equals(CaseInsensitiveStringIdentity other)
        {
            return CompareTo(other) == 0;
        }
    }
}
