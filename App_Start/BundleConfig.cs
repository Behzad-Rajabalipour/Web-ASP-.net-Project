using System.Web;
using System.Web.Optimization;

namespace WebApplication12
{
    public class BundleConfig
    {
        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",     // ino add mikonim baraye Ajax.BeginForm dar ("CreateComment","Comment")
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));       // ino add mikonim baraye Ajax.BeginForm dar ("CreateComment","Comment"

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));        // Case sensetive hast. Captial case ro havaset bashe
        }
    }
}
