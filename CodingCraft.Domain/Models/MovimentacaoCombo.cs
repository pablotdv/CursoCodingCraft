using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraft.Domain.Models
{
    public class MovimentacaoCombo : Entidade
    {
        public int MovimentacaoComboId { get; set; }

        public int MovimentacaoId { get; set; }

        public int ComboId { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        public virtual Movimentacao Movimentacao { get; set; }

        public virtual Combo Combo { get; set; }

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