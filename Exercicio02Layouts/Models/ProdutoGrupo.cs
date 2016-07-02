using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exercicio02Layouts.Models
{
    public class ProdutoGrupo
    {
        [Key]
        public Guid ProdutoGrupoId { get; set; }

        [Required]
        public String Descricao { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}