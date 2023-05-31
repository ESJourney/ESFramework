using Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public abstract class ConfigurationProvider<T> : IConfigurationProvider<T>, IDisposable
        where T : class
    {
        // Campos
        private readonly string jsonFile;
        private readonly FileSystemWatcher watcher;
        private Lazy<T> lazyConfig;
        private readonly object lockObject = new object();
        private T? configuration = null;
        private Timer? timer = null;
        // Propiedades
        public T Configuration => this.configuration ?? this.lazyConfig.Value;
        // Métodos virtuales
        protected virtual void OnConfigParsing(T? config, IConfigurationRoot configRoot)
        {
        }

        protected ConfigurationProvider(string jsonFile)
        {
            this.jsonFile = Ensured.NotNull(jsonFile, nameof(jsonFile));
            this.watcher = new FileSystemWatcher();
            this.lazyConfig = new Lazy<T>(this.BuildConfigurationFirstTime);
        }

        private T BuildConfigurationFirstTime()
        {
            lock (this.lockObject)
            {
                if (this.configuration is null)
                {
                    this.SetConfigFromFile();
                    this.StartFileWatching();
                }

                return this.configuration!;
            }
        }

        private void SetConfigFromFile()
        {
            var configRoot = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile(this.jsonFile)
                                    .Build();

            var config = configRoot.Get<T>();

            this.OnConfigParsing(config, configRoot);

            this.configuration = config;
        }
        private void StartFileWatching()
        {
            this.watcher.Path = Directory.GetCurrentDirectory();
            this.watcher.Filter = this.jsonFile;
            // this prevents to call for multiple changes
            this.watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            this.watcher.Changed += OnFileChanged;
            this.watcher.Renamed += OnFileChanged;
            this.watcher.EnableRaisingEvents = true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
