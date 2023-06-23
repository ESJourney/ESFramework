using Erp.Web.AspNet.WebHost.Common;
using Erp.Web.Configuration;
using Infrastructure.Logging;
using Infrastructure.Configuration.App;
using Erp.Web.WebHost;
using Infrastructure.EntityFramework;

namespace Erp.Web.AspNet.WebHost
{
    public sealed class ErpDatabaseManager : IDisposable
    {
        //Campos
        private readonly ILogLite log = LogManager.GetLoggerFor<ErpDatabaseManager>();
        private readonly AppConfig config;
        private readonly IServiceProvider container;
        private readonly string[] args;
        private readonly IWebHostEnvironment environment;

        public ErpDatabaseManager(AppConfig config, IServiceProvider container, string[] args)
        {
            this.config = config;
            this.container = container;
            this.args = args;
            this.environment = container.Resolve<IWebHostEnvironment>();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        internal void InitializeDatabases(CancellationToken token)
        {
            this.log.Info(string.Format("Initializing databases for {0}", this.environment.IsDevelopment() ? $"DEV {config.Development?.GetOLTP().ToString()} enviroment" : "PROD"));

            if (this.environment.IsDevelopment())
                this.InitInDevMode(token);
            else
                this.InitInProdMode(token);
        }

        private void InitInDevMode(CancellationToken token)
        {
            //var config = this.container.Resolve<AppConfig>();

            // Dropping databases if necessary
            var shouldDropDb = this.args is null ? false : CliParam.ParamIsSet(this.args, CliParam.DropDb);
            var shouldRestore = this.args is null ? false : CliParam.ParamIsSet(this.args, CliParam.Restore);

            if (shouldDropDb && shouldRestore)
                shouldDropDb = false;

            var taskList = new List<Task>();
            if (shouldDropDb)
                taskList.AddRange(
                        container
                            .ResolveAll<IEfDbInitializer>()
                            .Select(x => Task.Run(() => x.DropAndCreateDb())));
            else
                taskList.AddRange(
                        container
                            .ResolveAll<IEfDbInitializer>()
                            .Select(x => Task.Run(() => x.EnsureDatabaseExistsAndItsUpdated())));

        }

        private void InitInProdMode(CancellationToken token)
        {
            throw new NotImplementedException();
        }

    }
}
