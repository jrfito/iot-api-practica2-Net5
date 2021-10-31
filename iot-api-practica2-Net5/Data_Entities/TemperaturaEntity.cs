using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iot_api_practica2_Net5.Data_Entities
{
    public class TemperaturaEntity
    {
        [Key]
        public int Id { get; set; }
        public string Dispositivo { get; set; }
        public int Temperatura { get; set; }
        public DateTime Fecha { get; set; }
    }
}
