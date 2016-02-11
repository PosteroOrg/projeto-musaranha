using System.Collections.Generic;
using Musaranha.Models;

namespace Musaranha.ViewModels
{
    public class CompraIndexViewModel
    {
        public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public List<Compra> Compras { get; set; } = new List<Compra>();
    }
}