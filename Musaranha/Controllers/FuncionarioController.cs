using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Musaranha.Models;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public ActionResult Index()
        {
            return View();
        }

        //POST: funcionario/Cadastrar
        [HttpPost]
        public ActionResult Cadastrar(FormCollection form)
        {
            if (form.HasKeys())
            {
                Funcionario funcionario = new Funcionario();

                /* Dados Pessoais */
                funcionario.Pessoa = new Pessoa();
                funcionario.Pessoa.Tipo = "F";
                funcionario.Pessoa.Nome = form["txtNome"];
                funcionario.Pessoa.Telefone.Add(new Telefone { NumTelefone = form["txtTelefone"] });
                

                /* Funcionario */
                funcionario.NumIdentidade = form["txtIdentidade"];
                funcionario.NumCarteiraTrabalho = form["txtCarteiraTrabalho"];
                funcionario.Salario = Decimal.Parse(form["txtSalario"]);
                funcionario.Categoria = form["txtCategoria"];
                funcionario.Observacao = form["txtObservacao"];

                Funcionario.Inserir(funcionario);

                return Json(funcionario);
            }
            return Json(false);
        }
    }
}