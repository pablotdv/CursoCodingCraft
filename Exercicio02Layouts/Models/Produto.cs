using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercicio02Layouts.Models
{
    //Scaffold Portugues.Controller Produto -ModelType:Exercicio02Layouts.Models.Produto -Verbose -Force -DbContextType:Exercicio02Layouts.Models.ApplicationDbContext
    public class Produto
    {
        [Key]
        public Guid ProdutoId { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public decimal Valor { get; set; }
    }
}