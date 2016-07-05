using System.ComponentModel.DataAnnotations;

namespace Exercicio03Modularizacao.Common.ViewModels.Admin
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }
}
