using iot_api_practica2_Net5.Data_Context;
using iot_api_practica2_Net5.Middleware;
using iot_api_practica2_Net5.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iot_api_practica2_Net5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // IoTDDbContext_Connection
            // Contexto de la base de datos
            services.AddDbContext<IoT_Context>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IoTDDbContext_Connection"))
                .EnableSensitiveDataLogging(true).UseLazyLoadingProxies();
            });

            services.AddControllers();
            services.AddCors(options => {
                string[] frontendURL = { "http://localhost:3000", "http://192.168.1.98:3000", "http://192.168.1.98" }; //Configuration.GetValue<string>("frontend_url");
                
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "iot_api_practica2_Net5", Version = "v1" });
            });

            // Services of App estan en el archivo AppServices.cs
            services.AddAppServices();
            // SignalR
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IoT_Context dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iot_api_practica2_Net5 v1"));
            }
            
            var supportedCultures = new[] { "es-MX" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
               .AddSupportedCultures(supportedCultures)
               .AddSupportedUICultures(supportedCultures);

            // Manejo de expciones con MiddleWare
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Se efectua la migración de la base de datos
            dbContext.Database.Migrate();
        }
    }
}
