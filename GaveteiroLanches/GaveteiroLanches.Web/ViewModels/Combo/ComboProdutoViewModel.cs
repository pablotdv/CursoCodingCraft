using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.ViewModels.Combo
{
    public class ComboProdutoViewModel
    {
        public int ComboProdutoId { get; set; }
        public int ComboId { get; set; }
        public int ProdutoId { get; set; }        
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get { return this.ValorUnitario * this.Quantidade;  } }
    }
}