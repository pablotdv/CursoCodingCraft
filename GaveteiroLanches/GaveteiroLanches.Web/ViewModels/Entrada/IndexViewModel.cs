using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.ViewModels.Entrada
{
    public class IndexViewModel
    {
        public DateTime DataHora { get; internal set; }
        public string Fornecedor { get; internal set; }
        public int MovimentacaoId { get; internal set; }
    }
}