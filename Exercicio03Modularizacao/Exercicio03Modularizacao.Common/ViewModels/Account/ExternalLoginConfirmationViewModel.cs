using System.ComponentModel.DataAnnotations;

namespace Exercicio03Modularizacao.Common.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
