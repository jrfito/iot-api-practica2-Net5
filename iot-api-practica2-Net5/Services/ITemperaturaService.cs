using iot_api_practica2_Net5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iot_api_practica2_Net5.Services
{
    public interface ITemperaturaService
    {
        Task<TemperaturaViewModel> GetTemperaturaByIdAsync(int tempeturaId);
        Task<IEnumerable<TemperaturaViewModel>> GetTemperaturasAsync();
        Task PostTemperatura(TemperaturaPostModel model);
    }
}
