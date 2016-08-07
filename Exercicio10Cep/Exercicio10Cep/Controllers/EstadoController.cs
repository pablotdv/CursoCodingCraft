using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using PagedList.EntityFramework;
using Newtonsoft.Json;
using System.Net;
using Exercicio10Cep.Helpers;
using Exercicio10Cep.ViewModels;
using Exercicio10Cep.Models;

namespace Exercicio10Cep.Controllers
{
    public class EstadoController : Controller
    {
        private const string _PESQUISA_KEY = "5f1d4c03-7e27-4a9f-b29c-a57cf7ee6850";

        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Estado/
        public async Task<ActionResult> Indice()
        {

            var viewModel = JsonConvert.DeserializeObject<EstadoViewModel>(await PesquisaModelStore.GetAsync(Guid.Parse(_PESQUISA_KEY)));

            return await Pesquisa(viewModel ?? new EstadoViewModel());

        }

        //
        // GET: /Estado/Pesquisa
        public async Task<ActionResult> Pesquisa(EstadoViewModel viewModel)
        {
            await PesquisaModelStore.AddAsync(Guid.Parse(_PESQUISA_KEY), viewModel);

            var query = context.Estadoes.Include(estado => estado.Pais).Include(estado => estado.Cidades).AsQueryable();

            //TODO: parâmetros de pesquisa

            viewModel.Resultados = await query.OrderBy(a => a.Nome).ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            if (Request.IsAjaxRequest())
                return PartialView("_Pesquisa", viewModel);

            return View("Indice", viewModel);
        }

        //
        // GET: /Estado/Detalhes/5

        public async Task<ActionResult> Detalhes(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await context.Estadoes.FindAsync(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pais = new SelectList(await context.Pais.ToListAsync(), "PaisId", "Nome");
            return View(estado);
        }

        //
        // GET: /Estado/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.Pais = new SelectList(await context.Pais.ToListAsync(), "PaisId", "Nome");
            return View();
        }

        //
        // POST: /Estado/Criar

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Criar(Estado estado)
        {
            if (ModelState.IsValid)
            {
                estado.EstadoId = Guid.NewGuid();
                context.Estadoes.Add(estado);
                await context.SaveChangesAsync();
                return RedirectToAction("Indice");
            }

            ViewBag.Pais = new SelectList(await context.Pais.ToListAsync(), "PaisId", "Nome");
            return View(estado);
        }

        //
        // GET: /Estado/Editar/5

        public async Task<ActionResult> Editar(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await context.Estadoes.FindAsync(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pais = new SelectList(await context.Pais.ToListAsync(), "PaisId", "Nome");
            return View(estado);
        }

        //
        // POST: /Estado/Editar/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Estado estado)
        {
            if (ModelState.IsValid)
            {
                context.Entry(estado).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("Indice");
            }
            ViewBag.Pais = new SelectList(await context.Pais.ToListAsync(), "PaisId", "Nome");
            return View(estado);
        }

        //
        // GET: /Estado/Excluir/5

        public async Task<ActionResult> Excluir(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await context.Estadoes.FindAsync(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pais = new SelectList(await context.Pais.ToListAsync(), "PaisId", "Nome");

            return View(estado);
        }

        //
        // POST: /Estado/Excluir/5

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmacao(System.Guid id)
        {
            Estado estado = await context.Estadoes.FindAsync(id);
            context.Estadoes.Remove(estado);
            await context.SaveChangesAsync();
            return RedirectToAction("Indice");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
