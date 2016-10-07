namespace MeetEric.Common
{
    using System;

    public class StaticVersionService : IVersionService
    {
        public StaticVersionService(string version)
        {
            Version = version;
        }

        private string Version { get; }

        public string GetVersion()
        {
            return Version;
        }
    }
}
