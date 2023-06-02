using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class LazyLogger : ILogLite
    {
        private readonly Lazy<ILogLite> log;
        public bool VerboseEnabled => throw new NotImplementedException();

        public LazyLogger(Func<ILogLite> factory)
        {
            Ensure.NotNull(factory, nameof(factory));

            this.log = new Lazy<ILogLite>(factory);
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
            this.log.Value.Fatal(message);
        }

        public void Fatal(Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            this.log.Value.Info(message);
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
