using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class ConsoleLogger : ILogLite
    {
        public bool VerboseEnabled => throw new NotImplementedException();

        public ConsoleLogger(string componentName, bool showVerbose)
        {
            Ensure.NotEmpty(componentName, nameof(componentName));

        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void Error(object serializablePayload, Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Success(string message)
        {
            throw new NotImplementedException();
        }

        public void Verbose(string message)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }

        public void Warning(object serializablePayload, string message)
        {
            throw new NotImplementedException();
        }
    }
}
