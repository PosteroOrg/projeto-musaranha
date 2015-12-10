using System.Web.Optimization;

namespace Musaranha
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/libs/jquery-{version}.js",
                "~/Scripts/libs/metro.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/metro.css",
                "~/Content/metro-icons.css",
                "~/Content/metro-responsive.css"));
        }
    }
}