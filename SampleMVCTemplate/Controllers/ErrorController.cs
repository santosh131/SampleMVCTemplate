using SampleMVCTemplate.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCTemplate.Controllers
{
    [CustomAuthorizationAttr]
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            if (TempData.ContainsKey("HandleErrorMdoel"))
                ViewBag.HandleErrorInfo = TempData["HandleErrorMdoel"];
            else
                ViewBag.HandleErrorInfo = new HandleErrorInfo(new Exception(), "Error", "Index");
            return View();
        }
    }
}