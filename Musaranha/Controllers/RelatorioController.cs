using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class RelatorioController : Controller
    {
        // GET: relatorio
        public ActionResult Index() => RedirectToAction("Index", "Painel");

        // GET: relatorio/vendas
        public ActionResult Vendas() => View();

        // GET: relatorio/compras
        public ActionResult Compras() => View();
    }
}