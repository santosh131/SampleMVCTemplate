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

namespace SampleMVCTemplate.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users users, FormCollection collection)
        {
            string messageCode = string.Empty;
            string message = string.Empty;
            string encPwd = SecurityHelper.Encrypt(users.Password);
            Users u = new UserBO().GetUser(users.UserName, encPwd, out messageCode, out message);
            if (messageCode == CommonEnums.MessageCodes.Success.ToString())
            {
                FormsAuthenticationTicket fAuthTicket = new FormsAuthenticationTicket(1, u.UserId, DateTime.Now, DateTime.Now.AddMinutes(15), true, u.UserName, FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(fAuthTicket);
                HttpCookie hcookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                if (fAuthTicket.IsPersistent)
                    hcookie.Expires = fAuthTicket.Expiration;
                Response.Cookies.Add(hcookie);
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
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Logout(Users users, FormCollection collection)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
