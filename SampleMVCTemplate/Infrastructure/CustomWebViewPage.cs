using Helpers;
using Microsoft.AspNet.Identity;
using SampleBO;
using SampleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCTemplate.Infrastructure
{
    public abstract class CustomWebViewPage : WebViewPage
    {
        public bool IsUserAuthenticated
        {
            get
            {
                return SessionHelper.IsUserAuthenticated;
            }
        }

        public int SessionExpirationTime
        {
            get
            {
                return SessionHelper.GetSessionExpirationTime();
            }
        }

        public string GetUserName()
        {
            return SessionHelper.GetUserName();
        }

        public List<RoleMenuActionViewModel> GetRoleMenuActioVMs()
        {
            return SessionHelper.GetRoleMenuActioVMs();
        }

        public List<MenuCategory> GetMenuCategory()
        {
            return SessionHelper.GetMenuCategory();
        }

        public bool HasActionPermission(string menuActionCode)
        {
            CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
            if (cp != null && cp.HasActionPermission(menuActionCode))
                return true;
            else
                return false;
        }

        public static List<LookupModel> GetCommonDropdown(string fieldCode, string optionCode)
        {
            return new CommonBO().GetCommonLookup(fieldCode, optionCode);
        }
    }

    public abstract class CustomWebViewPage<T> : WebViewPage<T>
    {
        public bool IsUserAuthenticated
        {
            get
            {
                return SessionHelper.IsUserAuthenticated;
            }
        }

        public int SessionExpirationTime
        {
            get
            {
                return SessionHelper.GetSessionExpirationTime();
            }
        }

        public string GetUserName()
        {
            return SessionHelper.GetUserName();
        }         

        public List<RoleMenuActionViewModel> GetRoleMenuActioVMs()
        {
            return SessionHelper.GetRoleMenuActioVMs();
        }

        public List<MenuCategory> GetMenuCategory()
        {
            return SessionHelper.GetMenuCategory();
        }

        public bool HasActionPermission(string menuActionCode)
        {
            CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
            if (cp != null && cp.HasActionPermission(menuActionCode))
                return true;
            else
                return false;
        }

        public static List<LookupModel> GetCommonDropdown(string fieldCode, string optionCode)
        {
            return new CommonBO().GetCommonLookup(fieldCode, optionCode);
        }
    }
}