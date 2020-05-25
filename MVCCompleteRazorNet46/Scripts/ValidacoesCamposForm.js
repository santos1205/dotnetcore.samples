////////////////////////////////////////////////////////////////////////
///////////////            Validações BASE          ///////////////////
///////////////////////////////////////////////////////////////////////


// ex: (2).formatNumber()
Number.prototype.formatNumber = function (size) {
    var s = String(this);
    while (s.length < (size || 2)) { s = "0" + s; }
    return s;
}


function LastDayOfMonth(y, m) {
    return new Date(y, m, 0).getDate();
}


function formatDateToBackend(Date) {
    arrAux = Date.split('/');
    return `${arrAux[1]}/${arrAux[0]}/${arrAux[2]}`;
}

function getUrlParams(url, param = null) {
    if (param == null)
        return url;
    let arrUrl = url.split('&');
    let rsValue;
    arrUrl.forEach(function (item) {
        // console.log(item);
        if (item.includes(param))
            rsValue = item.split('=')[1];        
    });
    return rsValue;
}

function RemoverArrayElement(FormShareSelected, value) {
    var index = FormShareSelected.indexOf(value);
    if (index !== -1) FormShareSelected.splice(index, 1);
    return FormShareSelected;
}

//adiciona mascara de cnpj
function MascaraCNPJ(cnpj) {
    if (mascaraInteiro(cnpj) == false) {
        event.returnValue = false;
    }
    return formataCampo(cnpj, '00.000.000/0000-00', event);
}

function somenteNumeros(nr) {
    if (mascaraInteiro(nr) == false) {
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

function isNullOrEmpty(value) {
    if (value == '')
        return true;
    if (value == null)
        return true;
    if (value == 'undefined')
        return true;

    return false;
}

function PrimeiraLetraMaiuscula(text) {
    var loweredText = text.toLowerCase();
    var words = loweredText.split(" ");
    for (var a = 0; a < words.length; a++) {
        var w = words[a];

        var firstLetter = w[0];
        if (isNullOrEmpty(firstLetter) == false)
            w = firstLetter.toUpperCase() + w.slice(1);

        words[a] = w;
    }
    return words.join(" ");
}


function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};

function calculaDias(dataInicial, dataFinal) {
    var date1 = new Date(dataInicial);
    var date2 = new Date(dataFinal);
    var timeDiff = Math.abs(date2.getTime() - date1.getTime());
    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return diffDays;
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

//adiciona mascara de data
function MascaraDataMesAno(data) {
    if (mascaraInteiro(data) == false) {
        event.returnValue = false;
    }
    return formataCampo(data, '00/0000', event);
}

//adiciona mascara ao telefone
function MascaraTelefone(tel) {
    if (mascaraInteiro(tel) == false) {
        event.returnValue = false;
    }
    return formataCampo(tel, '(00) 0000-0000', event);
}

function MascaraPis(pis) {
    if (mascaraInteiro(pis) == false) {
        event.returnValue = false;
    }
    return formataCampo(pis, '000.00000.00-0', event);
}

function MascaraDN(dn) {
    if (mascaraInteiro(dn) == false) {
        event.returnValue = false;
    }
    return formataCampo(dn, '00-0000000-00', event);
}

function DateSerializer(Date) {
    if (Date != null)
        if (Date.length > 0) {
            Date = Date.split('/');
            Date = Date[1] + '-' + Date[0] + '-' + Date[2];
            return Date;
        } else
            return null;
}


function MascaraCNS(cns) {
    if (mascaraInteiro(cns) == false) {
        event.returnValue = false;
    }
    return formataCampo(cns, '000 0000 0000 0000', event);
}

function MascaraCelular(tel) {
    if (mascaraInteiro(tel) == false) {
        event.returnValue = false;
    }
    return formataCampo(tel, '(00) 00000-0000', event);
}

function MascaraTelefone_E_Celular(tel) {
    if (mascaraInteiro(tel) == false) {
        event.returnValue = false;
    }

    let auxTel = tel.value.replace(/[^\d]+/g, '');
    if (auxTel.length <= 10) 
        return formataCampo(tel, '(00) 0000-0000', event);
    else
        return formataCampo(tel, '(00) 00000-0000', event);
}

//adiciona mascara ao CPF
function MascaraCPF(cpf) {
    if (mascaraInteiro(cpf) == false) {
        event.returnValue = false;
    }
    return formataCampo(cpf, '000.000.000-00', event);
}

//adiciona mascara ao CPF
function MascaraCPFValue(cpf) {
    if (cpf == null)
        return cpf;
    if (mascaraInteiro(cpf) == false) {
        event.returnValue = false;
    }
    return formataValor(cpf, '000.000.000-00', event);
}

function MascaraCEPValue(cep) {
    if (mascaraInteiro(cep) == false) {
        event.returnValue = false;
    }
    return formataValor(cep, '00.000-00', event);
}

function MascaraTelefoneValue(fone) {
    if (mascaraInteiro(fone) == false) {
        event.returnValue = false;
    }
    return formataValor(fone, '(00) 0000-0000', event);
}

function MascaraCelularValue(fone) {
    if (mascaraInteiro(fone) == false) {
        event.returnValue = false;
    }
    return formataValor(fone, '(00) 00000-0000', event);
}


function MascaraPisValue(pis) {
    if (mascaraInteiro(pis) == false) {
        event.returnValue = false;
    }
    return formataValor(pis, '000.00000.00-0', event);
}

function MascaraCNSValue(cns) {
    if (mascaraInteiro(cns) == false) {
        event.returnValue = false;
    }
    return formataValor(cns, '000 0000 0000 0000', event);
}

function MascaraDNValue(dn) {
    if (mascaraInteiro(dn) == false) {
        event.returnValue = false;
    }
    return formataValor(dn, '00-0000000-00', event);
}


//valida dn
function ValidaPISPASEP(pispasep) {
    exp = /\d{3}\.\d{5}\.\d{2}\-\d{1}/;
    if (!exp.test(pispasep))
        return false;
}

//valida dn
function ValidaDN(dn) {
    exp = /\d{2}\-\d{7}\-\d{2}/;
    if (!exp.test(dn))
        return false;
}

//valida cns
function ValidaCNS(cns) {
    exp = /\d{3}\ \d{4}\ \d{4}\ \d{4}/;
    if (!exp.test(cns))
        return false;
}

//valida celular
function ValidaCelular(cel) {
    exp = /\(\d{2}\)\ \d{5}\-\d{4}/;
    if (!exp.test(cel))
        return false;
}

//valida telefone
function ValidaTelefone(tel) {
    exp = /\(\d{2}\)\ \d{4}\-\d{4}/;
    if (!exp.test(tel))
        return false;
}

//valida CEP
function ValidaCep(cep) {
    exp = /\d{2}\.\d{3}\-\d{3}/;
    if (!exp.test(cep))
        return false;
}

//valida numero inteiro com mascara
function mascaraInteiro() {
    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
        return false;
    }
    return true;
}

// replaceAll
String.prototype.replaceAll = function (de, para) {
    var str = this;
    var pos = str.indexOf(de);
    while (pos > -1) {
        str = str.replace(de, para);
        pos = str.indexOf(de);
    }
    return (str);
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


//formata de forma generica os campos
function formataValor(campo, Mascara, evento) {
    var boleanoMascara;

    var Digitato = evento.keyCode;
    exp = /\-|\.|\/|\(|\)| /g
    campoSoNumeros = campo.toString().replace(exp, "");

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
        return NovoValorCampo;
    } else {
        return NovoValorCampo;
    }
}


//****************************************************************************************

function _sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function Sleep(miliseconds) {
    console.log('Taking a break...');
    await _sleep(miliseconds);
    console.log('Two seconds later');
}

//Sleep(1);

function fomatarMoeda(numero) {
    var numero = numero.toFixed(2).split('.');
    numero[0] = numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}


function validaEmail(idCampoEmail) {
    var validarPassou = true;

    var txtEmail = $("#" + idCampoEmail);
    if (isNullOrEmpty(txtEmail.val()))
        return;
    var posP = txtEmail.val().indexOf(".");
    var posA = txtEmail.val().indexOf("@");

    if (txtEmail.val() == "" || txtEmail.val().length < 2 || txtEmail.val() == "usuario@dominio.com.br") {
        $("#SP" + idCampoEmail).show();
        $("#SP" + idCampoEmail).text("Insira um endereço de email!");
        validarPassou = false;
    } else if (posA == -1) {
        $("#SP" + idCampoEmail).text("Email Inválido, por favor verifique!");
        $("#SP" + idCampoEmail).show();
        validarPassou = false;
    } else if (posP == -1) {
        $("#SP" + idCampoEmail).text("Email Inválido, por favor verifique!");
        $("#SP" + idCampoEmail).show();
        validarPassou = false;
    } else {
        $("#SP" + idCampoEmail).hide();
        $("#SP" + idCampoEmail).text("");
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
    elemento.mask("9999999999");
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

//#####################//# VALIDAÇÃO DA DATA #//#####################
function ValidarData(cData) {
    var data = cData;
    var tam = data.length;
    if (tam != 10) {
        return false;
    }
    var dia = data.substr(0, 2);
    var mes = data.substr(3, 2)
    var ano = data.substr(6, 4);

    if (ano < 1980) {
        return false;
    }
    if (ano > 2200) {
        return false;
    }
    switch (mes) {
        case '01':
            if (dia <= 31) return (true);
            break;
        case '02':
            if (dia <= 29) return (true);
            break;
        case '03':
            if (dia <= 31) return (true);
            break;
        case '04':
            if (dia <= 30) return (true);
            break;
        case '05':
            if (dia <= 31) return (true);
            break;
        case '06':
            if (dia <= 30) return (true);
            break;
        case '07':
            if (dia <= 31) return (true);
            break;
        case '08':
            if (dia <= 31) return (true);
            break;
        case '09':
            if (dia <= 30) return (true);
            break;
        case '10':
            if (dia <= 31) return (true);
            break;
        case '11':
            if (dia <= 30) return (true);
            break;
        case '12':
            if (dia <= 31) return (true);
            break;
    } {
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


