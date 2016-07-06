using Exercicio03Modularizacao.Domain.Intranet.Models;
using Exercicio03Modularizacao.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio03Modularizacao.Domain.Extranet.Models
{
    public class ExtranetContext : ApplicationDbContext
    {
        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Comentario> Comentarios { get; set; }
    }
}
