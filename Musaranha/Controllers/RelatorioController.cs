using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Musaranha.Models;
using Musaranha.ViewModels;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class RelatorioController : Controller
    {
        // GET: relatorio
        public ActionResult Index() => RedirectToAction("Index", "Painel");

        // GET: relatorio/vendas
        [HttpGet]
        public ActionResult Vendas() => View(new RelatorioVendasViewModel() {
           Clientes = Cliente.Listar().OrderBy(c=>c.Pessoa.Nome).ToList(),
           Produtos = Produto.Listar().OrderBy(p=>p.Descricao).ToList()
        });

        [HttpPost]
        public ActionResult Vendas(FormCollection form)
        {
            if (form.HasKeys())
            {
                int cliente = int.Parse(form["txtCliente"]);
                int produto = int.Parse(form["txtProduto"]);

                List<Venda> vendas = Venda.Listar();

                if (cliente > 0)
                {
                    vendas = vendas.Where(v => v.CodCliente == cliente).ToList();
                }

                if (produto > 0)
                {
                    vendas = vendas.Where(v => v.VendaProduto.FirstOrDefault(vp => vp.CodProduto == produto) != null).ToList();
                }

                if (!String.IsNullOrWhiteSpace(form["txtDataInicio"]))
                {
                    DateTime data = DateTime.Parse(form["txtDataInicio"]);
                    vendas = vendas.Where(v => v.DtVenda >= data).ToList();
                }

                if (!String.IsNullOrWhiteSpace(form["txtDataTermino"]))
                {
                    DateTime data = DateTime.Parse(form["txtDataTermino"] + " 23:59:59");
                    vendas = vendas.Where(v => v.DtVenda <= data).ToList();
                }

                vendas = vendas.OrderBy(v => v.DtVenda).ToList();

                // PDF

                MemoryStream memStream = new MemoryStream();
                Document document = new Document(PageSize.A4.Rotate(), 36, 36, 54, 36);
                PdfWriter writer = PdfWriter.GetInstance(document, memStream);
                writer.CloseStream = false;
                writer.PageEvent = new PaginacaoPdfPageEventHelper();

                document.Open();

                Paragraph titulo = new Paragraph("Relatório de Vendas");
                titulo.Font.Size = 18;
                titulo.Font.SetStyle(Font.BOLD);
                document.Add(titulo);

                document.Add(Chunk.NEWLINE);

                if (!String.IsNullOrWhiteSpace(form["txtDataInicio"]) || !String.IsNullOrWhiteSpace(form["txtDataTermino"]))
                {
                    string periodo = "";
                    if (!String.IsNullOrWhiteSpace(form["txtDataInicio"]) && !String.IsNullOrWhiteSpace(form["txtDataTermino"]))
                    {
                        periodo = $"de {form["txtDataInicio"]} até {form["txtDataTermino"]}";
                    }
                    else if (!String.IsNullOrWhiteSpace(form["txtDataInicio"]))
                    {
                        periodo = $"de {form["txtDataInicio"]} até {DateTime.Today.ToString("dd/MM/yyyy")}";
                    }
                    else if (!String.IsNullOrWhiteSpace(form["txtDataTermino"]))
                    {
                        periodo = $"até {form["txtDataTermino"]}";
                    }
                    Paragraph filtroPeriodo = new Paragraph($"Período: {periodo}");
                    filtroPeriodo.Font.SetStyle(Font.BOLD);
                    document.Add(filtroPeriodo);
                }

                string descricao = "Todos";
                if (produto > 0)
                {
                    descricao = Produto.ObterPorCodigo(produto).Descricao;
                }
                Paragraph filtroProduto = new Paragraph($"Produto: {descricao}");
                filtroProduto.Font.SetStyle(Font.BOLD);
                document.Add(filtroProduto);

                string nome = "Todos";
                if (cliente > 0)
                {
                    nome = Cliente.ObterPorCodigo(cliente).Pessoa.Nome;
                }
                Paragraph filtroCliente = new Paragraph($"Cliente: {nome}");
                filtroCliente.Font.SetStyle(Font.BOLD);
                document.Add(filtroCliente);

                document.Add(Chunk.NEWLINE);

                PdfPTable tabela = new PdfPTable(6);
                tabela.SetWidths(new float[] { 1, 1, 1, 1, 1, 1 });
                tabela.TotalWidth = document.PageSize.Width - document.RightMargin - document.LeftMargin;
                tabela.LockedWidth = true;
                tabela.DefaultCell.MinimumHeight = 20;
                tabela.DefaultCell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                tabela.DefaultCell.BorderWidthBottom = 2;
                Font fontBold = new Font();
                fontBold.SetStyle(Font.BOLD);
                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabela.AddCell(new Phrase("Data", fontBold));
                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tabela.AddCell(new Phrase("Cliente", fontBold));
                tabela.AddCell(new Phrase("Produto", fontBold));
                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabela.AddCell(new Phrase("Quantidade", fontBold));
                tabela.AddCell(new Phrase("Preço Unitário", fontBold));
                tabela.AddCell(new Phrase("Valor Total", fontBold));
                tabela.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                tabela.DefaultCell.BorderWidthBottom = 1;

                foreach (Venda venda in vendas)
                {
                    foreach (VendaProduto vendaProduto in venda.VendaProduto)
                    {
                        if (produto > 0 && vendaProduto.CodProduto != produto)
                        {
                            continue;
                        }
                        tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabela.AddCell(venda.DtVenda.ToString("dd/MM/yyyy"));
                        tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        tabela.AddCell(venda.Cliente.Pessoa.Nome);
                        tabela.AddCell(vendaProduto.Produto.Descricao);
                        tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        tabela.AddCell(vendaProduto.QuantidadeUnidade);
                        tabela.AddCell(vendaProduto.PrecoUnitario.ToString("C", new System.Globalization.CultureInfo("pt-BR")));
                        tabela.AddCell(vendaProduto.Valor.ToString("C", new System.Globalization.CultureInfo("pt-BR")));
                    }
                }

                document.Add(tabela);

                document.Add(Chunk.NEWLINE);

                PdfPTable tabelaTotais = new PdfPTable(2);
                tabelaTotais.SetWidths(new float[] { 1, 1 });
                tabelaTotais.TotalWidth = document.PageSize.Width - document.RightMargin - document.LeftMargin;
                tabelaTotais.LockedWidth = true;
                tabelaTotais.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                tabelaTotais.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                PdfPCell totais = new PdfPCell(new Phrase("TOTAIS", fontBold));
                totais.Colspan = 2;
                totais.Border = Rectangle.BOTTOM_BORDER;
                totais.HorizontalAlignment = Element.ALIGN_CENTER;
                tabelaTotais.AddCell(totais);
                tabelaTotais.AddCell(new Phrase("Quantidades", fontBold));
                tabelaTotais.AddCell(new Phrase("Valores", fontBold));

                PdfPTable tabelaQuantidades = new PdfPTable(1);
                tabelaQuantidades.SetWidths(new float[] { 1 });
                tabelaQuantidades.TotalWidth = (document.PageSize.Width - document.RightMargin - document.LeftMargin) / 2;
                tabelaQuantidades.LockedWidth = true;
                tabelaQuantidades.DefaultCell.Border = Rectangle.NO_BORDER;
                tabelaQuantidades.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                Dictionary<string, double> totaisQuantidadePorUnidade = new Dictionary<string, double>();
                foreach (Venda venda in vendas)
                {
                    foreach (string unidade in venda.QuantidadePorUnidade.Keys)
                    {
                        if (totaisQuantidadePorUnidade.ContainsKey(unidade))
                        {
                            totaisQuantidadePorUnidade[unidade] += venda.QuantidadePorUnidade[unidade];
                        }
                        else
                        {
                            totaisQuantidadePorUnidade[unidade] = venda.QuantidadePorUnidade[unidade];
                        }
                    }
                }
                foreach (string unidade in totaisQuantidadePorUnidade.Keys)
                {
                    tabelaQuantidades.AddCell($"{totaisQuantidadePorUnidade[unidade]} {unidade}\n");
                }
                tabelaTotais.AddCell(tabelaQuantidades);

                PdfPTable tabelaValores = new PdfPTable(2);
                tabelaValores.SetWidths(new float[] { 3, 1 });
                tabelaValores.TotalWidth = (document.PageSize.Width - document.RightMargin - document.LeftMargin)/2;
                tabelaValores.LockedWidth = true;
                tabelaValores.DefaultCell.Border = Rectangle.NO_BORDER;
                tabelaValores.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                tabelaValores.AddCell(new Phrase("Desconto Total:", fontBold));
                tabelaValores.AddCell(new Phrase(vendas.Sum(v => v.Desconto)?.ToString("C", new System.Globalization.CultureInfo("pt-BR")) ?? "R$ 0,00"));

                tabelaValores.AddCell(new Phrase("Valor Total (com desconto):", fontBold));
                tabelaValores.AddCell(new Phrase(vendas.Sum(v => v.ValorTotal).ToString("C", new System.Globalization.CultureInfo("pt-BR"))));

                tabelaValores.AddCell(new Phrase("Valor Total (sem desconto):", fontBold));
                tabelaValores.AddCell(new Phrase(vendas.Sum(v => v.ValorTotalSemDesconto).ToString("C", new System.Globalization.CultureInfo("pt-BR"))));

                tabelaTotais.AddCell(tabelaValores);

                document.Add(tabelaTotais);

                document.Close();

                Response.AddHeader("Content-Disposition", "attachment; filename=\"relatorio-vendas-"+ DateTime.Today.ToString("dd-MM-yyyy") +".pdf\"");

                byte[] buf = new byte[memStream.Position];
                memStream.Position = 0;
                memStream.Read(buf, 0, buf.Length);

                return new BinaryContentResult(buf, "application/pdf");
            }

            return RedirectToAction("Vendas");
        }
        // GET: relatorio/compras
        public ActionResult Compras() => View();
    }
}