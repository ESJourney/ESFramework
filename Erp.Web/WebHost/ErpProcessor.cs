﻿using Erp.Web.Configuration;
using Infrastructure.Processing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

            //using (var webHot = AspNetWebHostManager.New(config, this.ConfigureServices, this.args))
            //{

            //}
        }
        partial void ConfigureServices(IServiceCollection container);
    }
}
