using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musaranha.Models
{
    public partial class CompraProduto
    {
        public string QuantidadeUnidade => $"{this.Quantidade} {this.Unidade}";
        public decimal Valor => this.PrecoUnitario * (decimal)this.Quantidade;
    }
}