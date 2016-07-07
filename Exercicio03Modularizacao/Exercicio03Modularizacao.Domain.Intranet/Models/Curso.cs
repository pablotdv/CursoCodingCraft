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
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [AllowHtml]
        public string Sobre { get; set; }

        [AllowHtml]
        [Display(Name = "Conteúdo programático")]
        public string ConteudoProgramatico { get; set; }

        [Display(Name = "Cursos relacionados")]
        public ICollection<Curso> CursosRelacionados { get; set; }

        [Display(Name = "Situações")]
        public ICollection<CursoSituacao> Situacoes { get; set; }

        [Display(Name = "Comentários")]
        public ICollection<Comentario> Comentarios { get; set; }
    }
}
