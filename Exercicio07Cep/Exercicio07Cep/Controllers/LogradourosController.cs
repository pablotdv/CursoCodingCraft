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
    public class LogradourosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Logradouros
        public async Task<ActionResult> Index()
        {
            return View(await db.Logradouros.ToListAsync());
        }

        // GET: Logradouros/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logradouro logradouro = await db.Logradouros.FindAsync(id);
            if (logradouro == null)
            {
                return HttpNotFound();
            }
            return View(logradouro);
        }

        // GET: Logradouros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logradouros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LogradouroId,BairroId,Descricao,DescricaoFonetizado,Cep,UltimaModificacao,DataCriacao")] Logradouro logradouro)
        {
            if (ModelState.IsValid)
            {
                logradouro.LogradouroId = Guid.NewGuid();
                db.Logradouros.Add(logradouro);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(logradouro);
        }

        // GET: Logradouros/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logradouro logradouro = await db.Logradouros.FindAsync(id);
            if (logradouro == null)
            {
                return HttpNotFound();
            }
            return View(logradouro);
        }

        // POST: Logradouros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LogradouroId,BairroId,Descricao,DescricaoFonetizado,Cep,UltimaModificacao,DataCriacao")] Logradouro logradouro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logradouro).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(logradouro);
        }

        // GET: Logradouros/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logradouro logradouro = await db.Logradouros.FindAsync(id);
            if (logradouro == null)
            {
                return HttpNotFound();
            }
            return View(logradouro);
        }

        // POST: Logradouros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Logradouro logradouro = await db.Logradouros.FindAsync(id);
            db.Logradouros.Remove(logradouro);
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
