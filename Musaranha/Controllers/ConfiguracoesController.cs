using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Musaranha.Models;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class ConfiguracoesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var mensagens = new List<Dictionary<string, string>>();

            if (form["txtSenhaNova"] == form["txtConfirmacao"] && Acesso.Autenticado((string)Session["Usuario"], form["txtSenhaAtual"]))
            {
                Acesso.AlterarSenha((string)Session["Usuario"], form["txtSenhaNova"]);
                mensagens.Add(new Dictionary<string, string>() {
                    { "Conteudo", "Senha alterada com sucesso." }, { "Tipo", "success" }
                });
            }
            else
            {
                mensagens.Add(new Dictionary<string, string>() {
                    { "Conteudo", "Ocorreu um erro na tentativa de alterar a senha." }, { "Tipo", "alert" }
                });
            }

            ViewBag.Mensagens = mensagens;

            return View();
        }
    }
}