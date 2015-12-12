using System;
using System.Collections.Generic;
using System.Linq;
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
                filterContext.Result = new RedirectResult("~/?continuar=" + filterContext.HttpContext.Request.Path);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}