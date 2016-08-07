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
    public class LogradouroController : Controller
    {
        private const string _PESQUISA_KEY = "1f234a84-c359-454f-a304-a4edfaffc280";

        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Logradouro/
        public async Task<ActionResult> Indice()
        {

            var viewModel = JsonConvert.DeserializeObject<LogradouroViewModel>(await PesquisaModelStore.GetAsync(Guid.Parse(_PESQUISA_KEY)));

            return await Pesquisa(viewModel ?? new LogradouroViewModel());

        }

        //
        // GET: /Logradouro/Pesquisa
        public async Task<ActionResult> Pesquisa(LogradouroViewModel viewModel)
        {
            await PesquisaModelStore.AddAsync(Guid.Parse(_PESQUISA_KEY), viewModel);

            var query = context.Logradouroes.AsQueryable();

            //TODO: parâmetros de pesquisa

            viewModel.Resultados = await query.OrderBy(a => a.Descricao).ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            if (Request.IsAjaxRequest())
                return PartialView("_Pesquisa", viewModel);

            return View("Indice", viewModel);
        }

        //
        // GET: /Logradouro/Detalhes/5

        public async Task<ActionResult> Detalhes(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logradouro logradouro = await context.Logradouroes.FindAsync(id);
            if (logradouro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bairroes = new SelectList(await context.Bairroes.ToListAsync(), "BairroId", "Nome");
            return View(logradouro);
        }

        //
        // GET: /Logradouro/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.Bairroes = new SelectList(await context.Bairroes.ToListAsync(), "BairroId", "Nome");
            return View();
        }

        //
        // POST: /Logradouro/Criar

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Criar(Logradouro logradouro)
        {
            if (ModelState.IsValid)
            {
                logradouro.LogradouroId = Guid.NewGuid();
                context.Logradouroes.Add(logradouro);
                await context.SaveChangesAsync();
                return RedirectToAction("Indice");
            }

            ViewBag.Bairroes = new SelectList(await context.Bairroes.ToListAsync(), "BairroId", "Nome");
            return View(logradouro);
        }

        //
        // GET: /Logradouro/Editar/5

        public async Task<ActionResult> Editar(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logradouro logradouro = await context.Logradouroes.FindAsync(id);
            if (logradouro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bairroes = new SelectList(await context.Bairroes.ToListAsync(), "BairroId", "Nome");
            return View(logradouro);
        }

        //
        // POST: /Logradouro/Editar/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Logradouro logradouro)
        {
            if (ModelState.IsValid)
            {
                context.Entry(logradouro).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("Indice");
            }
            ViewBag.Bairroes = new SelectList(await context.Bairroes.ToListAsync(), "BairroId", "Nome");
            return View(logradouro);
        }

        //
        // GET: /Logradouro/Excluir/5

        public async Task<ActionResult> Excluir(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logradouro logradouro = await context.Logradouroes.FindAsync(id);
            if (logradouro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bairroes = new SelectList(await context.Bairroes.ToListAsync(), "BairroId", "Nome");

            return View(logradouro);
        }

        //
        // POST: /Logradouro/Excluir/5

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmacao(System.Guid id)
        {
            Logradouro logradouro = await context.Logradouroes.FindAsync(id);
            context.Logradouroes.Remove(logradouro);
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
