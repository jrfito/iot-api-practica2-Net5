using iot_api_practica2_Net5.Models;
using iot_api_practica2_Net5.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iot_api_practica2_Net5.Controllers
{
    [DisableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TemperaturaController : ControllerBase
    {
        private readonly ITemperaturaService _temperaturaService;

        public TemperaturaController(ITemperaturaService temperaturaService)
        {
            this._temperaturaService = temperaturaService;
        }
        /// <summary>
        /// Se guarda la temperatura del deipositico
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post(TemperaturaPostModel model)
        {
            await _temperaturaService.PostTemperatura(model);
            return StatusCode(200);
        }
        /// <summary>
        /// Se obtiene todas las lecturas de temperarura guardadas del dispositico
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TemperaturaViewModel>),200)]
        public async Task<IActionResult> Get()
        {
            return StatusCode(200, await _temperaturaService.GetTemperaturasAsync());
        }
        /// <summary>
        /// Se obtiene el detalla de temperatura grabada del dispositivo
        /// </summary>
        /// <param name="temperaturaId"></param>
        /// <returns></returns>
        [HttpGet("{temperaturaId}")]
        [ProducesResponseType(typeof(IEnumerable<TemperaturaViewModel>), 200)]
        public async Task<IActionResult> GetById(int temperaturaId)
        {
            return StatusCode(200, await _temperaturaService.GetTemperaturaByIdAsync(temperaturaId));
        }
    }
}
