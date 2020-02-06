$(document).ready(function () {
    $(document).on('click', "#btnConsultar_Prop", function () {
        if (validaCampo()) {
            $('#mdLoader').modal({
                backdrop: 'static',
                keyboard: false
            });
            $('#mdLoader').modal('show'); 
            $.ajax({
                method: "POST",
                url: "consultaProposta.aspx/ListarPropostaPorIdEmpresaAsync",
                data: '{idEmpresa: "1"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (retorno) {
                //Convertendo obj JSON para VW JSON paginação
                $('#mdLoader').modal('hide'); 
                retorno.d.forEach(function(item, index){
                    var Proposta = new PropostaVW();
                    Proposta.IdOrcamento = item.IdOrcamento;
                    Proposta.Segurado = item.Segurado.Nome;
                    Proposta.DataCalculo = item.DataCalculoSTR;
                    Proposta.Usuario = item.Usuario.Nome;
                    Proposta.Modalidade = item.Societario.Modalidade;
                    Proposta.StatusDescricao = item.StatusDescricao;

                    objPropostaList.push(Proposta);
                });
                iniciarPaginacao(objPropostaList);       //param: número de linhas por páginas.
            });
            $('#dvResultadoProposta').css('display', 'block');
        }
    });

    $('#btnLimpar_Prop').click(function () {
        $('#txtCPFCNPJ_Prop').val('');
        $('#txtNomeSeg_Prop').val('');
    });



});

function validaCampo() {
    var validaPassou = true;

    var cpfcnpj = document.getElementById("txtCPFCNPJ_Prop").value.replace(/[^\d]+/g, '');
    var nome = document.getElementById("txtNomeSeg_Prop");
    var status = document.getElementById("cmbStatus_Prop");
    var usuario = document.getElementById("cmbUsuario_Prop");

    if (cpfcnpj == "" && nome.value == "" && status.value == "" && usuario.value == "") {
        $('#SPRetornoErro_Prop').show();
        validaPassou = false;
    } else {
        $('#SPRetornoErro_Prop').hide();
    }

    //if (cpfcnpj == "" || cpfcnpj.length < 11) {
    //    $('#txtCPFCNPJ').css("border-color", "red");
    //    $('#SPtxtCPFCNPJ').show();
    //    validaPassou = false;
    //} else {
    //    $('#txtCPFCNPJ').css("border-color", "");
    //    $('#SPtxtCPFCNPJ').hide();
    //}

    //if (nome.value == "" || nome.value.length < 4) {
    //    $('#txtNomeSeg').css("border-color", "red");
    //    $('#SPtxtNomeSeg').show();
    //    validaPassou = false;
    //} else {
    //    $('#txtNomeSeg').css("border-color", "");
    //    $('#SPtxtNomeSeg').hide();
    //}

    //if (status.value == "" || status.value.length < 1) {
    //    $('#cmbStatus').css('border-color', 'red');
    //    $('#SPcmbStatus').show();
    //    validaPassou = false;
    //} else {
    //    $('#cmbStatus').css('border-color', '');
    //    $('#SPcmbStatus').hide();
    //}

    //if (usuario.value == "" || usuario.value.length < 1) {
    //    $('#cmbUsuario').css('border-color', 'red');
    //    $('#SPcmbUsuario').show();
    //    validaPassou = false;
    //} else {
    //    $('#cmbUsuario').css('border-color', '');
    //    $('#SPcmbUsuario').hide();
    //}

    return validaPassou;
}

//******************** Classes JSON (View Model) *************************************
let objPropostaList = new Array();
class PropostaVW{
    constructor(){        
        this.IdOrcamento;        
        this.Segurado; 
        this.DataCalculo; 
        this.Usuario; 
        this.Modalidade; 
        this.StatusDescricao;         
    }       
}