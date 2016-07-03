using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraft.Common.ViewModels.MovimentacaoEntrada
{
    public class MovimentacaoProdutoViewModel
    {
        public int MovimentacaoProdutoId { get; set; }
        public int MovimentacaoId { get; set; }
        public int ProdutoId { get; set; }        
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get { return this.ValorUnitario * this.Quantidade;  } }
    }
}