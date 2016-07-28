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
using Exercicio10Cep.Helpers;
using Exercicio10Cep.ViewModels;
using Exercicio10Cep.Models;

namespace Exercicio10Cep.Controllers
{   
    public class PaisController : Controller
    {
		private const string _PESQUISA_KEY = "b988ab86-fa53-4013-8581-3f5e7798b7fc"; 
		
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

            viewModel.Resultados = await query.ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            if (Request.IsAjaxRequest())
                return PartialView("_Pesquisa", viewModel);

            return View("Indice", viewModel);
		}

        //
        // GET: /Pais/Detalhes/5

        public ViewResult Detalhes(System.Guid id)
        {
            Pais pais = context.Pais.Single(x => x.PaisId == id);
            return View(pais);
        }

        //
        // GET: /Pais/Criar

        public ActionResult Criar()
        {
            return View();
        } 

        //
        // POST: /Pais/Criar

        [HttpPost]
        public ActionResult Criar(Pais pais)
        {
            if (ModelState.IsValid)
            {
                pais.PaisId = Guid.NewGuid();
                context.Pais.Add(pais);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(pais);
        }
        
        //
        // GET: /Pais/Editar/5
 
        public ActionResult Editar(System.Guid id)
        {
            Pais pais = context.Pais.Single(x => x.PaisId == id);
            return View(pais);
        }

        //
        // POST: /Pais/Editar/5

        [HttpPost]
        public ActionResult Editar(Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.Entry(pais).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pais);
        }

        //
        // GET: /Pais/Excluir/5
 
        public ActionResult Excluir(System.Guid id)
        {
            Pais pais = context.Pais.Single(x => x.PaisId == id);
            return View(pais);
        }

        //
        // POST: /Pais/Excluir/5

        [HttpPost, ActionName("Exluir")]
        public ActionResult ExcluirConfirmacao(System.Guid id)
        {
            Pais pais = context.Pais.Single(x => x.PaisId == id);
            context.Pais.Remove(pais);
            context.SaveChanges();
            return RedirectToAction("Index");
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