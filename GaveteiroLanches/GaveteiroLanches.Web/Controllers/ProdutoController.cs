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
    public class ProdutoController : Controller
    {
        private GaveteiroLanchesContext3 context = new GaveteiroLanchesContext3();

        //
        // GET: /Produto/

        public ViewResult Index()
        {
            return View(context.Produto.Include(produto => produto.MovimentacaoProduto).ToList());
        }

        //
        // GET: /Produto/Details/5

        public ViewResult Details(int id)
        {
            Produto produto = context.Produto.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // GET: /Produto/Create

        public ActionResult CriarOuEditar()
        {
            return View();
        } 

        //
        // POST: /Produto/Create

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                context.Produto.Add(produto);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(produto);
        }
        
        //
        // GET: /Produto/Edit/5
 
        public ActionResult Edit(int id)
        {
            Produto produto = context.Produto.Single(x => x.ProdutoId == id);
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
        // GET: /Produto/Delete/5
 
        public ActionResult Delete(int id)
        {
            Produto produto = context.Produto.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // POST: /Produto/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = context.Produto.Single(x => x.ProdutoId == id);
            context.Produto.Remove(produto);
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