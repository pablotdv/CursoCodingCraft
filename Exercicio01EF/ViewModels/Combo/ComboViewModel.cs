using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercicio01EF.ViewModels.Combo
{
    public class ComboViewModel
    {
        public ComboViewModel()
        {
            this.Produtos = new List<ComboProdutoViewModel>();
        }
        public int ComboId { get; set; }
        public decimal ValorTotal { get; set; }

        public ICollection<ComboProdutoViewModel> Produtos { get; set; }
        public string Descricao { get; set; }
    }
}