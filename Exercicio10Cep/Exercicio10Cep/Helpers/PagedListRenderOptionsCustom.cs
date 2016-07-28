using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;

namespace Exercicio10Cep.Helpers
{
    public class PagedListRenderOptionsCustom
    {
        public static PagedListRenderOptions Custom
        {
            get
            {
                return PagedListRenderOptions.EnableUnobtrusiveAjaxReplacing(
                    new PagedListRenderOptions()
                    {
                        DisplayItemSliceAndTotal = true,
                        ItemSliceAndTotalFormat = "Mostrando itens {0} a {1} de {2}."
                    },
                    new AjaxOptions()
                    {
                        HttpMethod = "GET",
                        UpdateTargetId = "pesquisa"
                    });
            }
        }
    }
}