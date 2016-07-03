using CodingCraft.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraft.Domain.Models
{
    public class ApplicationRoleManager : RoleManager<Grupo, long>
    {
        public ApplicationRoleManager(IRoleStore<Grupo, long> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<Grupo, long, UsuarioGrupo>(context.Get<ApplicationDbContext>()));
        }
    }
}