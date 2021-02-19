using Helpers;
using SampleBO;
using SampleModels;
using SampleMVCTemplate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SampleMVCTemplate
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {

        }

        protected void Session_End(Object sender, EventArgs e)
        {
            //if (this.Session["SessionId"] != null)
            //    SessionHelper.ClearSession(this.Session["SessionId"].ToString());
        }

        protected void Application_OnPostAcquireRequestState(object sender, EventArgs e)
        {
            SessionHelper.SetPrincipalFromCookie();
        }

        protected void Application_PostAuthenticateRequest()
        {
            //HttpCookie authoCookies = Request.Cookies[SessionHelper.GetCookieName()];
            //if (authoCookies != null)
            //{
            //    FormsAuthenticationTicket fAuthTicket = FormsAuthentication.Decrypt(authoCookies.Value);
            //    string userData = fAuthTicket.UserData;
            //    UserSession userSession = new UserSession();
            //    UserSession userSessionOutput = new UserSession();
            //    userSession.SessionId = userData;
            //    List<Menus> menus = new List<Menus>();
            //    List<MenuCategory> menuCatgs = new List<MenuCategory>();
            //    List<RoleMenuActionViewModel> roleMenuActioVMs = new List<RoleMenuActionViewModel>();
            //    string messageCode = "";
            //    string message = "";
            //    new UserBO().GetUserSession(userSession, out userSessionOutput, out menus,out menuCatgs, out roleMenuActioVMs, out messageCode, out message);
            //    if (messageCode == CommonEnums.MessageCodes.SUCCESS.ToString())
            //    {
            //        CustomPrincipal cp = new CustomPrincipal(userSessionOutput.UserId, userSessionOutput.UserName);
            //        cp.Menus = menus;
            //        cp.MenuCategories= menuCatgs;
            //        cp.RoleMenuActions = roleMenuActioVMs;
            //        HttpContext.Current.User = cp;
            //    }
            //    else
            //        HttpContext.Current.User = null;
            //}

        }
    }
}
