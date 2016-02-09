using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Musaranha.Models;

namespace Musaranha.ViewModels
{
    public class FuncionarioReciboViewModel
    {
        public Funcionario Funcionario { get; set; }
        public List<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }
    }
}