using GaveteiroLanches.Web.Models;
using GaveteiroLanches.Web.ViewModels.Entrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GaveteiroLanches.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EntradaController : Controller
    {
        private GaveteiroLanchesContext context;

        public EntradaController()
        {
            context = new GaveteiroLanchesContext();
        }
        // GET: Entrada
        public ActionResult Index()
        {
            var entradas = context.MovimentacaoEntrada.Select(a => new IndexViewModel()
            {
                MovimentacaoId = a.MovimentacaoId,
                Fornecedor = a.Fornecedor.Nome,
                DataHora = a.DataHora
            }).ToList();

            return View(entradas);
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