using GaveteiroLanches.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaveteiroLanches.Dominio.Mapeamento
{
    public class BaseMap<TBaseEntity> : EntityTypeConfiguration<TBaseEntity>
        where TBaseEntity : class, IEntidade
    {
        public BaseMap()
        {
            Property(a => a.DataHoraCad)
                .IsRequired();

            Property(a => a.UsuarioCad)
                .IsRequired();            
        }
    }
}
