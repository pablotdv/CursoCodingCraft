using GaveteiroLanches.Web.Models;
using GaveteiroLanches.Web.ViewModels.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GaveteiroLanches.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProdutoController : Controller
    {
        private GaveteiroLanchesContext context;

        public ProdutoController()
        {
            context = new GaveteiroLanchesContext();
        }

        // GET: Produto
        public ActionResult Index()
        {
            var produtoes = context.Produto.Select(a => new IndexViewModel()
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

                if (Request.IsAjaxRequest())
                    return Json(produto, JsonRequestBehavior.AllowGet);
                else return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return Json(model, JsonRequestBehavior.AllowGet);
            else return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();

            base.Dispose(disposing);
        }
    }
}