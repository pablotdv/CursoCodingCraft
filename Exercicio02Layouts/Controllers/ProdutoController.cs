using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercicio02Layouts.Models;

namespace Exercicio02Layouts.Controllers
{   
    public class ProdutoController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Produto/

        public ViewResult Index()
        {
            return View(context.Produtoes.ToList());
        }

        //
        // GET: /Produto/Details/5

        public ViewResult Details(System.Guid id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // GET: /Produto/Criar

        public ActionResult Criar()
        {
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

            return View(produto);
        }
        
        //
        // GET: /Produto/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // POST: /Produto/Edit/5

        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                context.Entry(produto).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
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