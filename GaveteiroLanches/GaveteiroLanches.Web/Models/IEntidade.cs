using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio01EF.Models
{
    public interface IEntidade
    {
        DateTime DataHoraCad { get; set; }
        string UsuarioCad { get; set; }
    }
}
