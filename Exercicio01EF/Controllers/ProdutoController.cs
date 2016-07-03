using Exercicio01EF.Models;
using Exercicio01EF.ViewModels.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Exercicio01EF.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProdutoController : Controller
    {
        private Exercicio01EFContext context;

        public ProdutoController()
        {
            context = new Exercicio01EFContext();
        }

        // GET: Produto
        public ActionResult Index()
        {
            var produtoes = context.Produto.Select(a => new ProdutoIndexViewModel()
            {
                ProdutoId = a.ProdutoId,
                Descricao = a.Descricao,
                Valor = a.Valor,
                Quantidade = a.Quantidade
            }).OrderBy(a => a.Descricao).ToList();

            return View(produtoes);
        }

        private async Task<ActionResult> ProdutoView(string view, int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Produto produto = await context.Produto.FindAsync(id);
            if (produto == null)
            {
                return RedirectToAction("Index");
            }

            ProdutoViewModel model = new ProdutoViewModel()
            {
                ProdutoId = produto.ProdutoId,
                Descricao = produto.Descricao,
                Valor = produto.Valor,
                Quantidade = produto.Quantidade
            };

            return View(view, model);
        }

        public ActionResult Create()
        {
            return View("Produto", new ProdutoViewModel());
        }

        public async Task<ActionResult> Edit(int? id)
        {
            return await ProdutoView("Produto", id);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            return await ProdutoView("Delete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Produto produto = await context.Produto.FindAsync(id);
            context.Produto.Remove(produto);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            return await ProdutoView("Details", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Salvar(ProdutoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var produto = await context.Produto.FindAsync(model.ProdutoId);

                bool novo = produto == null;
                if (novo)
                    produto = new Produto();

                produto.Descricao = model.Descricao;
                produto.Quantidade = model.Quantidade;
                produto.Valor = model.Valor;

                if (novo)
                    context.Produto.Add(produto);

                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Produto", model);
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult PesquisarPorDescricao(string descricao)
        {
            var descricoes = descricao != null ? descricao.Trim().Split(' ') : new string[0];

            var produtos = context.Produto.Where(a => descricoes.All(b => a.Descricao.Contains(b.Trim())))
                .OrderBy(a => a.Descricao)
                .Select(a => new { ProdutoId = a.ProdutoId, Descricao = a.Descricao, ValorUnitario = a.Valor })
                .ToList();

            return Json(produtos, JsonRequestBehavior.AllowGet);
        }

        [OverrideAuthorization]
        [Authorize(Roles = "Admin,Cliente")]
        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult PesquisarPorId(int produtoId)
        {
            var produto = context.Produto.Find(produtoId);

            return Json(produto, JsonRequestBehavior.AllowGet);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();

            base.Dispose(disposing);
        }
    }
}