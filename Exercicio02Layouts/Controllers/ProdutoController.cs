using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercicio02ScaffoldLayouts.Models;

namespace Exercicio02ScaffoldLayouts.Controllers
{   
    public class ProdutoController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Produto/

        public ViewResult Index()
        {
            return View(context.Produtoes.Include(produto => produto.ProdutoGrupo).ToList());
        }

        //
        // GET: /Produto/Detalhes/5

        public ViewResult Detalhes(System.Guid id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // GET: /Produto/Criar

        public ActionResult Criar()
        {
            ViewBag.PossibleProdutoGrupoes = context.ProdutoGrupoes;
            return View();
        } 

        //
        // POST: /Produto/Criar

        [HttpPost]
        public ActionResult Criar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.ProdutoId = Guid.NewGuid();
                context.Produtoes.Add(produto);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleProdutoGrupoes = context.ProdutoGrupoes;
            return View(produto);
        }
        
        //
        // GET: /Produto/Editar/5
 
        public ActionResult Editar(System.Guid id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            ViewBag.PossibleProdutoGrupoes = context.ProdutoGrupoes;
            return View(produto);
        }

        //
        // POST: /Produto/Editar/5

        [HttpPost]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                context.Entry(produto).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleProdutoGrupoes = context.ProdutoGrupoes;
            return View(produto);
        }

        //
        // GET: /Produto/Excluir/5
 
        public ActionResult Excluir(System.Guid id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // POST: /Produto/Excluir/5

        [HttpPost, ActionName("Exluir")]
        public ActionResult ExcluirConfirmacao(System.Guid id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            context.Produtoes.Remove(produto);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}