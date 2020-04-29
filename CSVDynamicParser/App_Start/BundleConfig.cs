using System.Web;
using System.Web.Optimization;

namespace CSVDynamicParser.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                    "~/Scripts/libs/knockout-3.5.1.js",
                     "~/Scripts/libs/knockout.validation.2.0.4.min.js",
                     "~/Content/layui-v2.5.6/layui.js",
                    "~/Scripts/libs/lodash.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/layui-v2.5.6/css/layui.css",
                      "~/Content/font-awesome-4.7.0/css/font-awesome.css",
                      "~/Content/site.css"));
        }
    }
}
