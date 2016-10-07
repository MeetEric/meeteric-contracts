namespace MeetEric.Diagnostics
{
    using System.Threading.Tasks;
    using Security;

    public interface IWatchdog
    {
        IIdentifier Id { get; }

        Task Examine(IWatchdogContext context);
    }

    public interface IWatchdogContext
    {
        IWatchdogNodeContext Node { get; }
    }

    public interface IWatchdogNodeContext
    {
        Task ReportInformation(IIdentifier id, string message);

        Task ReportWarning(IIdentifier id, string message);

        Task ReportError(IIdentifier id, string message);
    }

    public interface IWatchdogLoggingFactory
    {
        IWatchdogContext CreateWatchdogContext();
    }
}
