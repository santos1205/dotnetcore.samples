////////////////////////////////////////////////////////////////////////
///////////////            Validações BASE          ///////////////////
///////////////////////////////////////////////////////////////////////


//adiciona mascara de cnpj
function MascaraCNPJ(cnpj) {
    if (mascaraInteiro(cnpj) == false) {
        event.returnValue = false;
    }
    return formataCampo(cnpj, '00.000.000/0000-00', event);
}

function carregaCombos(obj, idCombo) {
    obj.forEach(function (item, index) {
        var option = $('<option />');
        option.attr('value', item.Id).text(item.Descricao);
        $('#' + idCombo).append(option);
    });
}


function mascaraMoeda(valor) {
    tam = valor.length;
    if (tam <= 2) {
        return valor;
    }
    if ((tam > 2) && (tam <= 6)) {
        return valor.replace(".", ",");
    }
    if ((tam > 6) && (tam <= 9)) {
        return valor.substr(0, tam - 6) + '.' + valor.substr(tam - 6, 3) + ',' + valor.substr(tam - 2, tam);
    }
    if ((tam > 9) && (tam <= 12)) {
        return valor.substr(0, tam - 9) + '.' + valor.substr(tam - 9, 3) + '.' + valor.substr(tam - 6, 3) + ',' + valor.substr(tam - 2, tam);
    }
}

//adiciona mascara de cep
function MascaraCep(cep) {
    if (mascaraInteiro(cep) == false) {
        event.returnValue = false;
    }
    return formataCampo(cep, '00.000-000', event);
}

//adiciona mascara de data
function MascaraData(data) {
    if (mascaraInteiro(data) == false) {
        event.returnValue = false;
    }
    return formataCampo(data, '00/00/0000', event);
}

//adiciona mascara ao telefone
function MascaraTelefone(tel) {
    if (mascaraInteiro(tel) == false) {
        event.returnValue = false;
    }
    return formataCampo(tel, '(00) 0000-0000', event);
}

//adiciona mascara ao CPF
function MascaraCPF(cpf) {
    if (mascaraInteiro(cpf) == false) {
        event.returnValue = false;
    }
    return formataCampo(cpf, '000.000.000-00', event);
}

//valida telefone
function ValidaTelefone(tel) {
    exp = /\(\d{2}\)\ \d{4}\-\d{4}/
    if (!exp.test(tel.value))
        alert('Numero de Telefone Invalido!');
}

//valida CEP
function ValidaCep(cep) {
    exp = /\d{2}\.\d{3}\-\d{3}/
    if (!exp.test(cep.value))
        alert('Numero de Cep Invalido!');
}

//valida numero inteiro com mascara
function mascaraInteiro() {
    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
        return false;
    }
    return true;
}

//formata de forma generica os campos
function formataCampo(campo, Mascara, evento) {
    var boleanoMascara;

    var Digitato = evento.keyCode;
    exp = /\-|\.|\/|\(|\)| /g
    campoSoNumeros = campo.value.toString().replace(exp, "");

    var posicaoCampo = 0;
    var NovoValorCampo = "";
    var TamanhoMascara = campoSoNumeros.length;;

    if (Digitato != 8) { // backspace 
        for (i = 0; i <= TamanhoMascara; i++) {
            boleanoMascara = ((Mascara.charAt(i) == "-") || (Mascara.charAt(i) == ".")
                                                    || (Mascara.charAt(i) == "/"))
            boleanoMascara = boleanoMascara || ((Mascara.charAt(i) == "(")
                                                    || (Mascara.charAt(i) == ")") || (Mascara.charAt(i) == " "))
            if (boleanoMascara) {
                NovoValorCampo += Mascara.charAt(i);
                TamanhoMascara++;
            } else {
                NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
                posicaoCampo++;
            }
        }
        campo.value = NovoValorCampo;
        return true;
    } else {
        return true;
    }
}


//****************************************************************************************


function fomatarMoeda(numero) {
    var numero = numero.toFixed(2).split('.');
    numero[0] = numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}

function validaEmail(idCampoEmail) {
    var validarPassou = true;

    var txtEmail = $("#" + idCampoEmail.id);
    var posP = txtEmail.val().indexOf(".");
    var posA = txtEmail.val().indexOf("@");

    if (txtEmail.val() == "" || txtEmail.val().length < 2 || txtEmail.val() == "usuario@dominio.com.br") {
        $("#" + idCampoEmail.id).css("border-color", "red");
        $("#SP" + idCampoEmail.id).show();
        $("#SP" + idCampoEmail.id).text("Insira um endereço de email!");
        validarPassou = false;
    } else if (posA == -1) {
        $("#" + idCampoEmail.id).css("border-color", "red");
        $("#SP" + idCampoEmail.id).text("Email Inválido, por favor verifique!");
        $("#SP" + idCampoEmail.id).show();
        validarPassou = false;
    } else if (posP == -1) {
        $("#" + idCampoEmail.id).css("border-color", "red");
        $("#SP" + idCampoEmail.id).text("Email Inválido, por favor verifique!");
        $("#SP" + idCampoEmail.id).show();
        validarPassou = false;
    } else {
        $("#" + idCampoEmail.id).css("border-color", "");
        $("#SP" + idCampoEmail.id).hide();
        $("#SP" + idCampoEmail.id).text("");
    }
    return validarPassou;
}


//Máscara CEP
$('.cep').mask("9?9999-999").focusout(function (e) {
    var target, cep, elemento;
    target = $(this);
    cep = target.val().replace(/\D/g, ''); //Remove tudo que não é digito
    elemento = $(target);
    if (cep.length > 10) {
        elemento.mask("9?9999-999");
    } else {
        elemento.mask("9?9999-999");
    }
});


// Somente Números
$('.somente-nr').on('keydown.mask', function (e) {
    var target, rg, elemento;
    target = $(this);
    rg = target.val().replace(/\D/g, ''); //Remove tudo que não é digito
    elemento = $(target);
    elemento.mask("9?9999999");
    //if (rg.length > 9) {
    //    elemento.mask("9?9.999.999-9");
    //} else {
    //    elemento.mask("9?9.999.999-9");
    //}
});


// Máscara RG
$('.mask-rg').on('keydown.mask', function (e) {
    var target, rg, elemento;
    target = $(this);
    rg = target.val().replace(/\D/g, ''); //Remove tudo que não é digito
    elemento = $(target);
    if (rg.length > 9) {
        elemento.mask("9?9.999.999-9");
    } else {
        elemento.mask("9?9.999.999-9");
    }
});


//Máscara Telefone celular
$('.celular').mask("(99) 9?999-99999").focusout(function (e) {
    var target, telefone, elemento;
    target = $(this);
    telefone = target.val().replace(/\D/g, ''); //Remove tudo que não é digito
    elemento = $(target);
    if (telefone.length > 10) {
        elemento.mask("(99) 9?999-99999");
    } else {
        elemento.mask("(99) 9?999-99999");
    }
});


function converteData(vlrData) {
    var dataNasc = vlrData.split('/');
    dataNasc = dataNasc[1] + '-' + dataNasc[0] + '-' + dataNasc[2];
    dataNasc = new Date(dataNasc);
    return dataNasc;
}

//onde c é o valor do cpf sem máscara.
function verificarCPF(c) {
    var i;
    s = c;
    var c = s.substr(0, 9);
    var dv = s.substr(9, 2);
    var d1 = 0;
    var v = false;

    for (i = 0; i < 9; i++) {
        d1 += c.charAt(i) * (10 - i);
    }
    if (d1 == 0) {
        v = true;
        return false;
    }
    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(0) != d1) {
        v = true;
        return false;
    }

    d1 *= 2;
    for (i = 0; i < 9; i++) {
        d1 += c.charAt(i) * (11 - i);
    }
    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(1) != d1) {
        v = true;
        return false;
    }
    return true;
}

function validarCNPJ(idCampo) {

    var c = $('#' + idCampo.id).val().replace(/[^\d]+/g, '');
    var b = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    if (c.length != 14)
        return false;

    if (/0{14}/.test(c))
        return false;

    for (var i = 0, n = 0; i < 12; n += c[i] * b[++i]);
    if (c[12] != (((n %= 11) < 2) ? 0 : 11 - n))
        return false;

    for (var i = 0, n = 0; i <= 12; n += c[i] * b[i++]);
    if (c[13] != (((n %= 11) < 2) ? 0 : 11 - n))
        return false;

    return true;
}

function CampoValido(campo, nmSpan) {
    $("#" + nmSpan).hide();
    $(campo).css("border-color", "");

}


