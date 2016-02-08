Musaranha.Fornecedor = Musaranha.Fornecedor || (function () {
    function iniciar() {
        $('select').material_select();

        $('button.incluir').off().click(function () {
            abrirModalInclusao();
        });

        $('button.editar').off().click(function () {
            abrirModalEdicao(this);
        });

        $('button.excluir').off().click(function () {
            var $tr = $(this).parents('tr');
            var codPessoa = $tr.data('fornecedor');
            var nome = $tr.find('td').eq(0).text();
            var telefone = $tr.find('td').eq(1).text();
            abrirModalExclusao(codPessoa, nome, telefone);
        });

        $('table tr').hover(function () {
            var $tr = $(this);
            var $tfoot = $tr.parents('table').find('tfoot');
            $tfoot.find('[data-campo=endereco]').text($tr.data('endereco'));
            $tfoot.find('[data-campo=cpfcnpj]').text($tr.data('cnpj') || $tr.data('cpf'));
            $tfoot.find('[data-campo=email]').text($tr.data('email'));
        });
    }

    function abrirModalInclusao() {
        var $modal = $('.acao.modal');
        $('select').material_select();

        $modal.find('.header').text('Incluir Fornecedor');
        $modal.find('.primary').text('Incluir').off().click(function () {
            incluir();
        });

        $modal.openModal();
    }

    function abrirModalEdicao(button) {
        var $modal = $('.acao.modal'),
            $tds = $(button).parents('tr').find('td'),
            codPessoa = $(button).parents('tr').data('fornecedor');

        $modal.find('.header').text('Editar Fornecedor');

        $.ajax({
            url: '/fornecedor/json/' + codPessoa,
            type: 'POST',
            success: function (fornecedor) {
                $modal.find('#txtNome').val(fornecedor.Nome);
                $modal.find('#txtTelefone').val(fornecedor.Telefone);
                $modal.find('#txtLogradouro').val(fornecedor.Logradouro);
                $modal.find('#txtNumero').val(fornecedor.Numero);
                $modal.find('#txtComplemento').val(fornecedor.Complemento);
                $modal.find('#txtCEP').val(fornecedor.CEP);
                $modal.find('#txtBairro').val(fornecedor.Bairro);
                $modal.find('#txtCidade').val(fornecedor.Cidade);
                $modal.find('#txtEstado').val(fornecedor.Estado);
                $modal.find('#txtTipo').val(fornecedor.Tipo).material_select();
                $modal.find('#txtCPFOuCNPJ').val(fornecedor.CPF || fornecedor.CNPJ);
                $modal.find('#txtEmail').val(fornecedor.Email);

                $modal.find('.primary').text('Editar').off().click(function () {
                    editar(codPessoa);
                });

                Musaranha.reativarMask();

                $modal.openModal();
            }
        });
    }

    function abrirModalExclusao(codPessoa, nome, telefone) {
        var $modal = $('.excluir.modal');
        $modal.find('.info').html('');
        $modal.find('.info').append('<p><b>Nome: </b>' + nome + '</p>');
        $modal.find('.info').append('<p><b>Telefone(s): </b>' + telefone + '</p>');

        $modal.find('.primary').off().click(function () {
            excluir(codPessoa);
        });

        $modal.openModal();
    }

    function incluir() {
        if (validar()) {
            var form = $('form.acao.modal').serializeArray();
            $('form.acao.modal .modal-footer').append(
                '<div class="progress">' +
                  '<div class="indeterminate"></div>' +
                '</div>');
            $.ajax({
                type: 'POST',
                url: '/fornecedor/incluir',
                data: form,
                success: function (fornecedores) {
                    var $tbody = $('table tbody');
                    $tbody.html(fornecedores);
                    Materialize.toast('Fornecedor incluído com sucesso', 4000);
                    iniciar();
                },
                error: function () {
                    Materialize.toast('Ocorreu um erro na inclusão do Fornecedor', 4000);
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
        if (validar()) {
            var form = $('form.acao.modal').serializeArray();
            $('form.acao.modal .modal-footer').append(
                '<div class="progress">' +
                  '<div class="indeterminate"></div>' +
                '</div>');
            $.ajax({
                type: 'POST',
                data: form,
                url: '/fornecedor/editar/' + codPessoa,
                success: function (fornecedores) {
                    var $tbody = $('table tbody');
                    $tbody.html(fornecedores);
                    Materialize.toast('Fornecedor editado com sucesso', 4000);
                    iniciar();
                },
                error: function () {
                    Materialize.toast('Ocorreu um erro na edição do Fornecedor', 4000);
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
        $('form.excluir.modal .modal-footer').append(
                '<div class="progress">' +
                  '<div class="indeterminate"></div>' +
                '</div>');
        $.ajax({
            type: 'POST',
            url: '/fornecedor/excluir/' + codPessoa,
            success: function (fornecedores) {
                var $tbody = $('table tbody');
                $tbody.html(fornecedores);
                Materialize.toast('Fornecedor excluído com sucesso', 4000);
                iniciar();
            },
            error: function () {
                Materialize.toast('Ocorreu um erro na exclusão do Fornecedor', 4000);
            },
            complete: function () {
                $('form.excluir.modal .modal-footer .progress').remove();
                $('form.excluir.modal').find('.info').html('');
                $('form.excluir.modal').closeModal();
            }
        })
    }

    function validar() {
        var valido = true;
        var $form = $('form');

        if (!$('#txtNome').val()) {
            $('#txtNome').addClass("invalid");
            valido = false;
        }
        if (!$('#txtTelefone').val()) {
            $('#txtTelefone').addClass("invalid");
            valido = false;
        }

        return valido;
    }

    return {
        iniciar: iniciar
    }
})();