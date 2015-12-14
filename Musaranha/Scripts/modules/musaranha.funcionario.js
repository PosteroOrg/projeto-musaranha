Musaranha.Funcionario = Musaranha.Funcionario || (function () {
    function inciar() {
        $('.cadastrar.button').click(function () {
            abrirDialogCadastro();
        });
        alert('carregou');
    }

    function abrirDialogCadastro() {
        var $dialog = $('[data-role=dialog]');

        $dialog.find('h1').text('Cadastrar Funcionário');

        $dialog.open();
    }

    return {
        inciar: iniciar
    }
})();