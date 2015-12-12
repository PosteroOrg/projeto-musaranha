using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class PainelController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}