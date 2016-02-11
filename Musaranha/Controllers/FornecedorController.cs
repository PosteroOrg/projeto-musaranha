using System;
using System.Linq;
using System.Web.Mvc;
using Musaranha.Models;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class FornecedorController : Controller
    {
        // GET: /fornecedor
        public ActionResult Index() => View(Fornecedor.Listar());

        // POST: /fornecedor/incluir
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
                int n = 1;
                while (!String.IsNullOrWhiteSpace(form[$"txtTelefone{n}"]))
                {
                    string numTelefone = form[$"txtTelefone{n}"].SomenteNumeros();
                    if (numTelefone.Length == 11 || numTelefone.Length == 10)
                    {
                        fornecedor.Pessoa.Telefone.Add(new Telefone { NumTelefone = numTelefone });
                    }
                    n++;
                }
                switch (fornecedor.Pessoa.Tipo)
                {
                    case "F":
                        fornecedor.Pessoa.CPF = form["txtCPFOuCNPJ"].SomenteNumeros() ?? null;
                        break;
                    case "J":
                        fornecedor.Pessoa.CNPJ = form["txtCPFOuCNPJ"].SomenteNumeros() ?? null;
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
                    fornecedor.Pessoa.Endereco.CEP = form["txtCEP"].SomenteNumeros();
                }

                Fornecedor.Incluir(fornecedor);

                return PartialView("_Lista", Fornecedor.Listar());
            }
            return Json(false);
        }

        // POST: /fornecedor/editar/5
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
                int n = 1;
                while (!String.IsNullOrWhiteSpace(form[$"txtTelefone{n}"]))
                {
                    fornecedor.Pessoa.Telefone.Add(new Telefone { NumTelefone = form[$"txtTelefone{n}"].SomenteNumeros() });
                    n++;
                }
                switch (fornecedor.Pessoa.Tipo)
                {
                    case "F":
                        fornecedor.Pessoa.CPF = form["txtCPFOuCNPJ"].SomenteNumeros() ?? null;
                        fornecedor.Pessoa.CNPJ = null;
                        break;
                    case "J":
                        fornecedor.Pessoa.CNPJ = form["txtCPFOuCNPJ"].SomenteNumeros() ?? null;
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
                    fornecedor.Pessoa.Endereco.CEP = form["txtCEP"].SomenteNumeros();
                }

                Fornecedor.Editar(fornecedor);

                return PartialView("_Lista", Fornecedor.Listar());
            }
            return Json(false);
        }

        // POST: /fornecedor/excluir/5
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

        // POST: /fornecedor/json/5
        [HttpPost]
        public ActionResult Json(int cod)
        {
            Fornecedor fornecedor = Fornecedor.ObterPorCodigo(cod);
            return Json(new
            {
                Nome = fornecedor.Pessoa.Nome,
                Telefones = fornecedor.Pessoa.Telefone.Select(t => t.NumTelefone),
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