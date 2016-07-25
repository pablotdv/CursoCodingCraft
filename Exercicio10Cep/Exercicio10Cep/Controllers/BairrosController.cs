using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Exercicio10Cep.Models;
using IdentitySample.Models;

namespace Exercicio10Cep.Controllers
{
    public class BairrosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bairros
        public async Task<ActionResult> Index()
        {
            var bairros = db.Bairros.Include(b => b.Cidade);
            return View(await bairros.ToListAsync());
        }

        // GET: Bairros/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bairro bairro = await db.Bairros.FindAsync(id);
            if (bairro == null)
            {
                return HttpNotFound();
            }
            return View(bairro);
        }

        // GET: Bairros/Create
        public ActionResult Create()
        {
            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome");
            return View();
        }

        // POST: Bairros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BairroId,CidadeId,Nome,NomeAbreviado,NomeFonetizado,UltimaModificacao,DataCriacao")] Bairro bairro)
        {
            if (ModelState.IsValid)
            {
                bairro.BairroId = Guid.NewGuid();
                db.Bairros.Add(bairro);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome", bairro.CidadeId);
            return View(bairro);
        }

        // GET: Bairros/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bairro bairro = await db.Bairros.FindAsync(id);
            if (bairro == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome", bairro.CidadeId);
            return View(bairro);
        }

        // POST: Bairros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BairroId,CidadeId,Nome,NomeAbreviado,NomeFonetizado,UltimaModificacao,DataCriacao")] Bairro bairro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bairro).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome", bairro.CidadeId);
            return View(bairro);
        }

        // GET: Bairros/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bairro bairro = await db.Bairros.FindAsync(id);
            if (bairro == null)
            {
                return HttpNotFound();
            }
            return View(bairro);
        }

        // POST: Bairros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Bairro bairro = await db.Bairros.FindAsync(id);
            db.Bairros.Remove(bairro);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
