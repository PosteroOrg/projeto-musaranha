Musaranha.Funcionario = Musaranha.Funcionario || (function () {
    function iniciar() {
        $('.incluir.button').off().click(function () {
            abrirDialogInclusao();
        });

        $('.editar.button').off().click(function () {
            abrirDialogEdicao(this);
        });

        $('.excluir.button').off().click(function () {
            var $tr = $(this).parents('[data-funcionario]');
            var codPessoa = $tr.data('funcionario');
            var nome = $tr.find('td').eq(0).text();
            var categoria = $tr.find('td').eq(2).text();
            abrirDialogExclusao(codPessoa,nome,categoria);
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

    function abrirDialogEdicao(button) {
        var $dialog = $('.acao.dialog');
        var $tr = $(button).parents('[data-funcionario]');
        var codPessoa = $tr.data('funcionario');

        $dialog.find('h1').text('Editar Funcionário');

        $dialog.find('#txtNome').val($tr.find('td').eq(0).text());
        $dialog.find('#txtTelefone').val($tr.find('td').eq(1).text());
        $dialog.find('#txtCategoria').val($tr.find('td').eq(2).text()[0]);
        $dialog.find('#txtIdentidade').val($tr.find('td').eq(3).text());
        $dialog.find('#txtCarteiraTrabalho').val($tr.find('td').eq(4).text());
        $dialog.find('#txtSalario').val($tr.find('td').eq(5).text().split('R$ ').pop());
        $dialog.find('#txtObservacao').text($tr.find('td').eq(6).text());

        $dialog.find('.primary.button').text('Editar').off().click(function () {
            editar(codPessoa);
        });
        $dialog.find('.cancelar.button').off().click(function () {
            $dialog.data('dialog').close();
        });


        $dialog.data('dialog').open();
    }

    function abrirDialogExclusao(codPessoa,nome,categoria) {
        var $dialog = $('.excluir.dialog');
        $dialog.find('.info').html('');
        $dialog.find('.info').append('<p><b>Nome: </b>' + nome + '</p>');
        $dialog.find('.info').append('<p><b>Categoria: </b>' + categoria + '</p>');

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
        var $center = $('.acao.dialog center');
        $center.append('<div data-role="preloader" data-type="ring" data-style="dark"></div>');
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
                iniciar();
            },
            error: function () {
                $.Notify({
                    caption: 'Erro na operação',
                    content: 'Ocorreu um erro na inclusão do Funcionário',
                    type: 'alert'
                });
            },
            complete: function () {
                $center.html('');
                $('.acao.dialog').data('dialog').close();
                $('.acao.dialog form').reset();
            }
        })
    }

    function editar(codPessoa) {
        var $center = $('.acao.dialog center');
        var form = $('.acao.dialog form').serializeArray();
        $center.append('<div data-role="preloader" data-type="ring" data-style="dark"></div>');
        $.ajax({
            type: 'POST',
            data: form,
            url: '/funcionario/Editar/' + codPessoa,
            success: function (funcionarios) {
                var $tbody = $('.table.funcionarios tbody');
                $tbody.html(funcionarios);
                $.Notify({
                    caption: 'Operação Realizada!',
                    content: 'Funcionário editado com sucesso',
                    type: 'success'
                });
                iniciar();
            },
            error: function () {
                $.Notify({
                    caption: 'Erro na operação',
                    content: 'Ocorreu um erro na edição do Funcionário',
                    type: 'alert'
                });
            },
            complete: function () {
                $center.html('');
                $('.excluir.dialog').data('dialog').close();
            }
        })
    }

    function excluir(codPessoa) {
        var $center = $('.excluir.dialog center');
        $center.append('<div data-role="preloader" data-type="ring" data-style="dark"></div>');
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
                iniciar();
            },
            error: function () {
                $.Notify({
                    caption: 'Erro na operação',
                    content: 'Ocorreu um erro na exclusão do Funcionário',
                    type: 'alert'
                });
            },
            complete: function () {
                $center.html('');
                $('.excluir.dialog').find('.info').html('');
                $('.excluir.dialog').data('dialog').close();
            }
        })
    }

    return {
        iniciar: iniciar
    }
})();