using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Exercicio06Dapper.Models
{
    public class BancoMundialContext : DbContext
    {       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ex06");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dados> Dados { get; set; }

        public DbSet<Indicador> Indicadores { get; set; }

        public DbSet<Pais> Paises { get; set; }
    }
}