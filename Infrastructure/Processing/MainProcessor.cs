using Infrastructure.Configuration;
using Infrastructure.Logging;
using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Processing
{
    public abstract class MainProcessor<T> : IDisposable
    {
        protected readonly ILogLite log;
        protected IConfigurationProvider<T> configProvider;
        protected readonly string processorName;
        protected MainProcessor(string processorName, IConfigurationProvider<T> configProvider, string[] args)
        {
            this.processorName = Ensured.NotEmpty(processorName, nameof(processorName));
            this.configProvider = Ensured.NotNull(configProvider, nameof(configProvider));

            this.log = LogManager.GetLoggerFor(this.processorName);
        }

        public void Run()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
