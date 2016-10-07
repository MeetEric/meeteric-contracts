namespace MeetEric.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TimedEvent : ILoggingEvent
    {
        private TimedEvent(string eventType, string name, ILoggingContext log, TimeSpan minTime, bool logStart = true)
        {
            Logger = log;
            Name = name;
            EventName = eventType;
            Metrics = new Dictionary<string, double>();
            MinTime = minTime;
            Properties = new Dictionary<string, string>
            {
                { "Name", name }
            };

            if (logStart)
            {
                log.LogEvent($"Start{EventName}", Properties);
            }

            StartTime = DateTimeOffset.UtcNow;
        }

        private string Name { get; }

        private string EventName { get; }

        private DateTimeOffset StartTime { get; }

        private ILoggingContext Logger { get; }

        private TimeSpan MinTime { get; }

        private Dictionary<string, string> Properties { get; }

        private Dictionary<string, double> Metrics { get; }

        public static ILoggingEvent Log(string eventType, string name, ILoggingContext log, bool skipStart = false)
        {
            return Log(eventType, name, log, TimeSpan.FromSeconds(1.0), skipStart);
        }

        public static ILoggingEvent Log(string eventType, string name, ILoggingContext log, TimeSpan minTime, bool skipStart = false)
        {
            return new TimedEvent(eventType, name, log, minTime, logStart: !skipStart);
        }

        public static ILoggingEvent LongOperation(string name, ILoggingContext log)
        {
            return Log("LongOperation", name, log, TimeSpan.FromSeconds(1.0), true);
        }

        public void AddProperty(string name, string value)
        {
            Properties[name] = value;
        }

        public void AddMetric(string name, double value)
        {
            Metrics[name] = value;
        }

        public void Dispose()
        {
            var time = DateTimeOffset.UtcNow - StartTime;

            if (time > MinTime)
            {
                Metrics["Count"] = 1;
                Metrics["Duration"] = time.TotalSeconds;
                Logger.LogEvent($"Stop{EventName}", Properties, Metrics);
            }
        }
    }
}
