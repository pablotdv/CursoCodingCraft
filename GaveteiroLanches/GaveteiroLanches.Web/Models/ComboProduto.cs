using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.Models
{
    public class ComboProduto: Entidade
    {
        [Key]
        public int ComboProdutoId { get; set; }

        [Required]
        public int ComboId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        public decimal ValorTotal { get; set; }

        public virtual Combo Combo { get; set; }

        public virtual Produto Produto { get; set; }
    }
}