$(document).ready(function () {
    $('.btnMenu').removeClass('active');
    $('.btnMenu').removeClass('disabled');

    $('#liPasso1').addClass('disabled');
    $('#liPasso2').addClass('active');
    $('#liPasso3').addClass('disabled');
    $('#liPasso4').addClass('disabled');

    //Adiciona o DATAPICKER de acordo com a classe selecionada.
    $(".calendario").datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        changeMonth: true,
        changeYear: true,
        onSelect: function () {
            $(this).next().addClass("active");
        }
    });

    $('.valor-formato').priceFormat({
        prefix: 'R$ ',
        centsSeparator: ',',
        thousandsSeparator: '.'
    });
 $('#cmbStatus').val('1');    
});

function validaData(idCampoData) {
    var txtData = $('#' + idCampoData.id);
    var date = txtData.val();
    var dtAtual = new Date();
    var dia = dtAtual.getDate();
    var mes = dtAtual.getMonth() + 1;
    var ano = dtAtual.getFullYear();
    var str_ano = dia + '/' + mes + '/' + ano;
    var arrDT = new Array;
    var arrDT2 = new Array;
    var dtIniVigencia = $('#txtIniVigencia').val();
    var arrIniVig = new Array;
    arrIniVig = dtIniVigencia.split("/");
    arrDT = date.split("/");
    arrDT2 = str_ano.split("/");
    if (txtData.val() == "" || txtData.val().length < 10) {
        $('#' + idCampoData.id).css("border-color", "red");
        $('#SP' + idCampoData.id).show();
        $('#SP' + idCampoData.id).text("Insira uma data de válida!");
        return false;
    } else if (txtData.val() != "") {
        if (parseInt(arrDT[0]) > 31) {
            $('#' + idCampoData.id).css("border-color", "red");
            $('#SP' + idCampoData.id).show();
            $('#SP' + idCampoData.id).text("Data inválida, por favor verifique!");
            return false;
        }

        if (parseInt(arrDT[1]) > 12) {
            $('#' + idCampoData.id).css("border-color", "red");
            $('#SP' + idCampoData.id).show();
            $('#SP' + idCampoData.id).text("Data inválida, por favor verifique!");
            return false;
        }

        if (idCampoData.id == "txtIniVigencia" ) {
            if (parseInt(arrDT[2]) > ano) {
                $('#' + idCampoData.id).css("border-color", "red");
                $('#SP' + idCampoData.id).show();
                $('#SP' + idCampoData.id).text("Data inválida, por favor verifique!");
                return false;
            }
            ////Comparação se o ano digitado é o mesmo que o ano atual.
            //if (arrDT[2] == ano) {
            //    //Comparação se o mês digitado é o mesmo que o mês atual.
            //    if (parseInt(arrDT[1]) == parseInt(arrDT2[1])) {
            //        //Comparação se o dia digitado é o maior ou o mesmo que o dia atual.
            //        if (parseInt(arrDT[0]) < parseInt(arrDT2[0])) {
            //            $('#' + idCampoData.id).css("border-color", "red");
            //            $('#SP' + idCampoData.id).show();
            //            $('#SP' + idCampoData.id).text("Data vigência inicial não pode ser anterior a data atual!");
            //            return false;
            //        }
            //    }
            //}
        }

        if (idCampoData.id != "txtIniVigencia") {
            //Comparação se o ano digitado é o mesmo que o ano atual.
            if (arrDT[2] == arrIniVig[2]) {
                //Comparação se o mês digitado é o mesmo que o mês atual.
                if (parseInt(arrDT[1]) == parseInt(arrIniVig[1])) {
                    //Comparação se o dia digitado é o maior que o dia atual.
                    if (parseInt(arrDT[0]) < parseInt(arrIniVig[0])) {
                        $('#' + idCampoData.id).css("border-color", "red");
                        $('#SP' + idCampoData.id).show();
                        $('#SP' + idCampoData.id).text("Data vigência final não pode ser menor que a data inicial!");
                        return false;
                    }
                }
            }
        }
        
    }
    else {
        $('#' + idCampoData.id).css("border-color", "");
        $('#SP' + idCampoData.id).hide();
        $('#SP' + idCampoData.id).text("");
        return true;
    }
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


//Função para calculo de quantidade de Diarias.
function dataDiaria(dtIni, dtFim) {
    var DtIni = $(dtIni).val();
    var DtFim = $(dtFim).val();
    var dtIniAux = DtIni.split("/");
    var dtFimAux = DtFim.split("/");
    var dt1 = new Date(dtIniAux[1] + "/" + dtIniAux[0] + "/" + dtIniAux[2]); //Formata em padrão Date
    var dt2 = new Date(dtFimAux[1] + "/" + dtFimAux[0] + "/" + dtFimAux[2]); //Formata em padrão Date
    var milisegundos = (dt2 - dt1); //Retorna resultado da diferença em milisegundos.

    var dias = parseInt(milisegundos / 86400000); //Converte o resultado de milisegundos para dias / 86400000 milisegundos = 1 dia. /ou (1000 * 60 * 60 * 24) onde: 1000 = 1 seg, 60 seg = 1 min, 60 min = 1 hora, 24h = 1 dia.


    return dias + 1;
}

function populaDiarias() {
    var datainicio = $('#txtIniVigencia');
    var datafim = $('#txtFimVigencia');
    var diarias = dataDiaria(datainicio, datafim);

    $('#txtDiarias').val(diarias);
    $('#txtDiarias').focus();
}

function populaPremio(Diarias) {
   //Parametro Diarias enviado através da quantidade de dias populados no campo.

    $.ajax({
        method: "POST",
        url: "_BilhetesEmitidos.aspx/CalculaPremio",
        data: '{diarias: "' + Diarias + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //Recebe o retorno do valor da taxa final e popula o campo na tela.
        $('#txtPremioLic').val(retorno.d);
        $('#txtPremioLic').focus();
        //Mantem a formatação da moeda.
        //$('#txtPremioLic').priceFormat({
        //    prefix: 'R$ ',
        //    centsSeparator: ',',
        //    thousandsSeparator: '.'
        //});
    });
}

function validaBilhete() {
    validaPassou = true;

    var nome = document.getElementById("txtNome");
    if (nome.value == "" || nome.value.length < 4) {
        $("#txtNome").css('border-color', 'red');
        $('#SPtxtNome').show();
        validaPassou = false;
    } else {
        $("#txtNome").css('border-color', '');
        $('#SPtxtNome').hide();
    }
    
    var voucher = document.getElementById("nrVoucher");
    if (voucher.value == "" || voucher.value.length < 4) {
        $("#nrVoucher").css('border-color', 'red');
        $('#SPnrVoucher').show();
        validaPassou = false;
    } else {
        $("#nrVoucher").css('border-color', '');
        $('#SPnrVoucher').hide();
    }

    var inicioVig = document.getElementById("txtIniVigencia");
    if (validaData(inicioVig) == false) {
        $("#txtIniVigencia").css('border-color', 'red');
        $('#SPtxtIniVigencia').show();
        validaPassou = false;
    } else {
        $("#txtIniVigencia").css('border-color', '');
        $('#SPtxtIniVigencia').hide();
    }

    var fimVig = document.getElementById("txtFimVigencia");
    if (validaData(fimVig) == false) {
        $("#txtFimVigencia").css('border-color', 'red');
        $('#SPtxtFimVigencia').show();
        validaPassou = false;
    } else {
        $("#txtFimVigencia").css('border-color', '');
        $('#SPtxtFimVigencia').hide();
    }

    var diarias = document.getElementById("txtDiarias");
    var qntDiarias = dataDiaria(inicioVig, fimVig)
    if (diarias.value == "" || diarias.value.length < 1) {
        $("#txtDiarias").css('border-color', 'red');
        $('#SPtxtDiarias').show();
        validaPassou = false;
    } else if (parseInt(diarias.value) != qntDiarias) {
        $("#txtDiarias").css('border-color', 'red');
        $('#SPtxtDiarias').text('Quantidade de diárias incorreta, por favor atualize-a!');
        $('#SPtxtDiarias').show();
        validaPassou = false;
    } else {
        $("#txtDiarias").css('border-color', '');
        $('#SPtxtDiarias').hide();
    }
    var premio = document.getElementById("txtPremioLic");
    if (premio.value == "" || premio.value.length < 2) {
        $('#txtPremioLic').css('border-color', 'red');
        $('#SPtxtPremioLic').show();
        validaPassou = false;
    } else {
        $("#txtPremioLic").css('border-color', '');
        $('#SPtxtPremioLic').hide();
    }

    var status = document.getElementById("cmbStatus");
    if (status.value == "") {
        $('#cmbStatus').css('border-color', 'red');
        $('#SPcmbStatus').show();
        validaPassou = false;
    } else {
        $("#cmbStatus").css('border-color', '');
        $('#SPcmbStatus').hide();
    }

    var destino = document.getElementById("cmbDestino");
    if (destino.value == "" || destino.value.length < 2) {
        $('#cmbDestino').css('border-color', 'red');
        $('#SPcmbDestino').show();
        validaPassou = false;
    } else {
        $("#cmbDestino").css('border-color', '');
        $('#SPcmbDestino').hide();
    }
    
    if (validaPassou == true)
        InserirVoucher();

    return validaPassou;
}

function InserirVoucher() {

    var DtInicioVig = $('#txtIniVigencia').val();
    DtInicioVig = DtInicioVig.split('/');
    DtInicioVig = DtInicioVig[1] + '-' + DtInicioVig[0] + '-' + DtInicioVig[2];

    var DtFimVig = $('#txtFimVigencia').val();
    DtFimVig = DtFimVig.split('/');
    DtFimVig = DtFimVig[1] + '-' + DtFimVig[0] + '-' + DtFimVig[2];
    
    var PremioLic = $('#txtPremioLic').val();
    PremioLic = (PremioLic.replace(/[^\d]+/g, '') / 100).toFixed(2);

    var Voucher = $('#nrVoucher').val();
    Voucher = Voucher.replace(/[^\d]+/g, '');

    var CPF = $('#txtCPF').val();
    CPF = CPF.replace(/[^\d]+/g, '');

    var data = {
        objVoucher: {
            NomePassageiro: $('#txtNome').val(),
            CpfPassageiro: CPF,
            Diaria: $('#txtDiarias').val(),
            Destino: $('#cmbDestino').val(),
            DtInicioVig: DtInicioVig,
            DtFinalVig: DtFimVig,
            Premio: PremioLic,
            IdStatus: $('#cmbStatus').val(),
            NumVoucher: Voucher,
            Deletado: false
        }
    };

    $.ajax({
        method: "POST",
        url: "_BilhetesEmitidos.aspx/InserirVoucher",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d.msgErro != null) {
            console.log(retorno.d.msgErro);            
            $('#spMsgpSucesso').text('Ocorreu um erro na tentativa de cadastro.');
            $('#mdlConfirma').show();
        }else            
            $('#mdlConfirma').show();
    });
}

function limpaCampos() {

    $('#cmbStatus').val('');
    $('#txtNome').val('');
    $('#txtNome').focusout();
    $('#txtCPF').val('');
    $('#txtCPF').focusout();
    $('#nrVoucher').val('');
    $('#nrVoucher').focusout();
    $('#txtDiarias').val('');
    $('#txtDiarias').focusout();
    $('#txtIniVigencia').val('');
    $('#txtIniVigencia').focusout();
    $('#txtFimVigencia').val('');
    $('#txtFimVigencia').focusout();
    $('#txtPremioLic').val('');
    $('#txtPremioLic').focusout();
    $('#txtStatus').val('');
    $('#txtStatus').focusout();
    $('#txtDestino').val('');
    $('#txtDestino').focusout();

}


