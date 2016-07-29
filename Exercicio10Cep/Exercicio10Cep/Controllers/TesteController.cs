using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercicio10Cep.Models;

namespace Exercicio10Cep.Controllers
{   
    public class TesteController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Teste/

        public ViewResult Index()
        {
            return View(context.Cidades.Include(cidade => cidade.Estado).Include(cidade => cidade.Bairros).ToList());
        }

        //
        // GET: /Teste/Details/5

        public ViewResult Details(System.Guid id)
        {
            Cidade cidade = context.Cidades.Single(x => x.CidadeId == id);
            return View(cidade);
        }

        //
        // GET: /Teste/Create

        public ActionResult Create()
        {
            ViewBag.PossibleEstadoes = context.Estados;
            return View();
        } 

        //
        // POST: /Teste/Create

        [HttpPost]
        public ActionResult Create(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                cidade.CidadeId = Guid.NewGuid();
                context.Cidades.Add(cidade);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleEstadoes = context.Estados;
            return View(cidade);
        }
        
        //
        // GET: /Teste/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
            Cidade cidade = context.Cidades.Single(x => x.CidadeId == id);
            ViewBag.PossibleEstadoes = context.Estados;
            return View(cidade);
        }

        //
        // POST: /Teste/Edit/5

        [HttpPost]
        public ActionResult Edit(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                context.Entry(cidade).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleEstadoes = context.Estados;
            return View(cidade);
        }

        //
        // GET: /Teste/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            Cidade cidade = context.Cidades.Single(x => x.CidadeId == id);
            return View(cidade);
        }

        //
        // POST: /Teste/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            Cidade cidade = context.Cidades.Single(x => x.CidadeId == id);
            context.Cidades.Remove(cidade);
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