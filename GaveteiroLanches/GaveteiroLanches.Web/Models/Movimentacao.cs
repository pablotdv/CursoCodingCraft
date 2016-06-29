using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public int MovimentacaoId { get; set; }

        public decimal ValorTotal { get; set; }

        public DateTime DataHora { get; set; }

        public ICollection<MovimentacaoProduto> Produtos { get; set; }
    }
}