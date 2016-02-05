Musaranha.Compra = Musaranha.Compra || (function () {
    function iniciar() {
        $('select').material_select();
        $('.datepicker').pickadate();
        $('.modal-trigger').leanModal();


        $('button.incluir').off().click(function () {
            $('.acao.modal').openModal();
        });

        $('button.editar').off().click(function () {
            $('.acao.modal').openModal();
        });

        $('button.excluir').off().click(function () {
            $('.excluir.modal').openModal();
        });
    }

    return {
        iniciar: iniciar
    }
})();