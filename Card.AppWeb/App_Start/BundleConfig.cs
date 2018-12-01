using System.Web;
using System.Web.Optimization;

namespace Card.AppWeb
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                     "~/Content/angular-material.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angularJs").Include(
                      "~/Scripts/AngularJs/angular.min.js",
                      "~/Scripts/AngularJs/angular-route.min.js",
                      "~/Scripts/AngularJs/angular-aria.min.js",
                      "~/Scripts/AngularJs/angular-animate.min.js",
                      "~/Scripts/AngularJs/angular-material.min.js",
                      "~/Scripts/AngularJs/angular-messages.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/App").Include(
                     "~/App/App.js",
                     "~/App/services/commonService.js",
                     "~/App/services/httpService.js",
                     "~/App/services/queryStringService.js",
                     "~/App/Home/home.ctrl.js"
                            ));
            BundleTable.EnableOptimizations = false;
        }
    }
}
