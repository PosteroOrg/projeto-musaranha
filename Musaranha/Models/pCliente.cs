using System.Collections.Generic;
using System.Linq;

namespace Musaranha.Models
{
    public partial class Cliente
    {
        private static MusaranhaEntities c => Contexto.Current;

        public static void Incluir(Cliente cliente)
        {
            c.Cliente.Add(cliente);
            c.SaveChanges();
        }

        public static void Editar(Cliente cliente)
        {
            Cliente temp = ObterPorCodigo(cliente.CodPessoa);

            if (temp != null)
            {
                temp.Pessoa = cliente.Pessoa;
                c.SaveChanges();
            }
        }

        public static void Excluir(Cliente cliente)
        {
            Cliente temp = ObterPorCodigo(cliente.CodPessoa);
            if (temp != null)
            {
                c.Cliente.Remove(temp);
                c.SaveChanges();
            }
        }

        public static List<Cliente> Listar() => c.Cliente.ToList();
        public static Cliente ObterPorCodigo(int codPessoa) => c.Cliente.Find(codPessoa);
    }
}