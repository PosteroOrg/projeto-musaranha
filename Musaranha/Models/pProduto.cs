using System.Collections.Generic;
using System.Linq;

namespace Musaranha.Models
{
    public partial class Produto
    {
        private static MusaranhaEntities c => Contexto.Current;

        public static void Incluir(Produto produto)
        {
            c.Produto.Add(produto);
            c.SaveChanges();
        }

        public static void Editar(Produto produto)
        {
            Produto temp = ObterPorCodigo(produto.CodProduto);

            if (temp != null)
            {
                temp.Descricao = produto.Descricao;
                c.SaveChanges();
            }
        }

        public static void Excluir(Produto produto)
        {
            Produto temp = ObterPorCodigo(produto.CodProduto);
            if (temp != null)
            {
                c.Produto.Remove(temp);
                c.SaveChanges();
            }
        }

        public static List<Produto> Listar() => c.Produto.ToList();
        public static Produto ObterPorCodigo(int codigo) => c.Produto.Find(codigo);
    }
}