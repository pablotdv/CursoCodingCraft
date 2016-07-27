using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Exercicio10Cep.Models;
using System.Linq;

namespace Exercicio10Cep.Controllers
{
    public class PaisesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paises
        public async Task<ActionResult> Index()
        {
            
            return View(await db.Paises.ToListAsync());
        }

        // GET: Paises/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = await db.Paises.FindAsync(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // GET: Paises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PaisId,Nome,Sigla,UltimaModificacao,DataCriacao")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                pais = new Pais();
                if (pais.Estados == null)
                    pais.Estados = new List<Estado>();

                pais.Estados.Add(new Estado() { Sigla = "RS", Nome = "Rio Grande do Sul" });

                pais.PaisId = Guid.NewGuid();
                db.Paises.Add(pais);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pais);
        }

        // GET: Paises/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = await db.Paises.FindAsync(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // POST: Paises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PaisId,Nome,Sigla,UltimaModificacao,DataCriacao")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pais).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pais);
        }

        // GET: Paises/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = await db.Paises.FindAsync(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // POST: Paises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Pais pais = await db.Paises.FindAsync(id);
            db.Paises.Remove(pais);
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
