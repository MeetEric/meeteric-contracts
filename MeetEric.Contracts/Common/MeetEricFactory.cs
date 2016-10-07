namespace MeetEric.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using MeetEric.Threading.Tasks;

    public static class MeetEricFactory
    {
        private static readonly Dictionary<Type, Lazy<object>> _services;

        static MeetEricFactory()
        {
            _services = new Dictionary<Type, Lazy<object>>();
        }

        public static bool Exists<T>()
            where T : class
        {
            return _services.ContainsKey(typeof(T));
        }

        public static T GetService<T>()
            where T : class
        {
            Lazy<object> service = null;

            try
            {
                service = _services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"Unable to locate a service of type {typeof(T).Name}");
            }

            try
            {
                return service.Value as T;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to create service of type {typeof(T).Name}", ex);
            }
        }

        public static void RegisterService<T>(Func<T> service)
            where T : class
        {
            _services[typeof(T)] = new Lazy<object>(() => service());
        }
    }
}
