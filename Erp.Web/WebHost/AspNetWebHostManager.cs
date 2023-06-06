using Erp.Web.Configuration;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Web.WebHost
{
    internal static class AspNetWebHostManager
    {
        private static ILogLite log = LogManager.GetLoggerFor(nameof(AspNetWebHostManager));
       // public static IWebHostEnvironment? HostingEnvironment { get; private set; }

        //internal static IWebHost New(AppConfig config, Action<IServiceCollection> configureServices, string[] args)
        //{
        //    //return CreateHostBuilder(config, configureServices, args).Build();
        //}

        //private static IWebHostBuilder CreateHostBuilder(AppConfig config, Action<IServiceCollection> configureServices, string[] args) =>
        //    WebApplication.CreateBuilder(args)
        //        .WebHost
        //        //.ConfigureWebHostDefaults()
        //        .ConfigureAppConfiguration((hostContext, configApp) =>
        //        {
        //            HostingEnvironment = hostContext.HostingEnvironment;
        //        })

        //        .ConfigureServices((hostContext, services) =>
        //        {
        //            services.AddSingleton(config);

        //        })

        //    ;
    }
}
