Musaranha.Funcionario = Musaranha.Funcionario || (function () {
    function iniciar() {
        $('.incluir.button').click(function () {
            abrirDialogCadastro();
        });
    }

    function abrirDialogCadastro() {
        var $dialog = $('.dialog');

        $dialog.find('h1').text('Incluir Funcionário');
        $dialog.find('.primary.button').text('Incluir').click(function () {
            cadastrar();
        });

        $dialog.data('dialog').open();
    }

    function cadastrar() {
        var form = $('form').serialize();

        $.ajax({
            type: 'POST',
            url: '/funcionario/Cadastrar',
            data: form,
            success: function (funcionario) {

            },
            error: function () {
                $.Notify({
                    caption: 'Erro na operação',
                    content: 'Ocorreu um erro na inclusão do Funcionário',
                    type: 'alert'
                });
            }
        })
    }
    function editar() {

    }
    function remover() {

    }

    return {
        iniciar: iniciar
    }
})();