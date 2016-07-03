using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingCraft.Domain.ViewModels.Fornecedor
{
    public class FornecedorViewModel
    {
        public int FornecedorId { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Telefone { get; set; }
    }
}