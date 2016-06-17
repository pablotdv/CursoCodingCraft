using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaveteiroLanches.Entidades
{
    public class Entidade : IEntidade
    {
        public DateTime DataHoraCad { get; set; }

        public string UsuarioCad { get; set; }
    }
}
