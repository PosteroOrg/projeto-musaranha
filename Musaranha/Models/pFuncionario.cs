using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musaranha.Models
{
    public partial class Funcionario
    {
        public string GetCategoria
        {
            get
            {
                switch (this.Categoria)
                {
                    case "M":
                        return "Motorista";
                    case "T":
                        return "Técnico";
                    default:
                        return String.Empty;
                }
            }
        }

        private static MusaranhaEntities c => Contexto.Current;

        public static void Incluir(Funcionario funcionario)
        {
            c.Funcionario.Add(funcionario);
            c.SaveChanges();
        }

        public static void Editar(Funcionario funcionario)
        {
            Funcionario temp = c.Funcionario.Find(funcionario.CodPessoa);

            if (temp != null)
            {
                /* Dados Pessoais */
                temp.Pessoa.Nome = funcionario.Pessoa.Nome;
                temp.Pessoa.Telefone = funcionario.Pessoa.Telefone;

                /* Funcionario */
                temp.NumIdentidade = funcionario.NumIdentidade;
                temp.NumCarteiraTrabalho = funcionario.NumCarteiraTrabalho;
                temp.Salario = funcionario.Salario;
                temp.Categoria = funcionario.Categoria;
                temp.Observacao = funcionario.Observacao;

                c.SaveChanges();
            }
        }

        public static void Excluir(Funcionario funcionario)
        {
            Funcionario temp = c.Funcionario.Find(funcionario.CodPessoa);

            if (temp != null)
            {
                c.Funcionario.Remove(temp);
                c.SaveChanges();
            }
        }

        public static List<Funcionario> Listar()
        {
            return c.Funcionario.ToList();
        }

        public static Funcionario ObterPorCodigo(int codPessoa)
        {
            return c.Funcionario.Find(codPessoa);
        }
        
    }
}