using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exercicio06Dapper.Models
{
    [Table("Dados")]
    public class Dados
    {
        [Key]
        public long Id { get; set; }

        public string CodigoPais { get; set; }
        [ForeignKey("CodigoPais")]
        public virtual Pais Pais { get; set; }

        public string CodigoIndicador { get; set; }
        [ForeignKey("CodigoIndicador")]
        public virtual Indicador Indicador { get; set; }
        
        
        public int Ano { get; set; }

        public float Valor { get; set; }
    }
}