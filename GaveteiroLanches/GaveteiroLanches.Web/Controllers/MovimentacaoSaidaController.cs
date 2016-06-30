using GaveteiroLanches.Web.Models;
using GaveteiroLanches.Web.ViewModels.MovimentacaoSaida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace GaveteiroLanches.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MovimentacaoSaidaController : Controller
    {
        private GaveteiroLanchesContext context;

        public MovimentacaoSaidaController()
        {
            context = new GaveteiroLanchesContext();
        }

        // GET: MovimentacaoSaida
        public ActionResult Index()
        {
            var saidas = context.MovimentacaoSaida.Select(a => new MovimentacaoSaidaIndexViewModel()
            {
                MovimentacaoId = a.MovimentacaoId,
                Usuario = a.Usuario,
                DataHora = a.DataHora,
                ValorTotal = a.ValorTotal,
                DataFinalizacao = a.DataFinalizacao
            }).OrderByDescending(a => a.DataHora).ToList();

            return View(saidas);
        }

        private async Task<ActionResult> MovimentacaoSaidaView(string view, int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MovimentacaoSaida saida = await context.MovimentacaoSaida
                .Include(a => a.Produtos)
                .Where(a => a.MovimentacaoId == id)
                .FirstOrDefaultAsync();
            if (saida == null)
            {
                return RedirectToAction("Index");
            }

            MovimentacaoSaidaViewModel model = new MovimentacaoSaidaViewModel()
            {
                MovimentacaoId = saida.MovimentacaoId,
                Usuario = saida.Usuario,
                Valor = saida.ValorTotal,
                DataHora = saida.DataHora,
                Produtos = saida.Produtos.Select(a => new MovimentacaoProdutoViewModel()
                {
                    MovimentacaoId = a.MovimentacaoId,
                    MovimentacaoProdutoId = a.MovimentacaoProdutoId,
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
            return View("MovimentacaoSaida", new MovimentacaoSaidaViewModel());
        }

        public async Task<ActionResult> Edit(int? id)
        {
            return await MovimentacaoSaidaView("MovimentacaoSaida", id);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            return await MovimentacaoSaidaView("Delete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MovimentacaoSaida saida = await context.MovimentacaoSaida.FindAsync(id);
            context.MovimentacaoSaida.Remove(saida);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            return await MovimentacaoSaidaView("Details", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Salvar(MovimentacaoSaidaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var saida = await context.MovimentacaoSaida
                    .Include(a => a.Produtos)
                    .Where(a => a.MovimentacaoId == model.MovimentacaoId)
                    .FirstOrDefaultAsync();

                bool novo = saida == null;
                if (novo)
                    saida = new MovimentacaoSaida();

                saida.Usuario = User.Identity.GetUserName();
                saida.DataHora = DateTime.Now;

                foreach (var produto in model.Produtos)
                {
                    var saidaProdutos = saida.Produtos?.FirstOrDefault(a => a.MovimentacaoProdutoId == produto.MovimentacaoProdutoId);

                    if (saidaProdutos != null)
                    {
                        saidaProdutos.ProdutoId = produto.ProdutoId;
                        saidaProdutos.Quantidade = produto.Quantidade;
                        saidaProdutos.ValorUnitario = produto.ValorUnitario;
                    }
                    else
                    {
                        saida.Produtos.Add(new MovimentacaoProduto()
                        {
                            ProdutoId = produto.ProdutoId,
                            Quantidade = produto.Quantidade,
                            ValorUnitario = produto.ValorUnitario,
                        });
                    }
                }

                saida.ValorTotal = saida.Produtos?.Sum(a => a.ValorTotal) ?? 0;

                if (novo)
                    context.MovimentacaoSaida.Add(saida);

                await context.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                    return Json(saida, JsonRequestBehavior.AllowGet);
                else return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return Json(model, JsonRequestBehavior.AllowGet);
            else return View(model);
        }

        public ActionResult MovimentacaoProdutoLinha()
        {
            ViewBag.Produtos = new SelectList(context.Produto
                .Select(a => new ProdutoListViewModel()
                {
                    ProdutoId = a.ProdutoId,
                    Descricao = a.Descricao
                })
                .ToList(), "ProdutoId", "Descricao");

            return PartialView("_ProdutoLinha", new MovimentacaoProdutoViewModel() { MovimentacaoProdutoId = 0 });
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();

            base.Dispose(disposing);
        }
    }
}