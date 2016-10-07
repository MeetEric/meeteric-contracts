namespace MeetEric.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Security;

    public class ILoggerServiceContract : LoggingServiceDecorator
    {
        public ILoggerServiceContract(ILoggingService service)
            : base(service)
        {
        }

        public override ILoggingContext CreateLoggingContext(IIdentifier requestId)
        {
            Throw.IfArgumentNull(nameof(requestId), requestId);
            var context = base.CreateLoggingContext(requestId);
            Throw.IfArgumentNull(nameof(context), context);
            return new ILoggingContextContract(context);
        }

        public override ILoggingContext CreateLoggingContext()
        {
            var context = base.CreateLoggingContext();
            Throw.IfArgumentNull(nameof(context), context);
            return new ILoggingContextContract(context);
        }
    }
}
