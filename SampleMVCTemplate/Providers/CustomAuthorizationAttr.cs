using SampleMVCTemplate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace SampleMVCTemplate.Providers
{
    public class CustomAuthorizationAttr: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string reqPremission = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                                      new RouteValueDictionary {
                                                { "action", "Index" },
                                                { "controller", "Unauthorised" } ,
                                                {"errorText","User is not Authenticated" }});
            }
            else
            {
                CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
                if (cp == null || !cp.HasControllerPermission(reqPremission))
                {
                    filterContext.Result = new RedirectToRouteResult(
                                             new RouteValueDictionary {
                                                { "action", "Index" },
                                                { "controller", "Unauthorised" } ,
                                                {"errorText","User is not Authorized" }});
                }
            }
        } 
    }
}