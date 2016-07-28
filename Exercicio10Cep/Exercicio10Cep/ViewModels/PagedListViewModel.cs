using Newtonsoft.Json;
using PagedList;

namespace Exercicio10Cep.ViewModels
{
    public class PagedListViewModel<T> : IPagedListViewModel
    {
        public PagedListViewModel()
        {
            Pagina = 1;
            TamanhoPagina = 10;
        }

        public int Pagina { get; set; }

        public int TamanhoPagina { get; set; }

        [JsonIgnore]
        public IPagedList<T> Resultados { get; set; }
    }
}