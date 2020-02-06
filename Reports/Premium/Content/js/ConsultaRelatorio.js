$(document).ready(function () {

    //8854
    CarregaComboAno();   

    $(document).on('click', "#btnConfirmar", function () {        
        var dtCancelamento = '';
        dtCancelamento = $('#txtCancelamento').val();
        var status = parseInt($('input[name=radioStatus]:checked').val());

        if (dtCancelamento.length == 0){
            dtCancelamento = '01-01-0001';
        }

        if (validaDetalhamento() == true){
            $.ajax({
                method: "POST",
                url: "_ConsultaRelatorio.aspx/SalvarVoucher",
                data: '{idVoucher: "' + $('#hddIdVoucher').val() + '", idStatus: "' + $('input[name=radioStatus]:checked').val() + '", dt_cancelamento: "' + $('#txtCancelamento').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (retorno) {   
                if(retorno.d.includes('Erro')){
                    alert('Erro na tentativa de salvar o voucher.');
                    console.log(retorno);
                    return;
                }
                
                switch (retorno.d){
                    case "Data de cancelamento superior ou igual ao início de vigência do Voucher":
                        $('#SPMsgRetorno').text(retorno.d);
                        $('#SPMsgRetorno').css('color', 'red');
                        $('#SPMsgRetorno').show();
                        break;
                    case "Voucher cancelado com sucesso":
                        $('#SPMsgRetorno').text(retorno.d);
                        $('#SPMsgRetorno').css('color', '#0d408f');
                        $('#SPMsgRetorno').show();
                        $('.modalBox, #btnConfirmar').css('display', 'none');
                        doPostBack();
                        break;
                    case "Data de DESCONTINUIDADE não pode ser inferior à data de início de vigência do Voucher":
                        $('#SPMsgRetorno').text(retorno.d);
                        $('#SPMsgRetorno').css('color', 'red');
                        $('#SPMsgRetorno').show();
                        break;
                    case "Voucher descontinuado com sucesso":
                        $('#SPMsgRetorno').text(retorno.d);
                        $('#SPMsgRetorno').css('color', '#0d408f');
                        $('#SPMsgRetorno').show();
                        $('.modalBox, #btnConfirmar').css('display', 'none');
                        doPostBack();
                        break;
                    case "Registro alterado com sucesso":
                        $('#SPMsgRetorno').text(retorno.d);
                        $('#SPMsgRetorno').css('color', '#0d408f');
                        $('#SPMsgRetorno').show();
                        $('.modalBox, #btnConfirmar').css('display', 'none');
                        doPostBack();
                        break;
                }
            })
        }
    });
    
    //ListarVouchers($('#cmbAno').val(), 'janeiro');
    $(document).on('click', "#btnConsultar", function () {         
        ListarVouchers($('#cmbAno').val(), $('#cmbMes').val());
    });

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

    $('#txtCancelamento').on('click', function() {
        $('#SPMsgRetorno').css('display', 'none');
    });
});

//8854
function CarregaComboAno(){    
    $.ajax({
        method: "POST",
        url: "_ConsultaRelatorio.aspx/ListarDataCadastroAno",        
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {        
        var anos = retorno.d;
        $.each(anos, function (key, value) {
            $('#cmbAno').append($('<option>', {
                value: value,
                text: value
            }));        
        });  
        //Seta o ano vigente
        var dataVig = new Date();
        anoVig = dataVig.getFullYear();
        $('#cmbAno').val(anoVig);
        CarregaComboMes();
    })
}
//8854
function CarregaComboMes(){
    $.ajax({
        method: "POST",
        url: "_ConsultaRelatorio.aspx/CarregaComboMes",
        data: '{ano: "' + $('#cmbAno').val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {        
        var meses = retorno.d.split(';');
        $.each(meses, function (key, value) {
            if(value.length > 0){
                var newOption = $('<option value = "' + value + '">' + value + '</option>');
                $('#cmbMes').append(newOption);
            }            
        });
    })

}

function doPostBack() {
    setTimeout(function() {
        location.reload();
    },3500);
}

function impressaoRelatorio(){
    var Mes = $('#cmbMes').val();
    var Ano = $('#cmbAno').val();

    window.location.href = "_RelatorioBilhete.aspx?Mes="+ Mes + "&Ano=" + Ano;
}


function ListarVouchers(ano, mes) {

    $('#mdLoader').modal({
        backdrop: 'static',
        keyboard: false
    });
    $('#mdLoader').modal('show');             

    $.ajax({
        method: "POST",
        url: "_ConsultaRelatorio.aspx/ListarVouchers",
        data: '{ano: "' + ano + '", mes: "' + mes + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {      
        
        $('#mdLoader').modal('hide');
        
        //console.log(retorno.d);
        var VoucherList = [];
        retorno.d.forEach(function (item, index) {
            if(item.msgErro != null){
                alert('Erro na tentativa de consultar os vouchers');
                console.log(item.msgErro);
                return;
            }
            var Voucher = new VoucherVW();
            Voucher.IdVoucher = item.IdVoucher;
            Voucher.NomePassageiro = item.NomePassageiro;
            Voucher.CpfPassageiro = item.CpfPassageiro;
            Voucher.NumVoucher = item.NumVoucher;
            Voucher.Diaria = item.Diaria;
            Voucher.Destino = item.Destino;
            Voucher.strDtInicioVig = item.strDtInicioVig;                
            Voucher.strDtFinalVig = item.strDtFinalVig;
            Voucher.strDtCadastro = item.strDtCadastro;          

            var vlrPremio = String(item.Premio.toFixed(2));
            if(!vlrPremio.includes('.'))
                vlrPremio = vlrPremio + '.00';   
            Voucher.vlrPremio = 'R$ ' + mascaraMoeda(vlrPremio);
            Voucher.IdStatus = item.IdStatus;

            switch(item.IdStatus){
                case 1:
                    Voucher.Status = 'emitido';
                    break;
                case 2:
                    Voucher.Status = 'cancelado';
                    break;
                case 3:
                    Voucher.Status = 'descontinuado';
                    break;
            }
            Voucher.htmlButton = '<i class="fa fa-search fa-2x"style="cursor: pointer" onclick="alteraVoucher(' + Voucher.IdVoucher + ',' + Voucher.idStatus + ')"></i>'

            VoucherList.push(Voucher);
        });

        iniciarPaginacao(VoucherList);
        exibirPaginacao(1);
        var qntResult = retorno.d.length;
        $('#lblTotalResultado').text(qntResult);
        $('#btnImpressao').show();
    })
}


function alteraVoucher(idVoucher, idStatus){
    $('#mdlAlteraVoucher').modal('show');
    $('#SPMsgRetorno').hide();
    $('#txtCancelamento').val('');
    $('#hddIdVoucher').val(idVoucher);

    $.ajax({
        method: "POST",
        url: "_ConsultaRelatorio.aspx/DetalhaVoucher",
        data: '{idVoucher: "' + idVoucher + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    }).done(function(retorno) {
        if(retorno.d){
            if(retorno.d.IdStatus == 2 || retorno.d.IdStatus == 3){
                $('#btnConfirmar').hide();
                $('input[name=radioStatus]').prop("disabled", true);                
                $('#dvDtCancelamento').hide();                
            }else{
                $('#btnConfirmar').show();
                $('input[name=radioStatus]').prop("disabled", false);
                $('#dvDtCancelamento').show();
            }
                

            $('#lblNomePassageiro').text(retorno.d.NomePassageiro);
            $('#lblNumVoucher').text(retorno.d.NumVoucher);
            $('#lblIniVig').text(retorno.d.strDtInicioVig);
            $('#lblDiarias').text(retorno.d.Diaria + (retorno.d.Diaria > 1 ? ' Diarias' : ' Diaria'));
            $('#lblCPF').text(retorno.d.CpfPassageiro);
            $('#lblDestino').text(retorno.d.Destino);
            $('#lblFimVig').text(retorno.d.strDtFinalVig);
            var vlrPremio = String(retorno.d.Premio.toFixed(2));
            $('#lblPremioLic').text('R$ ' + mascaraMoeda(vlrPremio));
            $('#hddstrDtInicioVig').text(retorno.d.strDtInicioVig);
            $('#hddstrDtFimVig').text(retorno.d.strDtFinalVig);
            $('input[name=radioStatus]:checked').prop('checked', false);
            if(retorno.d.IdStatus > 0){
                $('input[name=radioStatus]').each(function() {
                    if (parseInt($(this).val()) == retorno.d.IdStatus){
                        $(this).prop('checked', true);
                    }
                });
            }
        }
    });
}

function validaDetalhamento(){
    var validaPassou = true;
    var dtCancelamento = document.getElementById('txtCancelamento');
    var inicioVig = $('#hddstrDtInicioVig').val();
    var fimVig = $('#hddstrDtFimVig').val();

    if (parseInt($('input[name=radioStatus]:checked').val()) != 1){

        //Converte os valores string para quantidade de dias.
        var dtAuxCancel = converteVigencia(dtCancelamento.value);
        var dtAuxInicio = converteVigencia(inicioVig);
        var dtAuxFim = converteVigencia(fimVig);
        var dt = new Date();
        var dia = dt.getDate();
        var mes = (dt.getMonth()+1 < 10 ? '0' : '') + (dt.getMonth()+1); //Insere o 0 para caso a data do mes seja menor que 10.
        var ano = dt.getFullYear();
        dt = dia + '/' + mes + '/' + ano;
        var dtAtualAux = converteVigencia(dt);

        //Se for "DESCONTINUADO"
        if(parseInt($('input[name=radioStatus]:checked').val()) == 3){
            //Se a data de cancelamento for menor q a inio OU maior que a data fim.
            if (dtAuxCancel < dtAuxInicio || dtAuxCancel > dtAuxFim){
                $('#txtCancelamento').css('border-color', 'red');
                $('#SPtxtCancelamento').text('A Data de Descontinuidade deve estar entre a data de inicio e fim da vigência!');
                $('#SPtxtCancelamento').show();
                validaPassou = false;
            } 
        } else if (dtCancelamento.value == "" ) {
            $('#txtCancelamento').css('border-color', 'red');
            $('#SPtxtCancelamento').text('A Data de Cancelamento é obrigatória!');
            $('#SPtxtCancelamento').show();
            validaPassou = false;
        } else {
            $("#txtCancelamento").css('border-color', '');
            $('#SPtxtCancelamento').hide();
        }
    } else {
        dtCancelamento.value = "";
    }
    return validaPassou;
}

//Função para calculo de quantidade de Diarias.
function converteVigencia(dtVig) {
    var dtVigAux = dtVig.split("/");
    var dt = new Date(dtVigAux[1] + "/" + dtVigAux[0] + "/" + dtVigAux[2]); //Formata em padrão Date
    var dias = parseInt(dt / 86400000); //Converte o resultado de milisegundos para dias / 86400000 milisegundos = 1 dia. /ou (1000 * 60 * 60 * 24) onde: 1000 = 1 seg, 60 seg = 1 min, 60 min = 1 hora, 24h = 1 dia.
    return dias;
}


//******************** Classes JSON (View Model) *************************************
class VoucherVW{
    constructor(){        
        this.IdVoucher;
        this.NomePassageiro;
        this.CpfPassageiro;
        this.NumVoucher;
        this.Diaria;
        this.Destino;
        this.strDtInicioVig;
        this.strDtFinalVig;
        this.strDtCadastro; 
        this.Premio;
        this.IdStatus;
        this.Status;
        this.htmlButton;
    }
}