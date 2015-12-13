using System.Web.Optimization;

namespace Musaranha
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/libs/jquery-{version}.js",
                "~/Scripts/libs/metro.js",
                "~/Scripts/musaranha.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cliente").Include(
                "~/Scripts/modules/musaranha.cliente.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/funcionario").Include(
                "~/Scripts/modules/musaranha.funcionario.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/fornecedor").Include(
                "~/Scripts/modules/musaranha.fornecedor.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/produto").Include(
                "~/Scripts/modules/musaranha.produto.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/compra").Include(
                "~/Scripts/modules/musaranha.compra.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/venda").Include(
                "~/Scripts/modules/musaranha.venda.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/metro.css",
                "~/Content/metro-icons.css",
                "~/Content/metro-responsive.css"));
        }
    }
}