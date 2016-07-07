using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Exercicio03Modularizacao.Domain.Filters
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public ClaimsAuthorizeAttribute() { }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;

            if (user.Claims.Where(c => c.Type == ClaimTypes.Country)
                .Any(x => x.Value == "Brasil")
                && user.IsInRole("Administrador"))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                                  new RouteValueDictionary
                                   {
                                         { "action", "Login" },
                                         { "controller", "Home" }
                                   });
            }
        }

    }
}
