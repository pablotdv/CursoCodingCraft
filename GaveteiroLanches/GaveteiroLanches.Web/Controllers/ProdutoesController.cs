using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GaveteiroLanches.Web.Models;

namespace GaveteiroLanches.Web.Controllers
{   
    public class ProdutoesController : Controller
    {
        private GaveteiroLanchesWebContext context = new GaveteiroLanchesWebContext();

        //
        // GET: /Produtoes/

        public ViewResult Index()
        {
            return View(context.Produtoes.Include(produto => produto.MovimentacaoProduto).ToList());
        }

        //
        // GET: /Produtoes/Details/5

        public ViewResult Details(int id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // GET: /Produtoes/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Produtoes/Create

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                context.Produtoes.Add(produto);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(produto);
        }
        
        //
        // GET: /Produtoes/Edit/5
 
        public ActionResult Edit(int id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // POST: /Produtoes/Edit/5

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
        // GET: /Produtoes/Delete/5
 
        public ActionResult Delete(int id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // POST: /Produtoes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
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