using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaveteiroLanches.Web.Models
{
    public class Fornecedor : Entidade
    {
        [Key]
        public int FornecedorId { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public ICollection<MovimentacaoEntrada> MovimentacaoEntrada { get; set; }


    }
}
