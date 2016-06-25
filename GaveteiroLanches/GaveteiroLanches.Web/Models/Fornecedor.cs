using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaveteiroLanches.Web.Models
{
    public class Fornecedor : Entidade
    {
        public int FornecedorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public ICollection<MovimentacaoEntrada> MovimentacaoEntrada { get; set; }
    }
}
