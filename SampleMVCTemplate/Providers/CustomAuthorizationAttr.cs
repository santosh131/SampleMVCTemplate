using SampleMVCTemplate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using SampleModels;
using System.Web.Security;
using SampleBO;
using Helpers;
using System.Net;
using System.Web.Helpers;

namespace SampleMVCTemplate.Providers
{
    public class CustomAuthorizationAttr : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string reqControllerPremission = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string reqActionPremission = filterContext.ActionDescriptor.ActionName;
            string menuActionCode = "";
            if (filterContext.RequestContext.HttpContext.Request.Headers != null)
            {
                menuActionCode = filterContext.RequestContext.HttpContext.Request.Headers["MenuActionCode"];
            }

            //var request = filterContext.HttpContext.Request;

            ////  Only validate POSTs
            //if (request.HttpMethod == WebRequestMethods.Http.Post)
            //{
            //    //  Ajax POSTs and normal form posts have to be treated differently when it comes
            //    //  to validating the AntiForgeryToken
            //    if (request.IsAjaxRequest())
            //    {
            //        var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];

            //        var cookieValue = antiForgeryCookie != null
            //            ? antiForgeryCookie.Value
            //            : null;

            //        AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
            //    }
            //    else
            //    {
            //        new ValidateAntiForgeryTokenAttribute()
            //            .OnAuthorization(filterContext);
            //    }
            //}

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
                if (cp == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                                             new RouteValueDictionary {
                                                { "action", "Index" },
                                                { "controller", "Unauthorised" } ,
                                                {"errorText","User is not Authorized" }});
                }
                else
                {
                    if (!string.IsNullOrEmpty(menuActionCode))
                    {
                        if (!cp.HasActionPermission(menuActionCode))
                        {
                            filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary {
                                                { "action", "Index" },
                                                { "controller", "Unauthorised" } ,
                                                {"errorText","User is not Authorized" }});
                        }
                    }
                    else
                    {
                        if (!cp.HasControllerActionPermission(reqControllerPremission, reqActionPremission))
                        {
                            filterContext.Result = new RedirectToRouteResult(
                                                 new RouteValueDictionary {
                                                { "action", "Index" },
                                                { "controller", "Unauthorised" } ,
                                                {"errorText","User is not Authorized" }});
                        }
                    }
                    SessionHelper.ResetSessionTime();
                }
            }
        }
    }
}