using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musaranha.Models
{
    public partial class Funcionario
    {
        private static MusaranhaEntities c => Contexto.Current;

        public static void Inserir(Funcionario funcionario)
        {
            c.Funcionario.Add(funcionario);
            c.SaveChanges();
        }

        public static List<Funcionario> Listar()
        {
            return c.Funcionario.ToList();
        }
    }
}