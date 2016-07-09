using Exercicio04Membership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Exercicio04Membership.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if(ModelState.IsValid && Membership.ValidateUser(model.Username, model.Password))
            {
               var authCookie = FormsAuthentication.GetAuthCookie(model.Username, false);

                Response.Cookies.Add(authCookie);
                Response.Redirect(FormsAuthentication.GetRedirectUrl(model.Username, false));

                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "Usuário ou senha inválidos!");
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}