using CodingCraft.Domain.Models;
using CodingCraft.Domain.ViewModels.Fornecedor;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Exercicio01EF.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FornecedorController : Controller
    {
        private CodingCraftDbContext context;

        public FornecedorController()
        {
            context = new CodingCraftDbContext();
        }

        // GET: Fornecedor
        public ActionResult Index()
        {
            var fornecedores = context.Fornecedor.Select(a => new FornecedorIndexViewModel()
            {
                FornecedorId = a.FornecedorId,
                Nome = a.Nome,
                Email = a.Email,
                Telefone = a.Telefone
            }).OrderBy(a => a.Nome).ToList();

            return View(fornecedores);
        }

        private async Task<ActionResult> FornecedorView(string view, int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Fornecedor fornecedor = await context.Fornecedor.FindAsync(id);
            if (fornecedor == null)
            {
                return RedirectToAction("Index");
            }

            FornecedorViewModel model = new FornecedorViewModel()
            {
                FornecedorId = fornecedor.FornecedorId,
                Nome = fornecedor.Nome,
                Email = fornecedor.Email,
                Telefone = fornecedor.Telefone,
            };

            return View(view, model);
        }

        public ActionResult Create()
        {
            return View("Fornecedor", new FornecedorViewModel());
        }

        public async Task<ActionResult> Edit(int? id)
        {
            return await FornecedorView("Fornecedor", id);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            return await FornecedorView("Delete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fornecedor fornecedor = await context.Fornecedor.FindAsync(id);
            context.Fornecedor.Remove(fornecedor);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            return await FornecedorView("Details", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Salvar(FornecedorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fornecedor = await context.Fornecedor.FindAsync(model.FornecedorId);

                bool novo = fornecedor == null;
                if (novo)
                    fornecedor = new Fornecedor();

                fornecedor.Nome = model.Nome;
                fornecedor.Email = model.Email;
                fornecedor.Telefone = model.Telefone;

                if (novo)
                    context.Fornecedor.Add(fornecedor);

                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Fornecedor", model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();

            base.Dispose(disposing);
        }
    }
}