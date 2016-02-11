using System.Collections.Generic;
using System.Linq;

namespace Musaranha.Models
{
    public partial class Venda
    {
        public decimal ValorTotal => this.VendaProduto.Sum(p => p.Valor) - (this.Desconto ?? 0);
        public decimal ValorTotalSemDesconto => this.VendaProduto.Sum(p => p.Valor);
        public Dictionary<string, double> QuantidadePorUnidade
        {
            get
            {
                Dictionary<string, double> retorno = new Dictionary<string, double>();

                foreach (VendaProduto vendaProduto in this.VendaProduto)
                {
                    if (retorno.ContainsKey(vendaProduto.Unidade))
                    {
                        retorno[vendaProduto.Unidade] += vendaProduto.Quantidade;
                    }
                    else
                    {
                        retorno[vendaProduto.Unidade] = vendaProduto.Quantidade;
                    }
                }

                return retorno;
            }
        }

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
            if (venda.VendaProduto.Count > 0)
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