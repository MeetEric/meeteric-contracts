namespace MeetEric.Collections
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Security;

    public interface IReliableObjectFactory
    {
        Task<IAsyncDictionary<TKey, TValue>> GetDictionary<TKey, TValue>(IIdentifier id, CancellationToken cancel)
            where TKey : IComparable<TKey>, IEquatable<TKey>;

        Task<IAsyncSet<TValue>> GetSet<TValue>(IIdentifier id, CancellationToken cancel)
            where TValue : IComparable<TValue>, IEquatable<TValue>;

        Task<IAsyncQueue<TItem>> GetQueue<TItem>(IIdentifier id, CancellationToken cancel);
    }
}
