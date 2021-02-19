using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCTemplate.Controllers
{
    public class DummyController : BaseController
    {
        // GET: Dummy
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dummy
        public ActionResult Index2()
        {
            return View();
        }
    }
}