using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Exercicio10Cep.Models;

namespace Exercicio10Cep.ViewModels
{ 

    public class PaisViewModel : PagedListViewModel<Pais>
    {
		public string Nome { get; set; }
	}
}