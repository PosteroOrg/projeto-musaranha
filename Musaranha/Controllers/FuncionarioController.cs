using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using Musaranha.Models;
using Musaranha.ViewModels;

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

        // POST: funcionario/Incluir
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
                funcionario.Pessoa.Telefone.Add(new Telefone { NumTelefone = form["txtTelefone"].SomenteNumeros() });


                /* Funcionario */
                funcionario.NumIdentidade = form["txtIdentidade"].SomenteNumeros();
                funcionario.NumCarteiraTrabalho = form["txtCarteiraTrabalho"].SomenteNumeros();
                funcionario.Salario = Decimal.Parse(form["txtSalario"], new CultureInfo("pt-BR"));
                funcionario.Categoria = form["txtCategoria"];
                funcionario.Observacao = form["txtObservacao"];

                Funcionario.Incluir(funcionario);

                return PartialView("_Lista", Funcionario.Listar());
            }
            return Json(false);
        }

        // POST: funcionario/Editar
        [HttpPost]
        public ActionResult Editar(int cod, FormCollection form)
        {
            if (cod != 0)
            {
                Funcionario funcionario = Funcionario.ObterPorCodigo(cod);

                /* Dados Pessoais */
                funcionario.Pessoa.Nome = form["txtNome"];
                funcionario.Pessoa.Telefone.Clear();
                funcionario.Pessoa.Telefone.Add(new Telefone { NumTelefone = form["txtTelefone"].SomenteNumeros() });

                /* Funcionario */
                funcionario.NumIdentidade = form["txtIdentidade"].SomenteNumeros();
                funcionario.NumCarteiraTrabalho = form["txtCarteiraTrabalho"].SomenteNumeros();
                funcionario.Salario = Decimal.Parse(form["txtSalario"], new CultureInfo("pt-BR"));
                funcionario.Categoria = form["txtCategoria"];
                funcionario.Observacao = form["txtObservacao"];

                Funcionario.Editar(funcionario);

                return PartialView("_Lista", Funcionario.Listar());
            }
            return Json(false);
        }

        // POST: funcionario/Excluir
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

        // GET: funcionario/pagamento
        public ActionResult Pagamento()
        {
            FuncionarioPagamentoViewModel model = new FuncionarioPagamentoViewModel();
            model.Funcionarios = Funcionario.Listar().OrderBy(f => f.Pessoa.Nome).ToList();
            return View(model);
        }

        // POST: funcionario/carregarpagamentos
        [HttpPost]
        public ActionResult CarregarPagamentos(int codigo, int mes, int ano)
        {
            Funcionario funcionario = Funcionario.ObterPorCodigo(codigo);
            return PartialView("_Pagamentos", funcionario.Pagamento.Where(p => p.AnoReferencia == ano && p.MesReferencia == mes).ToList());
        }

        // POST: funcioanario/incluirpagamento
        [HttpPost]
        public ActionResult IncluirPagamento(int codigo, string data, int mes, int ano, string valor)
        {
            Funcionario funcionario = Funcionario.ObterPorCodigo(codigo);
            funcionario.Pagamento.Add(new Pagamento {
                DtPagamento = DateTime.Parse(data),
                MesReferencia = mes,
                AnoReferencia = ano,
                Valor = Decimal.Parse(valor, new CultureInfo("pt-BR"))
            });
            Contexto.Current.SaveChanges();
            return PartialView("_Pagamentos", funcionario.Pagamento.Where(p => p.AnoReferencia == ano && p.MesReferencia == mes).ToList());
        }

        // POST: funcioanario/editarpagamento
        [HttpPost]
        public ActionResult EditarPagamento(int codigo, int pagamento, string data, int mes, int ano, string valor)
        {
            Funcionario funcionario = Funcionario.ObterPorCodigo(codigo);
            Pagamento temp = funcionario.Pagamento.FirstOrDefault(p => p.CodPagamento == pagamento);
            temp.DtPagamento = DateTime.Parse(data);
            temp.MesReferencia = mes;
            temp.AnoReferencia = ano;
            temp.Valor = Decimal.Parse(valor, new CultureInfo("pt-BR"));
            Contexto.Current.SaveChanges();
            return PartialView("_Pagamentos", funcionario.Pagamento.Where(p => p.AnoReferencia == ano && p.MesReferencia == mes).ToList());
        }

        // POST: funcioanario/excluirpagamento
        [HttpPost]
        public ActionResult ExcluirPagamento(int codigo, int pagamento, int ano, int mes)
        {
            Funcionario funcionario = Funcionario.ObterPorCodigo(codigo);
            Pagamento temp = funcionario.Pagamento.FirstOrDefault(p => p.CodPagamento == pagamento);
            Contexto.Current.Pagamento.Remove(temp);
            Contexto.Current.SaveChanges();
            return PartialView("_Pagamentos", funcionario.Pagamento.Where(p => p.AnoReferencia == ano && p.MesReferencia == mes).ToList());
        }

        // GET: funcionario/recibo/5?ano=2015&mes=5
        public ActionResult Recibo(int cod, int ano, int mes)
        {
            FuncionarioReciboViewModel model = new FuncionarioReciboViewModel();
            model.Funcionario = Funcionario.ObterPorCodigo(cod);
            model.Pagamentos = model.Funcionario.Pagamento.Where(p => p.AnoReferencia == ano && p.MesReferencia == mes).ToList();
            model.AnoReferencia = ano;
            model.MesReferencia = mes;
            Response.AddHeader("Content-Disposition", "attachment; filename=\"recibo-"+model.Funcionario.Pessoa.Nome.Split().First().ToLower()+"-"+mes+"-"+ano+".pdf\"");
            return new MvcRazorToPdf.PdfActionResult("Recibo", model, (writer, document) =>
            {
                document.SetPageSize(PageSize.A4);
                document.NewPage();
            });
        }
    }
}
