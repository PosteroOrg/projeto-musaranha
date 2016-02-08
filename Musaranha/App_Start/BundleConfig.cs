using System.Web.Optimization;

namespace Musaranha
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/scripts/plugins/jquery.mask-1.13.4.min.js",
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
        }
    }
}