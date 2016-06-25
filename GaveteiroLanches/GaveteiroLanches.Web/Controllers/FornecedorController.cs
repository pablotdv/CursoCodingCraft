using GaveteiroLanches.Web.Models;
using GaveteiroLanches.Web.ViewModels.Fornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GaveteiroLanches.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FornecedorController : Controller
    {
        private GaveteiroLanchesContext context;

        public FornecedorController()
        {
            context = new GaveteiroLanchesContext();
        }

        // GET: Fornecedor
        public ActionResult Index()
        {
            var fornecedores = context.Fornecedor.Select(a => new IndexViewModel()
            {
                FornecedorId = a.FornecedorId,
                Nome = a.Nome
            }).OrderBy(a => a.Nome).ToList();

            return View(fornecedores);
        }

        private async Task<ActionResult> FornecedorView(string view, int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Fornecedor fornecedor = await context.Fornecedor.FindAsync(id);
            if (fornecedor == null)
            {
                return RedirectToAction("Index");
            }

            FornecedorViewModel model = new FornecedorViewModel()
            {
                FornecedorId = fornecedor.FornecedorId,
                Nome = fornecedor.Nome,
                Email = fornecedor.Email
            };

            return View(view, model);
        }

        public ActionResult Create()
        {
            return View("Fornecedor", new FornecedorViewModel());
        }

        public async Task<ActionResult> Edit(int? id)
        {
            return await FornecedorView("Fornecedor", id);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            return await FornecedorView("Delete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fornecedor fornecedor = await context.Fornecedor.FindAsync(id);
            context.Fornecedor.Remove(fornecedor);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            return await FornecedorView("Details", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Salvar(FornecedorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fornecedor = await context.Fornecedor.FindAsync(model.FornecedorId);

                bool novo = fornecedor == null;
                if (novo)
                    fornecedor = new Fornecedor();

                fornecedor.Nome = model.Nome;
                fornecedor.Email = model.Email;

                if (novo)
                    context.Fornecedor.Add(fornecedor);

                await context.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                    return Json(fornecedor, JsonRequestBehavior.AllowGet);
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