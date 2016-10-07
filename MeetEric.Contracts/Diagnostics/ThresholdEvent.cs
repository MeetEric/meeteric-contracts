namespace MeetEric.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ThresholdEvent : IDisposable
    {
        public ThresholdEvent(string name, TimeSpan threshold, ILoggingContext log, Dictionary<string, string> properties)
        {
            Name = name;
            Threshold = threshold;
            Logger = log;
            StartTime = DateTimeOffset.UtcNow;
            Properties = properties;
        }

        private DateTimeOffset StartTime { get; }

        private TimeSpan Threshold { get; }

        private string Name { get; }

        private ILoggingContext Logger { get; }

        private Dictionary<string, string> Properties { get; }

        public static IDisposable Log(string name, TimeSpan threshold, ILoggingContext log)
        {
            var properties = new Dictionary<string, string>()
            {
                { "Name", name }
            };
            log.LogEvent("StartThreshold", properties);
            return new ThresholdEvent(name, threshold, log, properties);
        }

        public void Dispose()
        {
            var result = DateTimeOffset.UtcNow - StartTime;

            if (result > Threshold)
            {
                var metrics = new Dictionary<string, double>()
                {
                    { "Duration", result.TotalSeconds },
                    { "Threshold", Threshold.TotalSeconds },
                };
                Logger.LogEvent("FailedThreshold", Properties, metrics);
            }
        }
    }
}
