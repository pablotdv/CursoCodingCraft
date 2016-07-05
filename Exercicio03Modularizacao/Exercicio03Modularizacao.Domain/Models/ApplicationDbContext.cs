using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace Exercicio03Modularizacao.Domain.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Grupo, Guid, UsuarioLogin, UsuarioGrupo, UsuarioIdentidade>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role            
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
