using Erp.Web.Configuration;
using Infrastructure.Processing;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Web.WebHost
{
    internal sealed partial class ErpProcessor : MainProcessor<AppConfig>
    {
        public ErpProcessor(string[] args)
            : base("ErpProcessor", new AppConfigProvider(), args)
        {

        }

        protected override void KeepRunning(AppConfig config, ProcessCancellationAwaiter processCancellation)
        {
            config.OverrideConfigByCommandLineArgs(this.args);

            using (var webHost = AspNetWebHostManager.New(config, this.ConfigureServices, this.args))
            {
                var tasks = new List<Task>
                {
                    webHost.StartAsync(processCancellation.Token).ContinueWith(_ => this.log.Success("Web server is running")),
                };

                Task.WaitAll(tasks.ToArray());

                processCancellation.WaitCancellation();

                webHost.StopAsync().Wait();
            }
        }
        partial void ConfigureServices(IServiceCollection container);
    }
}
