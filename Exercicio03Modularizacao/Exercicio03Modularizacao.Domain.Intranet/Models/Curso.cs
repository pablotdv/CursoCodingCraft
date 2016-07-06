using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Exercicio03Modularizacao.Domain.Intranet.Models
{
    [Table("Cursos")]
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }

        [Required]
        public string Descricao { get; set; }

        [AllowHtml]
        public string Sobre { get; set; }

        [AllowHtml]
        public string ConteudoProgramatico { get; set; }
                
        public ICollection<Curso> CursosRelacionados { get; set; }

        public ICollection<CursoSituacao> Situacoes { get; set; }

        public ICollection<Comentario> Comentarios { get; set; }
    }
}
