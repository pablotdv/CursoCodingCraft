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
    public class PaisController : Controller
    {
        private const string _PESQUISA_KEY = "2fb46b2c-ccd0-4b82-9a5f-976d34660041";

        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Pais/
        public async Task<ActionResult> Indice()
        {

            var viewModel = JsonConvert.DeserializeObject<PaisViewModel>(await PesquisaModelStore.GetAsync(Guid.Parse(_PESQUISA_KEY)));

            return await Pesquisa(viewModel ?? new PaisViewModel());

        }

        //
        // GET: /Pais/Pesquisa
        public async Task<ActionResult> Pesquisa(PaisViewModel viewModel)
        {
            await PesquisaModelStore.AddAsync(Guid.Parse(_PESQUISA_KEY), viewModel);

            var query = context.Pais.Include(pais => pais.Estados).AsQueryable();

            //TODO: parâmetros de pesquisa

            viewModel.Resultados = await query.OrderBy(a => a.Nome).ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            if (Request.IsAjaxRequest())
                return PartialView("_Pesquisa", viewModel);

            return View("Indice", viewModel);
        }

        //
        // GET: /Pais/Detalhes/5

        public async Task<ActionResult> Detalhes(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = await context.Pais.FindAsync(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        //
        // GET: /Pais/Criar

        public async Task<ActionResult> Criar()
        {
            return View();
        }

        //
        // POST: /Pais/Criar

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Criar(Pais pais)
        {
            if (ModelState.IsValid)
            {
                pais.PaisId = Guid.NewGuid();
                context.Pais.Add(pais);
                await context.SaveChangesAsync();
                return RedirectToAction("Indice");
            }

            return View(pais);
        }

        //
        // GET: /Pais/Editar/5

        public async Task<ActionResult> Editar(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = await context.Pais.FindAsync(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        //
        // POST: /Pais/Editar/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.Entry(pais).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("Indice");
            }
            return View(pais);
        }

        //
        // GET: /Pais/Excluir/5

        public async Task<ActionResult> Excluir(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = await context.Pais.FindAsync(id);
            if (pais == null)
            {
                return HttpNotFound();
            }

            return View(pais);
        }

        //
        // POST: /Pais/Excluir/5

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmacao(System.Guid id)
        {
            Pais pais = await context.Pais.FindAsync(id);
            context.Pais.Remove(pais);
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
