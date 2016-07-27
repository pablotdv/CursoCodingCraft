using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Exercicio10Cep.Controllers
{
public abstract class Controller<TEntidade> : System.Web.Mvc.Controller
    where TEntidade: class
{
    private Exercicio10Cep.Models.ApplicationDbContext db = new Exercicio10Cep.Models.ApplicationDbContext();

    // GET: Paises
    public async Task<ActionResult> Index()
    {
        return View(await db.Set<TEntidade>().AsQueryable().ToListAsync());
    }

    // GET: Paises/Details/5
    public async Task<ActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        TEntidade entidade = await db.Set<TEntidade>().FindAsync(id);
        if (entidade == null)
        {
            return HttpNotFound();
        }
        return View(entidade);
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
    public async Task<ActionResult> Create(TEntidade entidade)
    {
        if (ModelState.IsValid)
        {
            var key = entidade.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).FirstOrDefault();
                                
            db.Set<TEntidade>().Add(entidade);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(entidade);
    }

    // GET: Paises/Edit/5
    public async Task<ActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        var entidade = await db.Set<TEntidade>().FindAsync(id);
        if (entidade == null)
        {
            return HttpNotFound();
        }
        return View(entidade);
    }

    // POST: Paises/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(TEntidade entidade)
    {
        if (ModelState.IsValid)
        {
            db.Entry<TEntidade>(entidade).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(entidade);
    }

    // GET: Paises/Delete/5
    public async Task<ActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        var entidade = await db.Paises.FindAsync(id);
        if (entidade == null)
        {
            return HttpNotFound();
        }
        return View(entidade);
    }

    // POST: Paises/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(Guid id)
    {
        var entidade = await db.Set<TEntidade>().FindAsync(id);
        db.Set<TEntidade>().Remove(entidade);
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