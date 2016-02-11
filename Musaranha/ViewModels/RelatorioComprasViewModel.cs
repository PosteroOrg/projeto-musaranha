﻿using System.Collections.Generic;
using Musaranha.Models;

namespace Musaranha.ViewModels
{
    public class RelatorioComprasViewModel
    {
        public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}