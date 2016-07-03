using CodingCraft.Common.ViewModels.Combo;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.UI;
using CodingCraft.Domain.Models;

namespace Exercicio01EF.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ComboController : Controller
    {
        private CodingCraftDbContext context;

        public ComboController()
        {
            context = new CodingCraftDbContext();
        }

        // GET: Combo
        public ActionResult Index()
        {
            var combos = context.Combo.Select(a => new ComboIndexViewModel()
            {
                ComboId = a.ComboId,
                Descricao = a.Descricao,
                ValorTotal = a.ValorTotal,
            }).OrderByDescending(a => a.Descricao).ToList();

            return View(combos);
        }

        private async Task<ActionResult> ComboView(string view, int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Combo combo = await context.Combo
                .Include(a => a.Produtos)
                .Where(a => a.ComboId == id)
                .FirstOrDefaultAsync();
            if (combo == null)
            {
                return RedirectToAction("Index");
            }

            ComboViewModel model = new ComboViewModel()
            {
                ComboId = combo.ComboId,
                Descricao = combo.Descricao,
                ValorTotal = combo.ValorTotal,

                Produtos = combo.Produtos.Select(a => new ComboProdutoViewModel()
                {
                    ComboId = a.ComboId,
                    ComboProdutoId = a.ComboProdutoId,
                    ProdutoId = a.ProdutoId,
                    Quantidade = a.Quantidade,
                    ValorUnitario = a.ValorUnitario
                }).ToList()
            };

            ViewBag.Produtos = new SelectList(context.Produto
                .Select(a => new ProdutoListViewModel()
                {
                    ProdutoId = a.ProdutoId,
                    Descricao = a.Descricao
                })
                .ToList(), "ProdutoId", "Descricao");

            return View(view, model);
        }

        public ActionResult Create()
        {
            return View("Combo", new ComboViewModel());
        }

        public async Task<ActionResult> Edit(int? id)
        {
            return await ComboView("Combo", id);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            return await ComboView("Delete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Combo combo = await context.Combo.FindAsync(id);
            context.Combo.Remove(combo);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            return await ComboView("Details", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Salvar(ComboViewModel model)
        {
            if (ModelState.IsValid)
            {
                var combo = await context.Combo
                    .Include(a => a.Produtos)
                    .Where(a => a.ComboId == model.ComboId)
                    .FirstOrDefaultAsync();

                bool novo = combo == null;
                if (novo)
                    combo = new Combo();

                combo.Descricao = model.Descricao;

                foreach (var produto in model.Produtos)
                {
                    ComboProduto comboProdutos = null;

                    comboProdutos = combo.Produtos?.FirstOrDefault(a => a.ComboProdutoId == produto.ComboProdutoId);

                    if (comboProdutos != null)
                    {

                        comboProdutos.ProdutoId = produto.ProdutoId;
                        comboProdutos.Quantidade = produto.Quantidade;
                        comboProdutos.ValorUnitario = produto.ValorUnitario;
                    }
                    else
                    {
                        combo.Produtos.Add(new ComboProduto()
                        {
                            ComboProdutoId = produto.ComboProdutoId,
                            ProdutoId = produto.ProdutoId,
                            Quantidade = produto.Quantidade,
                            ValorUnitario = produto.ValorUnitario,
                        });
                    }
                }

                combo.ValorTotal = combo.Produtos?.Sum(a => a.ValorTotal) ?? 0;

                if (novo)
                    context.Combo.Add(combo);

                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Combo", model);
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult ComboProdutoLinha(Guid? ComboProdutoId = null)
        {
            ViewBag.Produtos = new SelectList(context.Produto
                .Select(a => new ProdutoListViewModel()
                {
                    ProdutoId = a.ProdutoId,
                    Descricao = a.Descricao
                })
                .ToList(), "ProdutoId", "Descricao");

            return PartialView("_ProdutoLinha", new ComboProdutoViewModel() { ComboProdutoId = Guid.NewGuid() });
        }

        public ActionResult GetFornecedores(string query)
        {
            return Json(context.Fornecedor.Where(a => a.Nome.Contains(query))
                .Select(a => new Autocomplete()
                {
                    Id = a.FornecedorId,
                    Name = a.Nome,
                })
                .ToList(), JsonRequestBehavior.AllowGet);
        }

        [OverrideAuthorization]
        [Authorize(Roles = "Admin,Cliente")]
        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult PesquisarPorId(int comboId)
        {
            var combo = context.Combo.Find(comboId);

            return Json(combo, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();

            base.Dispose(disposing);
        }
    }
}