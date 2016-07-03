using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercicio01EF.ViewModels.Fornecedor
{
    public class IndexViewModel
    {
        public int FornecedorId { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
    }
}