using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercicio02ScaffoldLayouts.Models;

namespace Exercicio02ScaffoldLayouts.Controllers
{   
    public class ProdutoGrupoController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /ProdutoGrupo/

        public ViewResult Index()
        {
            return View(context.ProdutoGrupoes.Include(produtogrupo => produtogrupo.Produtos).ToList());
        }

        //
        // GET: /ProdutoGrupo/Detalhes/5

        public ViewResult Detalhes(System.Guid id)
        {
            ProdutoGrupo produtogrupo = context.ProdutoGrupoes.Single(x => x.ProdutoGrupoId == id);
            return View(produtogrupo);
        }

        //
        // GET: /ProdutoGrupo/Criar

        public ActionResult Criar()
        {
            return View();
        } 

        //
        // POST: /ProdutoGrupo/Criar

        [HttpPost]
        public ActionResult Criar(ProdutoGrupo produtogrupo)
        {
            if (ModelState.IsValid)
            {
                produtogrupo.ProdutoGrupoId = Guid.NewGuid();
                context.ProdutoGrupoes.Add(produtogrupo);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(produtogrupo);
        }
        
        //
        // GET: /ProdutoGrupo/Editar/5
 
        public ActionResult Editar(System.Guid id)
        {
            ProdutoGrupo produtogrupo = context.ProdutoGrupoes.Single(x => x.ProdutoGrupoId == id);
            return View(produtogrupo);
        }

        //
        // POST: /ProdutoGrupo/Editar/5

        [HttpPost]
        public ActionResult Editar(ProdutoGrupo produtogrupo)
        {
            if (ModelState.IsValid)
            {
                context.Entry(produtogrupo).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produtogrupo);
        }

        //
        // GET: /ProdutoGrupo/Excluir/5
 
        public ActionResult Excluir(System.Guid id)
        {
            ProdutoGrupo produtogrupo = context.ProdutoGrupoes.Single(x => x.ProdutoGrupoId == id);
            return View(produtogrupo);
        }

        //
        // POST: /ProdutoGrupo/Excluir/5

        [HttpPost, ActionName("Exluir")]
        public ActionResult ExcluirConfirmacao(System.Guid id)
        {
            ProdutoGrupo produtogrupo = context.ProdutoGrupoes.Single(x => x.ProdutoGrupoId == id);
            context.ProdutoGrupoes.Remove(produtogrupo);
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