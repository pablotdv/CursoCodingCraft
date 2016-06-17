using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaveteiroLanches.Entidades
{
    public interface IEntidade
    {
        DateTime DataHoraCad { get; set; }
        string UsuarioCad { get; set; }
    }
}
