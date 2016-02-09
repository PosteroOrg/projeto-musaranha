using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Musaranha.Models;

namespace Musaranha.ViewModels
{
    public class VendaEditarViewModel
    {
        public Venda Venda { get; set; }
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}