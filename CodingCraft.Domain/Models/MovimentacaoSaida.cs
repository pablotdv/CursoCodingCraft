using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraft.Domain.Models
{
    public class MovimentacaoSaida : Movimentacao
    {
        public string Usuario { get; set; }
    }
}