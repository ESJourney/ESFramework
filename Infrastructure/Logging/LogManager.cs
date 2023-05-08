using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public static class LogManager
    {
        private static Func<string, ILogLite> logFactory = n => new ConsoleLogger(n, EnableVerbose);
        public static bool EnableVerbose { get; set; }
        public static ILogLite GetLoggerFor<T>()
        {
            return GetLoggerFor(typeof(T).Name);
        }

        public static ILogLite GetLoggerFor(string name)
        {
            return new LazyLogger(() => logFactory.Invoke(name));
        }
    }
}
