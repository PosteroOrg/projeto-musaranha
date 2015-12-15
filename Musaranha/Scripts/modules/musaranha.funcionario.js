Musaranha.Funcionario = Musaranha.Funcionario || (function () {
    function iniciar() {
        $('.incluir.button').click(function () {
            abrirDialogInclusao();
        });

        $('.editar.button').click(function () {
            abrirDialogEdicao();
        });

        $('.excluir.button').click(function () {
            var codPessoa = $(this).parents('[data-funcionario]').data('funcionario');
            abrirDialogExclusao(codPessoa);
        });
    }

    function abrirDialogInclusao() {
        var $dialog = $('.acao.dialog');

        $dialog.find('h1').text('Incluir Funcionário');
        $dialog.find('.primary.button').text('Incluir').off().click(function () {
            incluir();
        });
        $dialog.find('.cancelar.button').off().click(function () {
            $dialog.data('dialog').close();
        });


        $dialog.data('dialog').open();
    }

    function abrirDialogEdicao() {
        var $dialog = $('.acao.dialog');

        $dialog.find('h1').text('Editar Funcionário');
        $dialog.find('.primary.button').text('Editar').off().click(function () {
            editar();
        });
        $dialog.find('.cancelar.button').off().click(function () {
            $dialog.data('dialog').close();
        });


        $dialog.data('dialog').open();
    }

    function abrirDialogExclusao(codPessoa) {
        var $dialog = $('.excluir.dialog');

        $dialog.find('.primary.button').off().click(function () {
            excluir(codPessoa);
        });
        $dialog.find('.cancelar.button').off().click(function () {
            $dialog.data('dialog').close();
        });

        $dialog.data('dialog').open();
    }

    function incluir() {
        var form = $('form').serializeArray();
        var $button = $('.acao.dialog').find('primary button');
        $.ajax({
            type: 'POST',
            url: '/funcionario/Incluir',
            data: form,
            success: function (funcionarios) {
                var $tbody = $('.table.funcionarios tbody');
                $tbody.html(funcionarios);
                $.Notify({
                    caption: 'Operação Realizada!',
                    content: 'Funcionário incluído com sucesso',
                    type: 'success'
                });
            },
            error: function () {
                $.Notify({
                    caption: 'Erro na operação',
                    content: 'Ocorreu um erro na inclusão do Funcionário',
                    type: 'alert'
                });
            },
            complete: function () {
                $('.acao.dialog').data('dialog').close();
            }
        })
    }

    function editar() {

    }

    function excluir(codPessoa) {
        var $button = $('dialog').find('primary button');
        $.ajax({
            type: 'POST',
            url: '/funcionario/Excluir/'+codPessoa,
            success: function (funcionarios) {
                var $tbody = $('.table.funcionarios tbody');
                $tbody.html(funcionarios);
                $.Notify({
                    caption: 'Operação Realizada!',
                    content: 'Funcionário excluído com sucesso',
                    type: 'success'
                });
            },
            error: function () {
                $.Notify({
                    caption: 'Erro na operação',
                    content: 'Ocorreu um erro na exclusão do Funcionário',
                    type: 'alert'
                });
            },
            complete: function () {
                $('.excluir.dialog').data('dialog').close();
            }
        })
    }

    return {
        iniciar: iniciar
    }
})();