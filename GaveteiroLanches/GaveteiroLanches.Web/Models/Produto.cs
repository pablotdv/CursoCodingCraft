using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaveteiroLanches.Web.Models
{
    public class Produto : Entidade
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        public string Descricao { get; set; }
        
        [Required]
        public decimal Valor { get; set; }

        public string Categoria { get; set; }

        public int Quantidade { get; set; }

        public ICollection<MovimentacaoProduto> MovimentacaoProduto { get; set; }
    }
}
