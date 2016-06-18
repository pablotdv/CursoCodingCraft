using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaveteiroLanches.Web.Models
{
    public class Auditoria
    {
        public int AuditoriaId { get; set; }

        public string Usuario { get; set; }

        public DateTime DataHora { get; set; }

        public string Tipo { get; set; }

        public string Entidade { get; set; }

        public string EntidadeId { get; set; }

        public string Propriedade { get; set; }

        public string ValorOriginal { get; set; }

        public string ValorNovo { get; set; }
    }
}
