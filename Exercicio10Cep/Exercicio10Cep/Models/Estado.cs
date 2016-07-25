using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercicio10Cep.Models
{
    [Table("Estados")]
    [DisplayColumn("Nome")]
    public class Estado : Entidade
    {
        [Key]
        public Guid EstadoId { get; set; }

        public Guid PaisId { get; set; }

        [Required]
        [StringLength(200)]
        public String Nome { get; set; }

        [Required]
        [StringLength(2)]
        public String Sigla { get; set; }

        public virtual Pais Pais { get; set; }

        public ICollection<Cidade> Cidades { get; set; }
    }
}