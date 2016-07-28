using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercicio10Cep.Models
{
    public class PesquisaModel
    {
        [Key]
        public Guid PesquisaModelId { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        [Required]
        public string Filtro { get; set; }
    }
}