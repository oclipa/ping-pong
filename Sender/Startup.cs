using Sender.Adapters;
using Sender.Ports;

namespace Sender
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            //services.AddHttpClient("responderClient", c => c.BaseAddress = new System.Uri("http://localhost:5083"));

            services.AddTransient<IPinger, ResponderPinger>();

            services.AddControllers();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Start Sender");

            app.UseRouting();

            app.UseCors();

            if (env.EnvironmentName == "Development")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
