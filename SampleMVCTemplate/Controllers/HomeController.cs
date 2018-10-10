using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleBO;
using SampleMVCTemplate.Providers;

namespace SampleMVCTemplate.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
             
            ViewBag.Title = "Home Page";
            SampleBO.Class1 cc = new Class1();
            cc.Func1();
            return View();
        }
    }
}
