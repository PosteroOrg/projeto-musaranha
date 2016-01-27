Musaranha.Funcionario = Musaranha.Funcionario || (function () {
    function iniciar() {
        $('select').material_select();

        $('button.incluir').off().click(function () {
            abrirModalInclusao();
        });

        $('button.editar').off().click(function () {
            abrirModalEdicao(this);
        });

        $('button.excluir').off().click(function () {
            var $tr = $(this).parents('[data-funcionario]');
            var codPessoa = $tr.data('funcionario');
            var nome = $tr.find('td').eq(0).text();
            var categoria = $tr.find('td').eq(2).text();
            abrirModalExclusao(codPessoa, nome, categoria);
        });
    }

    function abrirModalInclusao() {
        var $modal = $('.acao.modal');

        $modal.find('.header').text('Incluir Funcionário');
        $modal.find('.primary').text('Incluir').off().click(function () {
            incluir();
        });

        $modal.openModal();
    }

    function abrirModalEdicao(button) {
        var $modal = $('.acao.modal');
        var $tds = $(button).parents('[data-funcionario]').find('td');
        var codPessoa = $(button).parents('[data-funcionario]').data('funcionario');

        $modal.find('.header').text('Editar Funcionário');

        $modal.find('#txtNome').val($tds.eq(0).text());
        $modal.find('#txtTelefone').val($tds.eq(1).text());
        $modal.find('#txtCategoria').val($tds.eq(2).text()[0]);
        $modal.find('#txtIdentidade').val($tds.eq(3).text());
        $modal.find('#txtCarteiraTrabalho').val($tds.eq(4).text());
        $modal.find('#txtSalario').val($tds.eq(5).text().split('R$ ').pop());
        $modal.find('#txtObservacao').text($tds.eq(6).text());

        $modal.find('.primary').text('Editar').off().click(function () {
            editar(codPessoa);
        });

        $modal.openModal();
    }

    function abrirModalExclusao(codPessoa, nome, categoria) {
        var $modal = $('.excluir.modal');
        $modal.find('.info').html('');
        $modal.find('.info').append('<p><b>Nome: </b>' + nome + '</p>');
        $modal.find('.info').append('<p><b>Categoria: </b>' + categoria + '</p>');

        $modal.find('.primary').off().click(function () {
            excluir(codPessoa);
        });

        $modal.openModal();
    }

    function incluir() {
        if (validarFormulario()) {
            var form = $('form.acao.modal').serializeArray();
            $('form.modal .modal-footer').append(
                '<div class="progress">' +
                  '<div class="indeterminate"></div>' +
                '</div>');
            $.ajax({
                type: 'POST',
                url: '/funcionario/incluir',
                data: form,
                success: function (funcionarios) {
                    var $tbody = $('.table.funcionarios tbody');
                    $tbody.html(funcionarios);
                    Materialize.toast('Funcionário incluído com sucesso', 4000);
                    iniciar();
                },
                error: function () {
                    Materialize.toast('Ocorreu um erro na inclusão do Funcionário', 4000);
                },
                complete: function () {
                    $('form.acao.modal .modal-footer .progress').remove();
                    $('form.acao.modal').get(0).reset();
                    $('form.acao.modal').closeModal();
                }
            })
        }
        else return false;
    }

    function editar(codPessoa) {
        if (validarFormulario()) {
            var form = $('form.acao.modal').serializeArray();
            $('form.modal .modal-footer').append(
                '<div class="progress">' +
                  '<div class="indeterminate"></div>' +
                '</div>');
            $.ajax({
                type: 'POST',
                data: form,
                url: '/funcionario/editar/' + codPessoa,
                success: function (funcionarios) {
                    var $tbody = $('.table.funcionarios tbody');
                    $tbody.html(funcionarios);
                    Materialize.toast('Funcionário editado com sucesso', 4000);
                    iniciar();
                },
                error: function () {
                    Materialize.toast('Ocorreu um erro na edição do Funcionário', 4000);
                },
                complete: function () {
                    $('form.acao.modal .modal-footer .progress').remove();
                    $('form.acao.modal').get(0).reset();
                    $('form.acao.modal').closeModal();
                }
            })
        }
        else return false;
    }

    function excluir(codPessoa) {
        $('.excluir.modal .modal-footer').append(
                '<div class="progress">' +
                  '<div class="indeterminate"></div>' +
                '</div>');
        $.ajax({
            type: 'POST',
            url: '/funcionario/excluir/'+codPessoa,
            success: function (funcionarios) {
                var $tbody = $('.table.funcionarios tbody');
                $tbody.html(funcionarios);
                Materialize.toast('Funcionário excluído com sucesso', 4000);
                iniciar();
            },
            error: function () {
                Materialize.toast('Ocorreu um erro na exclusão do Funcionário', 4000);
            },
            complete: function () {
                $('.excluir.modal .modal-footer .progress').remove();
                $('.excluir.modal').find('.info').html('');
                $('.excluir.modal').closeModal();
            }
        })
    }

    function validarFormulario() {
        var valido = true;
        var $form = $('form');
        //var $listaErro = $('<div class="lista erro padding10 bg-red fg-white"></div>');

        //$form.find('.lista.erro').remove();

        if (!$('#txtNome').val()) {
            $('#txtNome').addClass("invalid");
            //$listaErro.append('<li>Preencha o campo Nome</li>');
            valido = false;
        }
        if (!$('#txtTelefone').val()) {
            $('#txtTelefone').addClass("invalid");
            //$listaErro.append('<li>Preencha o campo Telefone</li>');
            valido = false;
        }
        if (!$('#txtIdentidade').val()) {
            $('#txtIdentidade').addClass("invalid");
            //$listaErro.append('<li>Preencha o campo Identidade</li>');
            valido = false;
        }
        if (!$('#txtCarteiraTrabalho').val()) {
            $('#txtCarteiraTrabalho').addClass("invalid");
            //$listaErro.append('<li>Preencha o campo Carteira de Trabalho</li>');
            valido = false;
        }
        if (!$('#txtSalario').val()) {
            $('#txtSalario').addClass("invalid");
            //$listaErro.append('<li>Preencha o campo Salário</li>');
            valido = false;
        }
        if (!Musaranha.eDinheiro($('#txtSalario').val())) {
            $('#txtSalario').addClass("invalid");
            //$listaErro.append('<li>O campo Salário tem que ser numérico</li>');
            valido = false;
        }
        if (!$('#txtCategoria :selected').val()) {
            //$listaErro.append('<li>Preencha o campo Salário</li>');
            valido = false;
        }
        
        return valido;
    }

    return {
        iniciar: iniciar
    }
})();