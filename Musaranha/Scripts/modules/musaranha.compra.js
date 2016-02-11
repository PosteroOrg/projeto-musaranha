Musaranha.Compra = Musaranha.Compra || (function () {
    var _ajax;

    function iniciar() {
        $('select').material_select();
        $('.datepicker').pickadate();
        $('.modal-trigger').leanModal();

        $('button.incluir').off('click').click(function () {
            abrirModalInclusao();
        });

        $('button.editar').off('click').click(function () {
            abrirModalEdicao($(this).closest('[data-compra]').data('compra'));
        });

        $('button.excluir').off('click').click(function () {
            var $tr = $(this).parents('tr');
            var compra = $tr.data('compra');
            var data = $tr.find('td').eq(0).text();
            var fornecedor = $tr.find('td').eq(1).text();
            var desconto = $tr.find('td').eq(2).text();
            var valorTotal = $tr.find('td').eq(3).text();
            abrirModalExclusao(compra, data, fornecedor, desconto, valorTotal);
        });

        $('#txtProduto, #txtUnidade').off('change').change(function () {
            novoItem($(this));
        });

        $('#txtQuantidade, #txtPrecoUnitario').off('change').change(function () {
            apresentarValorTotal($(this).closest('div.row'));
            apresentarValorCompra($(this).closest('div.row'));
            novoItem($(this));
        });

        $('input#txtDesconto').off('change').change(function () {
            apresentarValorCompra($(this));
        });

        $('button.itens').off('click').click(function () {
            carregarItens($(this).closest('[data-compra]').data('compra'));
            $('#itens.modal').openModal();
        });

        $('section#filtro :input').off('change').change(function () {
            filtrar();
        });
    }

    function limparFiltro() {
        $('#txtFiltroFornecedor, #txtFiltroProduto').material_select('destroy');
        $('#txtFiltroDataInicio, #txtFiltroDataTermino').val('');
        $('section#filtro select').val(0).material_select();
    }

    function filtrar() {
        var fornecedor = $('#txtFiltroFornecedor').val(),
            produto = $('#txtFiltroProduto').val(),
            dataInicio = $('#txtFiltroDataInicio').val(),
            dataTermino = $('#txtFiltroDataTermino').val();

        if (_ajax && _ajax.readyState != 4) {
            _ajax.abort();
        }

        _ajax = $.ajax({
            type: 'POST',
            url: '/compra/listar',
            data: { fornecedor, produto, dataInicio, dataTermino },
            success: function (compras) {
                var $tbody = $('.table.compras tbody');
                $tbody.html(compras);
                iniciar();
            }
        });
    }

    function abrirModalEdicao(compra) {
        var $modal = $('.editar.modal');

        $.ajax({
            type: 'POST',
            url: '/compra/carregareditar/' + compra,
            success: function (conteudo) {
                $modal.find('.modal-content').html(conteudo);
            },
            error: function () {
                Materialize.toast('Ocorreu um erro no carregamento da Compra', 4000);
            },
            complete: function () {
                iniciar();
                Musaranha.reativarMask();
            }
        });

        $modal.find('.primary').off('click').click(function () {
            editar(compra);
        });

        $modal.openModal();
    }

    function editar(compra) {
        if (validar('form.editar.modal')) {
            var form = $('form.editar.modal').serializeArray();
            $.ajax({
                type: 'POST',
                url: '/compra/editar/' + compra,
                data: form,
                beforeSend: function () {
                    $('form.editar.modal .modal-footer').append(
                        '<div class="progress">' +
                          '<div class="indeterminate"></div>' +
                        '</div>');
                },
                success: function (compras) {
                    var $tbody = $('.table.compras tbody');
                    $tbody.html(compras);
                    Materialize.toast('Compra editada com sucesso', 4000);
                    iniciar();
                    limparFiltro();
                },
                error: function () {
                    Materialize.toast('Ocorreu um erro na edição da Compra', 4000);
                },
                complete: function () {
                    $('form.editar.modal .modal-footer .progress').remove();
                    $('form.editar.modal .modal-content').html(
                        '<div class="progress">' +
                            '<div class="indeterminate"></div>' +
                        '</div>'
                    );
                    $('form.editar.modal').get(0).reset();
                    $('form.editar.modal').closeModal();
                }
            })
        }
        else return false;
    }

    function abrirModalExclusao(compra, data, fornecedor, desconto, valorTotal) {
        var $modal = $('.excluir.modal');
        $modal.find('.info').html('');
        $modal.find('.info').append('<p><b>Data: </b>' + data + '</p>');
        $modal.find('.info').append('<p><b>Fornecedor: </b>' + fornecedor + '</p>');
        $modal.find('.info').append('<p><b>Desconto: </b>' + desconto + '</p>');
        $modal.find('.info').append('<p><b>Valor Total: </b>' + valorTotal + '</p>');

        $modal.find('.primary').off('click').click(function () {
            excluir(compra);
        });

        $modal.openModal();
    }

    function excluir(compra) {
        $.ajax({
            type: 'POST',
            url: '/compra/excluir/' + compra,
            beforeSend: function () {
                $('form.excluir.modal .modal-footer').append(
                   '<div class="progress">' +
                     '<div class="indeterminate"></div>' +
                   '</div>'
               );
            },
            success: function (compras) {
                var $tbody = $('.table.compras tbody');
                $tbody.html(compras);
                Materialize.toast('Compra excluída com sucesso', 4000);
                iniciar();
                limparFiltro();
            },
            error: function () {
                Materialize.toast('Ocorreu um erro na exclusão da Compra', 4000);
            },
            complete: function () {
                $('form.excluir.modal .modal-footer .progress').remove();
                $('form.excluir.modal').find('.info').html('');
                $('form.excluir.modal').closeModal();
            }
        })
    }

    function carregarItens(compra) {
        $.ajax({
            type: 'POST',
            url: '/compra/itens/' + compra,
            success: function (itens) {
                $('#itens.modal .modal-content').html(itens);
            },
            error: function () {
                Materialize.toast('Ocorreu um erro no carregamento de Itens da Compra', 4000);
            }
        });
    }

    function abrirModalInclusao() {
        var $modal = $('.incluir.modal');

        $('select').material_select();

        $modal.find('.header').text('Incluir Compra');
        while ($('form.incluir.modal section.itens div.row').length > 1) {
            $('form.incluir.modal section.itens div.row').last().remove();
        }
        $modal.find('.primary').text('Incluir').off('click').click(function () {
            incluir();
        });

        $modal.openModal();
    }

    function incluir() {
        if (validar('form.incluir.modal')) {
            var form = $('form.incluir.modal').serializeArray();
            $.ajax({
                type: 'POST',
                url: '/compra/incluir',
                data: form,
                beforeSend: function () {
                    $('form.incluir.modal .modal-footer').append(
                        '<div class="progress">' +
                          '<div class="indeterminate"></div>' +
                        '</div>');
                },
                success: function (compras) {
                    var $tbody = $('.table.compras tbody');
                    $tbody.html(compras);
                    Materialize.toast('Compra incluída com sucesso', 4000);
                    iniciar();
                    limparFiltro();
                },
                error: function () {
                    Materialize.toast('Ocorreu um erro na inclusão da Compra', 4000);
                },
                complete: function () {
                    $('form.incluir.modal .modal-footer .progress').remove();
                    while ($('form.incluir.modal section.itens div.row').length > 1) {
                        $('form.incluir.modal section.itens div.row').last().remove();
                    }
                    $('form.incluir.modal').get(0).reset();
                    $('form.incluir.modal').closeModal();
                }
            })
        }
        else return false;
    }

    function validar(modal) {
        var valido = true,
            $modal = $(modal);

        if (!$modal.find('#txtFornecedor').val()) {
            $modal.find('#txtFornecedor').addClass('invalid');
            valido = false;
        }

        if (!$modal.find('#txtData').val()) {
            $modal.find('#txtData').addClass('invalid');
            valido = false;
        }

        if (!checarItem($modal.find('section.itens div.row').first())) {
            valido = false;
        }

        return valido;
    }

    function novoItem($this) {
        var $section = $this.closest('section.itens');
        var $row = $this.closest('div.row');

        var $lastRow = $section.find('div.row').last();

        if (checarItem($lastRow)) {
            var $cloneRow = $lastRow.clone();
            var $select = $cloneRow.find('#txtProduto').clone();
            $cloneRow.find('#txtProduto').closest('div.select-wrapper').remove();
            $cloneRow.find('div.input-control').first().append($select);

            var inputs = $cloneRow.find(':input');

            for (var i = 0, length = inputs.length; i < length; i++) {
                var n = parseInt(inputs.eq(i).attr('name').replace(/\D/g, ''));
                var name = inputs.eq(i).attr('name').replace(/\d/g, '') + (++n);
                inputs.eq(i).val('');
                inputs.eq(i).attr('name', name);
            }

            $section.append($cloneRow);
            iniciar();
            Musaranha.reativarMask();
        }
    }

    function checarItem($row) {
        var produto = $row.find('#txtProduto').val();
        var unidade = $row.find('#txtUnidade').val();
        var quantidade = $row.find('#txtQuantidade').val();
        var precoUnitario = $row.find('#txtPrecoUnitario').val();
        return (produto && unidade && quantidade && precoUnitario);
    }

    function apresentarValorTotal($row) {
        var quantidade = $row.find('#txtQuantidade').val();
        var precoUnitario = $row.find('#txtPrecoUnitario').val();
        if (quantidade && precoUnitario) {
            var valorTotal = parseFloat(quantidade) * parseFloat(precoUnitario.replace('.', '').replace(',', '.'));
            $row.find('#txtValorTotal').val(valorTotal.toFixed(2));
            Musaranha.reativarMask();
        }
    }

    function apresentarValorCompra($object) {
        var valorCompra = 0.0;
        var valorDesconto = parseFloat($object.closest('.modal').find('#txtDesconto').val().replace('.', '').replace(',', '.'));

        var $rows = $object.closest('.modal').find('div.row');

        for (var i = 0, length = $rows.length; i < length; i++) {
            var valorTotal = $rows.eq(i).find('#txtValorTotal').val();
            if (valorTotal) {
                valorCompra += parseFloat(valorTotal.replace('.', '').replace(',', '.'));
            }
        }
        $object.closest('.modal').find('#txtCompraValor').val((valorCompra - valorDesconto).toFixed(2));
        Musaranha.reativarMask();
    }

    return {
        iniciar: iniciar
    }
})();