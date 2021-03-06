﻿namespace MeetEric.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IIdentityFactory
    {
        IIdentifier Create();

        IIdentifier Parse(string idMoniker);
    }
}
