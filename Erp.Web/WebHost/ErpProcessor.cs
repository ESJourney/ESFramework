using Erp.Web.Configuration;
using Infrastructure.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Web.WebHost
{
    internal sealed class ErpProcessor : MainProcessor<AppConfig>
    {
        public ErpProcessor(string[] args) 
            : base("ErpProcessor", new AppConfigProvider(), args)
        {

        }

        protected override void KeepRunning(AppConfig config, ProcessCancellationAwaiter processCancellation)
        {
            config.OverrideConfigByCommandLineArgs(this.args);
           
            this.log.Info("Fin del KeepRunning");
        }
    }
}
