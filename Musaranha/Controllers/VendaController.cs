using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Musaranha.Models;
using Musaranha.ViewModels;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class VendaController : Controller
    {
        // GET: Venda
        public ActionResult Index() => View(new VendaIndexViewModel {
            Clientes = Cliente.Listar().OrderBy(c=>c.Pessoa.Nome).ToList(),
            Produtos = Produto.Listar().OrderBy(p=>p.Descricao).ToList(),
            Vendas = Venda.Listar().OrderByDescending(v=>v.DtVenda).ToList()
        });

        // POST: venda/itens
        [HttpPost]
        public ActionResult Itens(int cod)
        {
            return PartialView("_Itens", Venda.ObterPorCodigo(cod));
        }

        // POST: venda/incluir
        [HttpPost]
        public ActionResult Incluir(FormCollection form)
        {
            if (form.HasKeys())
            {
                Venda venda = new Venda();

                venda.CodCliente = int.Parse(form["txtCliente"]);
                venda.DtVenda = DateTime.Parse(form["txtData"]);
                venda.Desconto = Decimal.Parse(form["txtDesconto"], new CultureInfo("pt-BR"));

                int n = 1;

                while (!StringExt.IsNullOrEmpty(form[$"txtProduto{n}"], form[$"txtUnidade{n}"], form[$"txtQuantidade{n}"], form[$"txtPrecoUnitario{n}"]))
                {
                    int produto = int.Parse(form[$"txtProduto{n}"]);
                    string unidade = form[$"txtUnidade{n}"];
                    double quantidade = Double.Parse(form[$"txtQuantidade{n}"], new CultureInfo("pt-BR"));
                    decimal precoUnitario = Decimal.Parse(form[$"txtPrecoUnitario{n}"], new CultureInfo("pt-BR"));

                    if(produto > 0 && !String.IsNullOrWhiteSpace(unidade) && quantidade > 0 && precoUnitario > 0)
                    {
                        venda.VendaProduto.Add(new VendaProduto {
                            CodProduto = produto,
                            Unidade = unidade,
                            Quantidade = quantidade,
                            PrecoUnitario = precoUnitario
                        });
                    }
                    n++;
                }

                if (venda.VendaProduto.Count > 0)
                {
                    Venda.Incluir(venda);
                }          

                return PartialView("_Lista", Venda.Listar());
            }
            return Json(false);
        }

        // POST: venda/carregareditar/5
        [HttpPost]
        public ActionResult CarregarEditar(int cod)
        {
            if (cod > 0)
            {
                VendaEditarViewModel model = new VendaEditarViewModel();
                model.Venda = Venda.ObterPorCodigo(cod);
                model.Clientes = Cliente.Listar().OrderBy(c => c.Pessoa.Nome).ToList();
                model.Produtos = Produto.Listar().OrderBy(p => p.Descricao).ToList();
                return PartialView("_Editar", model);
            }
            return Json(false);
        }

        // POST: venda/editar/5
        [HttpPost]
        public ActionResult Editar(int cod, FormCollection form)
        {
            if (cod > 0 && form.HasKeys())
            {
                Venda venda = Venda.ObterPorCodigo(cod);

                venda.CodCliente = int.Parse(form["txtCliente"]);
                venda.DtVenda = DateTime.Parse(form["txtData"]);
                venda.Desconto = Decimal.Parse(form["txtDesconto"], new CultureInfo("pt-BR"));

                int n = 1;

                venda.VendaProduto.Clear();

                while (!StringExt.IsNullOrEmpty(form[$"txtProduto{n}"], form[$"txtUnidade{n}"], form[$"txtQuantidade{n}"], form[$"txtPrecoUnitario{n}"]))
                {
                    int produto = int.Parse(form[$"txtProduto{n}"]);
                    string unidade = form[$"txtUnidade{n}"];
                    double quantidade = Double.Parse(form[$"txtQuantidade{n}"], new CultureInfo("pt-BR"));
                    decimal precoUnitario = Decimal.Parse(form[$"txtPrecoUnitario{n}"], new CultureInfo("pt-BR"));

                    if (produto > 0 && !String.IsNullOrWhiteSpace(unidade) && quantidade > 0 && precoUnitario > 0)
                    {
                        venda.VendaProduto.Add(new VendaProduto
                        {
                            CodProduto = produto,
                            Unidade = unidade,
                            Quantidade = quantidade,
                            PrecoUnitario = precoUnitario
                        });
                    }
                    n++;
                }
                
                Venda.Editar(venda);

                return PartialView("_Lista", Venda.Listar());
            }
            return Json(false);
        }

        // POST: venda/excluir/5
        [HttpPost]
        public ActionResult Excluir(int cod)
        {
            if (cod > 0)
            {
                Venda venda = Venda.ObterPorCodigo(cod);
                Venda.Excluir(venda);
                return PartialView("_Lista", Venda.Listar());
            }
            return Json(false);
        }
    }
}