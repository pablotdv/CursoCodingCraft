using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Exercicio10Cep.Models;

namespace Exercicio10Cep.Controllers
{
    public class EstadosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Estados
        public async Task<ActionResult> Index()
        {
            var estados = db.Estados.Include(e => e.Pais);
            return View(await estados.ToListAsync());
        }

        // GET: Estados/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await db.Estados.FindAsync(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // GET: Estados/Create
        public ActionResult Create()
        {
            ViewBag.PaisId = new SelectList(db.Paises, "PaisId", "Nome");
            return View();
        }

        // POST: Estados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EstadoId,PaisId,Nome,Sigla,UltimaModificacao,DataCriacao")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                estado.EstadoId = Guid.NewGuid();
                db.Estados.Add(estado);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PaisId = new SelectList(db.Paises, "PaisId", "Nome", estado.PaisId);
            return View(estado);
        }

        // GET: Estados/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await db.Estados.FindAsync(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaisId = new SelectList(db.Paises, "PaisId", "Nome", estado.PaisId);
            return View(estado);
        }

        // POST: Estados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EstadoId,PaisId,Nome,Sigla,UltimaModificacao,DataCriacao")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estado).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PaisId = new SelectList(db.Paises, "PaisId", "Nome", estado.PaisId);
            return View(estado);
        }

        // GET: Estados/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await db.Estados.FindAsync(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Estado estado = await db.Estados.FindAsync(id);
            db.Estados.Remove(estado);
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
