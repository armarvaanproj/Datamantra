using System.Web;
using System.Web.Optimization;

namespace Datamantra.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"));//jquery-1.10.2

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                    "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                        "~/Scripts/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                     "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/Slider").Include(
                        "~/Scripts/slider.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                   "~/Content/ckeditor4/ckeditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/rateitjs").Include(
             "~/Scripts/jquery.rateit.js",
             "~/Scripts/jquery.rateit.min.js"           
             ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/font-face.css",
                      "~/Content/css/Style.css",
                      "~/Content/css/toastr.css",
                      "~/Content/css/jquery-ui.css",
                      "~/Content/css/jquery.ui.autocomplete.css",
                       "~/Content/css/rateit.css"
                      ));
            //   BundleTable.EnableOptimizations = true;

        }
    }
}
