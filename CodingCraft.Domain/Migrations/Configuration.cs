namespace CodingCraft.Domain.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodingCraft.Domain.Models.CodingCraftDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CodingCraft.Domain.Models.CodingCraftDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<Grupo, long, UsuarioGrupo>(context);
                var manager = new RoleManager<Grupo, long>(store);
                var role = new Grupo { Name = "Admin" };

                manager.Create<Grupo, long>(role);
            }

            if (!context.Roles.Any(r => r.Name == "Cliente"))
            {
                var store = new RoleStore<Grupo, long, UsuarioGrupo>(context);
                var manager = new RoleManager<Grupo, long>(store);
                var role = new Grupo { Name = "Cliente" };

                manager.Create<Grupo, long>(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin@admin.com.br"))
            {
                var store = new UserStore<ApplicationUser, Grupo, long, UsuarioLogin, UsuarioGrupo, UsuarioIdentidade>(context);
                var manager = new UserManager<ApplicationUser, long>(store);
                var user = new ApplicationUser { UserName = "admin@admin.com.br", Email = "admin@admin.com.br", EmailConfirmed = true };

                manager.Create(user, "adminpwd");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "cliente@cliente.com.br"))
            {
                var store = new UserStore<ApplicationUser, Grupo, long, UsuarioLogin, UsuarioGrupo, UsuarioIdentidade>(context);
                var manager = new UserManager<ApplicationUser, long>(store);
                var user = new ApplicationUser { UserName = "cliente@cliente.com.br", Email = "cliente@cliente.com.br", EmailConfirmed = true };

                manager.Create(user, "cliente");
                manager.AddToRole(user.Id, "Cliente");
            }
        }
    }
}
