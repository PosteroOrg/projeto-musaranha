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

        public static void Incluir(Venda venda)
        {
            c.Venda.Add(venda);
            c.SaveChanges();
        }

        public static void Editar(Venda venda)
        {
            if(venda.VendaProduto.Count > 0)
            {
                c.SaveChanges();
            }
            else
            {
                c.Dispose();
                Contexto.Current = new MusaranhaEntities();
            }
        }

        public static void Excluir(Venda venda)
        {
            Venda temp = ObterPorCodigo(venda.CodVenda);
            if (temp != null)
            {
                c.Venda.Remove(temp);
                c.SaveChanges();
            }
        }
    }
}