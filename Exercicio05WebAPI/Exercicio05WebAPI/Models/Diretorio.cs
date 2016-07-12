using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exercicio05WebAPI.Models
{
    [Table("Diretorio")]
    public class Diretorio
    {
        [Key]
        public Guid DiretorioId { get; set; }

        [Required]
        public string Nome { get; set; }

        public Guid? DiretorioPaiId { get; set; }
        [ForeignKey("DiretorioPaiId")]
        public virtual Diretorio DiretorioPai { get; set; }
        
        public string UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public ApplicationUser Usuario { get; set; }

        public ICollection<Arquivo> Arquivos { get; set; }
    }
}