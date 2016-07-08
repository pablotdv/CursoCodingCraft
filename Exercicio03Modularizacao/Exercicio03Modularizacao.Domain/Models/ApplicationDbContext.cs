using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace Exercicio03Modularizacao.Domain.Models
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Grupo, Guid, UsuarioLogin, UsuarioGrupo, UsuarioIdentidade>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ex03");
            base.OnModelCreating(modelBuilder);
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
