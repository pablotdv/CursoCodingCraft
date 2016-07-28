using Exercicio10Cep.Models;
using Exercicio10Cep.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Exercicio10Cep.Helpers
{
    public class PesquisaModelStore
    {
        public static async Task AddAsync(Guid key, IPagedListViewModel viewModel)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Guid usuarioId = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());

                PesquisaModel model = await context.PesquisaModels.FindAsync(key);
                bool novo = model == null;
                if (novo)
                    model = new PesquisaModel()
                    {
                        PesquisaModelId = key
                    };

                model.UsuarioId = usuarioId;
                model.Filtro = JsonConvert.SerializeObject(viewModel);

                if (novo)
                    context.PesquisaModels.Add(model);

                await context.SaveChangesAsync();
            }
        }

        public static async Task<string> GetAsync(Guid key)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                PesquisaModel model = await context.PesquisaModels.FindAsync(key);

                if (model == null)
                    return "";

                return model.Filtro;
            }
        }
    }
}