namespace MeetEric.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Net.Http;
    using MeetEric.Security;

    public interface ILoggingContext : IDisposable
    {
        IIdentifier RequestId { get; }

        void LogEvent(string eventName, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null);

        void LogMetric(string metricName, double metricValue = 1, Dictionary<string, string> contextProperties = null);

        void LogException(Exception exception, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null);

        void LogRetry(Exception exception, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null);

        void LogRequest(DateTimeOffset origination, HttpRequestMessage request, HttpResponseMessage response);
    }
}
