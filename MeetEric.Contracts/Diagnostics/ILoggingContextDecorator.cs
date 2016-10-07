namespace MeetEric.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Security;

    public abstract class ILoggingContextDecorator : ILoggingContext
    {
        protected ILoggingContextDecorator(ILoggingContext context)
        {
            Context = context;
        }

        public virtual IIdentifier RequestId
        {
            get
            {
                return Context.RequestId;
            }
        }

        private ILoggingContext Context { get; }

        public virtual void Dispose()
        {
            Context.Dispose();
        }

        public virtual void LogEvent(string eventName, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
        {
            Context.LogEvent(eventName, contextProperties, metrics);
        }

        public virtual void LogException(Exception exception, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
        {
            Context.LogException(exception, contextProperties, metrics);
        }

        public virtual void LogMetric(string metricName, double metricValue = 1, Dictionary<string, string> contextProperties = null)
        {
            Context.LogMetric(metricName, metricValue, contextProperties);
        }

        public virtual void LogRequest(DateTimeOffset origination, HttpRequestMessage request, HttpResponseMessage response)
        {
            Context.LogRequest(origination, request, response);
        }

        public virtual void LogRetry(Exception exception, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
        {
            Context.LogRetry(exception, contextProperties, metrics);
        }
    }
}
