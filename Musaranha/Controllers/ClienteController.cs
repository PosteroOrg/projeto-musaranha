using Musaranha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Musaranha.Controllers
{
    public class ClienteController : Controller
    {
        // GET: cliente
        public ActionResult Index() => View(Cliente.Listar());

        // POST: cliente/incluir
        [HttpPost]
        public ActionResult Incluir(FormCollection form)
        {
            if (form.HasKeys())
            {
                Cliente cliente = new Cliente();

                /* Dados Pessoais */
                cliente.Pessoa = new Pessoa();
                cliente.Pessoa.Tipo = form["txtTipo"] ?? "N";
                cliente.Pessoa.Nome = form["txtNome"];
                cliente.Pessoa.Telefone.Add(new Telefone { NumTelefone = form["txtTelefone"] });
                switch (cliente.Pessoa.Tipo)
                {
                    case "F":
                        cliente.Pessoa.CPF = form["txtCPFOuCNPJ"] ?? null;
                        break;
                    case "J":
                        cliente.Pessoa.CNPJ = form["txtCPFOuCNPJ"] ?? null;
                        break;
                    default:
                        break;
                }
                cliente.Pessoa.Email = form["txtEmail"] ?? null;

                /* Endereço */
                if (!String.IsNullOrWhiteSpace(form["txtLogradouro"]))
                {
                    cliente.Pessoa.Endereco = new Endereco();
                    cliente.Pessoa.Endereco.Logradouro = form["txtLogradouro"];
                    cliente.Pessoa.Endereco.Numero = form["txtNumero"];
                    cliente.Pessoa.Endereco.Complemento = form["txtComplemento"];
                    cliente.Pessoa.Endereco.Bairro = form["txtBairro"];
                    cliente.Pessoa.Endereco.Cidade = form["txtCidade"];
                    cliente.Pessoa.Endereco.Estado = form["txtEstado"];
                    cliente.Pessoa.Endereco.CEP = form["txtCEP"];
                }

                Cliente.Incluir(cliente);

                return PartialView("_Lista", Cliente.Listar());
            }
            return Json(false);
        }

        // POST: cliente/editar/5
        [HttpPost]
        public ActionResult Editar(int cod, FormCollection form)
        {
            if (cod > 0 && form.HasKeys())
            {
                Cliente cliente = Cliente.ObterPorCodigo(cod);

                /* Dados Pessoais */
                cliente.Pessoa.Tipo = form["txtTipo"] ?? "N";
                cliente.Pessoa.Nome = form["txtNome"];
                cliente.Pessoa.Telefone.Clear();
                cliente.Pessoa.Telefone.Add(new Telefone { NumTelefone = form["txtTelefone"] });
                switch (cliente.Pessoa.Tipo)
                {
                    case "F":
                        cliente.Pessoa.CPF = form["txtCPFOuCNPJ"] ?? null;
                        cliente.Pessoa.CNPJ = null;
                        break;
                    case "J":
                        cliente.Pessoa.CNPJ = form["txtCPFOuCNPJ"] ?? null;
                        cliente.Pessoa.CPF = null;
                        break;
                    default:
                        break;
                }
                cliente.Pessoa.Email = form["txtEmail"] ?? null;

                /* Endereço */
                if (!String.IsNullOrWhiteSpace(form["txtLogradouro"]))
                {
                    if (cliente.Pessoa.Endereco == null)
                        cliente.Pessoa.Endereco = new Endereco();
                    cliente.Pessoa.Endereco.Logradouro = form["txtLogradouro"];
                    cliente.Pessoa.Endereco.Numero = form["txtNumero"];
                    cliente.Pessoa.Endereco.Complemento = form["txtComplemento"] ?? null;
                    cliente.Pessoa.Endereco.Bairro = form["txtBairro"];
                    cliente.Pessoa.Endereco.Cidade = form["txtCidade"];
                    cliente.Pessoa.Endereco.Estado = form["txtEstado"];
                    cliente.Pessoa.Endereco.CEP = form["txtCEP"];
                }
                else
                {
                    if (cliente.Pessoa.Endereco != null)
                        Contexto.Current.Endereco.Remove(cliente.Pessoa.Endereco);
                    cliente.Pessoa.Endereco = null;
                }

                Cliente.Editar(cliente);

                return PartialView("_Lista", Cliente.Listar());
            }
            return Json(false);
        }

        // POST: cliente/excluir/5
        [HttpPost]
        public ActionResult Excluir(int cod)
        {
            if (cod != 0)
            {
                Cliente cliente = Cliente.ObterPorCodigo(cod);

                Cliente.Excluir(cliente);

                return PartialView("_Lista", Cliente.Listar());
            }
            return Json(false);
        }

        // POST: cliente/json/5
        [HttpPost]
        public ActionResult Json(int cod)
        {
            Cliente cliente = Cliente.ObterPorCodigo(cod);
            return Json(new
            {
                Nome = cliente.Pessoa.Nome,
                Telefone = cliente.Pessoa.Telefone.First()?.NumTelefone,
                Logradouro = cliente.Pessoa.Endereco?.Logradouro ?? "",
                Numero = cliente.Pessoa.Endereco?.Numero ?? "",
                Complemento = cliente.Pessoa.Endereco?.Complemento ?? "",
                CEP = cliente.Pessoa.Endereco?.CEP ?? "",
                Bairro = cliente.Pessoa.Endereco?.Bairro ?? "",
                Cidade = cliente.Pessoa.Endereco?.Cidade ?? "",
                Estado = cliente.Pessoa.Endereco?.Estado ?? "",
                Tipo = cliente.Pessoa.Tipo ?? "",
                CPF = cliente.Pessoa.CPF ?? "",
                CNPJ = cliente.Pessoa.CNPJ ?? "",
                Email = cliente.Pessoa.Email ?? ""
            });
        }
    }
}