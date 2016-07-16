using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercicio06Dapper.Models
{
    public class PesquisaAnalitica
    {
        public string Indicador { get; set; }

        public float? DesvioPadrao { get; set; }

        public float? Minimo { get; set; }

        public float? Maximo { get; set; }

        public float? Media { get; set; }
    }
}