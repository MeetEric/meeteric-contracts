namespace MeetEric.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AsyncLock : ILocker
    {
        private readonly AsyncSemaphore _lock;

        public AsyncLock()
        {
            _lock = new AsyncSemaphore();
        }

        public async Task<IDisposable> Lock()
        {
            await _lock.WaitAsync();
            return new LockRelease(_lock);
        }

        private class LockRelease : IDisposable
        {
            private readonly AsyncSemaphore _lock;

            public LockRelease(AsyncSemaphore @lock)
            {
                _lock = @lock;
            }

            public void Dispose()
            {
                _lock.Release();
            }
        }
    }
}
