using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingCraft.Domain.Models
{
    public class CaixaMovimentacao : Entidade
    {
        [Key]
        public int CaixaMovimentacaoId { get; set; }

        [Required]
        public int CaixaId { get; set; }

        [Required]
        public int MovimentacaoId { get; set; }
        
        public decimal ValorMovimentacao { get; set; }

        public decimal ValorPago { get; set; }

        public virtual Caixa Caixa { get; set; }

        public virtual Movimentacao Movimentacao { get; set; }
    }
}