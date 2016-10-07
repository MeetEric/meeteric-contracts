namespace MeetEric.Collections
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAsyncCollection : IReliableObject
    {
        Task<long> Count { get; }

        Task Clear(CancellationToken cancelToken);
    }
}
