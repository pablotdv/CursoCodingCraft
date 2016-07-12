using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercicio05WebAPI.Models
{
    [Table("Arquivo")]
    public class Arquivo
    {
        public Guid ArquivoId { get; set; }

        [Required]
        public String Nome { get; set; }
        [Required]
        public String MimeType { get; set; }

        public Guid DiretorioId { get; set; }
        [ForeignKey("DiretorioId")]
        public virtual Diretorio Diretorio { get; set; }

    }
}