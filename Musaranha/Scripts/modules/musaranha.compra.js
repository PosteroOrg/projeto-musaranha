Musaranha.Compra = Musaranha.Compra || (function () {
    function iniciar() {
        $('select').material_select();
        $('.datepicker').pickadate({
            labelMonthNext: 'Próximo mês',
            labelMonthPrev: 'Mês anterior',
            labelMonthSelect: 'Selecione o mês',
            labelYearSelect: 'Selecione o ano',
            monthsFull: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthsShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
            weekdaysFull: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
            weekdaysShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb'],
            weekdaysLetter: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'],
            today: 'Hoje',
            clear: 'Limpar',
            close: 'Fechar'
        });
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