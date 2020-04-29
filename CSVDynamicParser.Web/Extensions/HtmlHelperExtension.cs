using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSVDynamicParser.Web
{
    public static class HtmlHelperExtension
    {

        public static MvcHtmlString Script(this HtmlHelper html, string contentPath)
        {
            return VersionContent(html, "<script src=\"{0}\" type=\"text/javascript\"></script>", contentPath);
        }

        public static MvcHtmlString Style(this HtmlHelper html, string contentPath)
        {
            return VersionContent(html, "<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\">", contentPath);
        }

        private static MvcHtmlString VersionContent(this HtmlHelper html, string template, string contentPath)
        {
            var httpContenxt = html.ViewContext.HttpContext;
            string hashValue = VersionUtils.GetFileVersion(httpContenxt.Server.MapPath(contentPath));
            contentPath = UrlHelper.GenerateContentUrl(contentPath, httpContenxt) + "?v=" + hashValue;
            return MvcHtmlString.Create(string.Format(template, contentPath));
        }

    }
}