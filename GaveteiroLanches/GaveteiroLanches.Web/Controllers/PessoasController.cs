using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GaveteiroLanches.Entidades;
using GaveteiroLanches.Web.Models;
using GaveteiroLanches.Dominio;

namespace GaveteiroLanches.Web.Controllers
{   
    public class PessoasController : Controller
    {
        private GaveteiroLanchesContext context = new GaveteiroLanchesContext();

        //
        // GET: /Pessoas/

        public ViewResult Index()
        {
            return View(context.Pessoa.ToList());
        }

        //
        // GET: /Pessoas/Details/5

        public ViewResult Details(int id)
        {
            Pessoa pessoa = context.Pessoa.Single(x => x.PessoaId == id);
            return View(pessoa);
        }

        //
        // GET: /Pessoas/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Pessoas/Create

        [HttpPost]
        public ActionResult Create(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                context.Pessoa.Add(pessoa);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(pessoa);
        }
        
        //
        // GET: /Pessoas/Edit/5
 
        public ActionResult Edit(int id)
        {
            Pessoa pessoa = context.Pessoa.Single(x => x.PessoaId == id);
            return View(pessoa);
        }

        //
        // POST: /Pessoas/Edit/5

        [HttpPost]
        public ActionResult Edit(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                context.Entry(pessoa).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        //
        // GET: /Pessoas/Delete/5
 
        public ActionResult Delete(int id)
        {
            Pessoa pessoa = context.Pessoa.Single(x => x.PessoaId == id);
            return View(pessoa);
        }

        //
        // POST: /Pessoas/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa pessoa = context.Pessoa.Single(x => x.PessoaId == id);
            context.Pessoa.Remove(pessoa);
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