using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.ViewModels.MovimentacaoSaida
{
    public class MovimentacaoComboViewModel
    {
        public int MovimentacaoComboId { get; set; }
        public int MovimentacaoId { get; set; }
        public int ComboId { get; set; }        
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get { return this.ValorUnitario * this.Quantidade;  } }
    }
}