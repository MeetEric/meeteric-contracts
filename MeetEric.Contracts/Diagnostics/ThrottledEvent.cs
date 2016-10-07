namespace MeetEric.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class ThrottledEvent : IDisposable
    {
        private int _count;

        public ThrottledEvent(string eventName, ILoggingContext context, TimeSpan maxOccurance)
        {
            Name = eventName;
            Log = context;
            MaxOccurance = maxOccurance;
            NextEvent = DateTimeOffset.UtcNow;
        }

        private string Name { get; }

        private ILoggingContext Log { get; }

        private TimeSpan MaxOccurance { get; }

        private DateTimeOffset NextEvent { get; set; }

        public void LogEvent(Dictionary<string, string> properties = null, Dictionary<string, double> metrics = null)
        {
            if (_count > 0)
            {
                if (metrics == null)
                {
                    metrics = new Dictionary<string, double>();
                }

                metrics["Count"] = _count;
            }

            if (DateTimeOffset.UtcNow > NextEvent)
            {
                Log.LogEvent(Name, properties, metrics);
                NextEvent = DateTimeOffset.UtcNow + MaxOccurance;
            }
        }

        public void Increment(int incrementBy = 1)
        {
            Interlocked.Add(ref _count, incrementBy);
        }

        public void Dispose()
        {
            NextEvent = DateTimeOffset.Now;
            LogEvent();
        }
    }
}
