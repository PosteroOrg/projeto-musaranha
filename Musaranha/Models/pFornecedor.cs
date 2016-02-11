using System.Collections.Generic;
using System.Linq;

namespace Musaranha.Models
{
    public partial class Fornecedor
    {
        private static MusaranhaEntities c => Contexto.Current;

        public static void Incluir(Fornecedor fornecedor)
        {
            c.Fornecedor.Add(fornecedor);
            c.SaveChanges();
        }

        public static void Editar(Fornecedor fornecedor)
        {
            Fornecedor temp = ObterPorCodigo(fornecedor.CodPessoa);

            if (temp != null)
            {
                temp.Pessoa = fornecedor.Pessoa;
                c.SaveChanges();
            }
        }

        public static void Excluir(Fornecedor fornecedor)
        {
            Fornecedor temp = ObterPorCodigo(fornecedor.CodPessoa);
            if (temp != null)
            {
                c.Fornecedor.Remove(temp);
                c.SaveChanges();
            }
        }

        public static List<Fornecedor> Listar() => c.Fornecedor.ToList();
        public static Fornecedor ObterPorCodigo(int codPessoa) => c.Fornecedor.Find(codPessoa);
    }
}