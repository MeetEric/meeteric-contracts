namespace MeetEric.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Diagnostics;
    using Threading.Tasks;

    public class MultiWorker : IWorkerRole
    {
        public MultiWorker(ILoggingContext log, params IWorkerRole[] workers)
        {
            Workers = workers;
            Log = log;
        }

        private IEnumerable<IWorkerRole> Workers { get; }

        private ILoggingContext Log { get; }

        public async Task Run(CancellationToken cancellationToken)
        {
            Log.LogEvent("Run");
            var tasks = Workers.Select(w => Task.Run(() => w.Run(cancellationToken)));
            await Task.WhenAll(tasks);
        }

        public async Task<bool> Start()
        {
            using (TimedEvent.Log("MultiWorkerStart", this.GetType().Name, Log))
            {
                var tasks = Workers.Select(w => new Func<Task<bool>>(w.Start)).InParallel();
                return (await Task.WhenAll(tasks)).All(x => x);
            }
        }

        public async Task Stop()
        {
            using (TimedEvent.Log("MultiWorkerStop", this.GetType().Name, Log))
            {
                var tasks = Workers.Select(w => new Func<Task>(w.Stop)).InParallel();
                await Task.WhenAll(tasks);
            }
        }
    }
}
