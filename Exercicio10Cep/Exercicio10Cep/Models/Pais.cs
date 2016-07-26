using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercicio10Cep.Models
{
    [Table("Paises")]
    [DisplayColumn("Nome")]
    public class Pais : Entidade
    {
        public Pais()
        {
            PaisId = Guid.NewGuid();
        }

        [Key]
        public Guid PaisId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        [Required]
        [StringLength(3)]
        public string Sigla { get; set; }

        public ICollection<Estado> Estados { get; set; }
    }

}