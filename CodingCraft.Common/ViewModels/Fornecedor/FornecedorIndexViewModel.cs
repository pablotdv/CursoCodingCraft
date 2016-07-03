using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraft.Common.ViewModels.Fornecedor
{
    public class FornecedorIndexViewModel
    {
        public int FornecedorId { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
    }
}