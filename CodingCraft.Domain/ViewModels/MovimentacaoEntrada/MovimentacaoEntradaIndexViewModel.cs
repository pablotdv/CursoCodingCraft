using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraft.Domain.ViewModels.MovimentacaoEntrada
{
    public class MovimentacaoEntradaIndexViewModel
    {
        public DateTime? DataFinalizacao { get; set; }
        public DateTime DataHora { get; set; }

        public string Fornecedor { get; set; }

        public int MovimentacaoId { get; set; }

        public decimal ValorTotal { get; set; }
    }
}