namespace MeetEric.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using MeetEric.Common;
    using Diagnostics;

    public abstract class WorkerBase : IWorkerRole
    {
        protected WorkerBase(ILoggingContext logger)
        {
            Log = logger;
            CancellationSource = new CancellationTokenSource();
            Completed = new ManualResetEvent(false);
            LogProperties = new Dictionary<string, string>()
            {
                { "Name", this.GetType().Name }
            };
        }

        protected ILoggingContext Log { get; }

        private CancellationTokenSource CancellationSource { get; }

        private ManualResetEvent Completed { get; }

        private Dictionary<string, string> LogProperties { get; }

        public async Task Run(CancellationToken cancellationToken)
        {
            var localSource = CancellationTokenSource.CreateLinkedTokenSource(CancellationSource.Token, cancellationToken);
            Log.LogEvent("WorkerRun", LogProperties);
            await RunInternal(localSource.Token);
            Completed.Set();
        }

        public virtual Task<bool> Start()
        {
            return Task.FromResult(true);
        }

        public virtual Task Stop()
        {
            using (TimedEvent.Log("WorkerStop", this.GetType().Name, Log))
            {
                CancellationSource.Cancel();

                if (!Completed.WaitOne(TimeSpan.FromMinutes(3)))
                {
                    Log.LogEvent("FailedToStop", LogProperties);
                }

                return Task.FromResult(true);
            }
        }

        protected abstract Task RunInternal(CancellationToken cancelled);
    }
}
