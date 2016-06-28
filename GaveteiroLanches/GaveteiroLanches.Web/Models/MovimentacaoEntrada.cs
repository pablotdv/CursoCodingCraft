using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.Models
{
    public class MovimentacaoEntrada : Movimentacao
    {        
        public int FornecedorId { get; set; }
                        
        public virtual Fornecedor Fornecedor { get; set; }
    }
}