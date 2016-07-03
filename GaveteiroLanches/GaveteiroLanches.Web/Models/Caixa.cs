using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercicio01EF.Models
{
    public class Caixa : Entidade
    {
        [Key]
        public int CaixaId { get; set; }

        [Required]
        public CaixaTipo Tipo { get; set; }
        
        [Required]
        public decimal Valor { get; set; }
    }
}