using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SampleMVCTemplate.Controllers
{

    public class UnauthorisedController : Controller
    {
        [AllowAnonymous]       
        public ActionResult Index(string errorText)
        {
            ViewBag.ErrorText = errorText;
            return View();
        }
    }
}