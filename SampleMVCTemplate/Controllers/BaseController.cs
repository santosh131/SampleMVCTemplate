using SampleMVCTemplate.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCTemplate.Controllers
{
    [CustomAuthorizationAttr]
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception,  "Controller", "Action");
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}