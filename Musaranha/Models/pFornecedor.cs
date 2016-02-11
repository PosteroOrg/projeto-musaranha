using System.Collections.Generic;
using System.Linq;

namespace Musaranha.Models
{
    public partial class Fornecedor
    {
        public string Telefone => this.Pessoa.Telefone.Count > 1 ? $"{this.Pessoa.Telefone.First().NumTelefone.MaskTelefone()} +{this.Pessoa.Telefone.Count - 1}" : $"{this.Pessoa.Telefone.FirstOrDefault()?.NumTelefone.MaskTelefone()}";

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