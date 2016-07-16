using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exercicio06Dapper.Models
{
    [Table("Pais")]
    public class Pais
    {
        [Key]
        public string Codigo { get; set; }

        public string Regiao { get; set; }

        public string GrupoEconomico { get; set; }

        public string Notas { get; set; }

        public string Nome { get; set; }
    }
}