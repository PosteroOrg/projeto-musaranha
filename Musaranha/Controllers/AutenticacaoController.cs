using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Musaranha.Models;

namespace Musaranha.Controllers
{
    public class AutenticacaoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

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

        public ActionResult Sair()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}