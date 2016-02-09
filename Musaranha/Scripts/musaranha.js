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

    function validarCPF(cpf) {
        var strCPF = cpf.replace(/\D/g, '');
        var Soma;
        var Resto;
        Soma = 0;
        if (strCPF == "00000000000") return false;
        for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
        Resto = (Soma * 10) % 11; if ((Resto == 10) || (Resto == 11)) Resto = 0;
        if (Resto != parseInt(strCPF.substring(9, 10))) return false;
        Soma = 0;
        for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
        Resto = (Soma * 10) % 11; if ((Resto == 10) || (Resto == 11)) Resto = 0;
        if (Resto != parseInt(strCPF.substring(10, 11))) return false;
        return true;
    }

    function validarCNPJ(cnpj) {
        cnpj = cnpj.replace(/\D/g, '');

        if (cnpj == '') return false;

        if (cnpj.length != 14)
            return false;

        if (cnpj == "00000000000000" ||
            cnpj == "11111111111111" ||
            cnpj == "22222222222222" ||
            cnpj == "33333333333333" ||
            cnpj == "44444444444444" ||
            cnpj == "55555555555555" ||
            cnpj == "66666666666666" ||
            cnpj == "77777777777777" ||
            cnpj == "88888888888888" ||
            cnpj == "99999999999999")
            return false;

        tamanho = cnpj.length - 2
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;

        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;

        return true;
    }

    return {
        iniciar: iniciar,
        eDinheiro: eDinheiro,
        eNumero: eNumero,
        reativarMask: ativarMask,
        validarCPF: validarCPF,
        validarCNPJ: validarCNPJ
    }
})();

Musaranha.iniciar();
