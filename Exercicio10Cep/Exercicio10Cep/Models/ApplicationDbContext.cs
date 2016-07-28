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

        public System.Data.Entity.DbSet<Exercicio10Cep.Models.Pais> Paises { get; set; }

        public System.Data.Entity.DbSet<Exercicio10Cep.Models.Estado> Estados { get; set; }

        public System.Data.Entity.DbSet<Exercicio10Cep.Models.Cidade> Cidades { get; set; }

        public System.Data.Entity.DbSet<Exercicio10Cep.Models.Bairro> Bairros { get; set; }

        public System.Data.Entity.DbSet<Exercicio10Cep.Models.Logradouro> Logradouros { get; set; }

        public DbSet<PesquisaModel> PesquisaModels { get; set; }
    }
}