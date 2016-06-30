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
            var entradas = context.MovimentacaoEntrada.Select(a => new MovimentacaoEntradaIndexViewModel()
            {
                MovimentacaoId = a.MovimentacaoId,
                Fornecedor = a.Fornecedor.Nome,
                DataHora = a.DataHora,
                ValorTotal = a.ValorTotal,
                DataFinalizacao = a.DataFinalizacao
            }).OrderByDescending(a => a.DataHora).ToList();

            return View(entradas);
        }

        private async Task<ActionResult> MovimentacaoEntradaView(string view, int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MovimentacaoEntrada entrada = await context.MovimentacaoEntrada
                .Include(a => a.Produtos)
                .Where(a => a.MovimentacaoId == id)
                .FirstOrDefaultAsync();
            if (entrada == null)
            {
                return RedirectToAction("Index");
            }

            MovimentacaoEntradaViewModel model = new MovimentacaoEntradaViewModel()
            {
                MovimentacaoId = entrada.MovimentacaoId,
                FornecedorId = entrada.FornecedorId,
                FornecedorNome = entrada.Fornecedor.Nome,
                Valor = entrada.ValorTotal,
                DataHora = entrada.DataHora,
                Produtos = entrada.Produtos.Select(a => new MovimentacaoProdutoViewModel()
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
            MovimentacaoEntrada entrada = await context.MovimentacaoEntrada.FindAsync(id);
            context.MovimentacaoEntrada.Remove(entrada);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            return await MovimentacaoEntradaView("Details", id);
        }

        public async Task<ActionResult> Finalizar(int? id)
        {
            return await MovimentacaoEntradaView("Finalizar", id);
        }

        [HttpPost, ActionName("Finalizar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FinalizarConfirmacao(int id)
        {
            MovimentacaoEntrada entrada = await context.MovimentacaoEntrada
                .Include(a => a.Produtos)
                .Where(a => a.MovimentacaoId == id)
                .FirstOrDefaultAsync();

            if (entrada.DataFinalizacao.HasValue)
                ModelState.AddModelError("", "Entrada já finalizada!");

            if (ModelState.IsValid)
            {
                entrada.DataFinalizacao = DateTime.Now;

                var produtos = entrada.Produtos.ToList();
                foreach (var produtoEntrada in produtos)
                {
                    var produto = entrada.Produtos.Where(a => a.ProdutoId == produtoEntrada.ProdutoId);
                    produtoEntrada.Produto.Quantidade += produto.Sum(a => a.Quantidade);
                    produtoEntrada.Produto.Valor = produto.FirstOrDefault().ValorUnitario;
                }

                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return await MovimentacaoEntradaView("Finalizar", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Salvar(MovimentacaoEntradaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entrada = await context.MovimentacaoEntrada
                    .Include(a => a.Produtos)
                    .Where(a => a.MovimentacaoId == model.MovimentacaoId)
                    .FirstOrDefaultAsync();

                bool novo = entrada == null;
                if (novo)
                    entrada = new MovimentacaoEntrada();

                var fornecedor = await context.Fornecedor.FindAsync(model.FornecedorId);

                if (fornecedor == null)
                {
                    fornecedor = new Fornecedor()
                    {
                        Nome = model.FornecedorNome,
                    };
                }

                if (fornecedor.FornecedorId > 0)
                    entrada.FornecedorId = fornecedor.FornecedorId;
                else entrada.Fornecedor = fornecedor;

                entrada.DataHora = DateTime.Now;

                foreach (var produto in model.Produtos)
                {
                    var entradaProdutos = entrada.Produtos?.FirstOrDefault(a => a.MovimentacaoProdutoId == produto.MovimentacaoProdutoId);

                    if (entradaProdutos != null)
                    {
                        entradaProdutos.ProdutoId = produto.ProdutoId;
                        entradaProdutos.Quantidade = produto.Quantidade;
                        entradaProdutos.ValorUnitario = produto.ValorUnitario;
                    }
                    else
                    {
                        entrada.Produtos.Add(new MovimentacaoProduto()
                        {
                            ProdutoId = produto.ProdutoId,
                            Quantidade = produto.Quantidade,
                            ValorUnitario = produto.ValorUnitario,
                        });
                    }
                }

                entrada.ValorTotal = entrada.Produtos?.Sum(a => a.ValorTotal) ?? 0;

                if (novo)
                    context.MovimentacaoEntrada.Add(entrada);

                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("MovimentacaoEntrada", model);
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