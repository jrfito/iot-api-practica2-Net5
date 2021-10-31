using AutoMapper;
using iot_api_practica2_Net5.Data_Context;
using iot_api_practica2_Net5.Data_Entities;
using iot_api_practica2_Net5.Hubs;
using iot_api_practica2_Net5.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iot_api_practica2_Net5.Services.Implementations
{
    public class TemperaturaService : ITemperaturaService
    {
        private readonly IoT_Context _dbContext;
        private readonly IMapper _mapper;
        private readonly IHubContext<DashBoardHub> _hubContext;

        public TemperaturaService(IoT_Context dbContext, 
            IMapper mapper,
            IHubContext<DashBoardHub> hubContext)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._hubContext = hubContext;
        }

        public async Task PostTemperatura(TemperaturaPostModel model)
        {
            try
            {
                var temperatura = await _dbContext.Temperatura.AddAsync(_mapper.Map<TemperaturaEntity>(model));
                await _dbContext.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("UpdateTemperatura",_mapper.Map<TemperaturaViewModel>(temperatura.Entity));
            }
            catch (DbUpdateException e)
            {

                throw new Exception(e.InnerException.Message);
            }
        }
        public async Task<IEnumerable<TemperaturaViewModel>> GetTemperaturasAsync()
        {
            try
            {
                return _mapper.Map<IEnumerable<TemperaturaViewModel>>(await _dbContext.Temperatura.ToListAsync());
            }
            catch (DbUpdateException e)
            {

                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<TemperaturaViewModel> GetTemperaturaByIdAsync(int tempeturaId)
        {
            try
            {
                TemperaturaViewModel temperatura = _mapper.Map<TemperaturaViewModel>(await _dbContext.Temperatura.FindAsync(tempeturaId));
                if(temperatura == null)
                {
                    throw new NullReferenceException("No se encontro temperatura");
                }

                return temperatura;
            }
            catch (DbUpdateException e)
            {

                throw new Exception(e.InnerException.Message);
            }
        }
    }
}
