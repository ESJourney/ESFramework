using Erp.Web.Configuration;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
        public static IWebHostEnvironment? HostingEnvironment { get; private set; }
        public static WebApplicationBuilder? webAppBuilder;

        internal static IHost New(AppConfig config, Action<IServiceCollection> configureServices, string[] args)
        {
            webAppBuilder = WebApplication.CreateBuilder(args);
            CreateHostBuilder(config, configureServices);
            var webApplication = webAppBuilder.Build();
            ConfigureApp(webApplication);

            return webApplication;
        }

        private static IWebHostBuilder CreateHostBuilder(AppConfig config, Action<IServiceCollection> configureServices) =>
                 // Aquí explica como la clase  WebApplicationBuilder contiene todo lo que contenía antes WebHost
                 // https://learn.microsoft.com/es-es/aspnet/core/migration/50-to-60?view=aspnetcore-7.0&tabs=visual-studio#building-libraries-for-aspnet-core-6
                 webAppBuilder!.WebHost
                .ConfigureKestrel((context, options) =>
                {
                    // More info: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.1#kestrel-maximum-request-body-size
                    // Handle resquests up to 50 MB
                    options.Limits.MaxRequestBodySize = 52428800;
                })

                // This method gets called by the runtime. Use this method to add services to the container.
                // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    HostingEnvironment = hostContext.HostingEnvironment;
                })
                .UseUrls(config.Url)
                // This method gets called by the runtime. Use this method to add services to the container.
                // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
                .ConfigureServices(services =>
                {
                    services
                       .AddSingleton(config)
                       .AddSingleton(HostingEnvironment!);
                    /// thanks to https://www.netmentor.es/Entrada/implementar-httpclient#:~:text=HttpClient%20es%20la%20clase%20que%20nos%20proporciona%20C%23,que%20consultamos%20o%20enviamos%20informaci%C3%B3n%20de%20una%20API.
                    /// <see cref="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.1#httpclient-and-lifetime-management"/>
                    services.AddHttpClient("SbTechology", client =>
                    {
                        client.BaseAddress = new Uri(config.ExternalApis?.SbTecnology?.BaseAddress!);
                    });
                    // MORE INFO IN LOG: https://stackoverflow.com/questions/47622544/asp-net-core-loglevel-not-working
                    LogManager.EnableVerbose = config.Settings.VerboseLogLevel;

                    if (config.Settings.VerboseLogLevel)
                        services.AddLogging(builder =>
                        {
                            builder.SetMinimumLevel(LogLevel.Debug);
                        });
                    else
                        services.AddLogging(builder =>
                        {
                            builder.SetMinimumLevel(LogLevel.Trace);
                            builder.AddFilter("Microsoft", LogLevel.Warning);
                            builder.AddFilter("System", LogLevel.Warning);
                            builder.AddFilter("Engine", LogLevel.Warning);
                        });

                    configureServices(services);

                    services.AddMvc()

                     .AddNewtonsoftJson(options =>
                     {
                         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // for navigation stuff of entity framework

                         /// To be coherent with <see cref="Infrastructure.Serialization.NewtonsoftJsonSerializer"/.
                         options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                         options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                     });
                });
        private static void ConfigureApp(WebApplication webApplication)
        {
            if (HostingEnvironment.IsDevelopment())
                webApplication.UseDeveloperExceptionPage();
            else
            {
                // Error handling: https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-3.1
                webApplication.UseExceptionHandler("/error");

                // TODO: Investigar si esto es necesario:
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            webApplication.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Para usar Razor Pages
            webApplication.UseStaticFiles();

            webApplication.UseRouting();

            webApplication.MapRazorPages();
            webApplication.MapControllers();
            webApplication.MapFallbackToFile("index.html");
        }


        //En lugar de configurar la tubería de middleware mediante el método Configure(),
        //debe utilizar la WebApplication devuelta por el método WebApplicationBuilder.Build(),
        //que proporciona acceso a un constructor de tuberías que le permite construir la tubería de middleware de su aplicación

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //.Configure(app =>
        //{
        //    if (HostingEnvironment.IsDevelopment())
        //        app.UseDeveloperExceptionPage();
        //    else
        //    {
        //        // Error handling: https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-3.1
        //        app.UseExceptionHandler("/error");

        //        // TODO: Investigar si esto es necesario:
        //        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //        //app.UseHsts();
        //    }

        //    app.UseForwardedHeaders(new ForwardedHeadersOptions
        //    {
        //        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        //    });

        //});
    }
}
