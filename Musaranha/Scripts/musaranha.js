var Musaranha = Musaranha || (function () {
    function iniciar() {
        $.extend($.fn.pickadate.defaults, {
            labelMonthNext: 'Próximo mês',
            labelMonthPrev: 'Mês anterior',
            labelMonthSelect: 'Selecione o mês',
            labelYearSelect: 'Selecione o ano',
            monthsFull: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthsShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
            weekdaysFull: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
            weekdaysShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb'],
            weekdaysLetter: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'],
            today: 'Hoje',
            clear: 'Limpar',
            close: 'Fechar',
            format: 'd !de mmmm !de yyyy',
            formatSubmit: 'yyyy-mm-dd'
        });

        $(function () {
            ativarMask();
            carregar();
        });
    }

    function carregar() {
        var pathname = window.location.pathname.toLowerCase();
        if (pathname.indexOf('/cliente') == 0) {
            Musaranha.Cliente.iniciar();
        }
        else if (pathname.indexOf('/funcionario') == 0) {
            if (pathname.indexOf('/pagamento') >= 0) {
                Musaranha.Funcionario.Pagamento.iniciar();
            }
            else {
                Musaranha.Funcionario.iniciar();
            }
        }
        else if (pathname.indexOf('/fornecedor') == 0) {
            Musaranha.Fornecedor.iniciar();
        }
        else if (pathname.indexOf('/produto') == 0) {
            Musaranha.Produto.iniciar();
        }
        else if (pathname.indexOf('/compra') == 0) {
            Musaranha.Compra.iniciar();
        }
        else if (pathname.indexOf('/venda') == 0) {
            Musaranha.Venda.iniciar();
        }
    }

    function ativarMask() {
        $('.mask-cep').unmask().mask('00000-000');
        $('.mask-dinheiro').unmask().mask('#.##0,00', { reverse: true });
        $('.mask-numero').unmask().mask('#0', { reverse: true });

        var telefoneMaskBehavior = function (val) {
            return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
        },
	    telefoneOptions = {
	        onKeyPress: function (val, e, field, options) {
	            field.mask(telefoneMaskBehavior.apply({}, arguments), options);
	        }
	    };
        $('.mask-telefone').unmask().mask(telefoneMaskBehavior, telefoneOptions);

        var cpfCnpjMaskBehavior = function (val) {
            return val.replace(/\D/g, '').length === 11 ? '000.000.000-009' : '00.000.000/0000-00';
        }
        var cpfCnpjOptions = {
            onKeyPress: function (val, e, field, options) {
                field.mask(cpfCnpjMaskBehavior.apply({}, arguments), options);
            }
        }
        $('.mask-cpf-cnpj').unmask().mask(cpfCnpjMaskBehavior, cpfCnpjOptions);
    }

    function eDinheiro(n) {
        var regex = /^[0-9]\d*(((.\d{3}){1})?(\,\d{0,2})?)$/;

        return regex.test(n);
    }

    function eNumero(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    return {
        iniciar: iniciar,
        eDinheiro: eDinheiro,
        eNumero: eNumero,
        reativarMask: ativarMask
    }
})();

Musaranha.iniciar();
