namespace MeetEric.Diagnostics
{
    using System;
    using MeetEric.Security;

    public class LoggingServiceDecorator : ILoggingService
    {
        private readonly ILoggingService _logger;

        public LoggingServiceDecorator(ILoggingService service)
        {
            _logger = service;
        }

        public virtual void AddExtension(ILoggingExtension extension)
        {
            _logger.AddExtension(extension);
        }

        public virtual ILoggingContext CreateLoggingContext()
        {
            return _logger.CreateLoggingContext();
        }

        public virtual ILoggingContext CreateLoggingContext(IIdentifier requestId)
        {
            return _logger.CreateLoggingContext(requestId);
        }

        public virtual void EnableVerboseLogging()
        {
            _logger.EnableVerboseLogging();
        }

        public virtual IDiagnostics ReadDiagnostics()
        {
            return _logger.ReadDiagnostics();
        }
    }
}
