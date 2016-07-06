using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio03Modularizacao.Domain.Intranet.Models
{
    [Table("Cursos")]
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }

        [Required]
        public string Descricao { get; set; }

        public string Sobre { get; set; }

        public string ConteudoProgramatico { get; set; }
                
        public ICollection<Curso> CursosRelacionados { get; set; }

        public ICollection<CursoSituacao> Situacoes { get; set; }

        public ICollection<Comentario> Comentarios { get; set; }
    }
}
