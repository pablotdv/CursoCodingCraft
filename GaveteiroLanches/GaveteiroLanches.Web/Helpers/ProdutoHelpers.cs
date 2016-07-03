using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercicio01EF.Helpers
{
    public static class ProdutoHelpers
    {
        public static SelectList Produtos(this HtmlHelper html)
        {
            using (Models.Exercicio01EFContext context = new Models.Exercicio01EFContext())
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