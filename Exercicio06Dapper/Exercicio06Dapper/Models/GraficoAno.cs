using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercicio06Dapper.Models
{
    public class GraficoAno
    {
        public int Ano { get; set; }
        public string CodigoPais { get; internal set; }
        public float Valor { get; set; }
    }
}