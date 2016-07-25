using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercicio10Cep.Models
{
    [Table("Bairros")]
    [DisplayColumn("Nome")]
    public class Bairro : Entidade
    {
        [Key]
        public Guid BairroId { get; set; }

        public Guid CidadeId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        public string NomeAbreviado { get; set; }

        public string NomeFonetizado { get; set; }

        public virtual Cidade Cidade { get; set; }

        public ICollection<Logradouro> Logradouros { get; set; }
    }
}