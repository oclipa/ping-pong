using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sender
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .UseConsoleLifetime()
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        { 
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureLogging((Action<HostBuilderContext, ILoggingBuilder>) ((hostingContext, logging) =>
            {
                logging.ClearProviders();
                logging.AddConfiguration((IConfiguration)hostingContext.Configuration.GetSection("Logging"));
                logging.AddDebug();
                logging.AddEventSourceLogger();

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    logging.AddConsole();
                }
            }));

            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

            return builder;
        }
    }
}