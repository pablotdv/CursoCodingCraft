using System.Web.Mvc;

namespace Exercicio03Modularizacao.Web.Extranet
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
