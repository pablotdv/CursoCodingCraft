using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraft.Common.ViewModels.MovimentacaoEntrada
{
    public class MovimentacaoEntradaViewModel
    {
        public MovimentacaoEntradaViewModel()
        {
            this.Produtos = new List<MovimentacaoProdutoViewModel>();
        }
        public DateTime DataHora { get; set; }
        public int FornecedorId { get; set; }
        public string FornecedorNome { get; set; }
        public int MovimentacaoId { get; set; }
        public decimal? Valor { get; set; }

        public ICollection<MovimentacaoProdutoViewModel> Produtos { get; set; }
    }
}