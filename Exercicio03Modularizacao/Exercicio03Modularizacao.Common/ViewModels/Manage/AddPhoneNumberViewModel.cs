using System.ComponentModel.DataAnnotations;

namespace Exercicio03Modularizacao.Common.ViewModels.Manage
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}
