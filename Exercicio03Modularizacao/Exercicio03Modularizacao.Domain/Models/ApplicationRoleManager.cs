using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace Exercicio03Modularizacao.Domain.Models
{
    public class ApplicationRoleManager : RoleManager<Grupo, Guid>
    {
        public ApplicationRoleManager(IRoleStore<Grupo, Guid> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<Grupo, Guid, UsuarioGrupo>(context.Get<ApplicationDbContext>()));
        }
    }
}
