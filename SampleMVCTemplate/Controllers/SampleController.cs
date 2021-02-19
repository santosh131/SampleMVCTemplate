using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCTemplate.Controllers
{
    public class SampleController : BaseController
    {
        // GET: Sample
        public ActionResult Index()
        {
            return View();
        }
    }
}