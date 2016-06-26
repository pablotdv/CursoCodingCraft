using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.ViewModels.Fornecedor
{
    public class FornecedorViewModel
    {
        public int FornecedorId { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
    }
}