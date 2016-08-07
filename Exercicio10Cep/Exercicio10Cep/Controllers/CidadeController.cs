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
    public class CidadeController : Controller
    {
        private const string _PESQUISA_KEY = "1510a7b5-1d5f-4448-b70c-074dea541d1d";

        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Cidade/
        public async Task<ActionResult> Indice()
        {

            var viewModel = JsonConvert.DeserializeObject<CidadeViewModel>(await PesquisaModelStore.GetAsync(Guid.Parse(_PESQUISA_KEY)));

            return await Pesquisa(viewModel ?? new CidadeViewModel());

        }

        //
        // GET: /Cidade/Pesquisa
        public async Task<ActionResult> Pesquisa(CidadeViewModel viewModel)
        {
            await PesquisaModelStore.AddAsync(Guid.Parse(_PESQUISA_KEY), viewModel);

            var query = context.Cidades.Include(cidade => cidade.Estado).Include(cidade => cidade.Bairros).AsQueryable();

            //TODO: parâmetros de pesquisa

            viewModel.Resultados = await query.OrderBy(a => a.Nome).ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            if (Request.IsAjaxRequest())
                return PartialView("_Pesquisa", viewModel);

            return View("Indice", viewModel);
        }

        //
        // GET: /Cidade/Detalhes/5

        public async Task<ActionResult> Detalhes(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = await context.Cidades.FindAsync(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estadoes = new SelectList(await context.Estadoes.ToListAsync(), "EstadoId", "Nome");
            return View(cidade);
        }

        //
        // GET: /Cidade/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.Estadoes = new SelectList(await context.Estadoes.ToListAsync(), "EstadoId", "Nome");
            return View();
        }

        //
        // POST: /Cidade/Criar

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Criar(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                cidade.CidadeId = Guid.NewGuid();
                context.Cidades.Add(cidade);
                await context.SaveChangesAsync();
                return RedirectToAction("Indice");
            }

            ViewBag.Estadoes = new SelectList(await context.Estadoes.ToListAsync(), "EstadoId", "Nome");
            return View(cidade);
        }

        //
        // GET: /Cidade/Editar/5

        public async Task<ActionResult> Editar(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = await context.Cidades.FindAsync(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estadoes = new SelectList(await context.Estadoes.ToListAsync(), "EstadoId", "Nome");
            return View(cidade);
        }

        //
        // POST: /Cidade/Editar/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                context.Entry(cidade).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("Indice");
            }
            ViewBag.Estadoes = new SelectList(await context.Estadoes.ToListAsync(), "EstadoId", "Nome");
            return View(cidade);
        }

        //
        // GET: /Cidade/Excluir/5

        public async Task<ActionResult> Excluir(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = await context.Cidades.FindAsync(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estadoes = new SelectList(await context.Estadoes.ToListAsync(), "EstadoId", "Nome");

            return View(cidade);
        }

        //
        // POST: /Cidade/Excluir/5

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmacao(System.Guid id)
        {
            Cidade cidade = await context.Cidades.FindAsync(id);
            context.Cidades.Remove(cidade);
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
