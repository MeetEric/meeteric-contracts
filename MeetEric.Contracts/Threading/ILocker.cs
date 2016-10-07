namespace MeetEric.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILocker
    {
        Task<IDisposable> Lock();
    }
}
