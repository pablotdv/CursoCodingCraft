using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercicio10Cep.Models
{
    [Table("Logradouros")]
    public class Logradouro : Entidade
    {
        public Guid LogradouroId { get; set; }

        public Guid BairroId { get; set; }

        public String Descricao { get; set; }

        public String DescricaoFonetizado { get; set; }

        public int? Cep { get; set; }

        public virtual Cidade Cidade { get; set; }
    }
}