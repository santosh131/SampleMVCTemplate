using Helpers;
using SampleBO;
using SampleModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace SampleMVCTemplate.Infrastructure
{
    public static class SessionHelper
    {
        #region Session

        public static void ResetSessionTime(bool force = false)
        {
            if (!force)
            {
                if (HttpContext.Current.Session["SessionId"] != null)
                {
                    DateTime dtExp = DateTime.Parse(HttpContext.Current.Session["ExpirationTime"].ToString());
                    TimeSpan ts = dtExp - DateTime.Now;
                    if (ts.TotalSeconds < 120)
                    {
                        HttpContext.Current.Session["ExpirationTime"] = DateTime.Now.AddMinutes(GetSessionExpirationTime());
                        HttpContext.Current.Session.Timeout = HttpContext.Current.Session.Timeout + GetSessionExpirationTime();
                    }
                }
            }
            else
            {
                HttpContext.Current.Session["ExpirationTime"] = DateTime.Now.AddMinutes(GetSessionExpirationTime());
                HttpContext.Current.Session.Timeout = HttpContext.Current.Session.Timeout + GetSessionExpirationTime();
            }
        }

        public static void ClearSession(string sessionId = "")
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["SessionId"] != null)
                    {
                        sessionId = HttpContext.Current.Session["SessionId"].ToString();
                    }
                    HttpContext.Current.Session.Abandon();
                }
                else
                {
                    string cookieName = GetAuthCookieName();
                    if (HttpContext.Current.Request.Cookies[cookieName] != null)
                    {
                        sessionId = HttpContext.Current.Request.Cookies[cookieName].Value;
                    }
                }
            }
            if (!string.IsNullOrEmpty(sessionId))
            {
                UserSession us = new UserSession();
                us.SessionId = sessionId;
                us.CUDOperationType = CommonEnums.DeleteOperationType;
                string messageCode = string.Empty;
                string messageText = string.Empty;
                string sessionIdTemp = string.Empty;
                new UserBO().UserSessionCUD(us, out sessionIdTemp, out messageCode, out messageText);
            }
        }

        public static int GetSessionExpirationTime()
        {
            int time = 15;
            if (Helpers.ValidationHelpers.IsInteger(ConfigurationManager.AppSettings["SessionExpiration"].ToString()))
                time = int.Parse(ConfigurationManager.AppSettings["SessionExpiration"].ToString());
            return time;
        }


        public static void SetSearchSession(string key, object val)
        {
            HttpContext.Current.Session[key+CommonEnums.SessionKeyHelper] = val;
        }

        public static T GetSearchSession<T>(string key)
        {
            if (HttpContext.Current.Session[key + CommonEnums.SessionKeyHelper] != null)
                return (T)HttpContext.Current.Session[key + CommonEnums.SessionKeyHelper];
            else
                return Activator.CreateInstance<T>();
        }

        public static void ClearSearchSession(string isMenu)
        {
            if (isMenu == "1")
            {
                List<int> lstIndex = new List<int>();
                string item = "";

                for (int i = 0; i < HttpContext.Current.Session.Keys.Count; i++)
                {
                    item = HttpContext.Current.Session.Keys[i];
                    if (item.ToString().IndexOf(CommonEnums.SessionKeyHelper) > -1)
                    {
                        lstIndex.Add(i);
                    }
                }
                foreach (int itemIndex in lstIndex)
                {
                    HttpContext.Current.Session.RemoveAt(itemIndex);
                }
            }
        }
        #endregion

        #region Cookie

        public static int GetCookieExpirationTime()
        {
            int time = 300;
            if (Helpers.ValidationHelpers.IsInteger(ConfigurationManager.AppSettings["PersistentCookieExpiration"].ToString()))
                time = int.Parse(ConfigurationManager.AppSettings["PersistentCookieExpiration"].ToString());
            return time;
        }

        public static string GetCookieName()
        {
            if (HttpContext.Current.Session["SessionId"] != null)
                return ConfigurationManager.AppSettings["CookieName"];// + "_" + HttpContext.Current.Session["SessionId"].ToString();
            else
                return ConfigurationManager.AppSettings["CookieName"];
        }

        public static string GetAuthCookieName()
        {
            if (HttpContext.Current.Session["SessionId"] != null)
                return ConfigurationManager.AppSettings["CookieName"] + "_" + "Auth";// + "_" + HttpContext.Current.Session["SessionId"].ToString();
            else
                return ConfigurationManager.AppSettings["CookieName"] + "_" + "Auth";
        }

        public static void SetTicketCookie(string userData, Users u)
        {
            FormsAuthenticationTicket fAuthTicket;
            int timeOut = GetSessionExpirationTime();
            string hash = "";
            string authCookieName = SessionHelper.GetAuthCookieName(); //Contains session info
            string cookieName = ""; //Contains username, persistent info
            HttpContext.Current.Session["SessionId"] = userData;
            HttpContext.Current.Session["IssueTime"] = DateTime.Now;
            HttpContext.Current.Session.Timeout = GetSessionExpirationTime();
            HttpContext.Current.Session["ExpirationTime"] = DateTime.Parse(HttpContext.Current.Session["IssueTime"].ToString()).AddMinutes(HttpContext.Current.Session.Timeout);

            fAuthTicket = new FormsAuthenticationTicket(1, u.UserId, DateTime.Now, DateTime.Now.AddMinutes(timeOut), u.RememberMe, userData, FormsAuthentication.FormsCookiePath);
            hash = FormsAuthentication.Encrypt(fAuthTicket);
            AddRemoveCookie(authCookieName, hash, fAuthTicket.Expiration);

            cookieName = GetCookieName();
            if (u.RememberMe)
            {
                timeOut = GetCookieExpirationTime();
                AddRemoveCookie(cookieName, u.UserName + CommonEnums.DataSeperator + u.RememberMe, DateTime.Now.AddMinutes(timeOut));
            }
            else
            {
                AddRemoveCookie(cookieName, "", new DateTime(1900, 1, 1));
            }
        }

        public static void AddRemoveCookie(string cookieName, string value, DateTime? expirationTime = null)
        {
            HttpContext.Current.Response.Cookies.Remove(cookieName);
            HttpCookie hcookie = new HttpCookie(cookieName, value);
            if (expirationTime.HasValue && expirationTime.Value != DateTime.MinValue)
                hcookie.Expires = expirationTime.Value;
            HttpContext.Current.Response.Cookies.Add(hcookie);
        }

        public static void ClearAuthCookie()
        {
            string authCookieName = GetAuthCookieName();
            AddRemoveCookie(authCookieName, "", new DateTime(1900, 1, 1));
        }

        public static Users GetUserInfoFromCookie()
        {
            Users us = new Users();
            string cookieName = GetCookieName();
            HttpCookie cookies = HttpContext.Current.Request.Cookies[cookieName];
            if (cookies != null && cookies.Value != "")
            {
                string[] ckSplit = cookies.Value.Split(new string[] { CommonEnums.DataSeperator }, StringSplitOptions.None);
                us.UserName = ckSplit[0];
                if (ValidationHelpers.IsBool(ckSplit[1].ToString()))
                    us.RememberMe = bool.Parse(ckSplit[1].ToString());
                else
                    us.RememberMe = false;
            }
            return us;
        }

        public static void SetPrincipalFromCookie()
        {
            string authCookieName = SessionHelper.GetAuthCookieName();
            HttpCookie authoCookies = HttpContext.Current.Request.Cookies[authCookieName];
            if (authoCookies != null && authoCookies.Value != "")
            {
                FormsAuthenticationTicket fAuthTicket = FormsAuthentication.Decrypt(authoCookies.Value);
                string userData = fAuthTicket.UserData;
                string[] ckSplit = userData.Split(new string[] { CommonEnums.DataSeperator }, StringSplitOptions.None);
                UserSession userSession = new UserSession();
                UserSession userSessionOutput = new UserSession();
                userSession.SessionId = ckSplit[0];
                List<Menus> menus = new List<Menus>();
                List<MenuCategory> menuCatgs = new List<MenuCategory>();
                List<RoleMenuActionViewModel> roleMenuActioVMs = new List<RoleMenuActionViewModel>();
                string messageCode = "";
                string message = "";
                new UserBO().GetUserSession(userSession, out userSessionOutput, out menus, out menuCatgs, out roleMenuActioVMs, out messageCode, out message);
                if (messageCode == CommonEnums.MessageCodes.SUCCESS.ToString())
                {
                    AddRemoveCookie(authCookieName, authoCookies.Value, DateTime.Now.AddMinutes(GetSessionExpirationTime()));
                    CustomPrincipal cp = new CustomPrincipal(userSessionOutput.UserId, userSessionOutput.UserName);
                    cp.Menus = menus;
                    cp.MenuCategories = menuCatgs;
                    cp.RoleMenuActions = roleMenuActioVMs;
                    HttpContext.Current.User = cp;
                    Thread.CurrentPrincipal = cp;
                }
                else
                {
                    HttpContext.Current.User = null;
                }
            }
            else
            {
                HttpContext.Current.User = null;
            }
        }

        #endregion

        #region LoginLogout

        public static void Logout()
        {
            string authCookieName = GetAuthCookieName();
            string cookieName = GetCookieName();
            ClearSession();
            AddRemoveCookie(authCookieName, "", new DateTime(1900, 1, 1));
            AddRemoveCookie(cookieName, "", new DateTime(1900, 1, 1));
            FormsAuthentication.SignOut();
            HttpContext.Current.User = null;
        }

        public static Users LoginClearAll()
        {
            Users us = new Users();
            string authCookieName = GetAuthCookieName();
            ClearSession();
            AddRemoveCookie(authCookieName, "", new DateTime(1900, 1, 1));
            us = GetUserInfoFromCookie();
            FormsAuthentication.SignOut();
            HttpContext.Current.User = null;
            return us;
        }

        public static bool Login(Users users, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            string encPwd = SecurityHelpers.Encrypt(users.Password);
            Users userInfo = new UserBO().GetLoginUser(users.UserName, encPwd, out messageCode, out message);
            if (messageCode.ToUpper() == CommonEnums.MessageCodes.SUCCESS.ToString())
            {
                List<RoleMenuActionViewModel> roleMenuActioVMs = new List<RoleMenuActionViewModel>();
                List<Menus> menus = new List<Menus>();
                List<MenuCategory> menuCatgs = new List<MenuCategory>();
                new UserBO().GetUserRolesMenusActions(users.UserName, out menus, out menuCatgs, out roleMenuActioVMs, out messageCode, out message);
                if (messageCode.ToUpper() == CommonEnums.MessageCodes.SUCCESS.ToString())
                {
                    string sessionId = Guid.NewGuid().ToString();
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    string userData = jss.Serialize(menus) + CommonEnums.DataSeperator + jss.Serialize(menuCatgs) + CommonEnums.DataSeperator + jss.Serialize(roleMenuActioVMs);
                    UserSession userSession = new UserSession();
                    userSession.SessionId = sessionId;
                    userSession.UserId = userInfo.UserId;
                    userSession.UserName = userInfo.UserName;
                    userSession.RoleMenuAction = SecurityHelpers.Encrypt(userData);
                    userSession.CUDOperationType = CommonEnums.CreateOperationType;
                    new UserBO().UserSessionCUD(userSession, out sessionId, out messageCode, out message);
                    if (messageCode.ToUpper() == CommonEnums.MessageCodes.SUCCESS.ToString())
                    {
                        userInfo.RememberMe = users.RememberMe;
                        SetTicketCookie(sessionId, userInfo);
                        CustomPrincipal cp = new CustomPrincipal(userInfo.UserId, userInfo.UserName);
                        cp.Menus = menus;
                        cp.MenuCategories = menuCatgs;
                        cp.RoleMenuActions = roleMenuActioVMs;
                        HttpContext.Current.User = cp;
                        Thread.CurrentPrincipal = cp;
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region  UserRelated

        public static string GetUserId()
        {
            CustomPrincipal cp = (HttpContext.Current.User as CustomPrincipal);
            if (cp != null)
                return cp.UserId;
            else
                return "";
        }

        public static string GetUserName()
        {
            CustomPrincipal cp = (HttpContext.Current.User as CustomPrincipal);
            if (cp != null)
                return cp.UserName;
            else
                return "";
        }

        public static bool IsUserAuthenticated
        {
            get
            {
                if (HttpContext.Current.User != null &&
                    HttpContext.Current.User.Identity != null &&
                    HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return HttpContext.Current.User.Identity.IsAuthenticated;
                }
                else
                    return false;
            }

        }


        public static List<RoleMenuActionViewModel> GetRoleMenuActioVMs()
        {
            CustomPrincipal cp = (HttpContext.Current.User as CustomPrincipal);
            if (cp != null)
                return cp.RoleMenuActions;
            else
                return null;
        }

        public static List<MenuCategory> GetMenuCategory()
        {
            CustomPrincipal cp = (HttpContext.Current.User as CustomPrincipal);
            if (cp != null)
                return cp.MenuCategories;
            else
                return null;
        }

        #endregion

    }
}