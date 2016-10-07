namespace MeetEric.Collections
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Security;

    public interface IAsyncDictionary<TKey, TValue> : IAsyncCollection
    {
        Task<IEnumerable<TKey>> GetKeys(CancellationToken cancel);

        Task<IEnumerable<KeyValuePair<TKey, TValue>>> GetItems(CancellationToken cancel);

        Task Add(TKey key, TValue value, CancellationToken cancel);

        Task Set(TKey key, TValue value, CancellationToken token);

        Task<TValue> Get(TKey key, CancellationToken cancel);

        Task<bool> ContainsKey(TKey key, CancellationToken cancel);

        Task<bool> Remove(TKey key, CancellationToken cancel);
    }
}
