using AutoMapper;
using iot_api_practica2_Net5.Data_Entities;
using iot_api_practica2_Net5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iot_api_practica2_Net5
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Add mappings here...
            SetTemperaturaMappings();
           
        }

        private void SetTemperaturaMappings()
        {
            CreateMap<TemperaturaPostModel, TemperaturaEntity>();
            CreateMap<TemperaturaEntity, TemperaturaViewModel>();
        }
    }
}
