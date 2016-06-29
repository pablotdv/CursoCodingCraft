using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.Models
{
    public class MovimentacaoProduto : Entidade
    {
        public int MovimentacaoProdutoId { get; set; }

        public int MovimentacaoId { get; set; }

        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }
        
        public virtual Movimentacao Movimentacao { get; set; }

        [NotMapped]
        public decimal ValorTotal
        {
            get
            {
                return this.ValorUnitario * this.Quantidade;
            }
        }
    }
}