using System.Web;
using System.Web.Optimization;

namespace WiserSoft.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Clear();
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Admin/css").Include(
                     "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/Admin").Include(
                       "~/Content/Admin/bootstrap.css",
                       "~/Content/Admin/sb-admin.css",
                       "~/Content/Admin/morris.css",
                       "~/Content/Admin/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/Admin").Include(
                        "~/Scripts/Admin/jquery.js",
                        "~/Scripts/Admin/bootstrap.js",
                        "~/Scripts/Admin/raphael.min.js",
                        "~/Scripts/Admin/morris.min.js",
                        "~/Scripts/Admin/morris-data.js"));

            bundles.Add(new StyleBundle("~/Content/Cliente").Include(
                       "~/Content/Cliente/card.min.css",
                       "~/Content/Cliente/styles.min.css",
                       "~/Content/Cliente/vendor.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/Cliente").Include(
                   "~/Scripts/Cliente/card.min.js",
                   "~/Scripts/Cliente/modernizr.min.js",
                   "~/Scripts/Cliente/scripts.min.js",
                   "~/Scripts/Cliente/vendor.min.js"));
        }
    }
}
