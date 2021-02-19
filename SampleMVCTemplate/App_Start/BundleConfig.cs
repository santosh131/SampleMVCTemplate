using System.Web;
using System.Web.Optimization;

namespace SampleMVCTemplate
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.js",
                        "~/Scripts/jquery-migrate-3.0.0.min.js",
                        "~/Scripts/jquery-ui.js",
                        //"~/Scripts/jquery.validate.min.js",
                          "~/Scripts/jquery.validate.js",
                        //"~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Content/fontawesome/js/all.min.js",
                        "~/Content/DataTables/datatables.min.js",                         
                        "~/Content/DataTables/DataTables-1.10.18/js/dataTables.bootstrap4.min.js",
                        "~/Content/twbs-pagination-master/jquery.twbsPagination.min.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/fontawesome/css/all.min.css",
                      "~/Content/DataTables/DataTables-1.10.18/css/dataTables.bootstrap4.css"));

            bundles.Add(new ScriptBundle("~/bundles/helpers").Include(
                "~/Scripts/CommonHelpers.js",
                "~/Scripts/AjaxHelpers.js",
                "~/Scripts/Session.js"));
        }
    }
}
