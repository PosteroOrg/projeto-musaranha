using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Musaranha.Models;

namespace Musaranha.ViewModels
{
    public class FuncionarioPagamentoViewModel
    {
        public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
    }
}