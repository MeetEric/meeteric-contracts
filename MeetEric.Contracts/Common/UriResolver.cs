namespace MeetEric.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Diagnostics;

    public class UriResolver
    {
        public UriResolver(Uri activeUri, ILoggingContext log,  Func<Task<Uri>> resolveUri)
        {
            ActiveUri = activeUri;
            ResolveUri = resolveUri;
            Log = log;
        }

        public Uri ActiveUri { get; private set; }

        private Func<Task<Uri>> ResolveUri { get; }

        private ILoggingContext Log { get; }

        public static async Task<UriResolver> Create(ILoggingContext log, Func<Task<Uri>> resolveUri)
        {
            var activeUri = await resolveUri();
            return new UriResolver(activeUri, log, resolveUri);
        }

        public async Task Refresh()
        {
            var previous = ActiveUri;
            ActiveUri = await ResolveUri();

            var properties = new Dictionary<string, string>
            {
                { "PreviousUri", previous.AbsoluteUri },
                { "ActiveUri", ActiveUri.AbsoluteUri }
            };

            Log.LogEvent("UriRefresh", properties);
        }
    }
}
