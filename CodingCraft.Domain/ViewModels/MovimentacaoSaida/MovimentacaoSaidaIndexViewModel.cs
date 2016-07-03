using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraft.Domain.ViewModels.MovimentacaoSaida
{
    public class MovimentacaoSaidaIndexViewModel
    {
        public DateTime DataHora { get; set; }

        public string Usuario { get; set; }

        public int MovimentacaoId { get; set; }

        public decimal ValorTotal { get; set; }
        public DateTime? DataFinalizacao { get; set; }
    }
}