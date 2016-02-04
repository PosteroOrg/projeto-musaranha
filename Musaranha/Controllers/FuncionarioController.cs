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
            List<Funcionario> funcionarios = Funcionario.Listar();
            return View(funcionarios);
        }

        //POST: funcionario/Incluir
        [HttpPost]
        public ActionResult Incluir(FormCollection form)
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
                funcionario.Salario = Decimal.Parse(form["txtSalario"].Replace(".", "").Replace(",", "."));
                funcionario.Categoria = form["txtCategoria"];
                funcionario.Observacao = form["txtObservacao"];

                Funcionario.Incluir(funcionario);

                return PartialView("_Lista",Funcionario.Listar());
            }
            return Json(false);
        }

        //POST: funcionario/Editar
        [HttpPost]
        public ActionResult Editar(int cod,FormCollection form)
        {
            if (cod != 0)
            {
                Funcionario funcionario = Funcionario.ObterPorCodigo(cod);

                /* Dados Pessoais */
                funcionario.Pessoa.Nome = form["txtNome"];
                funcionario.Pessoa.Telefone.Clear();
                funcionario.Pessoa.Telefone.Add(new Telefone { NumTelefone = form["txtTelefone"] });


                /* Funcionario */
                funcionario.NumIdentidade = form["txtIdentidade"];
                funcionario.NumCarteiraTrabalho = form["txtCarteiraTrabalho"];
                funcionario.Salario = Decimal.Parse(form["txtSalario"].Replace(".", "").Replace(",", "."));
                funcionario.Categoria = form["txtCategoria"];
                funcionario.Observacao = form["txtObservacao"];

                Funcionario.Editar(funcionario);

                return PartialView("_Lista", Funcionario.Listar());
            }
            return Json(false);
        }

        //POST: funcionario/Excluir
        [HttpPost]
        public ActionResult Excluir(int cod)
        {
            if (cod != 0)
            {
                Funcionario funcionario = Funcionario.ObterPorCodigo(cod);
                
                Funcionario.Excluir(funcionario);

                return PartialView("_Lista", Funcionario.Listar());
            }
            return Json(false);
        }

        //GET: funcionario/pagamento
        public ActionResult Pagamento(int? funcionario) => View();

    }
}
