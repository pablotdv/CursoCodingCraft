using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.Models
{
    public class Combo : Entidade
    {
        [Key]
        public int ComboId { get; set; }

        [Required]
        public string Descricao { get; set; }

        public decimal ValorTotal { get; set; }
    }
}