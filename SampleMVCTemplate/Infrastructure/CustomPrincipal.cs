using SampleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SampleMVCTemplate.Infrastructure
{
    public class CustomPrincipal : IPrincipal
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Roles { get; set; }
        public List<Menus> Menus { get; set; }
        public List<MenuActions> MenuActions { get; set; }

        public CustomPrincipal(string userId, string userName)
        {
            this.UserId = userId;
            this.UserName = userName;
            Identity = new GenericIdentity(userId);
        }

        public IIdentity Identity
        {
            get;
            private set;           
        }

        public bool IsInRole(string role)
        {
            return this.Roles.Contains(role);
        }

        public bool HasControllerPermission(string controllerName)
        {
            return this.Menus.Where(m=>m.ControllerName== controllerName).Count()>0?true:false;
        }

        public bool HasActionPermission(string menuActionCode)
        {
            return this.MenuActions.Where(m => m.MenuActionCode == menuActionCode).Count() > 0 ? true : false;
        }
    }
}