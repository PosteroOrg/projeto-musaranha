using System.Collections.Generic;
using Musaranha.Models;

namespace Musaranha.ViewModels
{
    public class VendaIndexViewModel
    {
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public List<Venda> Vendas { get; set; } = new List<Venda>();
    }
}