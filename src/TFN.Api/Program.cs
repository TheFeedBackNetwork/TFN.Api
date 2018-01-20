using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TFN.Mvc.Extensions;

namespace TFN.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot(Directory.GetCurrentDirectory() + "/wwwroot")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config.SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();

                    if (env.IsLocal())
                        config.AddUserSecrets("tfn-local");

                    if (!env.IsLocal())
                    {
                        //might pull from secrets?
                    }

                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));

                    if (env.IsLocal())
                        logging
                            .AddConsole()
                            .AddDebug();

                    if (!env.IsLocal())
                    {
                        logging.AddAzureWebAppDiagnostics();

                    }
                })
                .Build();

            host.Run();
        }
    }
}
