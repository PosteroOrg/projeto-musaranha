using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Musaranha.Models;
using Musaranha.ViewModels;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class CompraController : Controller
    {
        // GET: /compra
        public ActionResult Index() => View(new CompraIndexViewModel
        {
            Fornecedores = Fornecedor.Listar().OrderBy(f => f.Pessoa.Nome).ToList(),
            Produtos = Produto.Listar().OrderBy(p => p.Descricao).ToList(),
            Compras = Compra.Listar().OrderByDescending(c => c.DtCompra).ToList()
        });

        // POST: /compra/listar
        [HttpPost]
        public ActionResult Listar(int fornecedor, int produto, string dataInicio, string dataTermino)
        {
            List<Compra> compras = Compra.Listar();

            if (fornecedor > 0)
            {
                compras = compras.Where(v => v.CodFornecedor == fornecedor).ToList();
            }

            if (produto > 0)
            {
                compras = compras.Where(v => v.CompraProduto.FirstOrDefault(vp => vp.CodProduto == produto) != null).ToList();
            }

            if (!String.IsNullOrWhiteSpace(dataInicio))
            {
                DateTime data = DateTime.Parse(dataInicio, new CultureInfo("pt-BR"));
                compras = compras.Where(v => v.DtCompra >= data).ToList();
            }

            if (!String.IsNullOrWhiteSpace(dataTermino))
            {
                DateTime data = DateTime.Parse(dataTermino + " 23:59:59", new CultureInfo("pt-BR"));
                compras = compras.Where(v => v.DtCompra <= data).ToList();
            }

            return PartialView("_Lista", compras.OrderByDescending(c => c.DtCompra).ToList());
        }

        // POST: /compra/itens
        [HttpPost]
        public ActionResult Itens(int cod)
        {
            return PartialView("_Itens", Compra.ObterPorCodigo(cod));
        }

        // POST: /compra/incluir
        [HttpPost]
        public ActionResult Incluir(FormCollection form)
        {
            if (form.HasKeys())
            {
                Compra compra = new Compra();

                compra.CodFornecedor = int.Parse(form["txtFornecedor"]);
                compra.DtCompra = DateTime.Parse(form["txtData"]);
                compra.Desconto = Decimal.Parse(form["txtDesconto"], new CultureInfo("pt-BR"));

                int n = 1;

                while (!StringExt.IsNullOrEmpty(form[$"txtProduto{n}"], form[$"txtUnidade{n}"], form[$"txtQuantidade{n}"], form[$"txtPrecoUnitario{n}"]))
                {
                    int produto = int.Parse(form[$"txtProduto{n}"]);
                    string unidade = form[$"txtUnidade{n}"];
                    double quantidade = Double.Parse(form[$"txtQuantidade{n}"], new CultureInfo("pt-BR"));
                    decimal precoUnitario = Decimal.Parse(form[$"txtPrecoUnitario{n}"], new CultureInfo("pt-BR"));

                    if (produto > 0 && !String.IsNullOrWhiteSpace(unidade) && quantidade > 0 && precoUnitario > 0)
                    {
                        compra.CompraProduto.Add(new CompraProduto
                        {
                            CodProduto = produto,
                            Unidade = unidade,
                            Quantidade = quantidade,
                            PrecoUnitario = precoUnitario
                        });
                    }
                    n++;
                }

                if (compra.CompraProduto.Count > 0)
                {
                    Compra.Incluir(compra);
                }

                return PartialView("_Lista", Compra.Listar());
            }
            return Json(false);
        }

        // POST: /compra/carregareditar/5
        [HttpPost]
        public ActionResult CarregarEditar(int cod)
        {
            if (cod > 0)
            {
                CompraEditarViewModel model = new CompraEditarViewModel();
                model.Compra = Compra.ObterPorCodigo(cod);
                model.Fornecedores = Fornecedor.Listar().OrderBy(c => c.Pessoa.Nome).ToList();
                model.Produtos = Produto.Listar().OrderBy(p => p.Descricao).ToList();
                return PartialView("_Editar", model);
            }
            return Json(false);
        }

        // POST: /compra/editar/5
        [HttpPost]
        public ActionResult Editar(int cod, FormCollection form)
        {
            if (cod > 0 && form.HasKeys())
            {
                Compra compra = Compra.ObterPorCodigo(cod);

                compra.CodFornecedor = int.Parse(form["txtFornecedor"]);
                compra.DtCompra = DateTime.Parse(form["txtData"]);
                compra.Desconto = Decimal.Parse(form["txtDesconto"], new CultureInfo("pt-BR"));

                int n = 1;

                compra.CompraProduto.Clear();

                while (!StringExt.IsNullOrEmpty(form[$"txtProduto{n}"], form[$"txtUnidade{n}"], form[$"txtQuantidade{n}"], form[$"txtPrecoUnitario{n}"]))
                {
                    int produto = int.Parse(form[$"txtProduto{n}"]);
                    string unidade = form[$"txtUnidade{n}"];
                    double quantidade = Double.Parse(form[$"txtQuantidade{n}"], new CultureInfo("pt-BR"));
                    decimal precoUnitario = Decimal.Parse(form[$"txtPrecoUnitario{n}"], new CultureInfo("pt-BR"));

                    if (produto > 0 && !String.IsNullOrWhiteSpace(unidade) && quantidade > 0 && precoUnitario > 0)
                    {
                        compra.CompraProduto.Add(new CompraProduto
                        {
                            CodProduto = produto,
                            Unidade = unidade,
                            Quantidade = quantidade,
                            PrecoUnitario = precoUnitario
                        });
                    }
                    n++;
                }

                Compra.Editar(compra);

                return PartialView("_Lista", Compra.Listar());
            }
            return Json(false);
        }

        // POST: /compra/excluir/5
        [HttpPost]
        public ActionResult Excluir(int cod)
        {
            if (cod > 0)
            {
                Compra compra = Compra.ObterPorCodigo(cod);
                Compra.Excluir(compra);
                return PartialView("_Lista", Compra.Listar());
            }
            return Json(false);
        }
    }
}