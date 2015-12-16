var Musaranha = Musaranha || (function () {
    function iniciar() {
        $(function () {
            var pathname = window.location.pathname.toLowerCase();
            if (pathname.startsWith('/cliente')) {
                Musaranha.Cliente.iniciar();
            }
            else if (pathname.startsWith('/funcionario')) {
                Musaranha.Funcionario.iniciar();
            }
            else if (pathname.startsWith('/fornecedor')) {
                Musaranha.Fornecedor.iniciar();
            }
            else if (pathname.startsWith('/produto')) {
                Musaranha.Produto.iniciar();
            }
            else if (pathname.startsWith('/compra')) {
                Musaranha.Compra.iniciar();
            }
            else if (pathname.startsWith('/venda')) {
                Musaranha.Venda.iniciar();
            }
        });
    }

    return {
        iniciar: iniciar
    }
})();

Musaranha.iniciar();