using GaveteiroLanches.Web.Models;
using GaveteiroLanches.Web.ViewModels.MovimentacaoEntrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GaveteiroLanches.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MovimentacaoEntradaController : Controller
    {
        private GaveteiroLanchesContext context;

        public MovimentacaoEntradaController()
        {
            context = new GaveteiroLanchesContext();
        }

        // GET: MovimentacaoEntrada
        public ActionResult Index()
        {
            var movimentacaoEntradaes = context.MovimentacaoEntrada.Select(a => new IndexViewModel()
            {
                MovimentacaoId = a.MovimentacaoId,
                Fornecedor = a.Fornecedor.Nome,
                DataHora = a.DataHora,
                Valor = a.Valor
            }).OrderByDescending(a => a.DataHora).ToList();

            return View(movimentacaoEntradaes);
        }

        private async Task<ActionResult> MovimentacaoEntradaView(string view, int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MovimentacaoEntrada movimentacaoEntrada = await context.MovimentacaoEntrada
                .Include(a=>a.MovimentacaoProduto)
                .Where(a=>a.MovimentacaoId == id)
                .FirstOrDefaultAsync();
            if (movimentacaoEntrada == null)
            {
                return RedirectToAction("Index");
            }

            MovimentacaoEntradaViewModel model = new MovimentacaoEntradaViewModel()
            {
                MovimentacaoId = movimentacaoEntrada.MovimentacaoId,
                FornecedorId = movimentacaoEntrada.FornecedorId,
                FornecedorNome = movimentacaoEntrada.Fornecedor.Nome,
                Valor = movimentacaoEntrada.Valor,
                DataHora = movimentacaoEntrada.DataHora,
                Produtos = movimentacaoEntrada.MovimentacaoProduto.Select(a=>new MovimentacaoProdutoViewModel() {
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
            return View("MovimentacaoEntrada", new MovimentacaoEntradaViewModel());
        }

        public async Task<ActionResult> Edit(int? id)
        {
            return await MovimentacaoEntradaView("MovimentacaoEntrada", id);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            return await MovimentacaoEntradaView("Delete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MovimentacaoEntrada movimentacaoEntrada = await context.MovimentacaoEntrada.FindAsync(id);
            context.MovimentacaoEntrada.Remove(movimentacaoEntrada);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            return await MovimentacaoEntradaView("Details", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Salvar(MovimentacaoEntradaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movimentacaoEntrada = await context.MovimentacaoEntrada
                    .Include(a => a.MovimentacaoProduto)
                    .Where(a => a.MovimentacaoId == model.MovimentacaoId)
                    .FirstOrDefaultAsync();

                bool novo = movimentacaoEntrada == null;
                if (novo)
                    movimentacaoEntrada = new MovimentacaoEntrada();

                var fornecedor = await context.Fornecedor.FindAsync(model.FornecedorId);

                if (fornecedor == null)
                {
                    fornecedor = new Fornecedor()
                    {
                        Nome = model.FornecedorNome,
                    };
                }

                if (fornecedor.FornecedorId > 0)
                    movimentacaoEntrada.FornecedorId = fornecedor.FornecedorId;
                else movimentacaoEntrada.Fornecedor = fornecedor;

                movimentacaoEntrada.DataHora = DateTime.Now;
                movimentacaoEntrada.Valor = model.Produtos
                    .Sum(a => a.ValorTotal);

                foreach (var produto in model.Produtos)
                {
                    var me = movimentacaoEntrada.MovimentacaoProduto.FirstOrDefault(a => a.MovimentacaoProdutoId == produto.MovimentacaoProdutoId);
                    if (me != null)
                    {
                        me.ProdutoId = produto.ProdutoId;
                        me.Quantidade = produto.Quantidade;
                        me.ValorUnitario = produto.ValorUnitario;
                    }
                    else
                    {
                        movimentacaoEntrada.MovimentacaoProduto.Add(new MovimentacaoProduto()
                        {
                            ProdutoId = produto.ProdutoId,
                            Quantidade = produto.Quantidade,
                            ValorUnitario = produto.ValorUnitario,
                        });
                    }
                }

                if (novo)
                    context.MovimentacaoEntrada.Add(movimentacaoEntrada);

                await context.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                    return Json(movimentacaoEntrada, JsonRequestBehavior.AllowGet);
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