namespace MeetEric.Common
{
    using MeetEric.Security;

    public interface IEntity
    {
        IIdentifier Id { get; }

        bool IsHidden { get; }
    }
}
