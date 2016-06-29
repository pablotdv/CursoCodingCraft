using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.Models
{
    public class Movimentacao : Entidade
    {
        public Movimentacao()
        {
            this.Produtos = new List<MovimentacaoProduto>();
        }
        public int MovimentacaoId { get; set; }

        [NotMapped]
        public decimal? Valor
        {
            get
            {
                return this.Produtos?.Sum(a => a.ValorTotal);
            }
        }

        public DateTime DataHora { get; set; }

        public ICollection<MovimentacaoProduto> Produtos { get; set; }
    }
}