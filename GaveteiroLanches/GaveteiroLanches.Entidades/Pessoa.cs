using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaveteiroLanches.Entidades
{
    public class Pessoa : Entidade
    {
        public int PessoaId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
