using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GaveteiroLanches.Web.Helpers
{
    public static class ProdutoHelpers
    {
        public static SelectList Produtos(this HtmlHelper html)
        {
            using (Models.GaveteiroLanchesContext context = new Models.GaveteiroLanchesContext())
            { 
                var produtosList = new SelectList(context.Produto
                    .Select(a => new
                    {
                        ProdutoId = a.ProdutoId,
                        Descricao = a.Descricao
                    })
                    .ToList(), "ProdutoId", "Descricao");

                

                return produtosList;
            }
            
        }
    }
}