using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musaranha.Models
{
    public partial class Compra
    {
        public decimal ValorTotal => this.CompraProduto.Sum(p => p.Valor) - (this.Desconto ?? 0);
        public decimal ValorTotalSemDesconto => this.CompraProduto.Sum(p => p.Valor);
        public Dictionary<string, double> QuantidadePorUnidade
        {
            get
            {
                Dictionary<string, double> retorno = new Dictionary<string, double>();

                foreach (CompraProduto compraProduto in this.CompraProduto)
                {
                    if (retorno.ContainsKey(compraProduto.Unidade))
                    {
                        retorno[compraProduto.Unidade] += compraProduto.Quantidade;
                    }
                    else
                    {
                        retorno[compraProduto.Unidade] = compraProduto.Quantidade;
                    }
                }

                return retorno;
            }
        }

        private static MusaranhaEntities c => Contexto.Current;

        public static List<Compra> Listar() => c.Compra.ToList();

        public static Compra ObterPorCodigo(int cod) => c.Compra.Find(cod);

        public static void Incluir(Compra compra)
        {
            c.Compra.Add(compra);
            c.SaveChanges();
        }

        public static void Editar(Compra compra)
        {
            if(compra.CompraProduto.Count > 0)
            {
                c.SaveChanges();
            }
            else
            {
                c.Dispose();
                Contexto.Current = new MusaranhaEntities();
            }
        }

        public static void Excluir(Compra compra)
        {
            Compra temp = ObterPorCodigo(compra.CodCompra);
            if (temp != null)
            {
                c.Compra.Remove(temp);
                c.SaveChanges();
            }
        }
    }
}