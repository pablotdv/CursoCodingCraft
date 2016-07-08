using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Exercicio03Modularizacao.Domain.Models
{
    public class ApplicationSignInManager : SignInManager<Usuario, Guid>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        { }

        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(Usuario user)
        {
            ClaimsIdentity claimIdentity = await user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);

            claimIdentity.AddClaim(new Claim(ClaimTypes.Country, "Brasil"));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Gender, "Masculino"));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, "Administrador"));

            return claimIdentity;

            //return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
