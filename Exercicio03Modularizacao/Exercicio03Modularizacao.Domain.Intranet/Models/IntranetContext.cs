using Exercicio03Modularizacao.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio03Modularizacao.Domain.Intranet.Models
{
    public class IntranetContext : ApplicationDbContext
    {
        public DbSet<Curso> Cursos { get; set; }

        public DbSet<CursoSituacao> CursoStatus { get; set; }

        public DbSet<Comentario> Comentarios { get; set; }
    }
}
