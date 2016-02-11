using System.Web.Mvc;
using Musaranha.Models;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class ProdutoController : Controller
    {
        // GET: /produto
        public ActionResult Index() => View(Produto.Listar());

        // POST: /produto/incluir
        [HttpPost]
        public ActionResult Incluir(FormCollection form)
        {
            if (form.HasKeys())
            {
                Produto produto = new Produto();
                produto.Descricao = form["txtDescricao"];
                Produto.Incluir(produto);
                return PartialView("_Lista", Produto.Listar());
            }
            return Json(false);
        }

        // POST: /produto/editar/5
        [HttpPost]
        public ActionResult Editar(int cod, FormCollection form)
        {
            if (cod > 0 && form.HasKeys())
            {
                Produto produto = Produto.ObterPorCodigo(cod);
                produto.Descricao = form["txtDescricao"];
                Produto.Editar(produto);
                return PartialView("_Lista", Produto.Listar());
            }
            return Json(false);
        }

        // POST: /produto/excluir/5
        [HttpPost]
        public ActionResult Excluir(int cod)
        {
            if (cod != 0)
            {
                Produto produto = Produto.ObterPorCodigo(cod);
                Produto.Excluir(produto);
                return PartialView("_Lista", Produto.Listar());
            }
            return Json(false);
        }
    }
}