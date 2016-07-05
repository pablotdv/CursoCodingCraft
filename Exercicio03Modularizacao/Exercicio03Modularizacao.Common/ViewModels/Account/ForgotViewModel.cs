using System.ComponentModel.DataAnnotations;

namespace Exercicio03Modularizacao.Common.ViewModels.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
