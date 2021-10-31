using iot_api_practica2_Net5.Data_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iot_api_practica2_Net5.Data_Context
{
    public class IoT_Context : DbContext
    {
        public IoT_Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #region DbSet
        public DbSet<TemperaturaEntity> Temperatura { get; set; }
        #endregion


    }
}
