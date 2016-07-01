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
using System.Web.UI;

namespace GaveteiroLanches.Web.Controllers
{
    [Authorize]
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

            if (!User.IsInRole("Admin"))
                saidas = saidas.Where(a => a.Usuario == User.Identity.GetUserName()).ToList();

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
                .Include(a => a.Combos)
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
                }).ToList(),
                Combos = saida.Combos.Select(a => new MovimentacaoComboViewModel()
                {
                    MovimentacaoId = a.MovimentacaoId,
                    MovimentacaoComboId = a.MovimentacaoComboId,
                    ComboId = a.ComboId,
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

            ViewBag.Combos = new SelectList(context.Combo
                .Select(a => new ComboListViewModel()
                {
                    ComboId = a.ComboId,
                    Descricao = a.Descricao
                })
                .ToList(), "ComboId", "Descricao");

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

        public async Task<ActionResult> Finalizar(int? id)
        {
            return await MovimentacaoSaidaView("Finalizar", id);
        }

        [HttpPost, ActionName("Finalizar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FinalizarConfirmacao(int id)
        {
            MovimentacaoSaida saida = await context.MovimentacaoSaida
                .Include(a => a.Produtos)
                .Include(a => a.Combos)
                .Where(a => a.MovimentacaoId == id)
                .FirstOrDefaultAsync();

            if (saida.DataFinalizacao.HasValue)
                ModelState.AddModelError("", "Saida já finalizada!");

            if (ModelState.IsValid)
            {
                saida.DataFinalizacao = DateTime.Now;

                var produtos = (from mp in context.MovimentacaoProduto
                                where mp.MovimentacaoId == saida.MovimentacaoId
                                select new
                                {
                                    ProdutoId = mp.ProdutoId,
                                    Quantidade = mp.Quantidade,
                                }).Union((from c in context.MovimentacaoCombo
                                          from p in c.Combo.Produtos
                                          select new
                                          {
                                              ProdutoId = p.ProdutoId,
                                              Quantidade = p.Quantidade
                                          }));

                var prods = (from p in produtos
                             group new { p.Quantidade } by new { p.ProdutoId } into g
                             select new
                             {
                                 ProdutoId = g.Key.ProdutoId,
                                 Quantidade = g.Sum(a => a.Quantidade)
                             }).ToList();

                foreach (var prod in prods)
                {
                    var produto = context.Produto.Find(prod.ProdutoId);
                    produto.Quantidade -= prod.Quantidade;
                }


                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return await MovimentacaoSaidaView("Finalizar", id);
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
                    MovimentacaoProduto saidaProdutos = null;

                    if (produto.MovimentacaoProdutoId != 0)
                        saidaProdutos = saida.Produtos?.FirstOrDefault(a => a.MovimentacaoProdutoId == produto.MovimentacaoProdutoId);

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

                foreach (var combo in model.Combos)
                {
                    MovimentacaoCombo saidaCombos = null;

                    if (combo.MovimentacaoComboId != 0)
                        saidaCombos = saida.Combos?.FirstOrDefault(a => a.MovimentacaoComboId == combo.MovimentacaoComboId);

                    if (saidaCombos != null)
                    {
                        saidaCombos.ComboId = combo.ComboId;
                        saidaCombos.Quantidade = combo.Quantidade;
                        saidaCombos.ValorUnitario = combo.ValorUnitario;
                    }
                    else
                    {
                        saida.Combos.Add(new MovimentacaoCombo()
                        {
                            ComboId = combo.ComboId,
                            Quantidade = combo.Quantidade,
                            ValorUnitario = combo.ValorUnitario,
                        });
                    }
                }

                decimal valorProdutos = saida.Produtos?.Sum(a => a.ValorTotal) ?? 0;
                decimal valorCombos = saida.Combos?.Sum(a => a.ValorTotal) ?? 0;

                saida.ValorTotal = valorProdutos + valorCombos;


                if (novo)
                    context.MovimentacaoSaida.Add(saida);

                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("MovimentacaoSaida", model);
        }

        [OutputCache(Location = OutputCacheLocation.None)]
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

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult MovimentacaoComboLinha()
        {
            ViewBag.Combos = new SelectList(context.Combo
                .Select(a => new ComboListViewModel()
                {
                    ComboId = a.ComboId,
                    Descricao = a.Descricao
                })
                .ToList(), "ComboId", "Descricao");

            return PartialView("_ComboLinha", new MovimentacaoComboViewModel() { MovimentacaoComboId = 0 });
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