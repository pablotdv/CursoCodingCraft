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
    public class BairroController : Controller
    {
		private const string _PESQUISA_KEY = "f6f443e9-9b8e-42cb-9b9b-4fd2ba297209"; 
		
        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Bairro/
        public async Task<ActionResult> Indice()
        {

			var viewModel = JsonConvert.DeserializeObject<BairroViewModel>(await PesquisaModelStore.GetAsync(Guid.Parse(_PESQUISA_KEY)));

            return await Pesquisa(viewModel ?? new BairroViewModel());

        }

		//
        // GET: /Bairro/Pesquisa
		public async Task<ActionResult> Pesquisa(BairroViewModel viewModel)
		{
			await PesquisaModelStore.AddAsync(Guid.Parse(_PESQUISA_KEY), viewModel);

			var query = context.Bairroes.Include(bairro => bairro.Cidade).Include(bairro => bairro.Logradouros).AsQueryable();

			//TODO: parâmetros de pesquisa

            viewModel.Resultados = await query.OrderBy(a=>a.Nome).ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            if (Request.IsAjaxRequest())
                return PartialView("_Pesquisa", viewModel);

            return View("Indice", viewModel);
		}

        //
        // GET: /Bairro/Detalhes/5

        public async Task<ActionResult> Detalhes(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bairro bairro = await context.Bairroes.FindAsync(id);
            if (bairro == null)
            {
                return HttpNotFound();
            }            
            ViewBag.Cidades = new SelectList(await context.Cidades.ToListAsync(), "CidadeId", "Nome");
            return View(bairro);
        }

        //
        // GET: /Bairro/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.Cidades = new SelectList(await context.Cidades.ToListAsync(), "CidadeId", "Nome");
            return View();
        } 

        //
        // POST: /Bairro/Criar

        [HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> Criar(Bairro bairro)
        {
            if (ModelState.IsValid)
            {
                bairro.BairroId = Guid.NewGuid();
                context.Bairroes.Add(bairro);
                await context.SaveChangesAsync();
                return RedirectToAction("Indice");  
            }

            ViewBag.Cidades = new SelectList(await context.Cidades.ToListAsync(), "CidadeId", "Nome");
            return View(bairro);
        }
        
        //
        // GET: /Bairro/Editar/5
 
        public async Task<ActionResult> Editar(System.Guid id)
        {
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bairro bairro = await context.Bairroes.FindAsync(id);
            if (bairro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cidades = new SelectList(await context.Cidades.ToListAsync(), "CidadeId", "Nome");
            return View(bairro);
        }

        //
        // POST: /Bairro/Editar/5

        [HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Bairro bairro)
        {
            if (ModelState.IsValid)
            {
                context.Entry(bairro).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("Indice");
            }
            ViewBag.Cidades = new SelectList(await context.Cidades.ToListAsync(), "CidadeId", "Nome");
            return View(bairro);
        }

        //
        // GET: /Bairro/Excluir/5
 
        public async Task<ActionResult> Excluir(System.Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bairro bairro = await context.Bairroes.FindAsync(id);
            if (bairro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cidades = new SelectList(await context.Cidades.ToListAsync(), "CidadeId", "Nome");

            return View(bairro);
        }

        //
        // POST: /Bairro/Excluir/5

        [HttpPost, ActionName("Excluir")]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmacao(System.Guid id)
        {
            Bairro bairro = await context.Bairroes.FindAsync(id);
            context.Bairroes.Remove(bairro);
            await context.SaveChangesAsync();
            return RedirectToAction("Indice");
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
