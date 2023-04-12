namespace Responder
{
    public class Program
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

            builder.ConfigureLogging((Action<HostBuilderContext, ILoggingBuilder>)((hostingContext, logging) =>
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