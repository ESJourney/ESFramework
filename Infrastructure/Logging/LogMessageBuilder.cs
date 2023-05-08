using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class LogMessageBuilder
    {
        private static readonly int _processId = Process.GetCurrentProcess().Id;
        private readonly string componentName;
        private readonly bool verbose;

        public LogMessageBuilder(string componentName, bool verbose)
        {
            Ensure.NotEmpty(componentName, nameof(componentName));

            this.componentName = componentName;
            this.verbose = verbose;
        }

    }
}
