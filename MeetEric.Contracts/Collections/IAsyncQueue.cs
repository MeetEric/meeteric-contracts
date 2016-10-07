namespace MeetEric.Collections
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAsyncQueue<T> : IAsyncCollection
    {
        Task Enqueue(T item, CancellationToken cancel);

        Task<T> Dequeue(CancellationToken cancel);

        Task<T> Peek(CancellationToken cancel);
    }
}
