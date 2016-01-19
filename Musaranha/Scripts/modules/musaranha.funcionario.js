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

        ajustarTamanhoDoConteudo();
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
        if (validarFormulario()) {
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
                    $('.acao.dialog form .cancelar.button').click();
                }
            })
        }
        else return false;
    }

    function editar(codPessoa) {
        if (validarFormulario()) {
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
                    $('.acao.dialog').data('dialog').close();
                }
            })
        }
        else return false;
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

    function ajustarTamanhoDoConteudo() {
        var $conteudo = $('#cell-content');
        var $appBar = $('[data-role="appbar"');
        var appBarAltura = $appBar.height();
        var documentoAltura = $(window).height();

        $conteudo.css('max-height', documentoAltura - appBarAltura)
                 .css('overflow-y', 'auto');
    }

    function validarFormulario() {
        var valido = true;
        var $form = $('form');
        var $listaErro = $('<div class="lista erro padding10 bg-red fg-white"></div>');

        $form.find('.lista.erro').remove();

        if (!$('#txtNome').val()) {
            $listaErro.append('<li>Preencha o campo Nome</li>');
            valido = false;
        }
        if (!$('#txtTelefone').val()) {
            $listaErro.append('<li>Preencha o campo Telefone</li>');
            valido = false;
        }
        if (!$('#txtIdentidade').val()) {
            $listaErro.append('<li>Preencha o campo Identidade</li>');
            valido = false;
        }
        if (!$('#txtCarteiraTrabalho').val()) {
            $listaErro.append('<li>Preencha o campo Carteira de Trabalho</li>');
            valido = false;
        }
        if (!$('#txtSalario').val()) {
            $listaErro.append('<li>Preencha o campo Salário</li>');
            valido = false;
        }
        if(!Musaranha.EDinheiro($('#txtSalario').val())){
            $listaErro.append('<li>O campo Salário tem que ser numérico</li>');
            valido = false;
        }
        if (!valido) {
            $form.prepend($listaErro);
            return false;
        }
        
        return true;
    }

    return {
        iniciar: iniciar
    }
})();