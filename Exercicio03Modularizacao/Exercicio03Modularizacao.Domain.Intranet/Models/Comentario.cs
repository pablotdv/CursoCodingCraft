using Exercicio03Modularizacao.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercicio03Modularizacao.Domain.Intranet.Models
{
    public class Comentario
    {
        [Key]
        public int ComentarioId { get; set; }

        [Required]
        public string Descricao { get; set; }
        
        [Required]
        [ForeignKey("UsuarioId")]
        public virtual ApplicationUser Usuario { get; set; }  
        public Guid UsuarioId { get; set; }   
        
        [Required]
        public int CursoId { get; set; }
        
        [Required]
        [ForeignKey("CursoId")]
        public virtual Curso Curso { get; set; }   

        public DateTime? DataAprovacao { get; set; }

        [ForeignKey("UsuarioAprovacaoId")]
        public virtual ApplicationUser UsuarioAprovacao { get; set; }
        public Guid? UsuarioAprovacaoId { get; set; }


    }
}