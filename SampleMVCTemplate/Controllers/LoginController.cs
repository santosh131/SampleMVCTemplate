using Helpers;
using SampleBO;
using SampleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Text;
using System.Reflection;
using SampleMVCTemplate.Infrastructure;
using System.Threading;
using System.Web.Script.Serialization;

namespace SampleMVCTemplate.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        //public ActionResult Index(string message,Users u)
        public ActionResult Index(string message)
        {
            Users u = new Users();
            ViewBag.ErrorText = message;

            if (SessionHelper.IsUserAuthenticated)
            {
                u = SessionHelper.LoginClearAll();
                return RedirectToAction("Index");
            }
            else
            {
                u = SessionHelper.GetUserInfoFromCookie();
            }
            return View("Index", u);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users users, FormCollection collection)
        {
            string messageCode = string.Empty;
            string message = string.Empty;
            users.Password = SecurityHelpers.Encrypt(users.Password);
            SessionHelper.Login(users, out messageCode, out message);
            if (messageCode.ToUpper() == CommonEnums.MessageCodes.SUCCESS.ToString())
            {
                return new RedirectToRouteResult(
                          new RouteValueDictionary(
                             new
                             {
                                 controller = "Home",
                                 action = "Index"
                             }
                              )
                          );
            }
            else
            {
                return RedirectToAction("Index", new { message = message, users = new Users() });
            }
        }

        [AllowAnonymous]
        public ActionResult Logout(Users users, FormCollection collection)
        {
            SessionHelper.Logout();
            return RedirectToAction("Index");
        }
    }
}
