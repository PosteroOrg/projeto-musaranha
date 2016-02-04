Musaranha.Produto = Musaranha.Produto || (function () {
    function iniciar() {
        $('button.incluir').off().click(function () {
            abrirModalInclusao();
        });

        $('button.editar').off().click(function () {
            abrirModalEdicao(this);
        });

        $('button.excluir').off().click(function () {
            var $tr = $(this).parents('tr');
            var codProduto = $tr.data('produto');
            var descricao = $tr.find('td').eq(1).text();
            abrirModalExclusao(codProduto, descricao);
        });
    }

    function abrirModalInclusao() {
        var $modal = $('.acao.modal');

        $modal.find('.header').text('Incluir Produto');
        $modal.find('.primary').text('Incluir').off().click(function () {
            incluir();
        });

        $modal.openModal();
    }

    function abrirModalEdicao(button) {
        var $modal = $('.acao.modal');
        var $tds = $(button).parents('tr').find('td');
        var codProduto = $(button).parents('tr').data('produto');

        $modal.find('.header').text('Editar Produto');

        $modal.find('#txtCodigo').val(codProduto);
        $modal.find('#txtDescricao').val($tds.eq(1).text());

        $modal.find('.primary').text('Editar').off().click(function () {
            editar(codProduto);
        });

        $modal.openModal();
    }

    function abrirModalExclusao(codProduto, descricao) {
        var $modal = $('.excluir.modal');
        $modal.find('.info').html('');
        $modal.find('.info').append('<p><b>Código: </b>' + codProduto + '</p>');
        $modal.find('.info').append('<p><b>Descrição: </b>' + descricao + '</p>');

        $modal.find('.primary').off().click(function () {
            excluir(codProduto);
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
                url: '/produto/incluir',
                data: form,
                success: function (produtos) {
                    var $tbody = $('table tbody');
                    $tbody.html(produtos);
                    Materialize.toast('Produto incluído com sucesso', 4000);
                    iniciar();
                },
                error: function () {
                    Materialize.toast('Ocorreu um erro na inclusão do Produto', 4000);
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

    function editar(codProduto) {
        if (validar()) {
            var form = $('form.acao.modal').serializeArray();
            $('form.acao.modal .modal-footer').append(
                '<div class="progress">' +
                  '<div class="indeterminate"></div>' +
                '</div>');
            $.ajax({
                type: 'POST',
                data: form,
                url: '/produto/editar/' + codProduto,
                success: function (produtos) {
                    var $tbody = $('table tbody');
                    $tbody.html(produtos);
                    Materialize.toast('Produto editado com sucesso', 4000);
                    iniciar();
                },
                error: function () {
                    Materialize.toast('Ocorreu um erro na edição do Produto', 4000);
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

    function excluir(codProduto) {
        $('form.excluir.modal .modal-footer').append(
                '<div class="progress">' +
                  '<div class="indeterminate"></div>' +
                '</div>');
        $.ajax({
            type: 'POST',
            url: '/produto/excluir/' + codProduto,
            success: function (produtos) {
                var $tbody = $('table tbody');
                $tbody.html(produtos);
                Materialize.toast('Produto excluído com sucesso', 4000);
                iniciar();
            },
            error: function () {
                Materialize.toast('Ocorreu um erro na exclusão do Produto', 4000);
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

        if (!$('#txtDescricao').val()) {
            $('#txtDescricao').addClass("invalid");
            valido = false;
        }

        return valido;
    }

    return {
        iniciar: iniciar
    }
})();