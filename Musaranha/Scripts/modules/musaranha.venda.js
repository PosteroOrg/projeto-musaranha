Musaranha.Venda = Musaranha.Venda || (function () {
    function iniciar() {
        $('select').material_select();
        $('.datepicker').pickadate();
        $('.modal-trigger').leanModal();


        $('button.incluir').off('click').click(function () {
            abrirModalInclusao();
        });

        $('button.editar').off('click').click(function () {
            $('.acao.modal').openModal();
        });

        $('button.excluir').off('click').click(function () {
            $('.excluir.modal').openModal();
        });

        $('#txtProduto, #txtUnidade').off('change').change(function () {
            novoItem($(this));
        });

        $('#txtQuantidade, #txtPrecoUnitario').off('change').change(function () {
            apresentarValorTotal($(this).closest('div.row'));
            apresentarValorVenda($(this).closest('div.row'));
            novoItem($(this));
        });

        $('#txtDesconto').off('change').change(function () {
            apresentarValorVenda($(this));
        });

        $('button.itens').off('click').click(function () {
            carregarItens($(this).closest('[data-venda]').data('venda'));
            $('#itens.modal').openModal();
        });
    }

    function carregarItens(venda) {
        $.ajax({
            type: 'POST',
            url: '/venda/itens/' + venda,
            success: function (itens) {
                $('#itens.modal .modal-content').html(itens);
            },
            error: function () {
                Materialize.toast('Ocorreu um erro no carregamento de Itens da Venda', 4000);
            }
        });
    }

    function abrirModalInclusao() {
        var $modal = $('.incluir.modal');

        $('select').material_select();

        $modal.find('.header').text('Incluir Venda');
        $modal.find('.primary').text('Incluir').off().click(function () {
            incluir();
        });

        $modal.openModal();
    }

    function incluir() {
        if (validar('form.incluir.modal')) {
            var form = $('form.incluir.modal').serializeArray();
            $.ajax({
                type: 'POST',
                url: '/venda/incluir',
                data: form,
                beforeSend: function () {
                    $('form.incluir.modal .modal-footer').append(
                        '<div class="progress">' +
                          '<div class="indeterminate"></div>' +
                        '</div>');
                },
                success: function (vendas) {
                    var $tbody = $('.table.vendas tbody');
                    $tbody.html(vendas);
                    Materialize.toast('Venda incluída com sucesso', 4000);
                    iniciar();
                },
                error: function () {
                    Materialize.toast('Ocorreu um erro na inclusão da Venda', 4000);
                },
                complete: function () {
                    $('form.incluir.modal .modal-footer .progress').remove();
                    while($('form.incluir.modal section.itens div.row').length > 1) {
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

        if (!$modal.find('#txtCliente').val()) {
            $modal.find('#txtCliente').addClass('invalid');
            valido = false;
        }

        if (!$modal.find('#txtData').val()) {
            $modal.find('#txtData').addClass('invalid');
            valido = false;
        }

        if (!checarItem($modal.find('section.itens div.row').first()))
        {
            valido = false;
        }

        return valido;
    }

    function novoItem($this) {
        var $section = $this.closest('section.itens');
        var $row = $this.closest('div.row');

        var $lastRow = $section.find('div.row').last();

        if (checarItem($lastRow)) {
            var $cloneRow = $row.clone();
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

    function apresentarValorVenda($object) {
        var valorVenda = 0.0;
        var valorDesconto = parseFloat($object.closest('.modal').find('#txtDesconto').val().replace('.', '').replace(',', '.'));

        var $rows = $object.closest('.modal').find('div.row');

        for (var i = 0, length = $rows.length; i < length; i++) {
            var valorTotal = $rows.eq(i).find('#txtValorTotal').val();
            if (valorTotal) {
                valorVenda += parseFloat(valorTotal.replace('.', '').replace(',', '.'));
            }
        }
        $object.closest('.modal').find('#txtVendaValor').val((valorVenda - valorDesconto).toFixed(2));
        Musaranha.reativarMask();
    }

    return {
        iniciar: iniciar
    }
})();