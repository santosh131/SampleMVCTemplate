using Helpers;
using SampleBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SampleMVCTemplate.Infrastructure
{
    public static class CustomHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="menuActionCode"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomInputButton(this HtmlHelper helper, string id, string text, string type, string menuActionCode)
        {
            return CustomInputButton(helper, id, text, type, null, menuActionCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="menuActionCode"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomInputButton(this HtmlHelper helper, string id, string type, string menuActionCode)
        {
            return CustomInputButton(helper, id, "", type, null, menuActionCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="menuActionCode"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomInputButton(this HtmlHelper helper, string id, string text, string type, object htmlAttributes, string menuActionCode)
        {
            CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
            if (cp != null && cp.HasActionPermission(menuActionCode))
            {
                var builder = new TagBuilder("input");

                builder.GenerateId(id);

                if (!string.IsNullOrEmpty(type))
                    builder.MergeAttribute("type", type);
                else
                    builder.MergeAttribute("type", "submit");

                if (!string.IsNullOrEmpty(text))
                    builder.MergeAttribute("value", text);
                else
                    builder.MergeAttribute("value", cp.GetActionDescription(menuActionCode));

                builder.MergeAttribute("data-menu-action-code", menuActionCode);
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
                return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
            }
            else
                return MvcHtmlString.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="menuActionCode"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomButton(this HtmlHelper helper, string id, string text, string type, bool isFontAwesomeUsed, string fontwesomeclassName, string menuActionCode)
        {
            return CustomButton(helper, id, text, type, null, isFontAwesomeUsed, fontwesomeclassName, menuActionCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="menuActionCode"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomButton(this HtmlHelper helper, string id, string type, bool isFontAwesomeUsed, string fontwesomeclassName, string menuActionCode)
        {
            return CustomButton(helper, id, "", type, null, isFontAwesomeUsed, fontwesomeclassName, menuActionCode);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="isFontAwesomeUsed"></param>
        /// <param name="fontwesomeclassName"></param>
        /// <param name="menuActionCode"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomButton(this HtmlHelper helper, string id, string text, string type, object htmlAttributes, bool isFontAwesomeUsed, string fontwesomeclassName, string menuActionCode)
        {
            return CustomButton(helper, id, text, type, htmlAttributes, isFontAwesomeUsed, fontwesomeclassName, false, menuActionCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="isFontAwesomeUsed"></param>
        /// <param name="fontwesomeclassName"></param>
        /// <param name="showIconOnly"></param>
        /// <param name="menuActionCode"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomButton(this HtmlHelper helper, string id, string text, string type, object htmlAttributes, bool isFontAwesomeUsed, string fontwesomeclassName, bool showIconOnly, string menuActionCode)
        {
            CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
            if (cp != null && cp.HasActionPermission(menuActionCode))
            {
                var builder = new TagBuilder("button");

                builder.GenerateId(id);

                if (!string.IsNullOrEmpty(type))
                    builder.MergeAttribute("type", type);
                else
                    builder.MergeAttribute("type", "submit");

                if (!isFontAwesomeUsed)
                {
                    if (!string.IsNullOrEmpty(text))
                        builder.SetInnerText(text);
                    else
                        builder.SetInnerText(cp.GetActionDescription(menuActionCode));
                }
                else
                {
                    var liTagBuilder = new TagBuilder("li");
                    liTagBuilder.MergeAttribute("class", fontwesomeclassName);
                    if (!showIconOnly)//Show Icon & Text
                    {
                        if (!string.IsNullOrEmpty(text))
                            builder.InnerHtml += liTagBuilder.ToString() + text;
                        else
                        {
                            builder.InnerHtml += liTagBuilder.ToString() + cp.GetActionDescription(menuActionCode);
                        }
                    }
                    else //Show Icon only
                    {
                        builder.InnerHtml += liTagBuilder.ToString();
                    }
                }

                builder.MergeAttribute("data-menu-action-code", menuActionCode);
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
                return MvcHtmlString.Create(builder.ToString());
            }
            else
                return MvcHtmlString.Empty;
        }

        /// <summary>
        /// Custom ActionLink
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="text"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="menuActionCode"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomActionLink(this HtmlHelper helper, string id, string actionName, string controllerName, string text, object routeValues, object htmlAttributes, string menuActionCode)
        {
            CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
            if (cp != null && cp.HasActionPermission(menuActionCode))
            {
                var url = new UrlHelper(helper.ViewContext.RequestContext);
                var builder = new TagBuilder("a");
                string queryString = "";

                builder.GenerateId(id);
                queryString = SecurityHelpers.RouteDataToEncrytedString(routeValues);
                builder.MergeAttribute("href", url.Action(actionName, controllerName) + queryString);

                if (!string.IsNullOrEmpty(text))
                    builder.SetInnerText(text);
                else
                    builder.SetInnerText(cp.GetActionDescription(menuActionCode));

                builder.MergeAttribute("data-menu-action-code", menuActionCode);

                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
                return MvcHtmlString.Create(builder.ToString());
            }
            else
                return MvcHtmlString.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="src"></param>
        /// <param name="alternateText"></param>
        /// <param name="menuActionCode"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomImage(this HtmlHelper helper, string id, string src, string alternateText, string menuActionCode)
        {
            return CustomImage(helper, id, src, alternateText, null, menuActionCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="src"></param>
        /// <param name="alternateText"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="menuActionCode"></param>
        /// <returns></returns>
        public static MvcHtmlString CustomImage(this HtmlHelper helper, string id, string src, string alternateText, object htmlAttributes, string menuActionCode)
        {
            CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
            if (cp != null && cp.HasActionPermission(menuActionCode))
            {
                var builder = new TagBuilder("img");
                builder.GenerateId(id);
                builder.MergeAttribute("src", src);
                builder.MergeAttribute("alt", alternateText);
                builder.MergeAttribute("data-menu-action-code", menuActionCode);
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

                return MvcHtmlString.Create(builder.ToString());
            }
            else
                return MvcHtmlString.Empty;
        } 
    }
}