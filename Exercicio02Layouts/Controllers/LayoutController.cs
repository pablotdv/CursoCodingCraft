using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercicio02Layouts.Controllers
{
    public class LayoutController : Controller
    {
        //
        // GET: /Layout/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void DefinirLayout(string layout)
        {
            Response.Cookies["_layout"].Value = layout;            
        }
	}
}