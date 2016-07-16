using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exercicio06Dapper.Models
{
    [Table("Indicadores")]
    public class Indicador
    {
        [Key]
        public string Codigo { get; set; }

        public string Nome { get; set; }

        public string Nota { get; set; }

        public string Organizacao { get; set; }
    }
}