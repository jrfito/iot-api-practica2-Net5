using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using iot_api_practica2_Net5.Services;
using iot_api_practica2_Net5.Services.Implementations;

namespace iot_api_practica2_Net5
{
    public static class AppServices
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            //Automapper
            services.AddSingleton(provider =>
            {
                return new MapperConfiguration(config =>
                {
                    config.AddProfile<AutoMapperProfile>();
                    config.ConstructServicesUsing(type =>
                    ActivatorUtilities.GetServiceOrCreateInstance(provider, type));
                }).CreateMapper();
            });
            // Aquí se agregan los servicios injectables
            // Servcios de los endpoints
            services.AddTransient<ITemperaturaService, TemperaturaService>();
        }

    }
}
