namespace MeetEric.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface INormalizedIdentifier : IIdentifier
    {
        IIdentifier Normalized { get; }
    }
}
