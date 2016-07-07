using Exercicio03Modularizacao.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio03Modularizacao.Domain.Intranet.Models
{
    [Table("CursoStatus")]
    public class CursoSituacao
    {
        [Key]
        public int CursoStatusId { get; set; }

        public int CursoId { get; set; }

        public virtual Curso Curso { get; set; }

        [Required]
        public Situacao Status { get; set; }
                
        [Required]
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }

        [Required]
        public DateTime Data { get; set; }
    }
}
