using iot_api_practica2_Net5.Models;
using iot_api_practica2_Net5.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace iot_api_practica2_Net5.Hubs
{
    public class DashBoardHub : Hub
    {
        private readonly ITemperaturaService _temperaturaService;

        public DashBoardHub(ITemperaturaService temperaturaService)
        {
            this._temperaturaService = temperaturaService;
        }

        public async Task<object> InitDashBoard()
        {
            return new object();
        }

        public async Task UpdateDashBoard(string message)
        {
            await Clients.All.SendAsync("DashBoardUpdate", message);
        }

        public TemperaturaViewModel UpdateTemperatura(TemperaturaViewModel model)
        {
            return model;
        }
    }
}
