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
    [Authorize]
    public class CidadeController : Controller
    {
		private const string _PESQUISA_KEY = "b9b4c886-460c-491f-9b83-4d626c2a372b"; 
		
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

            viewModel.Resultados = await query.OrderBy(a=>a.Estado.Sigla).ThenBy(a=>a.Nome).ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            if (Request.IsAjaxRequest())
                return PartialView("_Pesquisa", viewModel);

            return View("Indice", viewModel);
		}

        //
        // GET: /Cidade/Detalhes/5

        public ViewResult Detalhes(System.Guid id)
        {
            Cidade cidade = context.Cidades.Single(x => x.CidadeId == id);
            return View(cidade);
        }

        //
        // GET: /Cidade/Criar

        public ActionResult Criar()
        {
            ViewBag.PossibleEstadoes = context.Estados;
            return View();
        } 

        //
        // POST: /Cidade/Criar

        [HttpPost]
        public ActionResult Criar([Bind()] Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                cidade.CidadeId = Guid.NewGuid();
                context.Cidades.Add(cidade);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleEstadoes = context.Estados;
            return View(cidade);
        }
        
        //
        // GET: /Cidade/Editar/5
 
        public ActionResult Editar(System.Guid id)
        {
            Cidade cidade = context.Cidades.Single(x => x.CidadeId == id);
            ViewBag.PossibleEstadoes = context.Estados;
            return View(cidade);
        }

        //
        // POST: /Cidade/Editar/5

        [HttpPost]
        public ActionResult Editar(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                context.Entry(cidade).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleEstadoes = context.Estados;
            return View(cidade);
        }

        //
        // GET: /Cidade/Excluir/5
 
        public ActionResult Excluir(System.Guid id)
        {
            Cidade cidade = context.Cidades.Single(x => x.CidadeId == id);
            return View(cidade);
        }

        //
        // POST: /Cidade/Excluir/5

        [HttpPost, ActionName("Exluir")]
        public ActionResult ExcluirConfirmacao(System.Guid id)
        {
            Cidade cidade = context.Cidades.Single(x => x.CidadeId == id);
            context.Cidades.Remove(cidade);
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