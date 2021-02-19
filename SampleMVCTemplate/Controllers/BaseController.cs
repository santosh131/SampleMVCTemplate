using Helpers;
using SampleMVCTemplate.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SampleMVCTemplate.Controllers
{
    [CustomAuthorizationAttr]
    public class BaseController : Controller
    {
        public void SetSuccessMessage(string message)
        {
            //TempData["SuccessText"] = message;
            ViewBag.SuccessText = message;
        }

        public void SetErrorMessage(string message)
        {
            //TempData["ErrorText"] = message;
            ViewBag.ErrorText = message;
        }      

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();
            if (filterContext.HttpContext.Request.QueryString.Get("q") != null)
            {
                string encryptedQueryString = filterContext.HttpContext.Request.QueryString.Get("q");
                string decrptedString = SecurityHelpers.DecryptUrl(encryptedQueryString.ToString());
                string[] paramsArrs = decrptedString.Split('?');

                for (int i = 0; i < paramsArrs.Length; i++)
                {
                    string[] paramArr = paramsArrs[i].Split('=');
                    decryptedParameters.Add(paramArr[0], (paramArr[1]));// pass two string parameters
                }
            }
            for (int i = 0; i < decryptedParameters.Count; i++)
            {
                filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
            }
            base.OnActionExecuting(filterContext);

        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            var model = new HandleErrorInfo(filterContext.Exception, filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString());
            TempData["HandleErrorMdoel"] = model;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.ExceptionHandled = false;

                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        filterContext.Exception.Message,
                        filterContext.Exception.StackTrace
                    }
                };
            }
            else
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectToRouteResult(
                                             new RouteValueDictionary {
                                                { "action", "Index" },
                                                { "controller", "Error" }});
            }

        }

        public string RenderRazorViewToString(string viewName, object model = null)
        {
            Controller controller = this;
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult;
                viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}