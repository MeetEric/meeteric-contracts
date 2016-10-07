namespace MeetEric.Collections
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Security;

    public interface IAsyncSet<TValue> : IAsyncCollection
    {
        Task<IEnumerable<TValue>> GetItems(CancellationToken cancel);

        Task<bool> Add(TValue value, CancellationToken cancel);

        Task<bool> Contains(TValue value, CancellationToken cancel);

        Task<bool> Remove(TValue value, CancellationToken cancel);
    }
}
