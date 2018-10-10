using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCTemplate.Infrastructure
{
    public static class CustomHelpers
    {
        public static string Button(this HtmlHelper helper, string target, string text, string className, string menuActionCode)
        {
            CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
            if (cp != null && cp.HasActionPermission(menuActionCode))
            {
                return String.Format("<input type='submit' id='{0}' name='{0}' value='{1}' class='{2}' />", target, text,className);
            }
            else
                return "";
        }
    }
}