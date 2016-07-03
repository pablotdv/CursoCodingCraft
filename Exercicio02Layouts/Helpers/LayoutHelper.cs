using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercicio02ScaffoldLayouts.Helpers
{
    public static class LayoutHelper
    {
        public static string Layout (this HtmlHelper html)
        {            
            return HttpContext.Current.Request.Cookies["_layout"]?.Value ?? "bootstrap";
        }
    }
}