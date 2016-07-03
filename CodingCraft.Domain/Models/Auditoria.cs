using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCraft.Domain.Models
{
    public class Auditoria
    {
        [Key]
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
