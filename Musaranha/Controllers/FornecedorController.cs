using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Musaranha.Models;

namespace Musaranha.Controllers
{
    public class FornecedorController : Controller
    {
        // GET: fornecedor
        public ActionResult Index() => View(Fornecedor.Listar());

        // POST: fornecedor/incluir
        [HttpPost]
        public ActionResult Incluir(FormCollection form)
        {
            if (form.HasKeys())
            {
                Fornecedor fornecedor = new Fornecedor();

                /* Dados Pessoais */
                fornecedor.Pessoa = new Pessoa();
                fornecedor.Pessoa.Tipo = form["txtTipo"] ?? "N";
                fornecedor.Pessoa.Nome = form["txtNome"];
                fornecedor.Pessoa.Telefone.Add(new Telefone { NumTelefone = form["txtTelefone"] });
                switch (fornecedor.Pessoa.Tipo)
                {
                    case "F":
                        fornecedor.Pessoa.CPF = form["txtCPFOuCNPJ"] ?? null;
                        break;
                    case "J":
                        fornecedor.Pessoa.CNPJ = form["txtCPFOuCNPJ"] ?? null;
                        break;
                    default:
                        break;
                }
                fornecedor.Pessoa.Email = form["txtEmail"] ?? null;

                /* Endereço */
                if (!String.IsNullOrWhiteSpace(form["txtLogradouro"]))
                {
                    fornecedor.Pessoa.Endereco = new Endereco();
                    fornecedor.Pessoa.Endereco.Logradouro = form["txtLogradouro"];
                    fornecedor.Pessoa.Endereco.Numero = form["txtNumero"];
                    fornecedor.Pessoa.Endereco.Complemento = form["txtComplemento"];
                    fornecedor.Pessoa.Endereco.Bairro = form["txtBairro"];
                    fornecedor.Pessoa.Endereco.Cidade = form["txtCidade"];
                    fornecedor.Pessoa.Endereco.Estado = form["txtEstado"];
                    fornecedor.Pessoa.Endereco.CEP = form["txtCEP"];
                }

                Fornecedor.Incluir(fornecedor);

                return PartialView("_Lista", Fornecedor.Listar());
            }
            return Json(false);
        }

        // POST: fornecedor/editar/5
        [HttpPost]
        public ActionResult Editar(int cod, FormCollection form)
        {
            if (cod > 0 && form.HasKeys())
            {
                Fornecedor fornecedor = Fornecedor.ObterPorCodigo(cod);

                /* Dados Pessoais */
                fornecedor.Pessoa.Tipo = form["txtTipo"] ?? "N";
                fornecedor.Pessoa.Nome = form["txtNome"];
                fornecedor.Pessoa.Telefone.Clear();
                fornecedor.Pessoa.Telefone.Add(new Telefone { NumTelefone = form["txtTelefone"] });
                switch (fornecedor.Pessoa.Tipo)
                {
                    case "F":
                        fornecedor.Pessoa.CPF = form["txtCPFOuCNPJ"] ?? null;
                        fornecedor.Pessoa.CNPJ = null;
                        break;
                    case "J":
                        fornecedor.Pessoa.CNPJ = form["txtCPFOuCNPJ"] ?? null;
                        fornecedor.Pessoa.CPF = null;
                        break;
                    default:
                        break;
                }
                fornecedor.Pessoa.Email = form["txtEmail"] ?? null;

                /* Endereço */
                if (!String.IsNullOrWhiteSpace(form["txtLogradouro"]))
                {
                    fornecedor.Pessoa.Endereco = new Endereco();
                    fornecedor.Pessoa.Endereco.Logradouro = form["txtLogradouro"];
                    fornecedor.Pessoa.Endereco.Numero = form["txtNumero"];
                    fornecedor.Pessoa.Endereco.Complemento = form["txtComplemento"];
                    fornecedor.Pessoa.Endereco.Bairro = form["txtBairro"];
                    fornecedor.Pessoa.Endereco.Cidade = form["txtCidade"];
                    fornecedor.Pessoa.Endereco.Estado = form["txtEstado"];
                    fornecedor.Pessoa.Endereco.CEP = form["txtCEP"];
                }

                Fornecedor.Editar(fornecedor);

                return PartialView("_Lista", Fornecedor.Listar());
            }
            return Json(false);
        }

        // POST: fornecedor/excluir/5
        [HttpPost]
        public ActionResult Excluir(int cod)
        {
            if (cod != 0)
            {
                Fornecedor fornecedor = Fornecedor.ObterPorCodigo(cod);

                Fornecedor.Excluir(fornecedor);

                return PartialView("_Lista", Fornecedor.Listar());
            }
            return Json(false);
        }

        // POST: fornecedor/json/5
        [HttpPost]
        public ActionResult Json(int cod)
        {
            Fornecedor fornecedor = Fornecedor.ObterPorCodigo(cod);
            return Json(new
            {
                Nome = fornecedor.Pessoa.Nome,
                Telefone = fornecedor.Pessoa.Telefone.First()?.NumTelefone,
                Logradouro = fornecedor.Pessoa.Endereco?.Logradouro ?? "",
                Numero = fornecedor.Pessoa.Endereco?.Numero ?? "",
                Complemento = fornecedor.Pessoa.Endereco?.Complemento ?? "",
                CEP = fornecedor.Pessoa.Endereco?.CEP ?? "",
                Bairro = fornecedor.Pessoa.Endereco?.Bairro ?? "",
                Cidade = fornecedor.Pessoa.Endereco?.Cidade ?? "",
                Estado = fornecedor.Pessoa.Endereco?.Estado ?? "",
                Tipo = fornecedor.Pessoa.Tipo ?? "",
                CPF = fornecedor.Pessoa.CPF ?? "",
                CNPJ = fornecedor.Pessoa.CNPJ ?? "",
                Email = fornecedor.Pessoa.Email ?? ""
            });
        }
    }
}