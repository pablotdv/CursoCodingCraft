using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Exercicio10Cep.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            // Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }        
        public DbSet<PesquisaModel> PesquisaModels { get; set; }

        public DbSet<Exercicio10Cep.Models.Pais> Pais { get; set; }

        public DbSet<Exercicio10Cep.Models.Cidade> Cidades { get; set; }

        public DbSet<Exercicio10Cep.Models.Estado> Estadoes { get; set; }

        public DbSet<Exercicio10Cep.Models.Bairro> Bairroes { get; set; }

        public DbSet<Exercicio10Cep.Models.Logradouro> Logradouroes { get; set; }
    }
}