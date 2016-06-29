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
            var movimentacaoSaidaes = context.MovimentacaoSaida.Select(a => new MovimentacaoSaidaIndexViewModel()
            {
                MovimentacaoId = a.MovimentacaoId,
                Usuario = a.Usuario,
                DataHora = a.DataHora,
                Valor = a.Valor
            }).OrderByDescending(a => a.DataHora).ToList();

            return View(movimentacaoSaidaes);
        }

        private async Task<ActionResult> MovimentacaoSaidaView(string view, int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MovimentacaoSaida movimentacaoSaida = await context.MovimentacaoSaida
                .Include(a => a.Produtos)
                .Where(a => a.MovimentacaoId == id)
                .FirstOrDefaultAsync();
            if (movimentacaoSaida == null)
            {
                return RedirectToAction("Index");
            }

            MovimentacaoSaidaViewModel model = new MovimentacaoSaidaViewModel()
            {
                MovimentacaoId = movimentacaoSaida.MovimentacaoId,
                Usuario = movimentacaoSaida.Usuario,
                Valor = movimentacaoSaida.Valor,
                DataHora = movimentacaoSaida.DataHora,
                Produtos = movimentacaoSaida.Produtos.Select(a => new MovimentacaoProdutoViewModel()
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
            MovimentacaoSaida movimentacaoSaida = await context.MovimentacaoSaida.FindAsync(id);
            context.MovimentacaoSaida.Remove(movimentacaoSaida);
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
                var movimentacaoSaida = await context.MovimentacaoSaida
                    .Include(a => a.Produtos)
                    .Where(a => a.MovimentacaoId == model.MovimentacaoId)
                    .FirstOrDefaultAsync();

                bool novo = movimentacaoSaida == null;
                if (novo)
                    movimentacaoSaida = new MovimentacaoSaida();

                movimentacaoSaida.Usuario = User.Identity.GetUserName();
                movimentacaoSaida.DataHora = DateTime.Now;

                foreach (var produto in model.Produtos)
                {
                    var me = movimentacaoSaida.Produtos.FirstOrDefault(a => a.MovimentacaoProdutoId == produto.MovimentacaoProdutoId);

                    if (me != null)
                    {
                        me.ProdutoId = produto.ProdutoId;
                        me.Quantidade = produto.Quantidade;
                        me.ValorUnitario = produto.ValorUnitario;
                    }
                    else
                    {
                        movimentacaoSaida.Produtos.Add(new MovimentacaoProduto()
                        {
                            ProdutoId = produto.ProdutoId,
                            Quantidade = produto.Quantidade,
                            ValorUnitario = produto.ValorUnitario,
                        });
                    }
                }

                if (novo)
                    context.MovimentacaoSaida.Add(movimentacaoSaida);

                await context.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                    return Json(movimentacaoSaida, JsonRequestBehavior.AllowGet);
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