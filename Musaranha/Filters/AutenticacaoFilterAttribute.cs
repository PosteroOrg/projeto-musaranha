using System.Web;
using System.Web.Mvc;

namespace Musaranha.Filters
{
    public class AutenticacaoFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var autenticado = HttpContext.Current.Session["Autenticado"];
            if (autenticado == null || autenticado.Equals(false))
            {
                if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "GET")
                {
                    filterContext.Result = new RedirectResult("~/?continuar=" + filterContext.HttpContext.Request.Path);
                }
                else
                {
                    filterContext.Result = new JsonResult();
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}