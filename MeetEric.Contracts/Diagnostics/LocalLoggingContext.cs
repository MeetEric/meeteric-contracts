namespace MeetEric.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using Exceptions;
    using Security;

    public class LocalLoggingContext : ILoggingContext
    {
        public LocalLoggingContext(IIdentifier requestId)
        {
            Throw.IfArgumentNull(nameof(requestId), requestId);
            RequestId = requestId;
        }

        public IIdentifier RequestId { get; }

        public void Dispose()
        {
        }

        public void LogEvent(string eventName, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
        {
            Debug.WriteLine($"Event: {eventName}");
        }

        public void LogException(Exception exception, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
        {
            Debug.WriteLine(exception.ToString());
        }

        public void LogMetric(string metricName, double metricValue = 1, Dictionary<string, string> contextProperties = null)
        {
            Debug.WriteLine($"Metric {metricName} '{metricValue}'");
        }

        public void LogRequest(DateTimeOffset origination, HttpRequestMessage request, HttpResponseMessage response)
        {
        }

        public void LogRetry(Exception exception, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
        {
            Debug.WriteLine("Retry");
            LogException(exception);
        }
    }
}
