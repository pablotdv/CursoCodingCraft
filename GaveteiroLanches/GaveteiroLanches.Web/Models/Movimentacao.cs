using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.Models
{
    public class Movimentacao
    {
        public int MovimentacaoId { get; set; }

        public int PessoaId { get; set; }

        public MovimentacaoTipo Tipo { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataHora { get; set; }
                
        public virtual Pessoa Pessoa { get; set; }
    }
}