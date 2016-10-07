namespace MeetEric.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MeetEric.Exceptions;

    public class AsyncSemaphore
    {
        private static readonly Task _completed;
        private readonly Queue<TaskCompletionSource<bool>> _waiters;

        static AsyncSemaphore()
        {
            _completed = Task.FromResult(true);
        }

        public AsyncSemaphore()
            : this(1)
        {
        }

        public AsyncSemaphore(int initialCount)
        {
            Throw.IfOutOfRange(nameof(initialCount), initialCount > 0);
            _waiters = new Queue<TaskCompletionSource<bool>>();
            CurrentCount = initialCount;
        }

        private int CurrentCount { get; set; }

        public Task WaitAsync()
        {
            lock (_waiters)
            {
                if (CurrentCount > 0)
                {
                    --CurrentCount;
                    return _completed;
                }
                else
                {
                    var waiter = new TaskCompletionSource<bool>();
                    _waiters.Enqueue(waiter);
                    return waiter.Task;
                }
            }
        }

        public void Release()
        {
            TaskCompletionSource<bool> toRelease = null;
            lock (_waiters)
            {
                if (_waiters.Count > 0)
                {
                    toRelease = _waiters.Dequeue();
                }
                else
                {
                    ++CurrentCount;
                }
            }

            if (toRelease != null)
            {
                toRelease.SetResult(true);
            }
        }
    }
}
