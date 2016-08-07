using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercicio10Cep.Models
{
    [Table("Cidades")]
    [DisplayColumn("Nome")]
    public class Cidade : Entidade
    {
        [Key]        
        public Guid CidadeId { get; set; }

        public Guid EstadoId { get; set; }

        [Required]
        [StringLength(200)]
        public String Nome { get; set; }

        public int? Cep { get; set; }

        public virtual Estado Estado { get; set; }

        public ICollection<Bairro> Bairros { get; set; }
    }
}