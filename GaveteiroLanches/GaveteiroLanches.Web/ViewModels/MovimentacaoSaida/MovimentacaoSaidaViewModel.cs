using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.ViewModels.MovimentacaoSaida
{
    public class MovimentacaoSaidaViewModel
    {
        public MovimentacaoSaidaViewModel()
        {
            this.Produtos = new List<MovimentacaoProdutoViewModel>();
        }
        public DateTime DataHora { get; set; }
        public string Usuario { get; set; }
        public int MovimentacaoId { get; set; }
        public decimal? Valor { get; set; }

        public ICollection<MovimentacaoProdutoViewModel> Produtos { get; set; }
    }
}