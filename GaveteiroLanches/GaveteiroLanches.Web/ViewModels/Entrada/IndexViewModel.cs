﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaveteiroLanches.Web.ViewModels.Entrada
{
    public class IndexViewModel
    {
        public DateTime DataHora { get; set; }
        public string Fornecedor { get; set; }
        public int MovimentacaoId { get; set; }
    }
}