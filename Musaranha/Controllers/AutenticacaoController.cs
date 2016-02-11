using System;
using System.Web.Mvc;
using Musaranha.Models;

namespace Musaranha.Controllers
{
    public class AutenticacaoController : Controller
    {
        // GET: /
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Autenticado"] != null)
            {
                return RedirectToAction("Index", "Painel");
            }
            return View();
        }

        // POST: /
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if (Acesso.Autenticado(form["txtUsuario"], form["txtSenha"]))
            {
                Session.Add("Usuario", form["txtUsuario"]);
                Session.Add("Autenticado", true);
                if (!String.IsNullOrEmpty(Request.QueryString["continuar"]))
                {
                    return Redirect(Request.QueryString["continuar"]);
                }
                return RedirectToAction("Index", "Painel");
            }
            return View();
        }

        // GET: /autenticacao/sair
        public ActionResult Sair()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}