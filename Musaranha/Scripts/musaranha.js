var Musaranha = Musaranha || (function () {
    function iniciar() {
        $(function () {
            var pathname = window.location.pathname.toLowerCase();
            if (pathname.indexOf('/cliente') >= 0) {
                Musaranha.Cliente.iniciar();
            }
            else if (pathname.indexOf('/funcionario') >= 0) {
                Musaranha.Funcionario.iniciar();
            }
            else if (pathname.indexOf('/fornecedor')>=0) {
                Musaranha.Fornecedor.iniciar();
            }
            else if (pathname.indexOf('/produto')>=0) {
                Musaranha.Produto.iniciar();
            }
            else if (pathname.indexOf('/compra')>=0) {
                Musaranha.Compra.iniciar();
            }
            else if (pathname.indexOf('/venda')>=0) {
                Musaranha.Venda.iniciar();
            }
        });
    }

    return {
        iniciar: iniciar
    }
})();

Musaranha.iniciar();
