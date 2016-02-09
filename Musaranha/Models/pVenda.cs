using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musaranha.Models
{
    public partial class Venda
    {
        public decimal ValorTotal => this.VendaProduto.Sum(p => p.Valor) - (this.Desconto ?? 0);

        private static MusaranhaEntities c => Contexto.Current;

        public static List<Venda> Listar() => c.Venda.ToList();

        public static Venda ObterPorCodigo(int cod) => c.Venda.Find(cod);
    }
}