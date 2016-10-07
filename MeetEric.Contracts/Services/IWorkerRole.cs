namespace MeetEric.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IWorkerRole
    {
        Task<bool> Start();

        Task Run(CancellationToken cancellationToken);

        Task Stop();
    }
}
