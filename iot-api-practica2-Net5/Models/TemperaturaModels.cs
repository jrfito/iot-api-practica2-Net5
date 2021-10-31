using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iot_api_practica2_Net5.Models
{
    public class TemperaturaPostModel
    {
        [Required]
        public string Dispositivo { get; set; }
        [Required]
        public int Temperatura { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }

    public class TemperaturaViewModel
    {
        public int Id { get; set; }
        public string Dispositivo { get; set; }
        public int Temperatura { get; set; }
        public DateTime Fecha { get; set; }
    }
}
