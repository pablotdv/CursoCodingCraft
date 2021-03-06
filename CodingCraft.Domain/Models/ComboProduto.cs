﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraft.Domain.Models
{
    public class ComboProduto : Entidade
    {
        [Key]
        public Guid ComboProdutoId { get; set; }

        [Required]
        public int ComboId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        [NotMapped]
        public decimal ValorTotal { get { return this.ValorUnitario * this.Quantidade; } }

        public virtual Combo Combo { get; set; }

        public virtual Produto Produto { get; set; }
    }
}