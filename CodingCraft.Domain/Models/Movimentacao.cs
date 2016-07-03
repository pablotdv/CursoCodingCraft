using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraft.Domain.Models
{
    public class Movimentacao : Entidade
    {
        public Movimentacao()
        {
            this.Produtos = new List<MovimentacaoProduto>();
            this.Combos = new List<MovimentacaoCombo>();
        }

        [Key]
        public int MovimentacaoId { get; set; }

        public decimal ValorTotal { get; set; }

        public DateTime DataHora { get; set; }

        public DateTime? DataFinalizacao { get; set; }
        
        public ICollection<MovimentacaoProduto> Produtos { get; set; }

        public ICollection<MovimentacaoCombo> Combos { get; set; }
    }
}