using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public abstract class ConfigurationProvider<T> : IConfigurationProvider<T>, IDisposable where T : class
    {
        private readonly string jsonFile;
        public T Configuration => throw new NotImplementedException();

        protected ConfigurationProvider(string jsonFile)
        {
            this.jsonFile = Ensured.NotNull(jsonFile, nameof(jsonFile));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
