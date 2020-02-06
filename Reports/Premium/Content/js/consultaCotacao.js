$(document).ready(function () {
    $(document).on('click', "#btnConsultar", function () {
        if (validaCampo()) {
            $('#mdLoader').modal({
                backdrop: 'static',
                keyboard: false
            });
            $('#mdLoader').modal('show'); 
            $.ajax({
                method: "POST",
                url: "consultaCotacao.aspx/ListarCotacaoPorParamsAsync",                
                data: '{cpf_cnpj: "'+ $('#txtCPFCNPJ').val().replace(/[^\d]+/g, '')  +'", segurado: "' + $('#txtNomeSeg').val()  + '", statusOrc: "' + $('#cmbStatus').val() + '", IdUsuario: "' + $('#cmbUsuario').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (retorno) {          
                //Convertendo obj JSON para VW JSON paginação
                //console.log(retorno.d);
                $('#mdLoader').modal('hide'); 
                retorno.d.forEach(function(item, index){
                    var Cotacao = new CotacaoVW();
                    Cotacao.IdOrcamento = item.IdOrcamento;
                    Cotacao.Segurado = item.Segurado.Nome;
                    Cotacao.DataCalculoSTR = item.DataCalculoSTR;
                    Cotacao.Usuario = item.Usuario.Nome;
                    Cotacao.Modalidade = '';
                    Cotacao.StatusDescricao = item.StatusDescricao;

                    objCotacaoList.push(Cotacao);
                });

                iniciarPaginacao(objCotacaoList);       //param: número de linhas por páginas.
            });
            $('#dvResultadoCotacao').css('display', 'block');
            }
    });

    CarregaComboStatus();
    CarregaComboUsuario();

    $('#btnLimpar').click(function () {
        $('#txtCPFCNPJ').val('');
        $('#txtNomeSeg').val('');
        $('#cmbStatus').options[0];
    });
    
});


function CarregaComboUsuario()
{
    $.ajax({
        method: "POST",
        url: "consultaCotacao.aspx/ListarUsuariosPorEmpresaAsync",      // Parametro empresa é carregado na session servidor. (Dados do usuário.)        
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);             
        var obj = new Array();
        retorno.d.forEach(function(item, index){
            var status = {Id: item.IdUsuario, Descricao: item.Nome } 
            obj.push(status);
        });
        //método encontra-se em ValidacoesCamposForm.js
        carregaCombos(obj, 'cmbUsuario');
    });
}


function CarregaComboStatus()
{
    $.ajax({
        method: "POST",
        url: "consultaCotacao.aspx/ListarStatusOrcamentoAsync",        
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);             
        var obj = new Array();
        retorno.d.forEach(function(item, index){
            var status = {Id: item.IdStatus, Descricao: item.Nome } 
            obj.push(status);
        });
        //método encontra-se em ValidacoesCamposForm.js
        carregaCombos(obj, 'cmbStatus');
    });
}


function validaCampo() {
    var validaPassou = true;

    var cpfcnpj = document.getElementById("txtCPFCNPJ").value.replace(/[^\d]+/g, '');
    var nome = document.getElementById("txtNomeSeg");
    var status = document.getElementById("cmbStatus");
    var usuario = document.getElementById("cmbUsuario");

    if (cpfcnpj == "" && nome.value == "" && status.value == "" && usuario.value == "") {
        $('#SPRetornoErro').show();
        validaPassou = false;
    } else {
        $('#SPRetornoErro').hide();
    }

    return validaPassou;
}

//******************** Classes JSON (View Model) *************************************
let objCotacaoList = new Array();
class CotacaoVW{
    constructor(){        
        this.IdOrcamento;        
        this.Segurado; 
        this.DataCalculoSTR; 
        this.Usuario; 
        this.Modalidade; 
        this.StatusDescricao;         
    }       
}