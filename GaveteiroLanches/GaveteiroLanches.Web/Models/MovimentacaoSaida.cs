using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.Models
{
    public class MovimentacaoSaida : Movimentacao
    {
        public string Usuario { get; set; }
    }
}