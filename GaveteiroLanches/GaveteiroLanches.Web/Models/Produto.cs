﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaveteiroLanches.Web.Models
{
    public class Produto : Entidade
    {
        public int ProdutoId { get; set; }

        public string Descricao { get; set; }
        
        public decimal Valor { get; set; }

        public string Categoria { get; set; }

        public int Quantidade { get; set; }

        public ICollection<MovimentacaoProduto> MovimentacaoProduto { get; set; }
    }
}