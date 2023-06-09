﻿using Adapters;
using Ports;

namespace Responder
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddTransient<IPinger, SenderPinger>();

            services.AddControllers();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Start Responder");

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
