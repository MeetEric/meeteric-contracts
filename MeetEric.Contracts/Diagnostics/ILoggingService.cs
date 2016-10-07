namespace MeetEric.Diagnostics
{
    using System;
    using System.Diagnostics.Contracts;
    using MeetEric.Security;

    public interface ILoggingService
    {
        ILoggingContext CreateLoggingContext();

        ILoggingContext CreateLoggingContext(IIdentifier requestId);

        void EnableVerboseLogging();

        void AddExtension(ILoggingExtension extension);

        IDiagnostics ReadDiagnostics();
    }

    public interface ILoggingExtension
    {
    }
}
