using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.Models
{
    public class Movimentacao : Entidade
    {
        public Movimentacao()
        {
            this.MovimentacaoProduto = new List<MovimentacaoProduto>();
        }
        public int MovimentacaoId { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataHora { get; set; }

        public ICollection<MovimentacaoProduto> MovimentacaoProduto { get; set; }
    }
}