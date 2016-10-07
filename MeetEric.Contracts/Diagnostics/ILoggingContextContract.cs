namespace MeetEric.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MeetEric.Exceptions;

    public class ILoggingContextContract : ILoggingContextDecorator
    {
        public ILoggingContextContract(ILoggingContext context)
            : base(context)
        {
        }

        public override void LogEvent(string eventName, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
        {
            Throw.IfNullOrEmpty(nameof(eventName), eventName);
            base.LogEvent(eventName, contextProperties, metrics);
        }

        public override void LogException(Exception exception, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
        {
            Throw.IfArgumentNull(nameof(exception), exception);
            base.LogException(exception, contextProperties, metrics);
        }

        public override void LogMetric(string metricName, double metricValue = 1, Dictionary<string, string> contextProperties = null)
        {
            Throw.IfNullOrEmpty(nameof(metricName), metricName);
            base.LogMetric(metricName, metricValue, contextProperties);
        }

        public override void LogRetry(Exception exception, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
        {
            Throw.IfArgumentNull(nameof(exception), exception);
            base.LogRetry(exception, contextProperties, metrics);
        }
    }
}
