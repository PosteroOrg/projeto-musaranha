using System.Web.Mvc;

namespace Musaranha.Controllers
{
    [Filters.AutenticacaoFilter]
    public class PainelController : Controller
    {
        // GET: /painel
        public ActionResult Index() => View();
    }
}