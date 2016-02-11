using System.Collections.Generic;
using System.Web.Mvc;

using Musaranha.Models;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class ConfiguracoesController : Controller
    {
        // GET: /configuracoes
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // POST: /configuracoes
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var mensagens = new List<string>();

            if (form["txtSenhaNova"] == form["txtConfirmacao"] && Acesso.Autenticado((string)Session["Usuario"], form["txtSenhaAtual"]))
            {
                Acesso.AlterarSenha((string)Session["Usuario"], form["txtSenhaNova"]);
                mensagens.Add("Senha alterada com sucesso.");
            }
            else
            {
                mensagens.Add("Ocorreu um erro na tentativa de alterar a senha.");
            }

            ViewBag.Mensagens = mensagens;

            return View();
        }
    }
}