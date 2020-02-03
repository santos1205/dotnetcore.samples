//DetalharSegurados(5597);
//ManterBaixaPagamentoManual(637); 
// ######################################################################### ConsultaSegurados.aspx #######################################################################################
// #region ConsultaSeguradosPage
$(document).ready(function () {
    $(document).keypress(function (e) {
        //if (e.which == 13) {
        //    //console.log('TECLA ENTER ACIONADA');
        //    ConsultarSegurado();
        //}

    });

    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)

    if (url.includes('ConsultaSegurado.aspx')) {
        CarregarComboSeguradora();
        CarregarComboEspecialidades();
        MenuNivelAcesso();
        ModuloNivelAcesso();
    }

});

$('#btnConsultarSegurado').click(function () {
    ConsultarSegurado();
});

$('#btnLimpar').click(function () {
    $('#mainContent_txtCPF').val('');
    $('#mainContent_txtSegurado').val('');
    $('#mainContent_txtStatus').val('');
    $('#mainContent_txtCRM').val('');
});

$('#btnLimpar_consultarSegurado').click(function () {
    $('.input-consultar-segurado').val('');
    $('#SPtxtCelular-CadSegurado, #SPtxtEmail_ConsultarSegurado').hide();
    $('#mainContent_txtCPF').focus();
});

$('#mainContent_txtCelular_ConsultarSegurado, #mainContent_txtEmail_ConsultarSegurado').keyup(function () {
    $('#SPtxtCelular-CadSegurado, #SPtxtEmail_ConsultarSegurado').hide();
});

function ModuloNivelAcesso() {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)

    if (url.includes('ConsultaSegurado.aspx'))
        ModuloNivelAcessoVisibility('ConsultaSegurado.aspx');
    if (url.includes('ConsultaLead.aspx'))
        ModuloNivelAcessoVisibility('ConsultaLead.aspx');
    if (url.includes('CadastraSegurado.aspx'))
        ModuloNivelAcessoVisibility('CadastraSegurado.aspx');
    if (url.includes('AprovaUsuario.aspx'))
        ModuloNivelAcessoVisibility('AprovaUsuario.aspx');


    if (url.includes('Planos.aspx'))
        ModuloNivelAcessoVisibility('Planos.aspx');
    if (url.includes('ConsultaLog.aspx'))
        ModuloNivelAcessoVisibility('ConsultaLog.aspx');


    if (url.includes('Pagamentos.aspx'))
        ModuloNivelAcessoVisibility('Pagamentos.aspx');
    if (url.includes('ConsultaFaturamento.aspx'))
        ModuloNivelAcessoVisibility('ConsultaFaturamento.aspx');
}



//Vefirica a visibilidade de acordo com o perfil.
function MenuNivelAcesso() {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)

    if (url.includes('ConsultaSegurado.aspx'))
        MenuNivelAcessoVisibility('ConsultaSegurado.aspx');
    if (url.includes('ConsultaLead.aspx'))
        MenuNivelAcessoVisibility('ConsultaLead.aspx');
    if (url.includes('CadastraSegurado.aspx'))
        MenuNivelAcessoVisibility('CadastraSegurado.aspx');
    if (url.includes('aprovaUsuario.aspx'))
        MenuNivelAcessoVisibility('aprovaUsuario.aspx');

    if (url.includes('Planos.aspx'))
        MenuNivelAcessoVisibility('Planos.aspx');
    if (url.includes('ConsultaLog.aspx'))
        MenuNivelAcessoVisibility('ConsultaLog.aspx');

    if (url.includes('Pagamentos.aspx'))
        MenuNivelAcessoVisibility('Pagamentos.aspx');
    if (url.includes('ConsultaFaturamento.aspx'))
        MenuNivelAcessoVisibility('ConsultaFaturamento.aspx');
}

function ModuloNivelAcessoVisibility(page) {
    $.ajax({
        method: "POST",
        url: `${page}/VerificarNivelAcessoUsuarioAsync`,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d
        ModuloNivelAcessoVerification(retorno.d, page);
    });
}

// Verificação do nivel de acesso por pagina
// O nivel de acesso do gestor não esta incluso pois o mesmo pode ver todos os modulos do sistema. 
function ModuloNivelAcessoVerification(nvlAcesso, page) {
    if (nvlAcesso == 1) { // nivel de acesso do comercial
        if (page == 'Planos.aspx' || page == 'Pagamentos.aspx' || page == 'ConsultaFaturamento.aspx' || page == 'ConsultaLog.aspx') {
            var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Usuario/Login.aspx';
            window.location.replace(urlLogin);
        }
    }
    if (nvlAcesso == 2) { // nivel de acesso do financeiro
        if (page == 'ConsultaSegurado.aspx' || page == 'aprovaSegurado.aspx' || page == 'CadastraSegurado' || page == 'ConsultaLead.aspx' || page == 'ConsultaLog.aspx' || page == 'Planos.aspx') {
            var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Usuario/Login.aspx';
            window.location.replace(urlLogin);

        }
    }
}


function MenuNivelAcessoVisibility(url) {
    $.ajax({
        method: "POST",
        url: `${url}/VerificarNivelAcessoUsuarioAsync`,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d)
        var idNivelAcesso = retorno.d;

        switch (idNivelAcesso) {
            case 1:                         // Comercial
                $('#liPasso1').show();
                $('#liPasso2').show();
                $('#liPasso4').show();
                $('#liPasso6').show();
                $('#liTitulo').show();
                $('#liTitulotext').text("Comercial");

                return;
            case 2:                         // Financeiro
                $('#liPasso7').show();
                $('#liPasso8').show();
                $('#liTitulo').show();
                $('#liTitulotext').text("Financeiro");
                return;
            case 3:                         // Gestor
                $('#liPasso1').show();
                $('#liPasso2').show();
                $('#liPasso3').show();
                $('#liPasso4').show();
                $('#liPasso5').show();
                $('#liPasso6').show();
                $('#liPasso7').show();
                $('#liPasso8').show();
                $('#liTitulo').show();
                $('#liTitulo9').show();
                $('#liTitulo10').show();
                $('#liTitulotext').text("Administrativo");
                return;
            default:
        }
    });
}




//Obtem Forma de Pagto
var formaPagamento = 3;
var fpagChange = false;
$('#lifpagBoleto').click(function () {
    $('#lifpagBoleto').html(`<div class="dv-fpag-border" style="margin-left: 2%"></div><img class="md-forma-pag-img" style="margin-left: 25px" src="../ContentAdm/img/boleto_icon.png" /><div style="display: inline"><span class="md-boleto-editSeg-label">Boleto Bancário</span></div>`);
    //$('#lifpagCC').html(`<div class="dv-fpag-border" style="margin-left: 8%; border-style: none"></div><img class="md-forma-pag-img" style="margin-left: 8%; opacity: 0.5" src="../ContentAdm/img/credit-card-icon.png" />        <div style="display: inline; opacity: 0.5"><span class="md-creditcard-edit-label">Cartão</span><span class="md-creditcard-edit-label" style="top: 50%">de Crédito</span>        </div>`);
    $('#lifpagCC').html(``);
    //$('#lifpagDebito').html(`<div class="dv-fpag-border" style="margin-left: 10%; border-style: none"></div><img class="md-forma-pag-img" style="margin-left: 13%; width: 10%; opacity: 0.5" src="../ContentAdm/img/debito_icon.png" /><div style="display: inline; opacity: 0.5"><span class="md-debito-edit-label">Débito</span><span class="md-debito-edit-label" style="top: 50%">em Conta</span></div>`);
    $('#lifpagDebito').html(``);
    formaPagamento = 1;
    fpagChange = true;
    $('.fp-debito-edit-segurado').hide();
});
$('#lifpagCC').click(function () {
    $('#lifpagBoleto').html(`<div class="dv-fpag-border" style="margin-left: 2%; border-style: none"></div><img class="md-forma-pag-img" style="margin-left: 25px; opacity: 0.5" src="../ContentAdm/img/boleto_icon.png" /><div style="display: inline; opacity: 0.5"><span class="md-boleto-editSeg-label">Boleto Bancário</span></div>`);
    //$('#lifpagCC').html(`<div class="dv-fpag-border" style="margin-left: 8%;"></div>    <img class="md-forma-pag-img" style="margin-left: 8%" src="../ContentAdm/img/credit-card-icon.png" />        <div style="display: inline">            <span class="md-creditcard-edit-label">Cartão</span><span class="md-creditcard-edit-label" style="top: 50%">de Crédito</span>        </div>`);
    $('#lifpagCC').html(``);
    //$('#lifpagDebito').html(`<div class="dv-fpag-border" style="margin-left: 10%; border-style: none"></div><img class="md-forma-pag-img" style="margin-left: 13%; width: 10%; opacity: 0.5" src="../ContentAdm/img/debito_icon.png" /><div style="display: inline; opacity: 0.5"><span class="md-debito-edit-label">Débito</span><span class="md-debito-edit-label" style="top: 50%">em Conta</span>                                                    </div>`);
    $('#lifpagDebito').html(``);
    formaPagamento = 2;
    fpagChange = true;
    $('.fp-debito-edit-segurado').hide();
});
$('#lifpagDebito').click(function () {
    $('#lifpagBoleto').html(`<div class="dv-fpag-border" style="margin-left: 2%; border-style: none"></div><img class="md-forma-pag-img" style="margin-left: 25px; opacity: 0.5" src="../ContentAdm/img/boleto_icon.png" /><div style="display: inline; opacity: 0.5"><span class="md-boleto-editSeg-label">Boleto Bancário</span></div>`);
    //$('#lifpagCC').html(`<div class="dv-fpag-border" style="margin-left: 8%; border-style: none"></div>    <img class="md-forma-pag-img" style="margin-left: 8%; opacity: 0.5" src="../ContentAdm/img/credit-card-icon.png" />        <div style="display: inline; opacity: 0.5">            <span class="md-creditcard-edit-label">Cartão</span><span class="md-creditcard-edit-label" style="top: 50%">de Crédito</span>        </div>`);
    $('#lifpagCC').html(``);
    //$('#lifpagDebito').html(`<div class="dv-fpag-border" style="margin-left: 10%"></div><img class="md-forma-pag-img" style="margin-left: 13%; width: 10%" src="../ContentAdm/img/debito_icon.png" /><div style="display: inline"><span class="md-debito-edit-label">Débito</span><span class="md-debito-edit-label" style="top: 50%">em Conta</span></div>`);
    $('#lifpagDebito').html(``);
    formaPagamento = 3;
    fpagChange = true;
    //$('.fp-debito-edit-segurado').show();

    CarregarComboBancos(0);
});


function ExibirModal() {
    $('#mdSegurado').modal('show');
}
function ConsultarSegurado() {
    //VerificaSession();
    $('#SPRetornoErro').hide();

    $('#mdLoader').modal({
        backdrop: 'static',
        keyboard: false
    });
    $('#mdLoader').modal('show');

    var CPF = $('#mainContent_txtCPF').val().replace(/[^\d]+/g, '');
    var Nome = $('#mainContent_txtSegurado').val();
    var CRM = $('#mainContent_txtCRM').val() == '' ? 0 : $('#mainContent_txtCRM').val();
    var Status = $('#mainContent_cmbStatusAprov').val();
    var Email = $('#mainContent_txtEmail_ConsultarSegurado').val();
    var Telefone = $('#mainContent_txtCelular_ConsultarSegurado').val();
    if (Telefone != null)
        if (Telefone.length > 0)
            Telefone = Telefone.replace(/[^\d]+/g, '');

    var camposValidos = validarCampos(CPF, Nome, Status, CRM, Email, Telefone);


    if (camposValidos) {
        $.ajax({
            method: "POST",
            url: "ConsultaSegurado.aspx/ListarSeguradoPorParams",
            data: '{crm: "' + CRM + '", nome: "' + Nome + '", cpf: "' + CPF + '", ativo: "' + Status + '", email: "' + Email + '", telefone: "' + Telefone + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //Convertendo obj JSON para VW JSON paginação
            //console.log(retorno.d);
            $('#mdLoader').modal('hide');
            var objSeguradoList = [];
            retorno.d.forEach(function (item, index) {
                if (item.MsgErro) {
                    alert(item.MsgErro);
                    return;
                }
                var segurado = new Segurado();
                segurado.IdSegurado = item.IdSegurado;
                segurado.Crm = item.Crm;
                segurado.Cpf = MascaraCPFValue(item.Cpf);
                segurado.Nome = item.Nome;
                segurado.Parentesco = item.Parentesco;
                segurado.Status = item.Status;
                $('#lblTotalResultado').text(item.MaxCount);



                //o atributo htmlButton deverá ter esse msm nome no objeto js da página, e receber o html do botão..                                   
                segurado.htmlButton = '<i class="fa fa-search fa-2x"style="cursor: pointer" data-toggle="tooltip" data-placement="top" title="Detalhar" onclick="DetalharSegurados(' + segurado.IdSegurado + ')"></i>';
                objSeguradoList.push(segurado);
            });

            iniciarPaginacaoCustom(objSeguradoList, 20);
            exibirPaginacaoCustom(1, 20);


            var qntResult = retorno.d.length;
            //$('#lblTotalResultado').text(qntResult);
            //$('#lblTotalResultado').text(retorno.d[0].MaxCount);

            $('#mdLoader').modal('hide');
        });
        $('#dvResultadoSegurado').css('display', 'block');
    }
}
function validarCampos(CPF, Nome, Status, CRM, Email, Telefone) {
    if (CPF == '' && Nome == '' && Status == '' && CRM == '' && Email == '' && Telefone == '') {
        $('#dvResultadoSegurado').css('display', 'none');
        $('#SPRetornoErro').show();
        $('#mdLoader').modal('hide');
        return false;
    }
    else if (CPF !== '') {
        var retorno = verificarCPF(CPF.replace('.', '').replace('.', '').replace('-', ''));
        if (retorno == false) {
            $('#dvResultadoSegurado').css('display', 'none');
            $('#SPtxtCPF').show();
            $('#mdLoader').modal('hide');
            return false;
        }
        return true;
    }

    if ($('#mainContent_txtCelular_ConsultarSegurado').val() != "") {
        if (Telefone.length < 10) {
            $('#dvResultadoSegurado').css('display', 'none');
            $('#SPtxtCelular-CadSegurado').show();
            $('#mdLoader').modal('hide');
            return false;
        }
    }

    if ($('#mainContent_txtEmail_ConsultarSegurado').val() != "") {
        var emailValido = validaEmail('mainContent_txtEmail_ConsultarSegurado');
        if (emailValido == false) {
            $('#dvResultadoSegurado').css('display', 'none');
            $('#SPtxtEmail_ConsultarSegurado').show();
            $('#mdLoader').modal('hide');
            return false;
        }
    }

    return true;
}

//******************** Classes JSON (View Model) *************************************
class Segurado {
    constructor() {
        this.IdSegurado;
        this.Crm;
        this.Cpf;
        this.Nome;
        this.Parentesco;
        this.Status;
        this.StrDtNascimento;
        this.Enderecos;
        this.Dependentes;
        this.Contatos;
        this.Plano;
    }
}
// #endregion
// ######################################################################### ConsultaSegurado.aspx <Modal Dados Segurado> #################################################################
// #region ConsultaSeguradosModalPage
$('document').ready(function () {

    $('#txtAgenciaBanco_editSegurado').keyup(function () {
        $('#SPTXTAgencia_editSegurado').hide();
    });

    $('#txtContaBanco_editSegurado').keyup(function () {
        $('#SPtxtContaBanco_editSegurado').hide();
    });

    $('#cmbPlanoSaude-editSegurado').change(function () {
        ValidarPlanoSaude();
    });
});

var PlanoSaudeChange = false;
var PlanoOdontoChange = false;


// #################   LISTENERS ##########################################################################################################################################################################
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

$(".calendarioPag").datepicker({
    dateFormat: 'dd/mm/yy',
    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
    changeMonth: true,
    changeYear: true,
    onClose: function (dateText, inst) {
        $("#ui-datepicker-div").removeClass("DataPagamentoModal");
    }, beforeShow: function () {
        $("#ui-datepicker-div").addClass("DataPagamentoModal");
    },
    onSelect: function () {
        $(this).next().addClass("active");
    }
});

$('.calendarioMesAno').datepicker({
    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
    changeMonth: true,
    changeYear: true,
    showButtonPanel: true,
    closeText: "Selecionar", // Display text for close link
    currentText: "Hoje",
    dateFormat: 'mm/yy',
    onClose: function (dateText, inst) {
        $("#ui-datepicker-div").removeClass("DatePikerEN");
        var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
        $(this).datepicker('setDate', new Date(year, month, 1));
    }, beforeShow: function () {
        $("#ui-datepicker-div").addClass("DatePikerEN");
    },
    onSelect: function () {
        $(this).next().addClass("active");
    },

});



$('#tglStatusTitular').click(function () {
    SeguradoDeps.Status = SeguradoDeps.Status == 'Ativo' ? 'Inativo' : 'Ativo';
    // Se titular estiver desativado, desativar todos os dependentes
    if (SeguradoDeps.Status == 'Inativo') {
        SeguradoDeps.Dependentes.forEach(function (value) {
            value.Status = 'R';
            DesativarToggleDeps(value.IdDependente);
        });
        InativarTogglesDependentes();
    } else
        AtivarTogglesDependentes();


    //console.log(Segurado.Dependentes);
});

$('#lkSaveTitular').click(function () {
    VerificaSession();
    SalvarDadosTitular();
});
$('#lkSaveCotato').click(function () {
    VerificaSession();
    SalvarContatosTitular();
});
$('#lkEditarTitular').click(function () {
    VerificaSession();
    ShowEdicaoDadosTitular();
});
$('#lk-editar-contato').click(function () {
    VerificaSession();
    ShowEdicaoContatos();
});
$('#faClose').click(function () {
    $('#mdSegurado').modal('hide');
});
$('#lkCollapseSegurado').click(function () {
    if ($('#detalheSegurado').is(':visible'))
        $('#detalheSegurado').hide();
    else
        $('#detalheSegurado').show();
});
$('#lkCollapseContatos').click(function () {
    if ($('#detalheContatos').is(':visible'))
        $('#detalheContatos').hide();
    else
        $('#detalheContatos').show();
});
$('#lkCollapsePlano').click(function () {
    if ($('#detalhePlano').is(':visible'))
        $('#detalhePlano').hide();
    else
        $('#detalhePlano').show();
});
$('#lkCollapseFP').click(function () {
    if ($('#detalheFP').is(':visible'))
        $('#detalheFP').hide();
    else
        $('#detalheFP').show();
});


// #################   EVENTS ########################################################################################################################################
function DetalharSegurados(idSegurado) {
    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/ConsultaSeguradoPorIdAsync",
        data: '{Id: "' + idSegurado + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d.Enderecos[0].Logradouro);
        if (retorno.d) {
            ResetTitular();
            //CarregarComboPlano(retorno.d.IdSegurado);
            let idPlanoSaude = null;
            let idPlanoOdonto = null;
            //Verifica se tem planos, caso exista carrega as combos, setando o plano.
            if (retorno.d.Planos != null)
                if (retorno.d.Planos.length > 0) {
                    retorno.d.Planos.forEach(function (value) {
                        if (value.TipoPlano == 1)       // TIPO DE PLANO SAÚDE                        
                            idPlanoSaude = value.IdPlano;
                        if (value.TipoPlano == 2)       // TIPO DE PLANO ODONTO
                            idPlanoOdonto = value.IdPlano;
                    });
                }

            if (idPlanoSaude) CarregarComboPlanoSaude(idPlanoSaude); else CarregarComboPlanoSaude();
            if (idPlanoOdonto) CarregarComboPlanoOdonto(idPlanoOdonto); else CarregarComboPlanoOdonto();
            CarregarComboSeguradora();

            var id = retorno.d.IdSegurado == null ? '' : retorno.d.IdSegurado;
            var nome = retorno.d.Nome == null ? '' : retorno.d.Nome;
            var crmCompleto = retorno.d.Crm == null ? '' : retorno.d.Crm;
            var crmEstado = retorno.d.CrmEstado == null ? '' : retorno.d.CrmEstado;
            var nrFiliacao = retorno.d.NrFiliacao == null ? '' : retorno.d.NrFiliacao;
            var nrProposta = retorno.d.NrProposta == null ? '' : retorno.d.NrProposta;
            var cpf = retorno.d.Cpf == null ? '' : retorno.d.Cpf;
            var dtNascimento = retorno.d.DataNasc == null ? '' : retorno.d.DataNasc;
            var email = retorno.d.Email == null ? '' : retorno.d.Email;
            var nomePlano = retorno.d.Plano == null ? '' : retorno.d.Plano;
            var dataFiliacao = retorno.d.DataFiliacao == null ? '' : retorno.d.DataFiliacao;
            var estadoCivil = retorno.d.EstadoCivil;
            var idMelhorDiaPagamento = retorno.d.IdMelhorDiaPagamento;
            var sexo = retorno.d.Sexo == null ? '' : retorno.d.Sexo;
            var especialidade = retorno.d.Especialidade == null ? '' : retorno.d.Especialidade;
            var idEspecialidade = retorno.d.IdEspecialidade;
            var nacionalidade = retorno.d.Nacionalidade == null ? '' : retorno.d.Nacionalidade;
            var pispasep = retorno.d.PisPasep == null ? '' : retorno.d.PisPasep;
            var cns = retorno.d.Cns == null ? '' : retorno.d.Cns;
            var nrCarteirinha = retorno.d.NrCarteirinha == null ? '' : retorno.d.NrCarteirinha;
            var dn = retorno.d.Dn == null ? '' : retorno.d.Dn;
            var nomeMae = retorno.d.NomeMae == null ? '' : retorno.d.NomeMae;
            var inicioVigencia = retorno.d.InicioVigencia == null ? '' : retorno.d.InicioVigencia;
            var fimVigencia = retorno.d.FimVigencia == null ? '' : retorno.d.FimVigencia;

            $('#hddIdSegurado').val(id);
            $('#sp-nome-titular').text(nome);
            $('#sp-nr-filiacao').text(nrFiliacao);
            $('#sp-crm').text(`${crmCompleto}-${crmEstado}`);
            $('#sp-nr-proposta').text(nrProposta);
            $('#sp-cpf-titular').text(MascaraCPFValue(cpf));
            $('#sp-dt-nascimento-titular').text(dtNascimento);
            $('#sp-dt-filiacao-titular').text(dataFiliacao);
            $('#sp-estado-civil-titular').text(estadoCivil);
            $('#sp-sexo-titular').text(sexo);
            $('#sp-especialidade-titular').text(especialidade);
            $('#sp-nacionalidade-titular').text(nacionalidade);
            $('#sp-pispasep-titular').text(pispasep);
            $('#sp-cns-titular').text(cns);
            $('#sp-nr-carteirinha-titular').text(nrCarteirinha);
            $('#sp-dn-titular').text(dn);
            $('#sp-nome-mae-titular').text(nomeMae);
            $('#sp-inicio-vigencia-titular').text(inicioVigencia);
            $('#sp-fim-vigencia-titular').text(fimVigencia);
            $('#hdd-plano-saude-atual').val(idPlanoSaude);

            //Carrega combos
            CarregarComboEspecialidadeManterSegurado(idEspecialidade);
            CarregarComboMelhorDia(idMelhorDiaPagamento);

            var logradouro;
            var bairro;
            var cidade;
            var uf;
            var cep;

            if (retorno.d.Enderecos.length > 0) {
                logradouro = retorno.d.Enderecos[0].end_endereco == null ? '' : retorno.d.Enderecos[0].end_endereco;
                bairro = retorno.d.Enderecos[0].end_bairro == null ? '' : retorno.d.Enderecos[0].end_bairro;
                cidade = retorno.d.Enderecos[0].end_cidade == null ? '' : retorno.d.Enderecos[0].end_cidade;
                uf = retorno.d.Enderecos[0].end_estado == null ? '' : retorno.d.Enderecos[0].end_estado;
                cep = retorno.d.Enderecos[0].end_cep == null ? '' : retorno.d.Enderecos[0].end_cep;
            }

            var DDD;
            var DDDCelular;
            var telefone;
            var celular;

            if (retorno.d.Contatos.length > 0) {
                DDD = retorno.d.Contatos[0].cnt_ddd == null ? '' : retorno.d.Contatos[0].cnt_ddd;
                DDDCelular = retorno.d.Contatos[0].cnt_ddd_celular == null ? '' : retorno.d.Contatos[0].cnt_ddd_celular;
                telefone = retorno.d.Contatos[0].cnt_fone == null ? '' : retorno.d.Contatos[0].cnt_fone;
                celular = retorno.d.Contatos[0].cnt_celular == null ? '' : retorno.d.Contatos[0].cnt_celular;
            }

            $('#hddIdSegurado').text(id);
            $('#sp-nome-titular').text(nome);
            $('#sp-cpf-titular').text(MascaraCPFValue(cpf));


            if (retorno.d.Enderecos.length > 0) {
                if (logradouro.length > 0)
                    $('#sp-endereco-titular').text(logradouro);
                else
                    $('#sp-endereco-titular').text('');

                $('#sp-bairro-titular').text(bairro);
                $('#sp-cidade-titular').text(cidade);
                $('#sp-uf-titular').text(uf);
                $('#sp-cep-titular').text(MascaraCEPValue(cep));
            }
            if (retorno.d.Contatos.length > 0) {
                let nrtel = telefone.trim();
                let nrcel = celular.trim();
                if (isNullOrEmpty(DDD) == false && isNullOrEmpty(nrtel) == false)
                    $('#sp-telefone-titular').text(MascaraTelefoneValue(DDD + '' + telefone));
                else
                    $('#sp-telefone-titular').text('');
                if (isNullOrEmpty(DDDCelular) == false && isNullOrEmpty(nrcel) == false)
                    $('#sp-celular-titular').text(MascaraCelularValue(DDDCelular + '' + celular));
                else
                    $('#sp-celular-titular').text('');
            }


            $('#sp-email-titular').text(email);
            $('#sp-plano').text(nomePlano);

            // Forma de pagamento
            switch (retorno.d.IdFormaPagamento) {
                case 1:
                    FPagBoletoChecked();
                    break;
                case 2:
                    FPagCCChecked();
                    break;
                case 3:
                    FPagDebitoChecked();
                    // Carrega dados de cobrança, caso exista
                    if (retorno.d.DadosCobranca != null)
                        if (retorno.d.DadosCobranca.length > 0) {
                            //let IdBanco = retorno.d.DadosCobranca[0].dco_ban_id;
                            let IdBanco = retorno.d.DadosCobranca[0].dco_ban_id;
                            let auxConta = retorno.d.DadosCobranca[0].dco_conta;
                            let auxAgencia = retorno.d.DadosCobranca[0].dco_agencia;

                            $('#txtAgenciaBanco_editSegurado').val(auxAgencia);
                            $('#txtContaBanco_editSegurado').val(auxConta);

                            CarregarComboBancos(IdBanco);
                            //$('#cmbBanco_debito').val(IdBanco);
                        }
                    break;
            }


            // Status do Titular
            if (retorno.d.Status == 'Ativo')
                AtivarTitular();
            else
                DesativarTitular();

            SeguradoDeps.IdSegurado = retorno.d.IdSegurado;
            SeguradoDeps.Status = retorno.d.Status;
            // Carrega lista de dependentes
            if (retorno.d.Dependentes.length > 0) {
                SeguradoDeps.Dependentes = retorno.d.Dependentes;
                ListaDependentes(SeguradoDeps.Dependentes);
            } else
                NenhumDependenteExibido();


            $('#mdSegurado').modal('show');
        }
    });
}
function AlterouPlanoSaude() {
    PlanoSaudeChange = true;
}
function AlterouPlanoOdonto() {
    PlanoOdontoChange = true;
}
function SalvarStatusSegurado() {
    //console.log(Segurado);
    var objSeg = SeguradoDeps;

    var objDep = {};
    var objDeps = Array();

    if (objSeg.Dependentes.length > 0)
        objSeg.Dependentes.forEach(function (value) {
            let stsDep = (value.Status == 'Ativo' || value.Status == 'A') ? 'A' : 'R';
            objDep = {
                IdDependente: value.IdDependente,
                Status: stsDep
            };

            objDeps.push(objDep);
        });


    var data = {
        Segurado: {
            IdSegurado: objSeg.IdSegurado,
            Status: objSeg.Status,
            Dependentes: objDeps
        }
    };
    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/SalvarStatusSeguradoAsync",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"

    }).done(function (retorno) {
        //console.log(retorno.d); 
        // Após a atualização do status do segurado, atualizar também a pesquisa anterior.
        ConsultarSegurado();
        $('#mdSegurado').modal('hide');
    });
}
function OnChangeCmbBancosCadSegurado() {
    $('#SPCMBBancoCadSegurado').hide();
}
function OnChangeCmbBancos() {
    $('#SPCMBBanco_debito').hide();
}
function ValidarCamposEditDeps(idDep) {
    if (isNullOrEmpty($('#cmbSexo-editDependente-' + idDep).val())) {
        $('#sp-msg-error-deps-' + idDep).text('Campo Sexo obrigatório');
        $('#sp-msg-error-deps-' + idDep).show();
        return false;
    }
    if (isNullOrEmpty($('#txtCPF-editDependente-' + idDep).val()) == false) {
        var CPFValido = $('#txtCPF-editDependente-' + idDep).val();
        CPFValido = CPFValido.replace(/[^\d]+/g, '');
        if (verificarCPF(CPFValido) == false) {
            $('#sp-msg-error-deps-' + idDep).text('Campo CPF inválido');
            $('#sp-msg-error-deps-' + idDep).show();
            return false;
        }
    }

    if (isNullOrEmpty($('#txtDtNasc-editDependente-' + idDep).val())) {
        $('#sp-msg-error-deps-' + idDep).text('Campo Data de Nascimento obrigatório');
        $('#sp-msg-error-deps-' + idDep).show();
        return false;
    }

    if (!isNullOrEmpty($('#txtTelefone-editDependente-' + idDep).val())) {
        let fone = $('#txtTelefone-editDependente-' + idDep).val().replace(/[^\d]+/g, '');
        if (fone.length < 10) {
            $('#sp-msg-error-deps-' + idDep).text('Número de Telefone inválido');
            $('#sp-msg-error-deps-' + idDep).show();
            return false;
        }
    }

    if (!isNullOrEmpty($('#txtCelular-editDependente-' + idDep).val())) {
        let cel = $('#txtCelular-editDependente-' + idDep).val().replace(/[^\d]+/g, '');
        if (cel.length < 11) {
            $('#sp-msg-error-deps-' + idDep).text('Número de Celular inválido');
            $('#sp-msg-error-deps-' + idDep).show();
            return false;
        }
    }
    if (!isNullOrEmpty($('#txtEmail-editDependente-' + idDep).val())) {
        var emailValido = validaEmail('txtEmail-editDependente-' + idDep);
        if (emailValido == false) {
            $('#sp-msg-error-deps-' + idDep).text('E-mail inválido');
            $('#sp-msg-error-deps-' + idDep).show();
            return false;
        }
    }

    $('#sp-msg-error-deps-' + idDep).text('');
    $('#sp-msg-error-deps-' + idDep).hide();
    return true;
}
function SalvarDadosDependente(idDep) {
    VerificaSession();

    ShowSmallLoader_dependente(idDep);

    if (ValidarCamposEditDeps(idDep) == false) {
        HideIcons_dependente(idDep);
        return;
    }


    var data = {
        Dependente: {
            dep_id: idDep,
            dep_nome: $('#sp-nome-dependente-' + idDep).text(),
            dep_civ_id: $('#cmbEstadoCivilDependente-' + idDep).val(),
            dep_sexo: $('#cmbSexo-editDependente-' + idDep).val(),
            dep_data_nascimento: DateSerializer($('#txtDtNasc-editDependente-' + idDep).val()),
            dep_prf_id: $('#cmbProfissaoDependente-editDependente-' + idDep).val(),
            dep_cpf: $('#txtCPF-editDependente-' + idDep).val().replace(/[^\d]+/g, ''),
            dep_nacionalidade: $('#cmbNacionalidadeDependente-' + idDep).val(),
            dep_nome_mae: $('#txtNomeMae-editDependente-' + idDep).val(),
            dep_cns: $('#txtCNS-editDependente-' + idDep).val(),
            dep_pispasep: $('#txtPIS-editDependente-' + idDep).val().replace(/[^\d]+/g, ''),
            dep_dn: $('#txtDN-editDependente-' + idDep).val(),
            dep_numero_carteira: $('#txtNrCarteirinha-editDependente-' + idDep).val(),
            dep_inicio_vigencia: DateSerializer($('#txtDtInicioVig-editDependente-' + idDep).val()),
            dep_fim_vigencia: DateSerializer($('#txtDtFimVig-editDependente-' + idDep).val()),
            dep_email: $('#txtEmail-editDependente-' + idDep).val(),
        },
        Endereco: {
            end_id: 0,
            end_dep_id: idDep,
            end_cpf: $('#txtCPF-editDependente-' + idDep).val().replace(/[^\d]+/g, ''),
            end_endereco: $('#txtEndereco-editDependente-' + idDep).val(),
            end_bairro: $('#txtBairro-editDependente-' + idDep).val(),
            end_cidade: $('#txtCidade-editDependente-' + idDep).val(),
            end_cep: $('#txtCEP-editDependente-' + idDep).val().replace(/[^\d]+/g, ''),
            end_estado: $('#cmbUF-editDependente-' + idDep).val(),
            end_par_id: 'D'
        },
        Contato: {
            cnt_dep_id: idDep,
            cnt_cpf: $('#txtCPF-editDependente-' + idDep).val().replace(/[^\d]+/g, ''),
            cnt_fone: $('#txtTelefone-editDependente-' + idDep).val().replace(/[^\d]+/g, ''),
            cnt_ddd: '',
            cnt_par_id: 'D',
            cnt_celular: $('#txtCelular-editDependente-' + idDep).val().replace(/[^\d]+/g, ''),
        },
        Plano:
        {
            pls_id: 0,
            pls_seg_id: 0,
            pls_pla_id: $('#cmbPlanoOdontoDependente-editDependente-' + idDep).val() != null ? $('#cmbPlanoOdontoDependente-editDependente-' + idDep).val() : 0,
            pls_par_id: 'D'
        }
    };

    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/SalvarDadosDependentesAsync",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d.includes('erro') || retorno.d.includes('Erro')) {
            //$('#msgErroDependente-' + idDep).text(retorno.d);
            $('#icon-wait-dependente-' + idDep).hide();
            $('#msgErroDependente-' + idDep).show();
        } else
            ShowIconSavedSegurado_dependente(idDep);
    });
}
function SalvarDadosTitular() {
    ShowSmallLoader_titular();


    let nrFiliacao = $('#txtNRFiliacao-editSegurado').val() != null ? $('#txtNRFiliacao-editSegurado').val() : 0;
    let nrProposta = $('#txtNRProposta-editSegurado').val() != null ? $('#txtNRProposta-editSegurado').val() : 0;
    let crm = $('#txtNRCRM-editSegurado').val() + '' + $('#cmbCRMestado-editSegurado').val();

    var data = {
        Segurado: {
            seg_id: $('#hddIdSegurado').val(),
            seg_nome: $('#sp-nome-titular').text(),
            seg_crm: $('#txtNRCRM-editSegurado').val(),
            seg_crm_estado: $('#cmbCRMestado-editSegurado').val(),
            seg_esp_id: $('#cmbEspecialidade-edit-titular').val(),
            seg_data_nascimento: DateSerializer($('#txtDtNasc-editSegurado').val()),
            seg_sexo: $('#cmbSexo-editSegurado').val(),
            seg_civ_id: $('#cmbEstadoCivilSegurado').val(),
            seg_cpf: $('#txtCPF-editSegurado').val().replace(/[^\d]+/g, ''),
            seg_nome_mae: $('#txtNomeMae-editSegurado').val(),
            seg_nacionalidade: $('#cmbNacionalidade-editSegurado').val(),
            seg_pispasep: $('#txtPIS-editSegurado').val().replace(/[^\d]+/g, ''),
            seg_cns: $('#txtCNS-editSegurado').val().replace(/[^\d]+/g, ''),
            seg_numero_carteira: $('#txtNrCarteirinha-editSegurado').val(),
            seg_dn: $('#txtDN-editSegurado').val().replace(/[^\d]+/g, ''),
            seg_pla_id: $('#cmbPlano-editSegurado').val(),
            seg_dt_filiacao: DateSerializer($('#txtDtFiliacao-editSegurado').val()),
            seg_inicio_vigencia: DateSerializer($('#txtDtInicioVig-editSegurado').val()),
            seg_fim_vigencia: DateSerializer($('#txtDtFimVig-editSegurado').val()),
            seg_num_filiacao: parseInt(nrFiliacao),
            seg_num_proposta: parseInt(nrProposta),
        },
        Endereco: {
            end_id: 0,
            end_seg_id: $('#hddIdSegurado').val(),
            end_cpf: $('#txtCPF-editSegurado').val().replace(/[^\d]+/g, ''),
            end_endereco: $('#txtEndereco-editSegurado').val(),
            end_bairro: $('#txtBairro-editSegurado').val(),
            end_cidade: $('#txtCidade-editSegurado').val(),
            end_cep: $('#txtCEP-editSegurado').val().replace(/[^\d]+/g, ''),
            end_estado: $('#cmbUF-editSegurado').val(),
            end_par_id: 'T'
        }
    };

    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/SalvarDadosTitularAsync",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);       
        if (retorno.d != null)
            if (retorno.d.includes('erro')) {
                alert('Erro na tentativa de Salvar o titular. Erro no dev tools log.');
                //console.log(retorno.d);
                return;
            }

        ShowIconSavedSegurado_titular();
    });
}
function CarregarComboEspecialidadeManterSegurado(idEsp = 0) {
    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/ListarEspecialidades",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                retorno.d.forEach(function (value) {
                    if (value.IdEspecialidade == idEsp)
                        $('#cmbEspecialidade-edit-titular').append('<option value="' + value.IdEspecialidade + '" selected>' + value.Descricao + '</option>');
                    else
                        $('#cmbEspecialidade-edit-titular').append('<option value="' + value.IdEspecialidade + '">' + value.Descricao + '</option>');
                });
            }
    });
}
function ValidarDadosContatoTitular() {
    if (!isNullOrEmpty($('#txtTelefone-editSegurado').val())) {
        let fone = $('#txtTelefone-editSegurado').val().replace(/[^\d]+/g, '');
        if (fone.length < 10) {
            HideSmallIcons_contato();
            $('#sp-msg-error-contato-titular').text('Número de Telefone inválido');
            $('#sp-msg-error-contato-titular').show();
            return false;
        }
    }

    if (!isNullOrEmpty($('#txtCelular-editSegurado').val())) {
        let cel = $('#txtCelular-editSegurado').val().replace(/[^\d]+/g, '');
        if (cel.length < 11) {
            HideSmallIcons_contato();
            $('#sp-msg-error-contato-titular').text('Número de Celular inválido');
            $('#sp-msg-error-contato-titular').show();
            return false;
        }
    }
    if (!isNullOrEmpty($('#txtEmail-editSegurado').val())) {
        var emailValido = validaEmail('txtEmail-editSegurado');
        if (emailValido == false) {
            HideSmallIcons_contato();
            $('#sp-msg-error-contato-titular').text('Email inválido');
            $('#sp-msg-error-contato-titular').show();
            return false;
        }
    }
    $('#sp-msg-error-contato-titular').hide();
    return true;
}
function SalvarContatosTitular() {

    if (ValidarDadosContatoTitular() == false)
        return;

    ShowSmallLoader_contato();
    var data = {
        Contato: {
            IdTitular: $('#hddIdSegurado').val(),
            Cpf: $('#sp-cpf-titular').text().replace(/[^\d]+/g, ''),
            Telefone: $('#txtTelefone-editSegurado').val(),
            Celular: $('#txtCelular-editSegurado').val(),
            Email: $('#txtEmail-editSegurado').val()
        }
    };

    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/SalvarContatosSeguradoAsync",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);
        ShowIconSavedSegurado_contato();
    });
}

function ValidarPlanoSaude() {
    let planoSaudeAtual = $('#hdd-plano-saude-atual').val();
    let planoSaudeSelecionado = $('#cmbPlanoSaude-editSegurado').val();
    if (planoSaudeAtual && planoSaudeSelecionado) {
        planoSaudeAtual = parseInt(planoSaudeAtual);
        planoSaudeSelecionado = parseInt(planoSaudeSelecionado);
    }

    if (planoSaudeSelecionado < planoSaudeAtual) {
        $('#dv-plano-error').show();
        return false;
    } else {
        $('#dv-plano-error').hide();
        return true;
    }
}

function SalvarPlanoTitular() {
    var arrPlanSeg = Array();
    var planSaude = {};
    var planOdonto = {};

    planSaude = {
        pls_seg_id: $('#hddIdSegurado').val(),
        pls_pla_id: $('#cmbPlanoSaude-editSegurado').val(),
    };
    arrPlanSeg.push(planSaude);

    planOdonto = {
        pls_seg_id: $('#hddIdSegurado').val(),
        pls_pla_id: $('#cmbPlanoOdonto-editSegurado').val(),
    };
    arrPlanSeg.push(planOdonto);

    if (arrPlanSeg.length > 0) {
        var data = {
            PlanosSegurado: arrPlanSeg
        };

        $.ajax({
            method: "POST",
            url: "ConsultaSegurado.aspx/SalvarPlanoTitularAsync",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            PlanoSaudeChange = false;
            PlanoOdontoChange = false;
        });
    }
}

function SalvarMelhorDiaPagamento() {
    var data = {
        Segurado: {
            seg_id: $('#hddIdSegurado').val(),
            seg_mdp_id: $('#cmbMelhorDia-editSegurado').val()
        }
    };


    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/SalvarMelhorDiaPagamentoAsync",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {

    });
}


function SalvarFormaPagamento() {
    var data = {
        Segurado: {
            seg_id: $('#hddIdSegurado').val(),
            seg_for_id: formaPagamento
        },
        DadosCobranca: {
            dco_id: 0,
            dco_seg_id: $('#hddIdSegurado').val(),
            dco_agencia: $('#txtAgenciaBanco_editSegurado').val(),
            dco_conta: $('#txtContaBanco_editSegurado').val(),
            dco_ban_id: $('#cmbBanco_debito').val()
        }
    };


    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/SalvarFormaPagamentoAsync",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {

    });
}
function ValidarFormaPagamento_CadSegurado() {
    let validou = true;
    let auxBanco = isNullOrEmpty($('#cmbBanco_cadSegurado').val()) ? '' : $('#cmbBanco_cadSegurado').val();
    let auxConta = isNullOrEmpty($('#mainContent_txtConta_cadSegurado').val()) ? '' : $('#mainContent_txtConta_cadSegurado').val();
    let auxAgencia = isNullOrEmpty($('#mainContent_txtAgencia_cadSeguro').val()) ? '' : $('#mainContent_txtAgencia_cadSeguro').val();

    if (auxAgencia == '') {
        $('#SPtxtAgencia_cadSeguro').show();
        $('#cmbBanco_cadSegurado').focus();
        validou = false;
    } else {
        $('#SPtxtAgencia_cadSeguro').hide();
    }
    if (auxConta == '') {
        $('#SPtxtConta_cadSegurado').show();
        $('#mainContent_txtConta_cadSegurado').focus();
        validou = false;
    } else {
        $('#SPtxtConta_cadSegurado').hide();
    }
    if (auxBanco == '0') {
        $('#SPCMBBancoCadSegurado').show();
        $('#mainContent_txtAgencia_cadSeguro').focus();
        validou = false;
    } else {
        $('#SPCMBBancoCadSegurado').hide();
    }
    return validou;
}
function ValidarFormaPagamento() {
    let validou = true;
    if ($('#txtAgenciaBanco_editSegurado').val() == '') {
        $('#SPTXTAgencia_editSegurado').show();
        validou = false;
    }
    if ($('#txtContaBanco_editSegurado').val() == '') {
        $('#SPtxtContaBanco_editSegurado').show();
        validou = false;
    }
    if ($('#cmbBanco_debito').val() == '0') {
        $('#SPCMBBanco_debito').show();
        validou = false;
    }
    return validou;
}
function SalvarTudo() {
    VerificaSession();

    // validação e salvar plano.
    if ($(`#detalhePlano`).is(':visible')) {
        if (ValidarPlanoSaude())
            SalvarPlanoTitular();
        else
            return;
    }

    // Salvando Forma pagamento titular    
    if (fpagChange)
        if (formaPagamento == 3)
            if (!ValidarFormaPagamento())
                return;

    if ($(`#detalheFP`).is(':visible'))
        SalvarMelhorDiaPagamento();
    //SalvarFormaPagamento();

    // verifica se container de edição do titular está visivel.
    if ($(`#detalheTitular-edit`).is(':visible'))
        SalvarDadosTitular();

    // verifica se container de edição dos contatos está visivel.
    if ($(`#detalheContatos-edit`).is(':visible')) {
        //console.log('salva contatos');
        SalvarContatosTitular();
    }

    // Iterar os containers, e verificar os q estão abertos. Somente os q estão abertos serão salvos.
    if (SeguradoDeps != null)
        if (SeguradoDeps.Dependentes.length > 0) {
            SeguradoDeps.Dependentes.forEach(function (value) {
                if ($(`#dv-dependente-collapse-edit-${value.IdDependente}`).is(':visible')) {
                    SalvarDadosDependente(value.IdDependente);
                }
            });
        }

    SalvarStatusSegurado();
}
function CarregarComboPlanoSaude(IdPlanoSaude = 99999) {
    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/CarregarComboPlanoAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        let options = '';
        if (retorno.d != null) {
            retorno.d.forEach(function (value, index) {
                if (value.IdPlano != 99999 && value.TipoPlano == 1) {   // Filtra os planos de saúde
                    if (IdPlanoSaude == value.IdPlano)
                        options += `<option value=${value.IdPlano} selected>${PrimeiraLetraMaiuscula((value.Descricao || ''))}</option>`;
                    else
                        options += `<option value=${value.IdPlano}>${PrimeiraLetraMaiuscula((value.Descricao || ''))}</option>`;
                }
            });
            let cmbPlanoSaude = `
                        <select class="dropdown-material-titular cmb-plano-saude" id="cmbPlanoSaude-editSegurado" onchange="AlterouPlanoSaude()" style="width: 45%;">                    
                            ${options}
                        </select>
                        `;
            $('#dv-cmbPlanoSaude-editSegurado').html(cmbPlanoSaude);
        }
    });
}

function CarregarComboMelhorDia(IdMelhorDia = 0) {
    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/CarregarComboMelhorDiaAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        let options = '';
        if (retorno.d != null) {
            retorno.d.forEach(function (value, index) {
                if (index == 0)
                    options += `<option value="0">Selecione</option>`;
                if (value.Id == IdMelhorDia)
                    options += `<option value=${value.Id} selected>${value.MelhorDia}</option>`;
                else
                    options += `<option value=${value.Id}>${value.MelhorDia}</option>`;

            });
            let cmbMelhorDia = `
                        <select class="dropdown-material-titular cmb-plano-saude" id="cmbMelhorDia-editSegurado" style="margin: 8px 0 0; width: 65%;">                    
                            ${options}
                        </select>
                        `;
            $('#dv-cmbMelhorDia-editSegurado').html(cmbMelhorDia);
        }
    });
}

function CarregarComboPlanoOdonto(IdPlanoOdonto = 99999) {
    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/CarregarComboPlanoAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        let options = '';
        if (retorno.d != null) {
            retorno.d.forEach(function (value, index) {
                if (value.IdPlano != 10000 && value.TipoPlano == 2) {   // Filtra os planos de odonto
                    if (IdPlanoOdonto == value.IdPlano)
                        options += `<option value=${value.IdPlano} selected>${PrimeiraLetraMaiuscula((value.Descricao || ''))}</option>`;
                    else
                        options += `<option value=${value.IdPlano}>${PrimeiraLetraMaiuscula((value.Descricao || ''))}</option>`;
                }
            });
            let cmbPlanoOdonto = `
                        <select class="dropdown-material-titular cmb-plano-odonto" id="cmbPlanoOdonto-editSegurado" onchange="AlterouPlanoOdonto()" style="width: 45%">
                            ${options}
                        </select>
                        `;
            $('#dv-cmbPlanoSaudeOdonto-editSegurado').html(cmbPlanoOdonto);
        }
    });
}
function CarregarComboBancosCadSegurado(idBanco = 0) {
    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/ListarBancos",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                let options = '';
                options = `<option value=0>Banco</option>`;
                retorno.d.forEach(function (value) {
                    if (idBanco == value.CodBanco)
                        options += `<option value=${value.CodBanco} selected>${value.CodBanco} - ${PrimeiraLetraMaiuscula(value.Nome)}</option>`;
                    else
                        options += `<option value=${value.CodBanco}>${value.CodBanco} - ${PrimeiraLetraMaiuscula(value.Nome)}</option>`;
                });
                let cmbBanco = `
                    <select id="cmbBanco_cadSegurado" class="dropdown-material" onchange="OnChangeCmbBancosCadSegurado()" style="margin-top: 23px">                    
                        ${options}
                    </select>
                    `;
                $('#dv_cmbBanco_cadSegurado').html(cmbBanco);
            }
    });
}
function CarregarComboBancos(idBanco = 0) {
    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/ListarBancos",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                let options = '';
                options = `<option value=0>Banco</option>`;
                retorno.d.forEach(function (value) {
                    if (value.IdPlano != 99999) {

                        if (idBanco == value.CodBanco)
                            options += `<option value=${value.CodBanco} selected>${value.CodBanco} - ${PrimeiraLetraMaiuscula(value.Nome)}</option>`;
                        else
                            options += `<option value=${value.CodBanco}>${value.CodBanco} - ${PrimeiraLetraMaiuscula(value.Nome)}</option>`;
                    }
                });
                let cmbBanco = `
                    <select class="dropdown-material-titular" id="cmbBanco_debito"  onchange="OnChangeCmbBancos()" style="margin-top: 8px">
                        ${options}
                    </select>
                    `;
                $('#dv_cmb_banco').html(cmbBanco);
            }

    });
}
function ShowEdicaoDadosDependente(idDep) {

    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/ConsultaDependentePorIdAsync",
        data: '{Id: "' + idDep + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        // CARREGA DADOS NOS INPUTTEXT
        if (retorno.d.EstadoCivil == 'Solteiro(a)')
            $('#cmbEstadoCivilDependente-' + idDep).val('1');
        if (retorno.d.EstadoCivil == 'Casado(a)')
            $('#cmbEstadoCivilDependente-' + idDep).val('2');
        if (retorno.d.EstadoCivil == 'Viúvo(a)')
            $('#cmbEstadoCivilDependente-' + idDep).val('3');
        if (retorno.d.EstadoCivil == 'Desquitado(a)')
            $('#cmbEstadoCivilDependente-' + idDep).val('4');
        if (retorno.d.EstadoCivil == 'Divorciado(a)')
            $('#cmbEstadoCivilDependente-' + idDep).val('5');
        let idPrf = retorno.d.IdProfissao;
        $('#cmbProfissaoDependente-editDependente-' + idDep).val(idPrf);
        let idPlano = retorno.d.IdPlanoOdonto;
        $('#cmbPlanoOdontoDependente-editDependente-' + idDep).val(idPlano);
        if (retorno.d.Sexo == 'Masculino')
            $('#cmbSexo-editDependente-' + idDep).val('M');
        if (retorno.d.Sexo == 'Feminino')
            $('#cmbSexo-editDependente-' + idDep).val('F');

        $('#txtDtNasc-editDependente-' + idDep).val(retorno.d.DataNasc);
        $('#txtProfissao-editDependente-' + idDep).val(retorno.d.Especialidade);
        let auxCpf = retorno.d.Cpf.trim();
        if (!isNullOrEmpty(auxCpf))
            $('#txtCPF-editDependente-' + idDep).val(retorno.d.Cpf);
        if (retorno.d.Enderecos.length > 0) {
            $('#txtCEP-editDependente-' + idDep).val(retorno.d.Enderecos[0].end_cep);
            $('#txtEndereco-editDependente-' + idDep).val(retorno.d.Enderecos[0].end_endereco);
            $('#txtBairro-editDependente-' + idDep).val(retorno.d.Enderecos[0].end_bairro);
            $('#txtCidade-editDependente-' + idDep).val(retorno.d.Enderecos[0].end_cidade);
            $('#cmbUF-editDependente-' + idDep).val(retorno.d.Enderecos[0].end_estado);
        }

        $('#cmbNacionalidadeDependente-' + idDep).val(retorno.d.Nacionalidade);
        $('#txtNomeMae-editDependente-' + idDep).val(retorno.d.NomeMae);
        $('#txtCNS-editDependente-' + idDep).val(retorno.d.Cns);
        $('#txtPIS-editDependente-' + idDep).val(retorno.d.PisPasep);
        $('#txtDN-editDependente-' + idDep).val(retorno.d.Dn);
        $('#txtNrCarteirinha-editDependente-' + idDep).val(retorno.d.NrCarteirinha);
        $('#txtDtInicioVig-editDependente-' + idDep).val(retorno.d.InicioVigencia);
        $('#txtDtFimVig-editDependente-' + idDep).val(retorno.d.FimVigencia);

        if (retorno.d.Contatos.length > 0) {
            let fone = retorno.d.Contatos[0].cnt_ddd.trim() + '' + retorno.d.Contatos[0].cnt_fone.trim();
            let celular = retorno.d.Contatos[0].cnt_ddd_celular.trim() + '' + retorno.d.Contatos[0].cnt_celular.trim();
            if (isNullOrEmpty(celular))
                celular = '';
            else
                celular = MascaraCelularValue(celular);

            if (isNullOrEmpty(fone))
                fone = '';
            else
                fone = MascaraTelefoneValue(fone);


            $('#txtCelular-editDependente-' + idDep).val(celular);
            $('#txtTelefone-editDependente-' + idDep).val(fone);
        }

        $('#txtEmail-editDependente-' + idDep).val(retorno.d.Email);
    });


    // Troca de quadro     
    $(`#dv-dependente-collapse-read-${idDep}`).hide();
    $(`#dv-dependente-collapse-edit-${idDep}`).show();
}
function ShowEdicaoDadosTitular() {
    // CARREGA DADOS NOS INPUTTEXT
    //$('#txtCRM-editSegurado').val($('#sp-crm').text());

    let spCrm = $('#sp-crm').text();
    let arrCrm = spCrm.split('-');
    if (arrCrm.length > 0) {
        $('#txtNRCRM-editSegurado').val(arrCrm[0]);
        $('#cmbCRMestado-editSegurado').val(arrCrm[1]);
    }

    $('#txtNRFiliacao-editSegurado').val($('#sp-nr-filiacao').text());
    $('#txtNRProposta-editSegurado').val($('#sp-nr-proposta').text());
    $('#txtEspecialidade-editSegurado').val($('#sp-especialidade-titular').text());
    $('#txtDtNasc-editSegurado').val($('#sp-dt-nascimento-titular').text());
    if ($('#sp-sexo-titular').text() == 'Masculino')
        $('#cmbSexo-editSegurado').val('M');
    if ($('#sp-sexo-titular').text() == 'Feminino')
        $('#cmbSexo-editSegurado').val('F');
    if ($('#sp-estado-civil-titular').text() == 'Solteiro(a)')
        $('#cmbEstadoCivilSegurado').val('1');
    if ($('#sp-estado-civil-titular').text() == 'Casado(a)')
        $('#cmbEstadoCivilSegurado').val('2');
    if ($('#sp-estado-civil-titular').text() == 'Viúvo(a)')
        $('#cmbEstadoCivilSegurado').val('3');
    if ($('#sp-estado-civil-titular').text() == 'Desquitado(a)')
        $('#cmbEstadoCivilSegurado').val('4');
    if ($('#sp-estado-civil-titular').text() == 'Divorciado(a)')
        $('#cmbEstadoCivilSegurado').val('5');



    $('#txtCPF-editSegurado').val($('#sp-cpf-titular').text());
    $('#txtNomeMae-editSegurado').val($('#sp-nome-mae-titular').text());
    $('#txtCEP-editSegurado').val($('#sp-cep-titular').text());
    $('#txtEndereco-editSegurado').val($('#sp-endereco-titular').text());
    $('#txtBairro-editSegurado').val($('#sp-bairro-titular').text());
    $('#txtBairro-editSegurado').val($('#sp-bairro-titular').text());
    $('#txtCidade-editSegurado').val($('#sp-cidade-titular').text());
    $('#cmbUF-editSegurado').val($('#sp-uf-titular').text());
    $('#txtNacionalidade-editSegurado').val($('#sp-nacionalidade-titular').text());
    $('#txtPIS-editSegurado').val($('#sp-pispasep-titular').text());
    $('#txtCNS-editSegurado').val($('#sp-cns-titular').text());
    $('#txtNrCarteirinha-editSegurado').val($('#sp-nr-carteirinha-titular').text());
    $('#txtDN-editSegurado').val($('#sp-dn-titular').text());
    $('#txtDtFiliacao-editSegurado').val($('#sp-dt-filiacao-titular').text());
    $('#txtDtInicioVig-editSegurado').val($('#sp-inicio-vigencia-titular').text());
    $('#txtDtFimVig-editSegurado').val($('#sp-fim-vigencia-titular').text());

    // bloquear campos 
    $("#txtCRM-editSegurado").prop("readonly", true);
    $("#txtEspecialidade-editSegurado").prop("readonly", true);
    $("#txtCPF-editSegurado").prop("readonly", true);

    // Troca de quadro 
    $('#detalheTitular-read').hide();
    $('#detalheTitular-edit').show();
}
function ShowEdicaoContatos() {
    // CARREGA DADOS NOS INPUTTEXT
    $('#txtTelefone-editSegurado').val($('#sp-telefone-titular').text());
    $('#txtCelular-editSegurado').val($('#sp-celular-titular').text());
    $('#txtEmail-editSegurado').val($('#sp-email-titular').text());

    $('#detalheContatos, #detalheContatos-read').hide();
    $('#detalheContatos, #detalheContatos-edit').show();
}
function ShowSmallLoader_contato() {
    $('#icon-saved-contato').hide();
    $('#icon-wait-contato').show();
}
function HideSmallIcons_contato() {
    $('#icon-saved-contato').hide();
    $('#icon-wait-contato').hide();
}
function ShowIconSavedSegurado_contato() {
    $('#icon-wait-contato').hide();
    $('#icon-saved-contato').show();
}
function ShowSmallLoader_titular() {
    $('#icon-saved-titular').hide();
    $('#icon-wait-titular').show();
}
function ShowSmallLoader_dependente(idDep) {
    $('#icon-saved-dependente-' + idDep).hide();
    $('#icon-wait-dependente-' + idDep).show();
}
function HideIcons_dependente(idDep) {
    $('#icon-saved-dependente-' + idDep).hide();
    $('#icon-wait-dependente-' + idDep).hide();
}
function ShowIconSavedSegurado_dependente(idDep) {
    $('#icon-wait-dependente-' + idDep).hide();
    $('#icon-saved-dependente-' + idDep).show();
}
function ShowIconSavedSegurado_titular() {
    $('#icon-wait-titular').hide();
    $('#icon-saved-titular').show();
}
function HideEdicaoContatos() {
    $('#detalheContatos, #detalheContatos-edit').hide();
    $('#detalheContatos, #detalheContatos-read').show();
}
function FPagCCChecked() {
    $('#lifpagBoleto').html(`<div class="dv-fpag-border" style="margin-left: 2%; border-style: none"></div><img class="md-forma-pag-img" style="margin-left: 25px; opacity: 0.5" src="../ContentAdm/img/boleto_icon.png" /><div style="display: inline; opacity: 0.5"><span class="md-boleto-editSeg-label">Boleto Bancário</span></div>`);
    $('#lifpagCC').html('<div class="dv-fpag-border" style="margin-left: 8%"></div>        <img class="md-forma-pag-img" style="margin-left: 8%" src="../ContentAdm/img/credit-card-icon.png" />        <div style="display: inline">            <span class="md-creditcard-edit-label">Cartão</span><span class="md-creditcard-edit-label" style="top: 50%">de Crédito</span>        </div>');
    $('#lifpagDebito').html('<div class="dv-fpag-border" style="margin-left: 10%; border-style: none"></div>        <img class="md-forma-pag-img" style="margin-left: 13%; width: 10%; opacity: 0.5" src="../ContentAdm/img/debito_icon.png" />        <div style="display: inline; opacity: 0.5">            <span class="md-debito-edit-label">Débito</span><span class="md-debito-edit-label" style="top: 50%">em Conta</span>        </div>');
    $('.fp-debito-edit-segurado').hide();
}
function FPagBoletoChecked() {
    $('#lifpagDebito').html(`
        <div class="dv-fpag-border" style="margin-left: 10%; border-style: none"></div>
            <img class="md-forma-pag-img" style="margin-left: 13%; width: 10%; opacity: 0.5" src="../ContentAdm/img/debito_icon.png" />
        <div style="display: inline; opacity: 0.5">
            <span class="md-debito-edit-label">Débito</span><span class="md-debito-edit-label" style="top: 50%">em Conta</span>
        </div>
    `);

    $('#lifpagBoleto').html(`
        <div class="dv-fpag-border" style="margin-left: 2%"></div>
            <img class="md-forma-pag-img" style="margin-left: 25px" src="../ContentAdm/img/boleto_icon.png" />
        <div style="display: inline">
            <span class="md-boleto-editSeg-label">Boleto Bancário</span>
        </div>
    `);

    $('#lifpagCC').html(`
        <div class="dv-fpag-border" style="margin-left: 8%; border-style: none"></div>
            <img class="md-forma-pag-img" style="margin-left: 8%; opacity: 0.5" src="../ContentAdm/img/credit-card-icon.png" />
        <div style="display: inline; opacity: 0.5">
            <span class="md-creditcard-edit-label">Cartão</span><span class="md-creditcard-edit-label" style="top: 50%">de Crédito</span>
        </div>
    `);

    $('.fp-debito-edit-segurado').hide();
}
function FPagDebitoChecked() {
    $('#lifpagBoleto').html(`
        <div class="dv-fpag-border" style="margin-left: 2%; border-style: none"></div>
            <img class="md-forma-pag-img" style="margin-left: 25px; opacity: 0.5" src="../ContentAdm/img/boleto_icon.png" />
            <div style="display: inline; opacity: 0.5"><span class="md-boleto-editSeg-label">Boleto Bancário</span>
        </div>
    `);

    $('#lifpagCC').html(`
        <div class="dv-fpag-border" style="margin-left: 8%; border-style: none"></div>
            <img class="md-forma-pag-img" style="margin-left: 8%; opacity: 0.5" src="../ContentAdm/img/credit-card-icon.png" />
            <div style="display: inline; opacity: 0.5"><span class="md-creditcard-edit-label">Cartão</span><span class="md-creditcard-edit-label" style="top: 50%">de Crédito</span>                                                    
        </div>
    `);
    $('#lifpagDebito').html(`
        <div class="dv-fpag-border" style="margin-left: 10%"></div>
            <img class="md-forma-pag-img" style="margin-left: 13%; width: 10%" src="../ContentAdm/img/debito_icon.png" />
            <div style="display: inline"><span class="md-debito-edit-label">Débito</span><span class="md-debito-edit-label" style="top: 50%">em Conta</span>
        </div>
    `);

    //$('.fp-debito-edit-segurado').show();
}
function AtivarTitular() {
    $('#spInativo').attr('style', 'opacity: 0.5');
    $('#spAtivo').attr('style', 'margin-left: 10px');
    $('#tglStatusTitular').prop('checked', true);
}
function DesativarTitular() {
    $('#spInativo').attr('style', '');
    $('#spAtivo').attr('style', 'margin-left: 10px; opacity: 0.5');
    $('#tglStatusTitular').prop('checked', false);
}
function AtivarToggleDeps(idDep) {
    $(`#tgl-dep-${idDep}`).prop('checked', true);
}
function DesativarToggleDeps(idDep) {
    $(`#tgl-dep-${idDep}`).prop('checked', false);
}
function AlterarCollapseDependentes(idDependente) {
    if ($(`#dv-collapse-dependente-container-${idDependente}`).is(':visible'))
        $(`#dv-collapse-dependente-container-${idDependente}`).hide();
    else
        $(`#dv-collapse-dependente-container-${idDependente}`).show();
}
function AtualizarStatusDep(idDep) {
    SeguradoDeps.Dependentes.forEach(function (value) {
        if (value.IdDependente == idDep && SeguradoDeps.Status != 'N' && SeguradoDeps.Status != 'R')
            value.Status = (value.Status == 'A' || value.Status == 'Ativo') ? 'R' : 'A';
    });
}
function InativarTogglesDependentes() {
    var html_dv_dependente = $('#dv-dependente').html();
    html_dv_dependente = html_dv_dependente.replaceAll('small-slider', 'sliderdisabled');
    $('#dv-dependente').html(html_dv_dependente);
}
function AtivarTogglesDependentes() {
    var html_dv_dependente = $('#dv-dependente').html();
    html_dv_dependente = html_dv_dependente.replaceAll('sliderdisabled', 'small-slider')
    $('#dv-dependente').html(html_dv_dependente);
}
function _replaceAll(bigTxt, txt, txtreplace) {
    var exists = true;
    while (exists) {
        bigTxt = bigTxt.replace(txt, txtreplace);
        if (!bigTxt.includes(txt))
            exists = false;
    }
    return bigTxt;
}
function NenhumDependenteExibido() {
    $('#msgSemDeps').show();
    $('.dependente-border').hide();
    $('.dependente-container').hide();
}
function ListaDependentes(listaDeps) {
    //console.log(listaDeps)
    // Zera os dependentes do html
    ResetDependentes();
    var html_dv_dependente = $('#dv-dependente').html();
    // COMPONENTE DINÂMICO - JQUERY - DV-DEPENDENTE
    const INITIAL_COMPONENT = html_dv_dependente;

    let componenteDependente = '';


    listaDeps.forEach(function (value, index) {
        //console.log(value.nome)
        // Bind dos dados        
        if (index == 0)
            componenteDependente = INITIAL_COMPONENT;
        else
            componenteDependente += INITIAL_COMPONENT;


        let auxSexo;
        if (value.Sexo == 'M')
            auxSexo = 'Masculino';
        if (value.Sexo == 'F')
            auxSexo = 'Feminino';

        componenteDependente = componenteDependente.replaceAll('@nome', value.Nome)
            .replaceAll('@estadoCivil', value.EstadoCivil)
            .replaceAll('@cpf', MascaraCPFValue(value.Cpf))
            .replaceAll('@pis', (value.Pis || ''))
            .replaceAll('@cns', (value.Cns || ''))
            .replaceAll('@nrCarteirinha', (value.NrCarteirinha || ''))
            .replaceAll('@dn', value.Dn)
            .replaceAll('@dtInicioVig', value.DtInicioVigencia)
            .replaceAll('@dtFimVig', value.DtFinalVigencia)
            .replaceAll('@mae_nome', (value.NomeMae || ''))
            .replaceAll('@profissao', (value.Profissao || ''))
            .replaceAll('@sexo', (auxSexo || ''))
            .replaceAll('@nacionalidade', (value.Nacionalidade || ''))
            .replaceAll('@mae_dep', (value.NomeMae || ''))
            .replaceAll('@plano', (value.PlanoOdonto || ''))
            .replaceAll('@nasc', value.DtNascimento);

        componenteDependente = componenteDependente.replaceAll('@idDependente', value.IdDependente);

        if (value.Email != null)
            componenteDependente = componenteDependente.replaceAll('@email', value.Email);
        else
            componenteDependente = componenteDependente.replaceAll('@email', '');

        if (value.Endereco != null) {
            let auxcep = value.Endereco.end_cep != null ? MascaraCEPValue(value.Endereco.end_cep) : '';
            componenteDependente = componenteDependente.replaceAll('@endereco', (value.Endereco.end_endereco || ''))
                .replaceAll('@cep', auxcep)
                .replaceAll('@bairro', (value.Endereco.end_bairro || ''))
                .replaceAll('@cidade', value.Endereco.end_cidade || '')      // n existe na base.
                .replaceAll('@uf', (value.Endereco.end_estado || ''));      // n existe na base.
        }

        if (value.Contato != null) {
            let dddFone = (value.Contato.cnt_ddd || '');
            let dddCelular = (value.Contato.cnt_ddd_celular || '');
            let auxFone = (value.Contato.cnt_fone || '');
            let auxCelular = (value.Contato.cnt_celular || '');
            auxFone = dddFone.trim() + '' + auxFone.trim();
            auxCelular = dddCelular.trim() + '' + auxCelular.trim();

            if (!isNullOrEmpty(auxFone))
                auxFone = MascaraTelefoneValue(`${value.Contato.cnt_ddd}${value.Contato.cnt_fone}`);
            if (!isNullOrEmpty(auxCelular))
                auxCelular = MascaraCelularValue(`${value.Contato.cnt_ddd_celular}${value.Contato.cnt_celular}`);

            componenteDependente = componenteDependente.replaceAll('@telefone', auxFone)
                .replaceAll('@celular', auxCelular);
        }
        // carregar combo profissões
        let cmbPrfOptions = '<option value="0">Selecione</option>';

        value.CmbProfissoes.forEach((item) => {
            if (item.Id == value.IdProfissao)
                cmbPrfOptions += `<option value="${item.Id}" checked>${item.Nome}</option>`;
            else
                cmbPrfOptions += `<option value="${item.Id}">${item.Nome}</option>`;
        });

        let cmbProfissoes = `
                    <select class="dropdown-material-dependente cmbProfissao-dependente" id="cmbProfissaoDependente-editDependente-${value.IdDependente}">
                        ${cmbPrfOptions}
                    </select>
        `;

        componenteDependente = componenteDependente.replaceAll(`@cmbProfissao-${value.IdDependente}`, cmbProfissoes)

        // carregar combo Plano odontologico
        let cmbPlanoOptions = '<option value="0">Selecione</option>';

        value.Planos.forEach((item) => {
            if (item.IdPlano == value.IdPlanoOdonto)
                cmbPlanoOptions += `<option value="${item.IdPlano}" checked>${item.Descricao}</option>`;
            else
                cmbPlanoOptions += `<option value="${item.IdPlano}">${item.Descricao}</option>`;
        });

        let cmbPlanoOdontoDep = `
                    <select class="dropdown-material-titular" id="cmbPlanoOdontoDependente-editDependente-${value.IdDependente}">
                        ${cmbPlanoOptions}
                    </select>
        `;

        componenteDependente = componenteDependente.replaceAll(`@cmbPlanoOdonto-${value.IdDependente}`, cmbPlanoOdontoDep)

    });


    $('#dv-dependente').html(componenteDependente);

    // Caso o segurado esteja Inativo. Trava os toggles dos dependentes.
    if (SeguradoDeps.Status == 'Inativo')
        InativarTogglesDependentes();
    else
        AtivarTogglesDependentes();

    // Ativa/Desativa toggle dos dependentes
    listaDeps.forEach(function (value) {
        if (value.Status == 'Ativo') AtivarToggleDeps(value.IdDependente);
    })

}
function ResetTitular() {
    $('#detalheSegurado, #detalheTitular-edit').hide();
    $('#detalheSegurado, #detalheTitular-read ').show();
    $('#detalhePlano').hide();
    $('#dv-plano-error').hide();
    $('#icon-wait-titular').hide();
    $('#icon-saved-titular').hide();
    $('#detalheContatos').hide();
    $('#detalheContatos-edit').hide();
    $('#detalheContatos-read').show();
    $('#icon-wait-contato').hide();
    $('#icon-saved-contato').hide();
    $('#txtAgenciaBanco_editSegurado, #txtContaBanco_editSegurado').val('');
}

function ResetDependentes() {
    // COMPONENTE DINÂMICO - JQUERY - DV-DEPENDENTE
    var html_dv_dependente =
        `<h5 id="msgSemDeps" style="margin-left: 8%; display:none;">Titular não possui dependentes.</h5>
                                            <div class="dependente-border" style="margin-top: 1%" onclick="AlterarCollapseDependentes(@idDependente);" >
                                                <div class="dependente-grid">
                                                    <label class="switch">
                                                        <input type="checkbox" id="tgl-dep-@idDependente" onclick="AtualizarStatusDep(@idDependente)" />
                                                        <span class="small-slider round" style="top: 4px"></span>                                                        
                                                    </label>
                                                    <span class="modal-label" id="sp-nome-dependente-@idDependente" style="margin-left: 5px">@nome</span>
                                                    <a href="#" style="float: right; color: black; cursor: pointer">
                                                        <i class="fa fa-sort-down" style="float: right; font-size: 19px; margin-top: 5px; margin-right: 1%"></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="dependente-container" id="dv-collapse-dependente-container-@idDependente" style="display: none">                                                    
                                                    <div id="dv-dependente-collapse-read-@idDependente" style="margin-bottom: 7%;">
                                                        <div class="dependente-grid-collapse">
                                                            <span class="modal-label">Estado Civil:</span><span class="modal-data">@estadoCivil</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse">
                                                            <span class="modal-label">Sexo:</span><span class="modal-data">@sexo</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse">
                                                            <span class="modal-label">Data de Nasc:</span><span class="modal-data">@nasc</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse">
                                                            <span class="modal-label">Profissão:</span><span class="modal-data">@profissao</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse">
                                                            <span class="modal-label">CPF:</span><span class="modal-data">@cpf</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse">
                                                            <span class="modal-label">CEP:</span><span class="modal-data">@cep</span>
                                                        </div>
                                                        <div>
                                                            <table class="dependente-table" style="margin-left: 3%; width: 97%;">
                                                                <tr style="width: 100%">
                                                                    <td style="width: 50%">Endereço: <span class="dependente-modal-data">@endereco</span></td>
                                                                    <td style="width: 50%">Bairro: <span class="dependente-modal-data">@bairro</span></td>
                                                                </tr>
                                                                <tr style="width: 100%">
                                                                    <td style="width: 50%">Cidade: <span class="dependente-modal-data">@cidade</span></td>
                                                                    <td style="width: 50%">UF: <span class="dependente-modal-data">@uf</span></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="dependente-grid-collapse" style="margin-top: 0px">
                                                            <span class="modal-label">Nacionalidade:</span><span class="modal-data">@nacionalidade</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse" style="margin-top: 0px">
                                                            <span class="modal-label">Nome da Mãe do dependente:</span><span class="modal-data">@mae_dep</span>
                                                        </div>
                                                          <div class="dependente-grid-collapse">
                                                            <span class="modal-label">Plano Odontológico:</span><span class="modal-data">@plano</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse" style="margin-top: 0px">
                                                            <span class="modal-label">Nrº CNS:</span><span class="modal-data">@cns</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse" style="margin-top: 0px">
                                                            <span class="modal-label">Número do PISPASEP:</span><span class="modal-data">@pis</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse" style="margin-top: 0px">
                                                            <span class="modal-label">DN:</span><span class="modal-data">@dn</span>
                                                        </div>

                                                        <div class="dependente-grid-collapse" style="margin-top: 0px">
                                                            <span class="modal-label">Nrº de Carteirinha:</span><span class="modal-data">@nrCarteirinha</span>
                                                        </div>

                                                        <div class="dependente-grid-collapse" style="margin-top: 0px">
                                                            <span class="modal-label">Início de Vigência:</span><span class="modal-data">@dtInicioVig</span>
                                                        </div>                                                        
                                                        <p></p>
                                                        <div class="dependente-grid-collapse" style="margin-top: 0px">
                                                            <span class="modal-label">Telefone:</span><span class="modal-data">@telefone</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse" style="margin-top: 0px">
                                                            <span class="modal-label">Celular:</span><span class="modal-data">@celular</span>
                                                        </div>
                                                        <div class="dependente-grid-collapse" style="margin-top: 0px">
                                                            <span class="modal-label">E-mail:</span><span class="modal-data">@email</span>
                                                        </div>
                                                    <div class="row-edit-save">
                                                        <span id="lkEditarDependente-@idDependente" onClick="ShowEdicaoDadosDependente(@idDependente)" class="lk-save-edit">Editar</span>
                                                    </div>
                                                        <p></p>
                                                    </div>

                                                    <div id="dv-dependente-collapse-edit-@idDependente" style="margin-bottom: 7%; display: none">                                                        
                                                        <div class="dependente-grid-collapse" style="margin: 16px 0px 15px 21px;" >
                                                            <span class="modal-label">Estado Civil:</span>
                                                            <select class="dropdown-material-dependente cmbEstadoCivil-dependente" id="cmbEstadoCivilDependente-@idDependente">
                                                                <option value="">Selecione </option>
                                                                <option value="1">Solteiro(a)</option>
                                                                <option value="2">Casado(a)</option>
                                                                <option value="3">Viúvo(a)</option>
                                                                <option value="4">Desquitado(a)</option>
                                                                <option value="5">Divorciado(a)</option>
                                                                <option value="6">Outros</option>
                                                            </select>
                                                        </div>
                                                        <div class="dependente-grid-collapse" style="margin-top: -8px;">
                                                            <span class="modal-label">Sexo:</span>
                                                            <select class="dropdown-material-titular cmbSexo-editDependente" id="cmbSexo-editDependente-@idDependente">
                                                                <option value="">Selecione </option>
                                                                <option value="M">Masculino</option>
                                                                <option value="F">Feminino</option>
                                                            </select>
                                                        </div>
                                                        <div class="dependente-grid-collapse">
                                                            <span class="modal-label">Data de Nasc:</span>
                                                                <input id="txtDtNasc-editDependente-@idDependente" type="text" class="input-material-titular data calendario txtDtNasc-editDependente" 
                                                                        onkeypress="MascaraData(this)" maxlength="10" />
                                                        </div>                                                        
                                                        <div class="dependente-grid-collapse cmb-profissao-editDependente">
                                                            <span class="modal-label">Profissão:</span>
                                                                @cmbProfissao-@idDependente
                                                        </div>
                                                        <div class="dependente-grid-collapse">
                                                            <span class="modal-label">*CPF:</span>
                                                                <input name="txtCpf" maxlength="14" id="txtCPF-editDependente-@idDependente" class="input-material-titular" 
                                                                        style="width: 86%" type="text" onkeypress="MascaraCPF(this)">
                                                        </div>
                                                        <div class="dependente-grid-collapse" style="margin-bottom: 10px">
                                                            <span class="modal-label">CEP:</span>
                                                                <input name="txtCEP" maxlength="10" id="txtCEP-editDependente-@idDependente" onblur="pesquisacep(this.id)" class="input-material-titular" 
                                                                        style="width: 86%" type="text" onkeypress="MascaraCep(this)" />
                                                        </div>
                                                        <div>
                                                            <table class="dependente-table" style="margin-left: 3%; width: 97%;">
                                                                <tr style="width: 100%">
                                                                    <td style="width: 50%">Endereço:
                                                                            <input name="txtEndereco" maxlength="100" id="txtEndereco-editDependente-@idDependente" 
                                                                                class="input-material-titular" type="text" value="" style="width: 68%" />
                                                                    </td>
                                                                    <td style="width: 50%">Bairro:
                                                                            <input name="txtBairro" maxlength="100" id="txtBairro-editDependente-@idDependente" 
                                                                                class="input-material-titular" type="text" value="" style="width: 68%" />
                                                                    </td>
                                                                </tr>
                                                                <tr style="width: 100%">
                                                                    <td style="width: 50%">Cidade: 
                                                                            <input name="txtCidade" maxlength="100" id="txtCidade-editDependente-@idDependente" 
                                                                                class="input-material-titular" type="text" value="" style="width: 74%" />
                                                                    </td>
                                                                    <td style="width: 50%">UF: 
                                                                        <select class="dropdown-material-titular" id="cmbUF-editDependente-@idDependente" style="width: 15%">
                                                                            <option value="AC">AC</option>
                                                                            <option value="AL">AL</option>
                                                                            <option value="AP">AP</option>
                                                                            <option value="AM">AM</option>
                                                                            <option value="BA">BA</option>
                                                                            <option value="CE">CE</option>
                                                                            <option value="DF">DF</option>
                                                                            <option value="ES">ES</option>
                                                                            <option value="GO">GO</option>
                                                                            <option value="MA">MA</option>
                                                                            <option value="MT">MT</option>
                                                                            <option value="MS">MS</option>
                                                                            <option value="MG">MG</option>
                                                                            <option value="PA">PA</option>
                                                                            <option value="PB">PB</option>
                                                                            <option value="PR">PR</option>
                                                                            <option value="PE">PE</option>
                                                                            <option value="PI">PI</option>
                                                                            <option value="RJ">RJ</option>
                                                                            <option value="RN">RN</option>
                                                                            <option value="RS">RS</option>
                                                                            <option value="RO">RO</option>
                                                                            <option value="RR">RR</option>
                                                                            <option value="SC">SC</option>
                                                                            <option value="SP">SP</option>
                                                                            <option value="SE">SE</option>
                                                                            <option value="TO">TO</option>
                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="dependente-grid-collapse" style="margin-top: 8px">
                                                            <span class="modal-label">Nacionalidade:</span>
                                                                    <!-- SOMENTE TEXTO -->
                                                            <select class="dropdown-material-dependente cmbEstadoCivil-dependente" id="cmbNacionalidadeDependente-@idDependente">
                                                                <option value="">Selecione</option>
                                                                <option value="brasileira">Brasileira</option>                                                                
                                                                <option value="estrangeira">Estrangeira</option>
                                                            </select>                                                                
                                                        </div>
                                                        <div class="dependente-grid-collapse" >
                                                            <span class="modal-label">Nome da Mãe do dependente:</span>
                                                                <input name="txtNomeMae" maxlength="255" id="txtNomeMae-editDependente-@idDependente" 
                                                                    class="input-material-titular txtNomeMae-editDependente" type="text" value="" />
                                                        </div>
                                                         <div class="dependente-grid-collapse cmb-planoodonto-editDependente" style="margin-top: 15px;margin-bottom: 15px;">
                                                            <span class="modal-label">Plano Odontológico:</span>
                                                                @cmbPlanoOdonto-@idDependente
                                                        </div>
                                                        <div class="dependente-grid-collapse" >
                                                            <span class="modal-label">Nrº CNS:</span>
                                                                <input name="txtCNS" maxlength="18" id="txtCNS-editDependente-@idDependente" class="input-material-titular" 
                                                                    style="width: 82%" type="text" onkeypress="MascaraCNS(this)" value="" />
                                                        </div>
                                                        <div class="dependente-grid-collapse" >
                                                            <span class="modal-label">Número do PISPASEP:</span>
                                                                <input name="txtPIS" maxlength="14" id="txtPIS-editDependente-@idDependente" class="input-material-titular txtPIS-editDependente" 
                                                                    type="text" onkeypress="MascaraPis(this)" value="" />
                                                        </div>
                                                        <div class="dependente-grid-collapse" >
                                                            <span class="modal-label">DN:</span>
                                                                <input name="txtDN" maxlength="11" id="txtDN-editDependente-@idDependente" onkeypress='mascaraInteiro()' class="input-material-titular" 
                                                                    type="text" value="" style="width: 87%" />
                                                        </div>

                                                        <div class="dependente-grid-collapse" >
                                                            <span class="modal-label">Nrº de Carteirinha:</span>
                                                                <input name="txtNrCarteirinha-editDependente-@idDependente" maxlength="20" id="txtNrCarteirinha-editDependente-@idDependente" onkeypress='mascaraInteiro()' class="input-material-titular" 
                                                                    type="text" value="" style="width: 70%" />
                                                        </div>

                                                        <div class="dependente-grid-collapse" >
                                                            <span class="modal-label">Início de Vigência:</span>
                                                                <input id="txtDtInicioVig-editDependente-@idDependente" type="text" class="input-material-titular data calendario txtDtInicioVig-editDependente" 
                                                                    onkeypress="MascaraData(this)" maxlength="10" />
                                                        </div>                                                        
                                                        <div class="dependente-grid-collapse" >
                                                            <span class="modal-label">Telefone:</span>
                                                                <input name="txtTelefone" maxlength="14" id="txtTelefone-editDependente-@idDependente" class="input-material-titular txtTelefone-editDependente" 
                                                                    type="text" onkeypress="MascaraTelefone(this)" value="" />
                                                        </div>
                                                        <div class="dependente-grid-collapse" >
                                                            <span class="modal-label">Celular:</span>
                                                                <input name="txtCelular" maxlength="15" id="txtCelular-editDependente-@idDependente" class="input-material-titular txtCelular-editDependente" type="text" onkeypress="MascaraCelular(this)" value="" />
                                                        </div>
                                                        <div class="dependente-grid-collapse" >
                                                            <span class="modal-label">E-mail:</span>
                                                                <input id="txtEmail-editDependente-@idDependente" type="text" name="registerEmail" style="width: 83%" 
                                                                    class="input-material-titular" maxlength="100" onkeydown="CampoValido(this,'SPTxtEmail');" value="" />
                                                        </div>
                                                        <div class="row-edit-save">
                                                            <span class='msg-error-deps' id='sp-msg-error-deps-@idDependente' style='display: none'>Campo @cmp_error obrigatório!</span>
                                                            <i id="icon-saved-dependente-@idDependente" class="fa fa-check-circle" style="display: none; margin-bottom: -33px;"></i>
                                                            <i id="icon-wait-dependente-@idDependente" class="fas fa-hourglass-half" style="display: none; margin-bottom: -33px;"></i>
                                                            <span style="color:red;display: none" id=msgErroDependente-@idDependente>Erro ao salvar dependente.<i class="fa fa-exclamation-circle" style="color: red"></i></span>
                                                            <span id="lkSavedependente-@idDependente" onClick="SalvarDadosDependente(@idDependente)" class="lk-save-edit">Salvar</span>
                                                        </div>
                                                        <p></p>
                                                    </div>

                                                </div>
                                            </div>`;
    $('#dv-dependente').html('');
    $('#dv-dependente').html(html_dv_dependente);
}

$(document).ready(function () {
    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $("#txtEndereco-editSegurado").val("");
        $("#txtBairro-editSegurado").val("");
        $("#txtCidade-editSegurado").val("");
        $("#cmbUF-editSegurado").val("");
    }
    $("#txtCEP-editSegurado").blur(function () {
        var cep = $(this).val().replace(/\D/g, '');
        if (cep != "") {
            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;
            //Valida o formato do CEP.
            if (validacep.test(cep)) {

                $.ajax({
                    method: "POST",
                    url: `ConsultaSegurado.aspx/ConsultaEnderecoBaseCep`,
                    data: '{cep: "' + cep + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (retorno) {
                    $("#txtEndereco-editSegurado").val(retorno.d.Endereco);
                    $("#txtBairro-editSegurado").val(retorno.d.Bairro);
                    $("#txtCidade-editSegurado").val(retorno.d.Cidade);
                    $("#cmbUF-editSegurado").val(retorno.d.Estado);
                });
            } //end if.

        } //end if.  
    });
});

function pesquisacep(valor) {
    // valor que a function receber é o id do dependente, para poder acessar as coisas
    var idDependente = valor.replace("txtCEP-editDependente-", "");

    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $("#txtEndereco-editDependente-" + idDependente).val("");
        $("#txtBairro-editDependente-" + idDependente).val("");
        $("#txtCidade-editDependente-" + idDependente).val("");
        $("#cmbUF-editDependente-" + idDependente).val("");
    }

    var cep = $("#txtCEP-editDependente-" + idDependente).val();
    cep = cep.replace(/\D/g, '');

    if (cep != "") {
        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;
        //Valida o formato do CEP.
        if (validacep.test(cep)) {
            $.ajax({
                method: "POST",
                url: `ConsultaSegurado.aspx/ConsultaEnderecoBaseCep`,
                data: '{cep: "' + cep + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (retorno) {
                //Atualiza os campos com os valores da consulta.
                $("#txtEndereco-editDependente-" + idDependente).val(retorno.d.Endereco);
                $("#txtBairro-editDependente-" + idDependente).val(retorno.d.Bairro);
                $("#txtCidade-editDependente-" + idDependente).val(retorno.d.Cidade);
                $("#cmbUF-editDependente-" + idDependente).val(retorno.d.Estado);
            });

        } //end if.
        else {
            //cep é inválido.
            limpa_formulário_cep();
        }
    } //end if.
    else {
        limpa_formulário_cep();
    }

};
// #endregion
// ######################################################################### CadastraSegurado.aspx ########################################################################################
// #region CadastraSeguradoPage
var campo;
var cadastrouTitular = false;
var erros = 0;
var carregouSegurado = false;
$(document).ready(function () {

    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)

    if (url.includes('CadastraSegurado.aspx')) {
        CarregarComboSeguradora();
        CarregarComboEspecialidades();
        CarregarComboEstadoCivil();
        CarregarComboPlanoSaudeCadSegurado();
        CarregarComboPlanoOdontoCadSegurado();
        CarregarComboPlanoOdontoCadDependente();
        CarregarComboMelhorDiaPagCadSegurado();
    }

});

// LISTENERS #############################################################################################################################################################################

$('#mainContent_txtDataFiliacaoCadSegurado').datepicker({
    dateFormat: 'dd/mm/yy'
});
$('#mainContent_txtInicioVigenciaCadSegurado').datepicker({
    dateFormat: 'dd/mm/yy'
});
$('#mainContent_txtFimVigenciaCadSegurado').datepicker({
    dateFormat: 'dd/mm/yy'
});
$('#mainContent_txtInicioVigenciaCadDependente').datepicker({
    dateFormat: 'dd/mm/yy'
});
$('#mainContent_txtFimVigenciaCadDependente').datepicker({
    dateFormat: 'dd/mm/yy'
});
$('#mainContent_txtCPFCadSegurado').blur(function () {
    VerificaSession();
    var cpf = $('#mainContent_txtCPFCadSegurado').val().replace(/[^\d]+/g, '');
    var cpfValido = verificarCPF(cpf);
    if (cpfValido) VerificarUnicidadeCPFSegurado(cpf, "Titular");
    else {
        $('#SPCPFCadSegurado').text('CPF inválido');
        $('#SPCPFCadSegurado').show();
    }
});

$('#mainContent_txtCPFCadDependente').blur(function () {
    var cpf = $('#mainContent_txtCPFCadDependente').val().replace(/[^\d]+/g, '');
    if (isNullOrEmpty(cpf))
        return;
    var cpfValido = verificarCPF(cpf);
    if (cpfValido) VerificarUnicidadeCPF(cpf, "Dependente");
    else {
        $('#SPCPFCadDependente').text('CPF inválido');
        $('#SPCPFCadDependente').show();
    }
});

$('#btncadastrarSegurado').click(function () {
    $('#dvCadDependentes').hide();
    $('#btncadastrarDependentes').removeClass('active');
    $('#btncadastrarSegurado').addClass('active');
    $('#dvCadTitular').show();
});
$('#btncadastrarDependentes').click(function () {
    VerificaSession();
    campo = 'CadSegurado';
    if (cadastrouTitular && validarDados()) {
        $('#dvCadTitular').hide();
        $('#btncadastrarSegurado').removeClass('active');
        $('#btncadastrarDependentes').addClass('active');

        CarregarDependentes();
    } else {
        $('#mdMensagem').text('Primeiro, cadastre ou carregue os dados do titular!');
        $('#mdCadSegurado').modal('show');
    }
});
$('#btnLimparCadDependente').click(function () {
    limparCamposDependente();
});
$('#btnSairCadDependente').click(function () {
    var urlSegurado = 'http://' + window.location.hostname + ':' + window.location.port + '/Segurado/ConsultaSegurado.aspx';
    window.location.replace(urlSegurado);
});

function LimparDadosSegurado() {
    cadastrouTitular = false;
    carregouSegurado = false;
    $('#hddIdSegurado').text('');
    $('#mainContent_txtNumFiliacaoCadSegurado').val('');
    $('#mainContent_txtCEPCadSegurado').val('');
    $('#mainContent_txtTelefoneCadSegurado').val('');
    $('#mainContent_txtCelularCadSegurado').val('');
    $('#cmbSeguradora').val('');
    $('#mainContent_txtNomeCadSegurado').val('');
    $('#mainContent_txtCRMCadSegurado').val('');
    $('#mainContent_cmbUFCRMCadSegurado').val('');
    $('#cmbEspecCadSegurado').val('');
    $('#mainContent_txtDataNascCadSegurado').val('');
    $('#cmbSexoCadSegurado').val('');
    $('#cmbCivilCadSegurado').val('');
    $('#mainContent_txtNomeMaeCadSegurado').val('');
    $('#mainContent_txtNacionalidadeCadSegurado').val('');
    $('#mainContent_txtPISPASEPCadSegurado').val('');
    $('#mainContent_txtCNSCadSegurado').val('');
    $('#mainContent_txtDNCadSegurado').val('');
    $('#cmbPlanoSaudeCadSegurado').val('');
    $('#cmbPlanoOdontoCadSegurado').val('');
    $('#mainContent_txtDataFiliacaoCadSegurado').val('');
    $('#mainContent_txtInicioVigenciaCadSegurado').val('');
    $('#mainContent_txtFimVigenciaCadSegurado').val('');
    $('#mainContent_txtEmailCadSegurado').val('');
    $('#mainContent_txtPropostaCadSegurado').val('');
    $('#mainContent_txtLogradouroCadSegurado').val('');
    $('#mainContent_txtBairroCadSegurado').val('');
    $('#mainContent_txtCidadeCadSegurado').val('');
    $('#mainContent_cmbUFCadSegurado').val('');
    FPagtoBoletoCadSegurado();
}

function limparCamposDependente() {
    $('#mainContent_txtNomeCadDependente').val('');
    $('#mainContent_cmbCivilCadDependente').val('');
    $('#mainContent_cmbSexoCadDependente').val('');
    $('#mainContent_txtDataNascCadDependente').val('');
    $('#cmbNacionalidadeCadDependente').val('');
    $('#mainContent_txtNacionalidadeCadDependente').val('');
    $('#mainContent_txtNomeMaeCadDependente').val('');
    $('#mainContent_txtCNSCadDependente').val('');
    $('#mainContent_txtPISPASEPCadDependente').val('');
    $('#cmbProfissaoCadDependente').val('');
    $('#mainContent_txtDNCadDependente').val('');
    $('#mainContent_txtInicioVigenciaCadDependente').val('');
    $('#mainContent_txtFimVigenciaCadDependente').val('');
    $('#mainContent_txtEmailCadDependente').val('');
    $('#mainContent_txtLogradouroCadDependente').val('');
    $('#mainContent_txtBairroCadDependente').val('');
    $('#mainContent_txtCidadeCadDependente').val('');
    $('#mainContent_txtCEPCadDependente').val('');
    $('#mainContent_cmbUFCadDependente').val('');
    $('#mainContent_txtTelefoneCadDependente').val('');
    $('#mainContent_txtCelularCadDependente').val('');
    $('#mainContent_cmbParentescoCadDependente').val('');
}

//Obtem Forma de Pagto
var formaPagamentoCadSegurado = 3;
var fpagCadSegChange = false;
$('#lifpagBoletoCadSegurado').click(function () {
    FPagtoBoletoCadSegurado();
});
$('#lifpagCCCadSegurado').click(function () {
    FPagtoCCCadSegurado();
});
$('#lifpagDebitoCadSegurado').click(function () {
    FPagtoDebitoCadSegurado();
});

$('#lifpagDebitoCadSegurado').click();

function FPagtoBoletoCadSegurado() {
    $('#lifpagBoletoCadSegurado').html('<div class="dv-fpag-border-CadSegurado" style="margin-left: 10%"></div> <img class="md-forma-pag-img-CadSegurado" id="imgBoletoCadSegurado" style="margin-left: 165px" src="../ContentAdm/img/boleto_icon.png" /> <span class="md-boleto-label" id="SPBoletoCadSegurado" style="display: inline; margin-left: 25px">Boleto<br />Bancário</span>');
    //$('#lifpagCCCadSegurado').html('<div class="dv-fpag-border-CadSegurado" style="border-style: none; margin-left: 13%"></div> <img class="md-forma-pag-img-CadSegurado" style="margin-left: 14%; opacity: 0.5" src="../ContentAdm/img/credit-card-icon.png" /> <span class="md-creditcard-label" id="SPCCCadSegurado" style="display: inline; opacity: 0.5; margin-left: 20px">Cartão<br />de Crédito</span>');
    $('#lifpagCCCadSegurado').html('');
    //$('#lifpagDebitoCadSegurado').html('<div class="dv-fpag-border-CadSegurado" style="border-style: none; margin-left: 13%"></div> <img class="md-forma-pag-img-CadSegurado" style="margin-left: 14%; width: 100px; opacity: 0.5" src="../ContentAdm/img/debito_icon.png" /> <span class="md-debito-label" id="SPDebitoCadSegurado" style="display: inline; opacity: 0.5">Débito<br />em Conta</span>');
    $('#lifpagDebitoCadSegurado').html('');
    formaPagamentoCadSegurado = 1;
    fpagCadSegChange = true;
    $('.fp-debito-cad-segurado').hide();
}

function FPagtoCCCadSegurado() {
    $('#lifpagBoletoCadSegurado').html('<div class="dv-fpag-border-CadSegurado" style="border-style: none; margin-left: 10%"></div> <img class="md-forma-pag-img-CadSegurado" id="imgBoletoCadSegurado" style="margin-left: 165px; opacity: 0.5" src="../ContentAdm/img/boleto_icon.png" /> <span class="md-boleto-label" id="SPBoletoCadSegurado" style="display: inline; opacity: 0.5; margin-left: 25px">Boleto<br />Bancário</span>');
    //$('#lifpagCCCadSegurado').html('<div class="dv-fpag-border-CadSegurado" style="margin-left: 13%"></div> <img class= "md-forma-pag-img-CadSegurado" style="margin-left: 14%" src="../ContentAdm/img/credit-card-icon.png" /> <span class="md-creditcard-label" id="SPCCCadSegurado" style="display: inline; margin-left: 20px">Cartão<br />de Crédito</span>');
    $('#lifpagCCCadSegurado').html('');
    //$('#lifpagDebitoCadSegurado').html('<div class="dv-fpag-border-CadSegurado" style="border-style: none; margin-left: 13%"></div> <img class= "md-forma-pag-img-CadSegurado" style="margin-left: 14%; width: 100px; opacity: 0.5" src="../ContentAdm/img/debito_icon.png" /> <span class="md-debito-label" id="SPDebitoCadSegurado" style="display: inline; opacity: 0.5">Débito<br />em Conta</span>');
    $('#lifpagDebitoCadSegurado').html('');
    formaPagamentoCadSegurado = 2;
    fpagCadSegChange = true;
    $('.fp-debito-cad-segurado').hide();
}

function FPagtoDebitoCadSegurado() {
    $('#lifpagBoletoCadSegurado').html('<div class="dv-fpag-border-CadSegurado" style="border-style: none; margin-left: 10%"></div> <img class="md-forma-pag-img-CadSegurado" id="imgBoletoCadSegurado" style="margin-left: 165px; opacity: 0.5" src="../ContentAdm/img/boleto_icon.png" /> <span class="md-boleto-label" id="SPBoletoCadSegurado" style="display: inline; opacity: 0.5; margin-left: 25px">Boleto<br />Bancário</span>');
    //$('#lifpagCCCadSegurado').html('<div class="dv-fpag-border-CadSegurado" style="border-style: none; margin-left: 13%"></div> <img class="md-forma-pag-img-CadSegurado" style="margin-left: 14%; opacity: 0.5" src="../ContentAdm/img/credit-card-icon.png" /> <span class="md-creditcard-label" id="SPCCCadSegurado" style="display: inline; opacity: 0.5; margin-left: 20px">Cartão<br />de Crédito</span>');
    $('#lifpagCCCadSegurado').html('');
    //$('#lifpagDebitoCadSegurado').html('<div class="dv-fpag-border-CadSegurado" style="margin-left: 13%"></div> <img class="md-forma-pag-img-CadSegurado" style="margin-left: 14%; width: 100px" src="../ContentAdm/img/debito_icon.png" /> <span class="md-debito-label" id="SPDebitoCadSegurado" style="display: inline">Débito<br />em Conta</span>');
    $('#lifpagDebitoCadSegurado').html('');
    formaPagamentoCadSegurado = 3;
    fpagCadSegChange = true;
    CarregarComboBancosCadSegurado(0);
    //$('.fp-debito-cad-segurado').show();
}


$('#btnSalvarCadSegurado').click(function () {
    VerificaSession();
    $('.required-error').hide();

    $('#mdLoader').modal({
        backdrop: 'static',
        keyboard: false
    });

    $('#mdLoader').modal('show');

    campo = 'CadSegurado';
    var retorno = validarDados();

    if (retorno) CadastrarTitular();
    else
        $('#mdLoader').modal('hide');

});
$('#btnAdicionarCadDependente').click(function () {
    $('.required-error').hide();

    $('#mdLoader').modal({
        backdrop: 'static',
        keyboard: false
    });
    $('#mdLoader').modal('show');

    campo = 'CadDependente';
    var retorno = validarDados();
    if (retorno) CadastrarDependente();
    else {
        $('#mdLoader').modal('hide');
    }

});

//Valida os dados obrigatórios
function validarDados() {
    let focusedInputError = false;
    ZerarErroValidacao();

    if (fpagCadSegChange)
        if (formaPagamentoCadSegurado == 3)
            if (!ValidarFormaPagamento_CadSegurado()) {
                erros += 1;
            }

    if ($('#mainContent_txtNome' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_txtNome' + campo).focus();
            focusedInputError = true;
        }

        $('#SPNome' + campo).show();
    }

    if ($('#mainContent_txtDataNasc' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_txtDataNasc' + campo).focus();
            focusedInputError = true;
        }

        $('#SPDataNasc' + campo).show();
    }
    if ($('#mainContent_txtCRM' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_txtCRM' + campo).focus();
            focusedInputError = true;
        }

        $('#SPCRM' + campo).show();
    }
    if ($('#mainContent_cmbUFCRM' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_cmbUFCRM' + campo).focus();
            focusedInputError = true;
        }
        $('#SPcmbUFCRM' + campo).show();
    }
    if ($('#cmbEspec' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#cmbEspec' + campo).focus();
            focusedInputError = true;
        }
        $('#SPEspec' + campo).show();
    }
    if ($('#cmbSexo' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#cmbSexo' + campo).focus();
            focusedInputError = true;
        }
        $('#SPSexo' + campo).show();
    }
    if ($('#cmbCivil' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#cmbCivilCadSegurado').focus();
            focusedInputError = true;
        }
        $('#SPCivil' + campo).show();
    }


    var auxCPFDep = $('#mainContent_txtCPF' + campo).val();
    var CPFValido = true;
    if (isNullOrEmpty(auxCPFDep) == false)
        CPFValido = verificarCPF(auxCPFDep.replace('.', '').replace('.', '').replace('-', ''));
    if (campo != 'CadDependente') {
        if (CPFValido == false) {
            erros += 1;
            if (focusedInputError == false) {
                $('#mainContent_txtCPF' + campo).focus();
                focusedInputError = true;
            }
            $('#SPCPF' + campo).text('CPF Obrigatório.');
            $('#SPCPF' + campo).show();
        }
    } else {
        if (isNullOrEmpty(auxCPFDep) == false) {
            if (CPFValido == false) {
                erros += 1;
                if (focusedInputError == false) {
                    $('#mainContent_txtCPF' + campo).focus();
                    focusedInputError = true;
                }
                $('#SPCPF' + campo).text('CPF Inválido.');
                $('#SPCPF' + campo).show();
            }
        }
    }



    if ($('#mainContent_txtNomeMae' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_txtNomeMae' + campo).focus();
            focusedInputError = true;
        }
        $('#SPNomeMae' + campo).show();
    }

    var CEPValido = ValidaCep($('#mainContent_txtCEP' + campo).val());
    if (CEPValido == false) {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_txtCEP' + campo).focus();
            focusedInputError = true;
        }
        $('#SPCEP' + campo).show();
    }

    if ($('#mainContent_txtTelefone' + campo).val() != "") {
        var telefoneValido = ValidaTelefone($('#mainContent_txtTelefone' + campo).val());
        if (telefoneValido == false) {
            erros += 1;
            if (focusedInputError == false) {
                $('#mainContent_txtTelefone' + campo).focus();
                focusedInputError = true;
            }
            $('#SPTelefone' + campo).show();
        }
    }

    if ($('#mainContent_txtCelular' + campo).val() != "") {
        var celularValido = ValidaCelular($('#mainContent_txtCelular' + campo).val());
        if (celularValido == false) {
            erros += 1;
            if (focusedInputError == false) {
                $('#mainContent_txtCelular' + campo).focus();
                focusedInputError = true;
            }
            $('#SPCelular' + campo).show();
        }
    }

    if ($('#mainContent_txtEmail' + campo).val() != "") {
        var emailValido = validaEmail('mainContent_txtEmail' + campo);
        if (emailValido == false) {
            erros += 1;
            if (focusedInputError == false) {
                $('#mainContent_txtEmail' + campo).focus();
                focusedInputError = true;
            }
        }
    }

    if ($('#mainContent_txtCNS' + campo).val() != "") {
        var cnsValido = ValidaCNS($('#mainContent_txtCNS' + campo).val());
        if (cnsValido == false) {
            erros += 1;
            if (focusedInputError == false) {
                $('#mainContent_txtCNS' + campo).focus();
                focusedInputError = true;
            }

            $('#SPCNS' + campo).show();
        }
    }

    if ($('#mainContent_txtDN' + campo).val() != "") {
        var dnValido = ValidaDN($('#mainContent_txtDN' + campo).val());
        if (dnValido == false) {
            erros += 1;
            if (focusedInputError == false) {
                $('#mainContent_txtDN' + campo).focus();
                focusedInputError = true;
            }
            $('#SPDN' + campo).show();
        }
    }

    if ($('#mainContent_txtPISPASEP' + campo).val() != "") {
        var pispasepValido = ValidaPISPASEP($('#mainContent_txtPISPASEP' + campo).val());
        if (pispasepValido == false) {
            erros += 1;
            if (focusedInputError == false) {
                $('#mainContent_txtPISPASEP' + campo).focus();
                focusedInputError = true;
            }
            $('#SPPISPASEP' + campo).show();
        }
    }

    if ($('#mainContent_txtLogradouro' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_txtLogradouro' + campo).focus();
            focusedInputError = true;
        }
        $('#SPLogradouro' + campo).show();
    }
    if ($('#mainContent_txtBairro' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_txtBairro' + campo).focus();
            focusedInputError = true;
        }
        $('#SPBairro' + campo).show();
    }
    if ($('#mainContent_txtCidade' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_txtCidade' + campo).focus();
            focusedInputError = true;
        }
        $('#SPCidade' + campo).show();
    }
    if ($('#mainContent_cmbUF' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_cmbUF' + campo).focus();
            focusedInputError = true;
        }
        $('#SPUF' + campo).show();
    }
    if ($('#mainContent_txtNacionalidade' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_txtNacionalidade' + campo).focus();
            focusedInputError = true;
        }
        $('#SPNacionalidade' + campo).show();
    }


    if ($('#mainContent_cmbParentesco' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_cmbParentesco' + campo).focus();
            focusedInputError = true;
        }

        $('#SPParentesco' + campo).show();
    }

    if ($('#mainContent_txtDataFiliacao' + campo).val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#mainContent_txtDataFiliacao' + campo).focus();
            focusedInputError = true;
        }
        $('#SPDataFiliacao' + campo).show();
    }

    if ($('#cmbplanoSaude_cadSegurado').val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#cmbplanoSaude_cadSegurado').focus();
            focusedInputError = true;
        }
        $('#SPcmbplanoSaude_cadSegurado').show();
    }

    if ($('#cmbMelhorDiaPag_cadSegurado').val() == '') {
        erros += 1;
        if (focusedInputError == false) {
            $('#SPcmbMelhorDiaPag_cadSegurado').focus();
            focusedInputError = true;
        }
        $('#SPcmbMelhorDiaPag_cadSegurado').show();
    }


    focusInputError = false;

    if (erros > 0) return false;
    else return true;
}
function CadastrarTitular() {
    var data = {
        Segurado: {
            seg_cia_id: $('#cmbSeguradora').val(),
            seg_nome: $('#mainContent_txtNomeCadSegurado').val(),
            seg_crm: $('#mainContent_txtCRMCadSegurado').val(),
            seg_crm_estado: $('#mainContent_cmbUFCRMCadSegurado').val(),
            seg_esp_id: $('#cmbEspecCadSegurado').val(),
            seg_data_nascimento: DateSerializer($('#mainContent_txtDataNascCadSegurado').val()),
            seg_sexo: $('#cmbSexoCadSegurado').val(),
            seg_civ_id: $('#cmbCivilCadSegurado').val(),
            seg_cpf: $('#mainContent_txtCPFCadSegurado').val().replace(/[^\d]+/g, ''),
            seg_nome_mae: $('#mainContent_txtNomeMaeCadSegurado').val(),
            seg_nacionalidade: $('#cmbNacionalidadeCadSegurado').val(),
            seg_pispasep: $('#mainContent_txtPISPASEPCadSegurado').val().replace(/[^\d]+/g, ''),
            seg_cns: $('#mainContent_txtCNSCadSegurado').val().replace(/[^\d]+/g, ''),
            seg_dn: $('#mainContent_txtDNCadSegurado').val().replace(/[^\d]+/g, ''),
            seg_dt_filiacao: DateSerializer($('#mainContent_txtDataFiliacaoCadSegurado').val()),
            seg_inicio_vigencia: DateSerializer($('#mainContent_txtInicioVigenciaCadSegurado').val()),
            seg_fim_vigencia: DateSerializer($('#mainContent_txtFimVigenciaCadSegurado').val()),
            seg_prf_id: 1, //Médico
            seg_email: $('#mainContent_txtEmailCadSegurado').val(),
            seg_for_id: formaPagamentoCadSegurado,
            seg_num_proposta: $('#mainContent_txtPropostaCadSegurado').val(),
            seg_num_filiacao: $('#mainContent_txtNumFiliacaoCadSegurado').val(),
            seg_numero_carteira: $('#mainContent_txtNrCarteirinhaCadSegurado').val(),
            seg_mdp_id: $('#cmbMelhorDiaPag_cadSegurado').val()
        },
        DadosCobranca: {
            dco_seg_id: 0,
            dco_agencia: $('#mainContent_txtAgencia_cadSeguro').val(),
            dco_conta: $('#mainContent_txtConta_cadSegurado').val(),
            dco_ban_id: $('#cmbBanco_cadSegurado').val()
        },
        Endereco: {
            end_id: 0,
            end_cpf: $('#mainContent_txtCPFCadSegurado').val().replace(/[^\d]+/g, ''),
            end_endereco: $('#mainContent_txtLogradouroCadSegurado').val(),
            end_bairro: $('#mainContent_txtBairroCadSegurado').val(),
            end_cidade: $('#mainContent_txtCidadeCadSegurado').val(),
            end_cep: $('#mainContent_txtCEPCadSegurado').val().replace(/[^\d]+/g, ''),
            end_estado: $('#mainContent_cmbUFCadSegurado').val(),
            end_complemento: '',
            end_par_id: 'T',
            end_seg_id: 0,
            end_dep_id: null
        },
        Contato: {
            cnt_par_id: 'T',
            cnt_ddd: '',
            cnt_cpf: $('#mainContent_txtCPFCadSegurado').val().replace(/[^\d]+/g, ''),
            cnt_fone: $('#mainContent_txtTelefoneCadSegurado').val(),
            cnt_celular: $('#mainContent_txtCelularCadSegurado').val(),
            cnt_seg_id: 0,
            cnt_dep_id: null
        },
        PlanosSegurado: [
            {
                pls_id: 0,
                pls_seg_id: 0,
                pls_pla_id: $('#cmbplanoSaude_cadSegurado').val(),
                pls_par_id: 'T'
            },
            {
                pls_id: 0,
                pls_seg_id: 0,
                pls_pla_id: $('#cmbplanoOdonto_cadSegurado').val(),
                pls_par_id: 'T'
            }
        ]
    };

    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/CadastrarTitularAsync",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null) {
            if (retorno.d.includes('erro')) {
                alert('erro no cadastro do titular. erro no dev tools log.');
                console.log(retorno.d);
            } else {
                cadastrouTitular = true;
                $('#mdLoader').modal('hide');
                $('#mdMensagem').text('Dados do titular salvos com sucesso!');
                $('#mdCadSegurado').modal('show');
                $('#hddCpfTitular').val(data.Segurado.seg_cpf);

                CarregarDependentes();
            }
        } else {
            $('#mdLoader').modal('hide');
            $('#mdMensagem').text('Não foi possível cadastrar o titular!');
            $('#mdCadSegurado').modal('show');
        }
    });
}
function CarregarComboSeguradora() {
    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/ListarSeguradoraAnterior",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                retorno.d.forEach(function (value) {    // Carregar Seguradora anterior cad. seg. e manter seg.
                    $('#cmbSeguradora').append('<option value="' + value.IdSeguradora + '">' + value.Nome + '</option>');
                    $('#cmbOrigemSegurado-editSegurado').append('<option value="' + value.IdSeguradora + '">' + value.Nome + '</option>');
                });
            }
    });
}

function CarregarComboEspecialidades() {
    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/ListarEspecialidades",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                retorno.d.forEach(function (value) {
                    $('#cmbEspecCadSegurado').append('<option value="' + value.IdEspecialidade + '">' + value.Descricao + '</option>');
                });
            }
    });
}

function CarregarComboEstadoCivil() {       // Carrega para titular e dependente
    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/CarregarComboEstadoCivilAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                retorno.d.forEach(function (value, index) {
                    if (index == 0) {
                        $('#cmbCivilCadSegurado').append('<option value="">Selecione</option>');
                        $('#cmbCivilCadDependente').append('<option value="">Selecione</option>');
                    }
                    $('#cmbCivilCadSegurado').append('<option value="' + value.Id + '">' + PrimeiraLetraMaiuscula((value.Descricao || '')) + '</option>');
                    $('#cmbCivilCadDependente').append('<option value="' + value.Id + '">' + PrimeiraLetraMaiuscula((value.Descricao || '')) + '</option>');
                });
            }
    });
}

function CarregarComboPlanoSaudeCadSegurado() {
    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/CarregarComboPlanoAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                retorno.d.forEach(function (value) {
                    if (value.TipoPlano == 1 && value.IdPlano != 99999)       // tipo saúde
                        $('#cmbplanoSaude_cadSegurado').append('<option value="' + value.IdPlano + '">' + PrimeiraLetraMaiuscula((value.Descricao || '')) + '</option>');
                });
            }
    });
}

function CarregarComboPlanoOdontoCadSegurado() {
    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/CarregarComboPlanoAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                retorno.d.forEach(function (value) {
                    if (value.TipoPlano == 2 && value.IdPlano != 10000)       // tipo odonto
                        if (value.IdPlano == 19) $('#cmbplanoOdonto_cadSegurado').append('<option value="' + value.IdPlano + '" selected="selected">' + PrimeiraLetraMaiuscula((value.Descricao || '')) + '</option>');
                        else $('#cmbplanoOdonto_cadSegurado').append('<option value="' + value.IdPlano + '">' + PrimeiraLetraMaiuscula((value.Descricao || '')) + '</option>');
                });
            }
    });
}

function CarregarComboPlanoOdontoCadDependente() {
    $.ajax({
        method: "POST",
        url: "ConsultaSegurado.aspx/CarregarComboPlanoAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                retorno.d.forEach(function (value) {
                    if (value.TipoPlano == 2 && value.IdPlano != 10000)       // tipo odonto
                        if (value.IdPlano == 19) $('#cmbplanoOdonto_CadDependente').append('<option value="' + value.IdPlano + '" selected="selected">' + PrimeiraLetraMaiuscula((value.Descricao || '')) + '</option>');
                        else $('#cmbplanoOdonto_CadDependente').append('<option value="' + value.IdPlano + '">' + PrimeiraLetraMaiuscula((value.Descricao || '')) + '</option>');
                });
            }
    });
}

function CarregarComboMelhorDiaPagCadSegurado() {
    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/CarregarComboMelhorDiaAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                retorno.d.forEach(function (value) {
                    $('#cmbMelhorDiaPag_cadSegurado').append('<option value="' + value.Id + '">' + PrimeiraLetraMaiuscula((value.MelhorDia || '')) + '</option>');
                });
            }
    });
}

function VerificarUnicidadeCPFSegurado(cpf) {
    $('.required-error').hide();

    $('#mdLoader').modal({
        backdrop: 'static',
        keyboard: false
    });

    $('#mdLoader').modal('show');

    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/ConsultaSeguradoPorCpfAsync",
        data: '{cpf: "' + cpf + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d.IdSegurado != 0) {

            $('#hddCpfTitular').val(retorno.d.Cpf);

            var id = retorno.d.IdSegurado == null ? '' : retorno.d.IdSegurado;
            var nome = retorno.d.Nome == null ? '' : retorno.d.Nome;
            var crm = retorno.d.Crm == null ? '' : parseInt(retorno.d.Crm);
            var ufcrm = (retorno.d.CrmEstado || '');
            var cpf = retorno.d.Cpf == null ? '' : retorno.d.Cpf;
            var idSeguradora = retorno.d.IdSeguradora == null ? '' : retorno.d.IdSeguradora;
            var dtNascimento = retorno.d.DataNasc == null ? '' : retorno.d.DataNasc;
            var email = retorno.d.Email == null ? '' : retorno.d.Email;
            var dataFiliacao = retorno.d.DataFiliacao == null ? '' : retorno.d.DataFiliacao;
            var estadoCivil = retorno.d.EstadoCivil;
            var idEstadoCivil = retorno.d.IdEstadoCivil;
            var sexo = retorno.d.Sexo == null ? '' : retorno.d.Sexo;
            var especialidade = retorno.d.Especialidade == null ? '' : retorno.d.Especialidade;
            var nacionalidade = retorno.d.Nacionalidade == null ? '' : retorno.d.Nacionalidade;
            var pispasep = retorno.d.PisPasep == null ? '' : retorno.d.PisPasep;
            var cns = retorno.d.Cns == null ? '' : retorno.d.Cns;
            var dn = retorno.d.Dn == null ? '' : retorno.d.Dn;
            var nomeMae = retorno.d.NomeMae == null ? '' : retorno.d.NomeMae;
            var inicioVigencia = retorno.d.InicioVigencia == null ? '' : retorno.d.InicioVigencia;
            var fimVigencia = retorno.d.FimVigencia == null ? '' : retorno.d.FimVigencia;
            var proposta = retorno.d.NrProposta == null ? '' : retorno.d.NrProposta;
            var filiacao = retorno.d.NrFiliacao == null ? '' : retorno.d.NrFiliacao;
            var nrCarteirinha = retorno.d.NrCarteirinha == null ? '' : retorno.d.NrCarteirinha;
            var MDPag = retorno.d.IdMelhorDiaPagamento == null ? '' : retorno.d.IdMelhorDiaPagamento;

            var logradouro = '';
            var bairro = '';
            var cidade = '';
            var uf = '';
            var cep = '';

            var DDD = '';
            var DDDCelular = '';
            var telefone = '';
            var celular = '';

            if (retorno.d.Enderecos.length > 0) {
                logradouro = retorno.d.Enderecos[0].end_endereco == null ? '' : retorno.d.Enderecos[0].end_endereco;
                bairro = retorno.d.Enderecos[0].end_bairro == null ? '' : retorno.d.Enderecos[0].end_bairro;
                cidade = retorno.d.Enderecos[0].end_cidade == null ? '' : retorno.d.Enderecos[0].end_cidade;
                uf = retorno.d.Enderecos[0].end_estado == null ? '' : retorno.d.Enderecos[0].end_estado;
                cep = retorno.d.Enderecos[0].end_cep == null ? '' : retorno.d.Enderecos[0].end_cep;
            }
            if (retorno.d.Contatos.length > 0) {
                DDD = retorno.d.Contatos[0].cnt_ddd == null ? '' : retorno.d.Contatos[0].cnt_ddd.trim();
                DDDCelular = retorno.d.Contatos[0].cnt_ddd_celular == null ? '' : retorno.d.Contatos[0].cnt_ddd_celular.trim();
                telefone = retorno.d.Contatos[0].cnt_fone == null ? '' : retorno.d.Contatos[0].cnt_fone.trim();
                celular = retorno.d.Contatos[0].cnt_celular == null ? '' : retorno.d.Contatos[0].cnt_celular.trim();
            }

            $.ajax({
                method: "POST",
                url: "CadastraSegurado.aspx/ConsultarPlanoSeguradoPorId",
                data: '{idSegurado: "' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (plano) {
                if (plano.d != null) {
                    if (plano.d.length > 0) {
                        plano.d.forEach(function (value) {
                            if (value.TipoPlano == 1) $('#cmbplanoSaude_cadSegurado').append('<option value="' + value.IdPlano + '" selected="selected">' + value.Descricao + '</option>');
                            else if (value.TipoPlano == 2) $('#cmbplanoOdonto_cadSegurado').append('<option value="' + value.IdPlano + '" selected="selected">' + value.Descricao + '</option>');
                        });
                    }
                }
            });

            $.ajax({
                method: "POST",
                url: "CadastraSegurado.aspx/ListarSeguradoraAnterior",
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (retorno) {
                if (retorno.d != null)
                    if (retorno.d.length > 0) {
                        retorno.d.forEach(function (value) {
                            if (value.IdSeguradora == idSeguradora) $('#cmbSeguradora').append('<option value="' + value.IdSeguradora + '" selected="selected">' + value.Nome + '</option>');
                        });
                    }
            });



            $('#hddIdSegurado').text(id);
            $('#mainContent_txtCPFCadSegurado').val(MascaraCPFValue(cpf));

            if (DDD != '' && telefone != '') {
                if (telefone.length >= 8)
                    $('#mainContent_txtTelefoneCadSegurado').val(MascaraTelefoneValue(DDD + '' + telefone)).focus();
                else
                    $('#mainContent_txtTelefoneCadSegurado').val('');
            }
            else
                $('#mainContent_txtTelefoneCadSegurado').val('');

            if (DDDCelular != '' && celular != '') {
                if (celular.length >= 9)
                    $('#mainContent_txtCelularCadSegurado').val(MascaraCelularValue(DDDCelular + '' + celular)).focus();
                else
                    $('#mainContent_txtCelularCadSegurado').val('');
            }
            else
                $('#mainContent_txtCelularCadSegurado').val('');

            $('#mainContent_txtCRMCadSegurado').val(crm).focus();
            $('#cmbEspecCadSegurado').val($('option:contains("' + especialidade + '")').val());
            $('#mainContent_txtDataNascCadSegurado').val(dtNascimento).focus();
            $('#cmbSexoCadSegurado').val($('option:contains("' + sexo + '")').val());
            $('#cmbCivilCadSegurado').val(idEstadoCivil);
            $('#cmbMelhorDiaPag_cadSegurado').val(MDPag);
            $('#mainContent_txtNomeMaeCadSegurado').val(nomeMae).focus();
            $('#cmbNacionalidadeCadSegurado').val(nacionalidade).focus();
            $('#mainContent_txtPISPASEPCadSegurado').val(MascaraPisValue(pispasep)).focus();
            $('#mainContent_txtCNSCadSegurado').val(MascaraCNSValue(cns)).focus();
            $('#mainContent_txtDNCadSegurado').val(MascaraDNValue(dn)).focus();
            $('#mainContent_txtDataFiliacaoCadSegurado').val(dataFiliacao);
            $('#mainContent_txtInicioVigenciaCadSegurado').val(inicioVigencia);
            $('#mainContent_txtFimVigenciaCadSegurado').val(fimVigencia);
            $('#mainContent_txtEmailCadSegurado').val(email).focus();
            $('#mainContent_txtPropostaCadSegurado').val(proposta).focus();
            $('#mainContent_txtLogradouroCadSegurado').val(logradouro).focus();
            $('#mainContent_txtBairroCadSegurado').val(bairro).focus();
            $('#mainContent_txtCidadeCadSegurado').val(cidade).focus();
            $('#mainContent_cmbUFCadSegurado').val(uf);
            $('#mainContent_cmbUFCRMCadSegurado').val(ufcrm);
            $('#mainContent_txtCEPCadSegurado').val(MascaraCEPValue(cep)).focus();
            $('#mainContent_txtNomeCadSegurado').val(nome).focus();
            $('#mainContent_txtNumFiliacaoCadSegurado').val(filiacao).focus();
            $('#mainContent_txtNrCarteirinhaCadSegurado').val(nrCarteirinha).focus();

            // Forma de pagamento
            switch (retorno.d.IdFormaPagamento) {
                case 1:
                    FPagtoBoletoCadSegurado();
                    break;
                case 2:
                    FPagtoCCCadSegurado();
                    break;
                case 3:
                    FPagtoDebitoCadSegurado();
                    // Carrega dados de cobrança, caso exista
                    if (retorno.d.DadosCobranca != null)
                        if (retorno.d.DadosCobranca.length > 0) {
                            let IdBanco = retorno.d.DadosCobranca[0].dco_ban_id;
                            let auxConta = retorno.d.DadosCobranca[0].dco_conta;
                            let auxAgencia = retorno.d.DadosCobranca[0].dco_agencia;

                            $('#mainContent_txtAgencia_cadSeguro').val(auxAgencia).focus();
                            $('#mainContent_txtConta_cadSegurado').val(auxConta).focus();
                            //Da o foco no input do nome
                            $('#mainContent_txtNomeCadSegurado').focus();

                            CarregarComboBancosCadSegurado(IdBanco);
                            $('#cmbBanco_cadSegurado').val(IdBanco);
                        }
                    break;
            }

            //Desabilita clique das formas de pagto através da variável
            carregouSegurado = true;
            cadastrouTitular = true;

            //} else VerificarUnicidadeCPF($('#mainContent_txtCPFCadSegurado').val().replace(/[^\d]+/g, ''), "Titular");
        }

        $('#mdLoader').modal('hide');
    });
}
function VerificarUnicidadeCPF(cpf, pessoa) {
    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/VerificarUnicidadeCPF",
        data: '{cpf: "' + cpf + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d && pessoa == 'Titular') {
            $('#SPCPFCadSegurado').text('CPF já cadastrado!');
            $('#SPCPFCadSegurado').show();
            erros += 1;
        } else if (retorno.d && pessoa == 'Dependente') {
            $('#SPCPFCadDependente').text('CPF já cadastrado!');
            $('#SPCPFCadDependente').show();
            erros += 1;
            $(".input-dependente").prop("readonly", true);
            limparCamposDependente();
        } else {
            $(".input-dependente").prop("readonly", false);
        }
    });
}

//Zera os erros de validação quando usuário altera o CPF já cadastrado
function ZerarErroValidacao() { erros = 0 }


function CadastrarDependente() {
    var data = {
        Dependente: {
            dep_par_id: 'D',
            dep_nome: $('#mainContent_txtNomeCadDependente').val(),
            dep_civ_id: $('#cmbCivilCadDependente').val(),
            dep_sexo: $('#cmbSexoCadDependente').val(),
            dep_data_nascimento: DateSerializer($('#mainContent_txtDataNascCadDependente').val()),
            dep_prf_id: $('#cmbProfissaoCadDependente').val(),
            dep_cpf: $('#mainContent_txtCPFCadDependente').val().replace(/[^\d]+/g, ''),
            dep_nacionalidade: $('#cmbNacionalidadeCadDependente').val(),
            dep_nome_mae: $('#mainContent_txtNomeMaeCadDependente').val(),
            dep_cns: $('#mainContent_txtCNSCadDependente').val().replace(/ [^\d] + /g, ''),
            dep_pispasep: $('#mainContent_txtPISPASEPCadDependente').val().replace(/[^\d]+/g, ''),
            dep_cpf_titular: $('#mainContent_txtCPFCadSegurado').val().replace(/[^\d]+/g, ''),
            //Verificar o que será passado            
            dep_pla_id: $('#mainContent_cmbPlanoSaudeCadSegurado').val(),
            dep_dn: $('#mainContent_txtDNCadDependente').val().replace(/[^\d]+/g, ''),
            dep_inicio_vigencia: DateSerializer($('#mainContent_txtInicioVigenciaCadDependente').val()),
            dep_fim_vigencia: DateSerializer($('#mainContent_txtFimVigenciaCadDependente').val()),
            dep_tpa_id: $('#mainContent_cmbParentescoCadDependente').val(),
            dep_email: $('#mainContent_txtEmailCadDependente').val(),
            dep_numero_carteira: $('#mainContent_txtNrCarteirinhaCadDep').val()
        },
        Endereco: {
            end_id: 0,
            end_par_id: 'D',
            end_complemento: '',
            end_cpf: $('#mainContent_txtCPFCadDependente').val().replace(/[^\d]+/g, ''),
            end_endereco: $('#mainContent_txtLogradouroCadDependente').val(),
            end_bairro: $('#mainContent_txtBairroCadDependente').val(),
            end_cidade: $('#mainContent_txtCidadeCadDependente').val(),
            end_cep: $('#mainContent_txtCEPCadDependente').val().replace(/[^\d]+/g, ''),
            end_estado: $('#mainContent_cmbUFCadDependente').val(),
            end_seg_id: null,
            end_dep_id: 0
        },
        Contato: {
            cnt_par_id: 'D',
            cnt_cpf: $('#mainContent_txtCPFCadDependente').val().replace(/[^\d]+/g, ''),
            cnt_fone: $('#mainContent_txtTelefoneCadDependente').val(),
            cnt_ddd: '',
            cnt_celular: $('#mainContent_txtCelularCadDependente').val(),
            cnt_seg_id: null,
            cnt_dep_id: 0
        },
        PlanoDependente:
        {
            pls_id: 0,
            pls_seg_id: 0,
            pls_pla_id: $('#cmbplanoOdonto_CadDependente').val(),
            pls_par_id: 'D'
        }
    };

    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/CadastrarDependenteAsync",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.includes('erro')) {
                alert('erro adicionar dependente. Erro dev tools log.');
                //console.log(retorno.d);
                $('#mdLoader').modal('hide');
                return;
            }
        $('#mdLoader').modal('hide');
        if (retorno.d) {
            $('#mdMensagem').text('Dados do dependente cadastrados com sucesso!');
            $('#mdCadSegurado').modal('show');
            $('.input-dependente').val('');
            $('.cmb-dependente').val('');
            $('#mainContent_txtCPFCadDependente').val('');
            CarregarDependentes();
        }
        else {
            $('#mdMensagem').text('Não foi possível cadastrar o dependente!');
            $('#mdCadSegurado').modal('show');
        }
    });
}
function CarregarDependentes() {
    $('#mdLoader').modal('show');

    var cpf = $('#hddCpfTitular').val();
    $('#dvCadTitular').hide();
    $('#btncadastrarSegurado').removeClass('active');
    $('#btncadastrarDependentes').addClass('active');

    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/ConsultaDependentesPorCpfTitularAsync",
        data: '{cpf: "' + cpf + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                $('#dvDependentesCadastrados').show();
                CarregarListaDependentes(cpf, retorno.d);
            } else {
                $('#dvDependentesCadastrados').hide();
            }
        CarregaComboProfissoesCadDependentes();
        $('#dvCadDependentes').show();
    });

    $('#mdLoader').modal('hide');
}
function ConfirmarExclusaoDependente(cpfTitular, IdDependente) {
    $('#mdCadDependente').modal('show');
    $('#btnExcluirCadDependente').click(function () {
        ExcluirDependente(cpfTitular, IdDependente);
        $('#mdCadDependente').modal('hide');
    });
}
function ExcluirDependente(cpfTitular, IdDependente) {
    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/ExcluirDependentePorIdAsync",
        data: '{Id: "' + IdDependente + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function () {
        // TODO: RETORNA DEPEDNETES POR CPF TITULAR.
        $('#mdMensagem').text('Dependente excluído com sucesso!');
        $('#mdCadSegurado').modal('show');
        CarregarDependentes();
    });
}

function CarregaComboProfissoesCadDependentes() {
    $.ajax({
        method: "POST",
        url: "CadastraSegurado.aspx/ListarProfissoes",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        let options = `<option value='0'>Selecione</option>`;
        if (retorno.d != null)
            if (retorno.d.length > 0) {
                retorno.d.forEach((item) => {
                    options += `<option value='${item.Id}'>${PrimeiraLetraMaiuscula(item.Nome)}</option>`;
                });
            }

        let cmbPrf = `<select id="cmbProfissaoCadDependente" class="dropdown-material cmb-dependente">
                            ${options}
                        </select>`;

        $('#dv-cmbCadDependentes').html(cmbPrf);
    });
}

function CarregarListaDependentes(cpfTitular, objDependentes) {
    var objDependenteList = [];
    objDependentes.forEach(function (item) {
        if (item.MsgErro) {
            alert(item.MsgErro);
            return;
        }

        let DepGrid = class {
            constructor() {
                this.Id;
                this.Nome;
                this.htmlButton;
            }
        };

        DepGrid.Id = item.IdDependente;
        DepGrid.Nome = item.Nome;

        //o atributo htmlButton deverá ter esse msm nome no objeto js da página, e receber o html do botão..                                   
        DepGrid.htmlButton = '<span class="sp-link" onClick="ConfirmarExclusaoDependente(' + cpfTitular + ' , ' + DepGrid.Id + ')">Excluir </span>';
        objDependenteList.push(DepGrid);
    });


    //iniciarPaginacao(objDependenteList);
    //exibirPaginacao(1);

    iniciarPaginacaoCustom(objDependenteList, 50);
    exibirPaginacaoCustom(1, 50);

}

$(document).ready(function () {

    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $("#mainContent_txtLogradouroCadSegurado").val("");
        $("#mainContent_txtBairroCadSegurado").val("");
        $("#mainContent_txtCidadeCadSegurado").val("");
        $("#mainContent_cmbUFCadSegurado").val("");
    }

    $("#mainContent_txtCEPCadSegurado").blur(function () {
        var cep = $(this).val().replace(/\D/g, '');

        if (cep != "") {
            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;
            //Valida o formato do CEP.
            if (validacep.test(cep)) {
                //Consulta o webservice viacep.com.br/
                //Atualiza os campos com os valores da consulta.
                $.ajax({
                    method: "POST",
                    url: `ConsultaSegurado.aspx/ConsultaEnderecoBaseCep`,
                    data: '{cep: "' + cep + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (retorno) {
                    $("#mainContent_txtLogradouroCadSegurado").val(retorno.d.Endereco).focus();
                    $("#mainContent_txtBairroCadSegurado").val(retorno.d.Bairro).focus();
                    $("#mainContent_txtCidadeCadSegurado").val(retorno.d.Cidade).focus();
                    $("#mainContent_cmbUFCadSegurado").val(retorno.d.Estado).focus();
                });

            } //end if.
        } //end if.        
    });
});

$(document).ready(function () {

    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $("#mainContent_txtLogradouroCadDependente").val("");
        $("#mainContent_txtBairroCadDependente").val("");
        $("#mainContent_txtCidadeCadDependente").val("");
        $("#mainContent_cmbUFCadDependente").val("");
    }

    $("#mainContent_txtCEPCadDependente").blur(function () {
        var cep = $(this).val().replace(/\D/g, '');

        if (cep != "") {
            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;
            //Valida o formato do CEP.
            if (validacep.test(cep)) {
                //Consulta o webservice viacep.com.br/
                //Atualiza os campos com os valores da consulta.
                $.ajax({
                    method: "POST",
                    url: `ConsultaSegurado.aspx/ConsultaEnderecoBaseCep`,
                    data: '{cep: "' + cep + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (retorno) {
                    $("#mainContent_txtLogradouroCadDependente").val(retorno.d.Endereco).focus();
                    $("#mainContent_txtBairroCadDependente").val(retorno.d.Bairro).focus();
                    $("#mainContent_txtCidadeCadDependente").val(retorno.d.Cidade).focus();
                    $("#mainContent_cmbUFCadDependente").val(retorno.d.Estado).focus();
                });
            } //end if.        
        }
    });
});
// #endregion
// ######################################################################### Planos.aspx ##################################################################################################
// #region PlanosPage
$('#dv-aba-odonto').click(function () {
    ShowOdonto();
});
$('#dv-aba-saude').click(function () {
    ShowSaude();
});

// EVENTS ##########################################################
function ShowOdonto() {
    $('#dv-aba-odonto').addClass('active');

    $('#dv-aba-saude').removeClass('active');

    $('#dv-content-saude').hide();
    $('#dv-content-odonto').show();

}
function ShowSaude() {
    $('#dv-aba-odonto').removeClass('active');

    $('#dv-aba-saude').addClass('active');

    $('#dv-content-saude').show();
    $('#dv-content-odonto').hide();
}
// #endregion
// ######################################################################### Pagamentos.aspx ##############################################################################################
    //#region PagamentosPage
let urlPag = new URL(window.location)
urlPag = String(urlPag)

if (urlPag.includes('Pagamentos.aspx')) {
    var idAba = '';
    var spltUrl = urlPag.split("?");
    if (spltUrl.length > 1)
        if (urlPag.includes('aba'))
            idAba = spltUrl[1].replace('aba=', '');
}

if (idAba === '1')
    onClickManual();

if (idAba === '2')
    onClickAuto();


$('#dv-aba-manual').click(function () {
    var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Pagamentos/Pagamentos.aspx?aba=1';
    window.location.replace(urlLogin);
});

$('#dv-aba-automatico').click(function () {
    var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Pagamentos/Pagamentos.aspx?aba=2';
    window.location.replace(urlLogin);
});

$('#btnConsultarPagamento').click(function () {
    ConsultarPagamento();
});

$(document).ready(function() {

    $('#mainContent_txtNrCarteirinhaeditPagamento').on('input', function (event) {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $('form1').on('submit', function (e) {
        e.preventDefault();
    });

    $('#txtPeriodoBaixaAutomatica').click(function () {
        $('#SPtxtPeriodoBaixaAutomatica').hide();
    });

    $('#txtPeriodoBaixaAutomatica').keydown(function () {
        $('#SPtxtPeriodoBaixaAutomatica').hide();
    });

});



$('#mainContent_txtNrCarteirinhaeditPagamento').attr('maxLength', '20').keypress(limitMe);

function limitMe(e) {
    if (e.keyCode == 8) { return true; }
    return this.value.length < $(this).attr("maxLength");
}



$('#btnLimparPagamento').click(function () {
    $('#mainContent_txtCPFeditPagamento').val('');
    $('#mainContent_txtNomeeditPagamento').val('');
    $('#mainContent_txtCRMeditPagamento').val('');
    $('#mainContent_cmbProdutoeditPagamento').val('');
    $('#mainContent_txtNrCarteirinhaeditPagamento').val('');
    $('#mainContent_cmbStatuseditPagamento').val('');
    $('#txtDataInicioeditPagamento').val('');
    $('#txtDataFimeditPagamento').val('');
    $('#SPtxtCPF').css('display', 'none');
    $('#SPRetornoErro').css('display', 'none');
    $('#SPtxtDataInicio').css('display', 'none');
    $('#SPtxtDataFim').css('display', 'none');
});

function getFileData(myFile){
    $("#SPUploadBaixaManualValida").hide();
    var file = myFile.files[0];  
    var filename = file.name;
    let validos = /(\.csv|\.txt)$/i;
    
    var id_layout = $('input[name=rd-config]:checked').val();
    if (validos.test(filename)) {
        
        $("#SPUploadArquivoManual").hide();
        $("#mainContent_btnValidarArquivo").prop('disabled', false);

    } else {
        $("#SPUploadBaixaManualValida").text("Formato do arquivo inválido");
        $("#SPUploadBaixaManualValida").show();
        $("#img-upload-baixa-manual").val('');
        $('#mainContent_btnValidarArquivo').prop('disabled', true);
    }
}


// EVENTS ##########################################################

function onClickAuto() {
    // Este procedimento é necessário, pois existem 2 tabelas c as classes .tbody, logo só poderia existir uma.
    $('#tblPagManual').html('');
    $('.tbody').html('');
    $(this).parent().parent('#tblPagManual').remove();
    $(this).parent().parent('#tblPagAuto').remove();
    $('#tblPagAuto').html('');
    $('#tblPagAuto').html(`
          <table class='table table-hover'>
                                    <tr>
                                        <td align='center'><b>Data Importação</b></td>
                                        <td align='center'><b>Mês Referencia</b></td>
                                        <td align='center'><b>Responsável</b></td>
                                        <td align='center'><b>Nome Arquivo</b></td>
                                        <td align='center'><b>Status</b></td>
                                    </tr>
                                    <tbody class='tbody'>
                                        <tr>
                                            <td class='DataEvento' align='center'></td>
                                            <td class='MesRef' align='center'></td>
                                            <td class='Usuario' align='center'></td>
                                            <td class='NomeArquivo' align='center'></td>
                                            <td class='Status' align='center'></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div class='col-md-12 btns-nav' style='margin: 0;'>
                                    <div class='row'>
                                        <button type='button' onclick='voltarPag()' id='btnVoltar' class='page-link'>&laquo; Anterior</button>
                                        <ul class='pagination pag-1'></ul>
                                        <ul class='pagination pag-2'></ul>
                                        <button type='button' onclick='avancarPag()' id='btnProximo' class='page-link'>Próximo &raquo;</button>
                                    </div>
                                </div>
`);

    ShowPagamentoAutomatico();
}

function onClickManual() {

    $('#tblPagAuto').html('');
    $('.tbody').html('');
    $(this).parent().parent('#tblPagManual').remove();
    $(this).parent().parent('#tblPagAuto').remove();

    $('#tblPagManual').html('');

    $('#tblPagManual').html(`
   <table class='table table-hover active'>
                                    <tr>
                                        <td align='left'><b>Nome</b></td>
                                        <td align='center'><b>Plano</b></td>
                                        <td align='center'><b>Vencimento</b></td>                                        
                                        <td align='center'><b>Forma de Pagamento</b></td>
                                        <td align='center'><b>Data Pagamento</b></td>
                                        <td align='center'><b>Prêmio</b></td>
                                        <td align='center'><b>Status</b></td>
                                    </tr>
                                    <tbody class='tbody'>
                                        <tr>
                                            <td class='NomeSegurado' align='left'></td>
                                            <td class='Plano' align='center'></td>
                                            <td class='DtVencimento' align='center'></td>                                            
                                            <td class='FPagamento' align='center'></td>
                                            <td class='DtPagamento' align='center'></td>
                                            <td class='VlPremio' align='center'></td>
                                            <td class='Status' align='center'></td>
                                            <td class='Acao'></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div class='col-md-12 btns-nav' style='margin: 0;'>
                                    <div class='row'>
                                        <button type='button' onclick='voltarPag()' id='btnVoltar' class='page-link'>&laquo; Anterior</button>
                                        <ul class='pagination pag-1'></ul>
                                        <ul class='pagination pag-2'></ul>
                                        <button type='button' onclick='avancarPag()' id='btnProximo' class='page-link'>Próximo &raquo;</button>
                                    </div>
                                </div>
`); 
    ShowPagamentoManual();
}

function SucessoValidacaoImportacao(mensagem)
{
    $("#SPRetornoImportacao").text("");
    $("#SPUploadBaixaManual").text(mensagem);
    $("#SPUploadBaixaManual").show();
    $('#mdPagamentoAutomatico').modal('show');
    ListarHistoricoImportacao();

    return false;
}

function ExibirModalImportacaoSucesso()
{
    $('#mdPagamentoAutomatico').modal('hide');
    $('#mdPagamentoAutomaticoSucesso').modal('show');
}

function ListarHistoricoImportacao() {
    $.ajax({
        method: "POST",
        url: `Pagamentos.aspx/ListarHistoricoImportacao`,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        console.log(retorno.d);
        let objHistorico = [];
        retorno.d.forEach(function (value) {
            let Hist = class {
                constructor() {
                    this.DataEvento;
                    this.Usuario;
                    this.MesRef;
                    this.Status;
                }
            };

            Hist.DataEvento = value.DataEvento;
            Hist.Usuario = value.Usuario;
            Hist.NomeArquivo = value.NomeArquivo;
            Hist.MesRef = value.MesRef;
            Hist.Status = value.Status;

            objHistorico.push(Hist);
        });

        var qntResult = objHistorico.length;
        $('#lblTotalResultadoAutomatico').text(qntResult);
       
        iniciarPaginacaoCustom(objHistorico, 20);
        exibirPaginacaoCustom(1, 20);

        $('#dvResultadoPagamentoAutomatico').css('display', 'block');
    });
}

function FalhaValidacaoImportacao(mensagem)
{
    $("#SPUploadBaixaManualValida").html("");

    letra = mensagem.split(";");
    letra.forEach(function (value)  {
        $("#SPUploadBaixaManualValida").append('<div>'+value+'</div>')
    })
    $("#SPUploadBaixaManualValida").show();

    ListarHistoricoImportacao();
    return false;
}

function ValidacaoImportacao()
{
    $("#SPUploadBaixaManualValida").hide();
    $("#SPtxtPeriodoBaixaAutomatica").hide();

    if($("#txtPeriodoBaixaAutomatica").val() == "")
    {
        $("#SPtxtPeriodoBaixaAutomatica").text("Selecione um período para importação");
        $("#SPtxtPeriodoBaixaAutomatica").show();
        return false;
    }

    if( $("#mainContent_UploadBaixaManual").val() == "")
    {
        $("#SPUploadBaixaManualValida").text("Selecione um arquivo para importar");
        $("#SPUploadBaixaManualValida").show();
        return false;
    }
    var data = new FormData();
    var files = $("#mainContent_UploadBaixaManual").get(0).files;

    if (files.length > 0) {
        data.append("UploadArquivoValidacao", files[0]);
    }
    var ajaxRequest = $.ajax({
        type: "POST",
        url: "Pagamentos.aspx",
        contentType: false,
        processData: false,
        data: data
    });

    $('#mdLoader').modal('show');

    ajaxRequest.done(function (xhr, textStatus) {
      
        var MesRefencia = $("#txtPeriodoBaixaAutomatica").val();
        var txtNome = MesRefencia;
        var mensagemErro = "";
        var mensagemSucesso = "";
        $.ajax({
            url: 'Pagamentos.aspx/Validar',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({MesReferencia: txtNome }),
            success: function (msg) {
                msg.d.forEach(function (item, index) {

                    if (item == "Arquivo validado com sucesso") {
                        mensagemSucesso += item;
                    }
                    else{
                        mensagemErro += item;
                    }

                });

                if(mensagemErro != "")
                {
                    $('#mdLoader').modal('hide');
                    FalhaValidacaoImportacao(mensagemErro);
                }

                if(mensagemSucesso != "")
                {
                    $('#mdLoader').modal('hide');
                    SucessoValidacaoImportacao(mensagemSucesso);
                }
                return false;
            },
            error: function (msg) {
                $('#mdLoader').modal('hide');
                alert("Error while inserting data. look at console.log");
                console.log(msg.d[0]);
            }
        });
    });
    return false;
}

function ImportarArquivoBaixaAutomatica()
{
    var data = new FormData();
    var files = $("#mainContent_UploadBaixaManual").get(0).files;

    if (files.length > 0) {
        data.append("UploadArquivoValidacao", files[0]);
    }

    var ajaxRequest = $.ajax({
        type: "POST",
        url: "Pagamentos.aspx",
        contentType: false,
        processData: false,
        data: data
    });

    $('#mdPagamentoAutomatico').modal('hide');
    $('#mdLoader').modal('show');
    ajaxRequest.done(function (xhr, textStatus) {
        var MesRefencia = $("#txtPeriodoBaixaAutomatica").val();
        var txtNome = MesRefencia;
        var mensagemErro = "";
        var mensagemSucesso = "";
        $.ajax({
            url: 'Pagamentos.aspx/Importar',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({MesReferencia: txtNome }),
            success: function (msg) {
                msg.d.forEach(function (item, index) {

                    if (item == "O arquivo foi importado com sucesso") {
                        mensagemSucesso += item;
                    }
                    else{
                        mensagemErro += item;
                    }

                });

                if(mensagemErro != "")
                {
                    ListarHistoricoImportacao();
                    $('#mdPagamentoAutomatico').modal('show');
                    $('#mdLoader').modal('hide');
                    ExibirModalImportacaoSucesso();
                }

                if(mensagemSucesso != "")
                {
                    ListarHistoricoImportacao();
                    $('#mdPagamentoAutomatico').modal('show');
                    $('#mdLoader').modal('hide');
                    ExibirModalImportacaoSucesso();
                }
                return false;
            },
            error: function () {
                $('#mdLoader').modal('hide');
                $('#mdPagamentoAutomatico').modal('show');
                alert("Error while inserting data");
            }
        });
    });

    return false;
}

$('#faClosePagamento').click(function () {
    $('#mdPagamento').modal('hide');
});

$('#faClosePagamentoAutomatico').click(function () {
    $('#mdPagamentoAutomatico').modal('hide');
    $('#SPUploadBaixaManualValida').hide();
});
$('#faClosePagamentoAutomaticoSucesso').click(function () {
    $('#mdPagamentoAutomaticoSucesso').modal('hide');
    $('#SPUploadBaixaManualValida').hide();
});
$('#btnFecharImportacaoBaixaAutomatica').click(function () {
    $('#mdPagamentoAutomatico').modal('hide');
    $('#SPUploadBaixaManualValida').hide();
});

function ShowPagamentoManual() {
    $('#dv-aba-manual').addClass('active');
    $('#dv-aba-automatico').removeClass('active');

    $('#dv-content-automatico').hide();
    $('#dv-content-manual').show();
    $('#dvResultadoPagamento').show();
    $('#dvResultadoPagamentoAutomatico').hide();

}
function ShowPagamentoAutomatico() {
    $('#dv-aba-manual').removeClass('active');
    $('#dv-aba-automatico').addClass('active');

    $('#dv-content-automatico').show();
    $('#dv-content-manual').hide();
    $('#dvResultadoPagamento').css('display', 'none');
    $('#dvResultadoPagamentoAutomatico').show();

    ListarHistoricoImportacao();
}

function baixaAutomatica() {
    $('#mdPagamentoAutomatico').modal('show');
    return false;
}

function ConsultarPagamento() {
    $('#SPRetornoErro').hide();
    onClickManual();    

    var CPF = $('#mainContent_txtCPFeditPagamento').val().replace(/[^\d]+/g, '');
    var Nome = $('#mainContent_txtNomeeditPagamento').val();
    var CRM = $('#mainContent_txtCRMeditPagamento').val();
    var Produto = $('#mainContent_cmbProdutoeditPagamento').val();
    var Carterinha = '';
    if ($('#mainContent_txtNrCarteirinhaeditPagamento').val() != '') {
        Carterinha = $('#mainContent_txtNrCarteirinhaeditPagamento').val().replace(/[^\d]+/g, '');
    }

    var Status = $('#mainContent_cmbStatuseditPagamento').val();
    var DataInicio = $('#txtDataInicioeditPagamento').val();
    var Datafim = $('#txtDataFimeditPagamento').val();

    var camposValidos = validarCamposPagamento(CPF, Nome, CRM, Produto, Carterinha, Status, DataInicio, Datafim);

    if (DataInicio !== '') {
        if (DataInicio.length > 0) {
            DataInicio = DataInicio.split('/');
            DataInicio = DataInicio[1] + '-' + DataInicio[0] + '-' + DataInicio[2];
        }
    }
    if (Datafim !== '') {
        if (Datafim.length > 0) {
            Datafim = Datafim.split('/');
            Datafim = Datafim[1] + '-' + Datafim[0] + '-' + Datafim[2];
        }
    }

    if (camposValidos) {
        $('#mdLoader').modal({
            backdrop: 'static',
            keyboard: false
        });
        $('#mdLoader').modal('show');

        $.ajax({
            method: "POST",
            url: "Pagamentos.aspx/ListarPagamentoPorParams",
            data: '{DtInicial: "' + DataInicio + '", DtFinal: "' + Datafim + '", NrCarteira: "' + Carterinha + '", IdPlano: "' + Produto + '", Crm: "' + CRM + '", IdStatus: "' + Status + '", Cpf: "' + CPF + '", Nome: "' + Nome + '", Ativo: true}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            var objPagamentosList = [];
            retorno.d.forEach(function (item, index) {
                if (item.MsgErro) {
                    alert(item.MsgErro);
                    return;
                }
                var pagamento = new class {
                    constructor() {
                        this.Id;
                        this.NomeSegurado;
                        this.DtVencimento;
                        this.VlPremio;
                        this.Plano;
                        this.FPagamento;
                        this.DtPagamento;
                        this.Status;
                    }
                }
                pagamento.Id = item.Id;
                pagamento.NomeSegurado = item.NomeSegurado;
                pagamento.DtVencimento = item.DtVencimento;
                pagamento.VlPremio = item.VlPremio;
                pagamento.FPagamento = item.FPagamento;
                pagamento.DtPagamento = item.DtPagamento;
                pagamento.Status = item.Status;
                pagamento.Plano = item.Plano;


                pagamento.htmlButton = '<i class="fa fa-search-dollar fa-2x"style="cursor: pointer" data-toggle="tooltip" data-placement="top" title="Visualizar Baixa" onclick="ManterBaixaPagamentoManual(' + pagamento.Id + ')"></i>';
                objPagamentosList.push(pagamento);
            });
           
            var qntResult = objPagamentosList.length;
            $('#lblTotalResultado').text(qntResult);

            iniciarPaginacaoCustom(objPagamentosList, 20);
            exibirPaginacaoCustom(1, 20);

            $('#mdLoader').modal('hide');
            $('#dvResultadoPagamento').css('display', 'block');
        });
    }
}
function validarCamposPagamento(CPF, Nome, CRM, Produto, Carterinha, Status, DataInicio, Datafim) {
    if (CPF == '' && Nome == '' && CRM == '' && Produto == '' && Carterinha == '' && Status == '' && DataInicio == '' && Datafim == '') {
        $('#dvResultadoPagamento').css('display', 'none');
        $('#SPRetornoErro').show();
        return false;
    }
    else if (CPF !== '') {
        var retorno = verificarCPF(CPF.replace('.', '').replace('.', '').replace('-', ''));
        if (retorno == false) {
            $('#dvResultadoPagamento').css('display', 'none');
            $('#SPtxtCPF').show();
            $('#mdLoader').modal('hide');
            return false;
        }
        return true;
    }
    var dt1 = DataInicio;
    var dt2 = Datafim;
    if (DataInicio !== '') {
        if (DataInicio.length > 0) {
            DataInicio = DataInicio.split('/');
            DataInicio = DataInicio[1] + '-' + DataInicio[0] + '-' + DataInicio[2];
        }
    }
    if (Datafim !== '') {
        if (Datafim.length > 0) {
            Datafim = Datafim.split('/');
            Datafim = Datafim[1] + '-' + Datafim[0] + '-' + Datafim[2];
        }
    }
    if (dt1 !== '' || dt2 !== '') {
        var dt1Valida = validaData(dt1);
        var dt2Valida = validaData(dt2);

        if (dt1Valida == false || dt2Valida == false) {
            return false;
        }
    }

    return true;
}

function ManterBaixaPagamentoManual(idPagamento) {
    $.ajax({
        method: "POST",
        url: "Pagamentos.aspx/ConsultaPagamentoPorIdAsync",
        data: '{Id: "' + idPagamento + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {

        var nome = retorno.d.NomeSegurado == null ? '' : retorno.d.NomeSegurado;
        var cpf = retorno.d.CpfSegurado == null ? '' : retorno.d.CpfSegurado;
        var data_vencimento = retorno.d.DtVencimento == null ? '' : retorno.d.DtVencimento;
        var crm = retorno.d.CrmTitular == null ? '' : retorno.d.CrmTitular;
        var plano = retorno.d.Plano == null ? '' : retorno.d.Plano;
        var forma_de_pagamento = retorno.d.FPagamento == null ? '' : retorno.d.FPagamento;
        var id = retorno.d.Id == null ? '' : retorno.d.Id;
        var dtPagamento = retorno.d.DtPagamento == null ? '' : retorno.d.DtPagamento;
        var nrDoc = retorno.d.NrDocumentoPagamento == null ? '' : retorno.d.NrDocumentoPagamento;
        var premioC = retorno.d.VlPremio == null ? '' : retorno.d.VlPremio;
        var status = retorno.d.Status == null ? '' : retorno.d.Status;
        var Juros = retorno.d.VlJuros == null ? '' : retorno.d.VlJuros;
        var vlrVencimento = retorno.d.VlVencimento == null ? '' : retorno.d.VlVencimento;

        $('#hddId').val(id);
        $('#sp-nome').text(nome);
        $('#sp-cpf').text(MascaraCPFValue(cpf));
        $('#sp-data-vencimento').text(data_vencimento);
        $('#sp-crm').text(crm);
        $('#sp-plano').text(plano);
        $('#sp-forma-pagamento').text(forma_de_pagamento);
        $('#sp-valor-premio').text(vlrVencimento);

        if (status == "Aberto" || status == "Vencido") {
            $('#txtDtPagamento-editSegurado').val('');
            $('#txtnrDocumento-editSegurado').val('');
            $('#txtpremioCorrigido-editSegurado').val('');
            $('#txtjuros-editSegurado').val('');
            $('#txtDtPagamento-editSegurado').attr('disabled', false);
            $('#txtnrDocumento-editSegurado').attr('readonly', false);
            $('#txtpremioCorrigido-editSegurado').attr('readonly', false);
            $('#txtjuros-editSegurado').attr('readonly', false);
            $('#btnBaixaPag').html(`<button id="btnSalvar" type="button" onclick="SalvarDadosManterBaixa('${status}')" class="btn btn-primary btn-concluir">Salvar e Sair</button>`);
        }
        else {
            $('#txtDtPagamento-editSegurado').val(dtPagamento);
            $('#txtnrDocumento-editSegurado').val(nrDoc);
            $('#txtpremioCorrigido-editSegurado').val(premioC);
            $('#txtjuros-editSegurado').val(Juros);
            $('.campo-obrigatorio').css("color", "white");
            $('#txtDtPagamento-editSegurado').attr('disabled', true);
            $('#txtnrDocumento-editSegurado').attr('readonly', true);
            $('#txtpremioCorrigido-editSegurado').attr('readonly', true);
            $('#txtjuros-editSegurado').attr('readonly', true);
            $('#btnBaixaPag').html(`<button id="btnAlterar" type="button" style="margin-right: 70px;" onclick="editarBaixaPagamento('${status}')" class="btn btn-primary btn-concluir">Alterar</button><button id="btnSalvar" type="button" onclick="SalvarDadosManterBaixa()" class="btn btn-primary btn-concluir">Sair</button>`);
        }

        $('#SPRetornoErroModalTxtnrDocumento').hide();
        $('#SPRetornoErroModalDtPagamento').hide();
        $('#SPRetornoErroModaltxtpremioCorrigido').hide();
        $('#SPRetornoErroModal').hide();

        $('#txtpremioCorrigido-editSegurado').priceFormat({
            prefix: '',
            centsSeparator: ',',
            thousandsSeparator: '.'
        });


        $('#txtjuros-editSegurado').priceFormat({
            prefix: '',
            centsSeparator: ',',
            thousandsSeparator: '.'
        });

        $('#mdPagamento').modal('show');
    });
}

function SalvarDadosManterBaixa(status = null) {

    if (status == null) {
        $('#mdPagamento').modal('hide');
        return;
    }

    $('#SPRetornoErroModalTxtnrDocumento').hide();
    $('#SPRetornoErroModalDtPagamento').hide();
    $('#SPRetornoErroModaltxtpremioCorrigido').hide();
    $('#SPRetornoErroModal').hide();

    var nr_documento = $('#txtnrDocumento-editSegurado').val();
    var dt_pagamento = $('#txtDtPagamento-editSegurado').val();
    var premio = $('#txtpremioCorrigido-editSegurado').val();
    var camposValidos = validarCamposBaixaPagamento(nr_documento, dt_pagamento, premio);
    if (camposValidos) {
        var DtPagamento = $('#txtDtPagamento-editSegurado').val();
        var DtVencimento = $('#sp-data-vencimento').text();
        var pagamento_id = $('#hddId').val()
        var identificacao = $('#txtnrDocumento-editSegurado').val();
        var vlr_recebido = $('#txtpremioCorrigido-editSegurado').val();
        var vlr_juros = $('#txtjuros-editSegurado').val();

        vlr_recebido = vlr_recebido.replace(".", "").replace(",", ".");

        vlr_juros = vlr_juros.replace(".", "").replace(",", ".");

        if (DtPagamento.length > 0) {
            DtPagamento = DtPagamento.split('/');
            DtPagamento = DtPagamento[1] + '-' + DtPagamento[0] + '-' + DtPagamento[2];

            //DtPagamento = formatDate(DtPagamento);
        }

        if (DtVencimento.length > 0) {
            DtVencimento = DtVencimento.split('/');
            DtVencimento = DtVencimento[1] + '-' + DtVencimento[0] + '-' + DtVencimento[2];

            //DtVencimento = formatDate(DtVencimento);
        }

        let hoje = new Date();

        hoje = formatDate(hoje);

        var data = {
            Pagamento: {
                pag_id: pagamento_id,
                pag_identificacao: identificacao,
                pag_data_pagamento: DtPagamento,
                pag_data_vencimento: DtVencimento,
                pag_data_baixa_pagamento: hoje,
                pag_valor_recebido: vlr_recebido,
                pag_juros: vlr_juros
            }
        };

        $.ajax({
            method: "POST",
            url: "Pagamentos.aspx/SalvarBaixaPagamentoAsync",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            if (retorno.d != null)
                if (retorno.d.includes('erro')) {
                    alert('Erro na tentativa de Salvar o titular. Erro no dev tools log.');
                    console.log(retorno.d);
                    return;
                }
            $('#mdPagamento').modal('hide');
            ConsultarPagamento();
        });
    }
}

function editarBaixaPagamento(status = null) {
    $('#btnSalvar').hide();
    $('#txtDtPagamento-editSegurado').attr('disabled', false);
    $('#txtnrDocumento-editSegurado').attr('readonly', false);
    $('#txtpremioCorrigido-editSegurado').attr('readonly', false);
    $('#txtjuros-editSegurado').attr('readonly', false);
    $('#btnBaixaPag').html(`<button id="btnAlterar" type="button" style="margin-right: 135px;" onclick="editarBaixaPagamento()" class="btn btn-primary btn-concluir">Alterar</button> <button id="btnSalvar" type="button" onclick="SalvarDadosManterBaixa('${status}')" class="btn btn-primary btn-concluir">Salvar e Sair</button>`);

}
function validarCamposBaixaPagamento(nrDocumento, dt_pagamento, vlrPremio) {
    if (nrDocumento == '') {
        $('#SPRetornoErroModalTxtnrDocumento').text('É obrigatório preencher o Nº do documento');
        $('#SPRetornoErroModalTxtnrDocumento').show();
        return false;
    }
    if (dt_pagamento == '') {
        $('#SPRetornoErroModalDtPagamento').text('É obrigatório preencher a data de pagamento');
        $('#SPRetornoErroModalDtPagamento').show();
        return false;
    }
    if (vlrPremio == '') {
        $('#SPRetornoErroModaltxtpremioCorrigido').text('É obrigatório preencher o prêmio corrigido');
        $('#SPRetornoErroModaltxtpremioCorrigido').show();
        return false;
    }

    if (dt_pagamento.length > 0) {
        dt_pagamento = dt_pagamento.split('/');
        dt_pagamento = dt_pagamento[1] + '-' + dt_pagamento[0] + '-' + dt_pagamento[2];
    }

    if (dt_pagamento !== '') {
        let hoje = new Date();
        let auxData = new Date(dt_pagamento);


        if (auxData > hoje) {
            $('#SPRetornoErroModalDtPagamento').text('Data deve ser igual ou retroativa à data de hoje!');
            $('#SPRetornoErroModalDtPagamento').show();
            return false;
        }
    }

    return true;
}
function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}
//#endregion
// ######################################################################### ConsultaMudancaFaixa.aspx ####################################################################################
// #region ConsultaMudancaFaixaPage
// Verificar modulo por url.
let url = new URL(window.location)
url = String(url)

if (url.includes('ConsultaFaturamento.aspx')) {
    var idAba = '';
    var spltUrl = url.split("?");
    if (spltUrl.length > 1)
        if (url.includes('aba'))
            idAba = spltUrl[1].replace('aba=', '');
}

if (idAba === '1')
    onClickFat();

if (idAba === '2')
    onClickMF();

$('#btnConsultarMudancaFaixa').click(function () {
    ConsultarMFaixa();
});

$('#btnLimparMudancaFaixa').click(function () {
    $('#mainContent_txtCPFeditMudancaFaixa').val('');
    $('#mainContent_txtNomeeditMudancaFaixa').val('');
    $('#mainContent_txtCRMeditMudancaFaixa').val('');
    $('#mainContent_cmbProdutoeditMudancaFaixa').val('');
    $('#mainContent_txtDataInicioeditMudancaFaixa').val('');
    $('#mainContent_txtDataFimeditMudancaFaixa').val('');
    $('#SPtxtCPF').css('display', 'none');
    $('#SPRetornoErro').css('display', 'none');
    $('#SPtxtDataInicio').css('display', 'none');
    $('#SPtxtDataFim').css('display', 'none');
});

// EVENTS ##########################################################

function ConsultarMFaixa() {
    $('#SPRetornoErro').hide();

    var CPF = $('#mainContent_txtCPFeditMudancaFaixa').val().replace(/[^\d]+/g, '');
    var Nome = $('#mainContent_txtNomeeditMudancaFaixa').val();
    var CRM = $('#mainContent_txtCRMeditMudancaFaixa').val() == '' ? 0 : $('#mainContent_txtCRMeditMudancaFaixa').val();
    var Produto = $('#mainContent_cmbProdutoeditMudancaFaixa').val() == '' ? 0 : $('#mainContent_cmbProdutoeditMudancaFaixa').val();
    var DataInicio = $('#mainContent_txtDataInicioeditMudancaFaixa').val();
    var Datafim = $('#mainContent_txtDataFimeditMudancaFaixa').val();

    var camposValidos = validarCamposMFaixa(CPF, Nome, CRM, Produto, DataInicio, Datafim);
    if (DataInicio !== '') {
        if (DataInicio.length > 0) {
            DataInicio = DataInicio.split('/');
            DataInicio = DataInicio[1] + '-' + DataInicio[0] + '-' + DataInicio[2];
        }
    }
    if (Datafim !== '') {
        if (Datafim.length > 0) {
            Datafim = Datafim.split('/');
            Datafim = Datafim[1] + '-' + Datafim[0] + '-' + Datafim[2];
        }
    }

    if (camposValidos) {
        $('#mdLoader').modal({
            backdrop: 'static',
            keyboard: false
        });
        $('#mdLoader').modal('show');

        $.ajax({
            method: "POST",
            url: "ConsultaFaturamento.aspx/ListarMFSeguradosPorParams",
            data: '{DtAniversarioInicial: "' + DataInicio + '", DtAniversarioFinal: "' + Datafim + '", IdPlano: "' + Produto + '", Crm: "' + CRM + '", Cpf: "' + CPF + '", Nome: "' + Nome + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            var objPagamentosList = [];
            retorno.d.forEach(function (item, index) {
                if (item.MsgErro) {
                    alert(item.MsgErro);
                    return;
                }
                var mf_segurado = new class {
                    constructor() {
                        this.Id;
                        this.TipoSegurado;
                        this.Nome;
                        this.Cpf;
                        this.VlPremioAnterior;
                        this.VlPremio;
                        this.Plano;
                        this.DtAniversario;
                        this.Idade;
                        this.htmlButton;
                    }
                }

                mf_segurado.Id = item.Id;
                mf_segurado.Nome = item.Nome;
                mf_segurado.TipoSegurado = item.Parentesco == 'T' ? 'Titular' : 'Dependente';
                mf_segurado.VlPremio = item.PremioAtual;
                mf_segurado.VlPremioAnterior = item.PremioAnterior;
                if (!isNullOrEmpty(item.CPF))
                    mf_segurado.Cpf = MascaraCPFValue(item.CPF);
                mf_segurado.Idade = item.Idade;
                mf_segurado.Plano = item.Plano;
                mf_segurado.DtAniversario = item.Aniversario;

                if (mf_segurado.TipoSegurado == 'Titular')
                    if (mf_segurado.VlPremioAnterior != null)
                        mf_segurado.htmlButton = '<i class="fa fa-user fa-1x" style="cursor: pointer; color: #ff7e2b" data-toggle="tooltip" data-placement="top" title="Detalhar"></i>';
                    else
                        mf_segurado.htmlButton = '<i class="fa fa-user" style="cursor: pointer" data-toggle="tooltip" data-placement="top" title="Detalhar"></i>';

                else
                    mf_segurado.htmlButton = '<i class="fa fa-users" style="cursor: pointer; color: #ff7e2b" data-toggle="tooltip" data-placement="top" title="Detalhar"></i>';

                objPagamentosList.push(mf_segurado);
            });

            var qntResult = objPagamentosList.length;
            $('#lblTotalResultado').text(qntResult);

            //iniciarPaginacaoCustom(objPagamentosList);
            iniciarPaginacaoCustom(objPagamentosList, 20);
            exibirPaginacaoCustom(1, 20);


            //exibirPaginacao(1);
            //var qntResult = retorno.d.length;
            $('#mdLoader').modal('hide');
            $('#dvResultadoMudancaFaixa').css('display', 'block');
        });
    }
}


function validarCamposMFaixa(CPF, Nome, CRM, Produto, DataInicio, Datafim) {
    if (CPF == '' && Nome == '' && CRM == '' && isNullOrEmpty(Produto) && DataInicio == '' && Datafim == '') {
        $('#dvResultadoMudancaFaixa').css('display', 'none');
        $('#SPRetornoErroMF').show();
        return false;
    }

    else if (CPF !== '') {
        var retorno = verificarCPF(CPF.replace('.', '').replace('.', '').replace('-', ''));
        if (retorno == false) {
            $('#dvResultadoMudancaFaixa').css('display', 'none');
            $('#SPtxtCPF').show();
        }
    }


    $('#SPRetornoErroMF').hide();
    return true;
}


function validaData(stringData) {
    /******** VALIDA DATA NO FORMATO DD/MM/AAAA *******/

    stringData = stringData.replace("-", "/").replace("-", "/");
    var regExpCaracter = /[^\d]/;     //Expressão regular para procurar caracter não-numérico.
    var regExpEspaco = /^\s+|\s+$/g;  //Expressão regular para retirar espaços em branco.


    splitData = stringData.split('/');

    if (splitData.length != 3) {
        $('#SPRetornoErro').text('Data fora do padrão DD/MM/AAAA');
        $('#SPRetornoErro').show();
        return false;
    }

    /* Retira os espaços em branco do início e fim de cada string. */
    splitData[0] = splitData[0].replace(regExpEspaco, '');
    splitData[1] = splitData[1].replace(regExpEspaco, '');
    splitData[2] = splitData[2].replace(regExpEspaco, '');

    if ((splitData[0].length != 2) || (splitData[1].length != 2) || (splitData[2].length != 4)) {
        $('#SPRetornoErro').text('Data fora do padrão DD/MM/AAAA');
        $('#SPRetornoErro').show();
        return false;
    }

    /* Procura por caracter não-numérico. EX.: o "x" em "28/09/2x11" */
    if (regExpCaracter.test(splitData[0]) || regExpCaracter.test(splitData[1]) || regExpCaracter.test(splitData[2])) {
        $('#SPRetornoErro').text('Data com caractere invalido encontrado');
        $('#SPRetornoErro').show();
        return false;
    }

    dia = parseInt(splitData[0], 10);
    mes = parseInt(splitData[1], 10) - 1; //O JavaScript representa o mês de 0 a 11 (0->janeiro, 1->fevereiro... 11->dezembro)
    ano = parseInt(splitData[2], 10);

    var novaData = new Date(ano, mes, dia);

    if ((novaData.getDate() != dia) || (novaData.getMonth() != mes) || (novaData.getFullYear() != ano)) {
        $('#SPRetornoErro').text('Data Invalida - Preencha com uma data valida');
        $('#SPRetornoErro').show();
        return false;
    }
    else {
        return true;
    }
}
// #endregion
// ######################################################################### ConsultaLog.aspx #############################################################################################
//#region ConsultaLogPage
CarregarComboAcoes();
CarregarComboUsuarios();
PreenchimentoCamposPeríodo();

// ################# LISTENERS ###########################################################################################################################################################
$('#btnConsultarLog').click(function () {
    ConsultarLogs();
});

// ################# FUNÇÕES #############################################################################################################################################################
function ConsultarLogs() {
    //VerificaSession();
    $('#SPRetornoErro').hide();

    $('#mdLoader').modal({
        backdrop: 'static',
        keyboard: false
    });
    $('#mdLoader').modal('show');

    var IdUsuario = $('#cmbUsuario').val();
    var IdAcao = $('#cmbAcoes').val();
    var PInicio = $('#txtPeriodoInicio').val();
    var PFim = $('#txtPeriodoFim').val();

    if (PInicio.length > 0) {
        PInicio = PInicio.split('/');
        PInicio = PInicio[1] + '-' + PInicio[0] + '-' + PInicio[2];
    }

    if (PFim.length > 0) {
        PFim = PFim.split('/');
        PFim = PFim[1] + '-' + PFim[0] + '-' + PFim[2];
    }


    // Verifica DataInicial e Final. Final n pode ser menor.
    let hoje = new Date();
    let auxPInicio = new Date(PInicio);
    let auxFim = new Date(PFim);

    if (auxPInicio > hoje || auxFim > hoje) {
        $('#SPtxtPeriodoInicio').text('Data deve ser igual ou retroativa à data de hoje!');
        $('#SPtxtPeriodoInicio').show();
        $('#mdLoader').modal('hide');
        return;
    }
    else
        $('#SPtxtPeriodoInicio').hide();

    // Verifica se período é maior q 30 dias.    
    var timeDiff = Math.abs(auxFim.getTime() - auxPInicio.getTime());
    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
    //console.log('dias: ' + diffDays);
    if (diffDays > 30) {
        $('#SPtxtPeriodoInicio').text('Informar período máximo de trinta dias entre as datas!');
        $('#SPtxtPeriodoInicio').show();
        $('#mdLoader').modal('hide');
        return;
    } else
        $('#SPtxtPeriodoInicio').hide();


    $.ajax({
        method: "POST",
        url: "ConsultaLog.aspx/ListarLogPorParams",
        data: '{IdUsuario: "' + IdUsuario + '", IdAcao: "' + IdAcao + '", PInicio: "' + PInicio + '", PFim: "' + PFim + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //Convertendo obj JSON para VW JSON paginação
        //console.log(retorno.d);
        $('#mdLoader').modal('hide');
        var objLogList = [];
        retorno.d.forEach(function (item) {
            if (item.MsgErro) {
                alert(item.MsgErro);
                return;
            }
            var Log = new class LogVW {
                constructor() {
                    this.Id;
                    this.Nome;
                    this.Departamento;
                    this.ActLog;
                    this.NomeSegurado;
                    this.Data;
                }
            }
            Log.Id = item.Id;
            Log.Nome = item.Nome;
            Log.Departamento = item.Departamento;
            Log.ActLog = item.ActLog;
            Log.NomeSegurado = item.NomeSegurado;
            Log.Data = item.Data;


            //o atributo htmlButton deverá ter esse msm nome no objeto js da página, e receber o html do botão..                                   
            Log.htmlButton = '';
            objLogList.push(Log);
        });


        //iniciarPaginacao(objLogList);
        iniciarPaginacaoCustom(objLogList, 20);
        //exibirPaginacao(1);
        exibirPaginacaoCustom(1, 20);
        var qntResult = objLogList.length;
        //$('#lblTotalResultado').text(qntResult);
        $('#lblTotalResultado').text(qntResult);

        $('#mdLoader').modal('hide');
    });
    $('#dvResultadoSegurado').css('display', 'block');

}
function CarregarComboAcoes() {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)


    if (url.includes('ConsultaLog.aspx')) {
        $.ajax({
            method: "POST",
            url: "ConsultaLog.aspx/ListarAcoesLogAsync",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);
            if (retorno.d != null)
                if (retorno.d.length > 0) {
                    let options = '';
                    retorno.d.forEach(function (value) {
                        options += `<option value=${value.Id}>${value.Nome}</option>`;
                    });
                    let cmbAcao = `
                                <select id="cmbAcoes" class="dropdown-material">
                                    <option value="0">-- Selecione --</option>
                                    ${options}
                                </select>
                                `;
                    $('#dv-cmb-acoes').html(cmbAcao);
                }
        });
    }
}
function CarregarComboUsuarios() {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)

    if (url.includes('ConsultaLog.aspx')) {
        $.ajax({
            method: "POST",
            url: "ConsultaLog.aspx/ListarUsuariosAsync",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);
            if (retorno.d != null)
                if (retorno.d.length > 0) {
                    let options = '';
                    retorno.d.forEach(function (value) {
                        options += `<option value=${value.IdUsuario}>${value.Nome}</option>`;
                    });
                    let cmbUsuario = `
                                <select id="cmbUsuario" class="dropdown-material">
                                    <option value="0">-- Selecione --</option>
                                    ${options}
                                </select>
                                `;

                    $('#dv-cmb-usuario').html(cmbUsuario);
                }
        });
    }
}
function PreenchimentoCamposPeríodo() {
    //$('#txtPeriodoFim').val();
    // Use of Date.now() function 
    //var d = Date(Date.now());
    var dFim = DateNow();


    var today = new Date();
    var dInicial = new Date();
    dInicial.setDate(today.getDate() - 30);      // 30 dias pra trás
    dInicial = DateToString(dInicial);

    $('#txtPeriodoInicio').val(dInicial);
    $('#txtPeriodoFim').val(dFim);

}
function DateToString(date) {
    mydate = new Date(date);
    var dd = mydate.getDate();
    var mm = mydate.getMonth() + 1; //January is 0!

    var yyyy = mydate.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    var today = dd + '/' + mm + '/' + yyyy;
    return today;
}
function DateNow() {
    //var today = new Date();    
    mydate = new Date();
    var dd = mydate.getDate();
    var mm = mydate.getMonth() + 1; //January is 0!

    var yyyy = mydate.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    var today = dd + '/' + mm + '/' + yyyy;
    return today;
}

// ######################################################################### aprovaUsuario.aspx ################################################################################################
//carregaListaUsuario('', 'A');
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


$(document).on('click', "#btnConsultar", function () {
    $('#dvResultadoConsulta').css('display', 'block');
    $('#dvListaUsrPendentes').show();
});

$('#btnLimpar').click(function () {
    $('#txtCPFCNPJ').val('');
    $('#txtNomeSeg').val('');
    $('#cmbStatus').options[0];
});


// Esconde o td
$("td[colspan=12]").hide();

$("#tdUsuario").click(function (e) {
    e.stopPropagation();
    var $target = $(e.target);
    // Seleciona o td mais proximo do atual e o expande
    if ($target.closest("td").attr("colspan") > 1) {
        $target.slideUp();
    } else {
        $target.closest("tr").next().find("td").slideToggle();
    }
});

function carregaListaUsuario() {
    $('#mdLoader').modal('show');

    //Envia parametros para serem usados na proc caso o usuário queira usar os filtros.
    let nmUsuario = $('#mainContent_txtNomeUsuario').val();
    let cmbStatus = $('#mainContent_cmbStatusAprov').val();

    if (nmUsuario.length == 0 && cmbStatus.length == 0)
        return;

    $.ajax({
        method: "POST",
        url: "AprovaUsuario.aspx/ListarUsuariosPendentes",
        data: '{NomeUsuario: "' + nmUsuario + '", Status: "' + cmbStatus + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //Convertendo obj JSON para VW JSON paginação
        //console.log(retorno.d);
        $('#mdLoader').modal('hide');
        objUsuariosPendentes = [];
        retorno.d.forEach(function (item, index) {
            if (item.MsgErro) {
                alert(item.MsgErro);
                return;
            }
            var Usuario = new UsuarioVW();
            Usuario.IdUsuario = item.IdUsuario;
            Usuario.Nome = (PrimeiraLetraMaiuscula(item.Nome) || '');
            Usuario.Cpf = (MascaraCPFValue(item.Cpf) || "");
            Usuario.Email = item.Email;
            Usuario.NvlAcesso = item.NvlAcesso;
            Usuario.DataCadastro = item.DataCadastro;
            Usuario.Aprovado = item.Aprovado;
            //o atributo htmlButton deverá ter esse msm nome no objeto js da página, e receber o html do botão.. 
            if (Usuario.Aprovado == "A") {
                Usuario.htmlButton = '<img src="../ContentAdm/img/aprovado_on.png" style="width:25px;high:25px;cursor: pointer" id="btnAprov' + Usuario.IdUsuario + '" onclick="aprovaUsuario(' + Usuario.IdUsuario + ')" />&nbsp;&nbsp;<img src="../ContentAdm/img/reprovado_off.png" id="btnAprov' + Usuario.IdUsuario + '" style="width:25px;high:25px;cursor: pointer" onclick="reprovaUsuario(' + Usuario.IdUsuario + ')" />';
            } else if (Usuario.Aprovado == "R") {
                Usuario.htmlButton = '<img src="../ContentAdm/img/aprovado_off.png" style="width:25px;high:25px;cursor: pointer" id="btnAprov' + Usuario.IdUsuario + '" onclick="aprovaUsuario(' + Usuario.IdUsuario + ')" />&nbsp;&nbsp;<img src="../ContentAdm/img/reprovado_on.png" id="btnAprov' + Usuario.IdUsuario + '" style="width:25px;high:25px;cursor: pointer" onclick="reprovaUsuario(' + Usuario.IdUsuario + ')" />';
            } else {
                Usuario.htmlButton = '<img src="../ContentAdm/img/aprovado_off.png" style="width:25px;high:25px;cursor: pointer" id="btnAprov' + Usuario.IdUsuario + '" onclick="aprovaUsuario(' + Usuario.IdUsuario + ')" />&nbsp;&nbsp;<img src="../ContentAdm/img/reprovado_off.png" id="btnAprov' + Usuario.IdUsuario + '" style="width:25px;high:25px;cursor: pointer" onclick="reprovaUsuario(' + Usuario.IdUsuario + ')" />';
            }

            //o atributo htmlPerfil deverá ter esse msm nome no objeto js da página, e receber o html dos ícones do perfil. 
            let perfilComercialAtivo = '';
            let perfilFinanceiroAtivo = '';
            let perfilAdministrativoAtivo = '';
            let ativaMetodoComercial = `alteraPerfil('C', ${Usuario.IdUsuario})`;
            let ativaMetodoFinanceiro = `alteraPerfil('F', ${Usuario.IdUsuario})`;
            let ativaMetodoAdm = `alteraPerfil('A', ${Usuario.IdUsuario})`;
            if (Usuario.NvlAcesso == 1) {
                ativaMetodoComercial = '';
                perfilComercialAtivo = 'ic-active';
            }                
            if (Usuario.NvlAcesso == 2) {
                ativaMetodoFinanceiro = '';
                perfilFinanceiroAtivo = 'ic-active';
            }
                
            if (Usuario.NvlAcesso == 3) {
                ativaMetodoAdm = '';
                perfilAdministrativoAtivo = 'ic-active';
            }                
            Usuario.htmlPerfil = `
                <span data-tooltip="Comercial" data-tooltip-position="right" class="ic-perfil ${perfilComercialAtivo}" onclick="${ativaMetodoComercial}">C</span>
                <span data-tooltip="Financeiro" data-tooltip-position="right" class="ic-perfil ${perfilFinanceiroAtivo}" onclick="${ativaMetodoFinanceiro}">F</span>
                <span data-tooltip="Administrativo" data-tooltip-position="right" class="ic-perfil ${perfilAdministrativoAtivo}" onclick="${ativaMetodoAdm}">A</span>
            `;                
            

            objUsuariosPendentes.push(Usuario);
        });

        // Paginação especial para mostrar os ícones de perfil
        iniciarPaginacaoAprovacaoUsuario(objUsuariosPendentes, 20);
        exibirPaginacaoCustom(1, 20);

    });
    $('#dvResultadoCotacao').css('display', 'block');
}

function alteraPerfil(tpPerfil, idUsuario) {
    $.ajax({
        method: "POST",
        data: '{TpAcesso: "' + tpPerfil + '", IdUsuario: ' + idUsuario + ' }',
        url: "AprovaUsuario.aspx/AlteraPerfilUsuarioAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.includes('erro')) {
                alert('erro durante a atualização do perfil.');
                console.log(retorno.d);                
                return;
            }
        carregaListaUsuario();
    });
}

function iniciarPaginacaoAprovacaoUsuario(obj, qtdeRegsPorPagina) {
    if (listaInicialHtml == '')
        listaInicialHtml = $('.tbody').html();

    $('.tbody').html(listaInicialHtml);

    var contAux = 1;
    contAux2 = qtdeRegsPorPagina;
    var listaHtml = '';

    obj.forEach(function (item, index) {
        //Controle da paginação
        if ((index + 1) > contAux2) {
            contAux++;
            contAux2 = contAux2 + qtdeRegsPorPagina;
        }
        contAux = index + 1 > contAux2 ? contAux++ : contAux;

        //Limpa pesquisa antes de popular a tabela.
        //Seta html inicial da página para poder reconsultar.        
        $('.tbody').find('tr').remove();
        $('.tbody').append(listaInicialHtml);

        $('.tbody').find('tr').removeClass();
        $('.tbody').find('tr').addClass('clsPag' + contAux);


        //loop das colunas da tabela
        for (var i = 0; i < $('.tbody').find('tr').children().length; i++) {
            var classe = $('.tbody').find('td:eq(' + i + ')').attr('class');
            var elemTemp = $('.tbody').find('.' + classe).html();
            $('.tbody').find('td:eq(' + i + ')').text(item[classe]);    //A classe tem/deverá ter o msm nome do atributo do objeto obj (parâmet)            
            if (classe == undefined)
                break;
            if (classe.includes('Acao')) {                //Se na paginação tiver classe Acao: ex: botão excluir / incluir / editar..... inclui propriedade do botão.
                $('.tbody').find('td:eq(' + i + ')').html(item.htmlButton);    //o atributo htmlButton deverá ter esse msm nome no objeto js da página, e receber o html do botão..                
                $('.tbody').find('td:eq(' + i + ')').addClass('acao' + item.IdUsuario);
            }
            if (classe.includes('Perfil')) {                //Se na paginação tiver classe Acao: ex: botão excluir / incluir / editar..... inclui propriedade do botão.
                $('.tbody').find('td:eq(' + i + ')').html(item.htmlPerfil);    //o atributo htmlButton deverá ter esse msm nome no objeto js da página, e receber o html do botão..                
                $('.tbody').find('td:eq(' + i + ')').addClass('perfil' + item.IdUsuario);
            }
            
        }
        listaHtml += $('.tbody').html();
    });

    if (obj.length == 0)
        $('.btns-nav').html('Nenhum registro encontrado');
    else
        $('.btns-nav').html('<div class="row"><button type="button" onclick="voltarPag()" id="btnVoltar" class="page-link">&laquo; Anterior</button><ul class="pagination pag-1"></ul><button type="button" onclick="avancarPag()" id="btnProximo" class="page-link">Próximo &raquo;</button></div>');

    $('.tbody').html(listaHtml);
    exibirPaginacao(pagAtual);
}

function aprovaUsuario(idUsuario) {
    $('.acao' + idUsuario).html("<div class='image-wrapper'></div>");
    $.ajax({
        method: "POST",
        data: '{IdUsuario: "' + idUsuario + '" }',
        url: "AprovaUsuario.aspx/AprovarUsuarioAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d.includes('Erro')) {
            alert('Erro na tentativa de aprovar o usuário: ' + retorno.d);
            //console.log(retorno.d);
            return;
        }

        $('.acao' + idUsuario).html('<img src="../ContentAdm/img/aprovado_on.png" style="width:25px;high:25px;cursor: pointer" id="btnAprov' + idUsuario + '" />&nbsp;&nbsp;<img src="../ContentAdm/img/reprovado_off.png" id="btnAprov' + idUsuario + '" style="width:25px;high:25px;cursor: pointer" onclick="reprovaUsuario( ' + idUsuario + ')" />');
    });
}
function reprovaUsuario(idUsuario) {
    $('.acao' + idUsuario).html("<div class='image-wrapper'></div>");

    $.ajax({
        method: "POST",
        data: '{IdUsuario: "' + idUsuario + '" }',
        url: "AprovaUsuario.aspx/ReprovarUsuarioAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d.includes('Erro')) {
            alert('Erro na tentativa de reprovar o usuário: ' + retorno.d);
            //console.log(retorno.d);
            return;
        }
        $('.acao' + idUsuario).html('<img src="../ContentAdm/img/aprovado_off.png" style="width:25px;high:25px;cursor: pointer" onclick="aprovaUsuario( ' + idUsuario + ')" id="btnAprov' + idUsuario + '" />&nbsp;&nbsp;<img src="../ContentAdm/img/reprovado_on.png" id="btnAprov' + idUsuario + '" style="width:25px;high:25px;cursor: pointer" />');
    });
}
//#endregion
// ######################################################################### ConsultaLeads.aspx ###########################################################################################
// #region ConsultaLeadsPage
$('#btnConsultarLeads').click(function () {
    ConsultarLeads();
});


function ConsultarLeads() {
    //VerificaSession();
    $('#SPRetornoErro').hide();

    $('#mdLoader').modal({
        backdrop: 'static',
        keyboard: false
    });
    $('#mdLoader').modal('show');

    var PInicio = $('#txtPeriodoInicio-lead').val();
    var PFim = $('#txtPeriodoFim-lead').val();

    //Preenchimento do período obrigatório
    if (PInicio == null || PFim == null) {
        $('#SPtxtPeriodoInicio-leads').text('Favor preencher os campos Período Inicial e o Período Final para continuar!');
        $('#SPtxtPeriodoInicio-leads').show();
        $('#mdLoader').modal('hide');
        return;
    }
    if (PFim != null)
        if (PFim.length == 0) {
            $('#SPtxtPeriodoInicio-leads').text('Favor preencher os campos Período Inicial e o Período Final para continuar!');
            $('#SPtxtPeriodoInicio-leads').show();
            $('#mdLoader').modal('hide');
            return;
        }
    if (PInicio != null)
        if (PInicio.length == 0) {
            $('#SPtxtPeriodoInicio-leads').text('Favor preencher os campos Período Inicial e o Período Final para continuar!');
            $('#SPtxtPeriodoInicio-leads').show();
            $('#mdLoader').modal('hide');
            return;
        }

    $('#SPtxtPeriodoInicio-leads').hide();

    if (PInicio.length > 0) {
        PInicio = PInicio.split('/');
        PInicio = PInicio[1] + '-' + PInicio[0] + '-' + PInicio[2];
    }


    if (PFim.length > 0) {
        PFim = PFim.split('/');
        PFim = PFim[1] + '-' + PFim[0] + '-' + PFim[2];
    }


    // Verifica DataInicial e Final. Final n pode ser menor.
    let hoje = new Date();
    let auxPInicio = new Date(PInicio);
    let auxFim = new Date(PFim);

    if (auxPInicio > hoje || auxFim > hoje) {
        $('#SPtxtPeriodoInicio-leads').text('Data deve ser igual ou retroativa à data de hoje!');
        $('#SPtxtPeriodoInicio-leads').show();
        $('#mdLoader').modal('hide');
        return;
    }
    else
        $('#SPtxtPeriodoInicio-leads').hide();

    $.ajax({
        method: "POST",
        url: "ConsultaLead.aspx/ListarLeadsPorParams",
        data: '{PInicio: "' + PInicio + '", PFim: "' + PFim + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //Convertendo obj JSON para VW JSON paginação
        //console.log(retorno.d);
        $('#mdLoader').modal('hide');
        var objLeadList = [];
        retorno.d.forEach(function (item) {
            if (item.MsgErro) {
                alert(item.MsgErro);
                return;
            }
            var Lead = new class {
                constructor() {
                    this.IdLead;
                    this.Nome;
                    this.CRM;
                    this.Celular;
                    this.Email;
                    this.DtCadastro;
                }
            }
            Lead.IdLead = item.IdLead;
            Lead.Nome = item.Nome;
            Lead.CRM = item.CRM;
            if (item.Celular != null)
                if (item.Celular.length > 0)
                    Lead.Celular = MascaraCelularValue(item.Celular);
            Lead.Email = item.Email;
            Lead.DtCadastro = item.DtCadastro;


            //o atributo htmlButton deverá ter esse msm nome no objeto js da página, e receber o html do botão..                                   
            Lead.htmlButton = '';
            objLeadList.push(Lead);
        });

        iniciarPaginacaoCustom(objLeadList, 20);
        exibirPaginacaoCustom(1, 20);
        var qntResult = objLeadList.length;
        $('#lblTotalResultado').text(qntResult);
        $('#mdLoader').modal('hide');

        $('#dvResultadoSegurado').css('display', 'block');
    });
}


//******************** Classes JSON (View Model) *************************************
var Profissoes = new class {
    constructor() {
        this.Id;
        this.Nome;
    }
}


class SeguradoVW {
    constructor() {
        this.Status;
        this.Dependentes = new Array();
    }
}
var SeguradoDeps = new SeguradoVW();

class UsuarioVW {
    constructor() {
        this.IdUsuario;
        this.Nome;
        this.Cpf;
        this.Email;
        this.NvlAcesso;
        this.DataCadastro;
        this.Aprovado;
        this.htmlButton;
    }
}
// #endregion
// ######################################################################### ConsultaFaturamento.aspx #####################################################################################
// #region ConsultaFaturamentoPage
$('#btnConsultarFaturamento').click(function () {
    ConsultarFaturamento();
});

$('#btnLimparFaturamento').click(function () {
    $('#mainContent_txtCPFeditFaturamento').val('');
    $('#mainContent_txtNomeeditFaturamento').val('');
    $('#mainContent_txteditCarteirinhaFaturamento').val('');
    $('#mainContent_txtCRMeditFaturamento').val('');
    $('#mainContent_txteditCarteirinha').val('');
    $('#mainContent_cmbProdutoeditFaturamento').val('');
    $('#mainContent_cmbStatuseditFaturamento').val('');
    //$('#mainContent_cmbProdutoeditFaturamentoStatus').val('');
    $('#mainContent_txtDataInicioeditFaturamento').val('');
    $('#mainContent_txtDataFimeditFaturamento').val('');
    $('#SPtxtCPFFaturamento').css('display', 'none');
    $('#SPtxtDataInicioFaturamento').css('display', 'none');
    $('#SPtxtDataFimFaturamento').css('display', 'none');
    $('#SPRetornoErroFaturamento').css('display', 'none');
});

function ConsultarFaturamento() {
    $('#SPRetornoErroFaturamento').hide();

    var CPF = $('#mainContent_txtCPFeditFaturamento').val().replace(/[^\d]+/g, '');
    var Nome = $('#mainContent_txtNomeeditFaturamento').val();
    var CRM = $('#mainContent_txtCRMeditFaturamento').val() == '' ? 0 : $('#mainContent_txtCRMeditFaturamento').val();
    var Carteirinha = '';
    if ($('#mainContent_txteditCarteirinha').val() != '')
        Carteirinha = $('#mainContent_txteditCarteirinha').val().replace(/[^\d]+/g, '');
    var Produto = $('#mainContent_cmbProdutoeditFaturamento').val();
    var Status = $('#mainContent_cmbProdutoeditFaturamentoStatus').val() == '' ? 0 : $('#mainContent_cmbProdutoeditFaturamentoStatus').val();
    var DataInicio = $('#mainContent_txtDataInicioeditFaturamento').val();
    var Datafim = $('#mainContent_txtDataFimeditFaturamento').val();

    var camposValidos = validarCamposFaturamento(CPF, Nome, CRM, Produto, Carteirinha, DataInicio, Datafim);
    if (DataInicio !== '') {
        if (DataInicio.length > 0) {
            DataInicio = DataInicio.split('/');
            DataInicio = DataInicio[1] + '-' + DataInicio[0] + '-' + DataInicio[2];
        }
    }
    if (Datafim !== '') {
        if (Datafim.length > 0) {
            Datafim = Datafim.split('/');
            Datafim = Datafim[1] + '-' + Datafim[0] + '-' + Datafim[2];
        }
    }

    if (camposValidos) {
        $('#mdLoader').modal({
            backdrop: 'static',
            keyboard: false
        });
        $('#mdLoader').modal('show');

        $.ajax({
            method: "POST",
            url: "ConsultaFaturamento.aspx/ListarFaturamentoPorParams",
            data: '{DtInicial: "' + DataInicio + '", DtFinal: "' + Datafim + '", NrCarteira: "' + Carteirinha + '", IdPlano: "' + Produto + '", Crm: "' + CRM + '", Cpf: "' + CPF + '", Nome: "' + Nome + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            var objFaturamentoList = [];
            retorno.d.forEach(function (item, index) {
                if (item.MsgErro) {
                    alert(item.MsgErro);
                    return;
                }
                var _faturamento = new class {
                    constructor() {
                        this.Id;
                        this.NomeSegurado;
                        this.Produto;
                        this.Plano;
                        this.DtVencimento;
                        this.NrDependentes;
                        this.DtPagamento;
                        this.VlPremio;
                        this.FPagamento;
                        this.htmlButton;
                    }
                }
                _faturamento.Id = item.Id;
                _faturamento.NomeSegurado = item.NomeSegurado;
                _faturamento.Produto = item.Produto;
                _faturamento.Plano = item.Plano;
                _faturamento.DtVencimento = item.DtVencimento;
                _faturamento.NrDependentes = item.NrDependentes;
                _faturamento.DtPagamento = item.DtPagamento;
                _faturamento.VlPremio = item.VlPremio;
                _faturamento.FPagamento = item.FPagamento;

                _faturamento.htmlButton = '<i class="fa fa-users fa-2x" style="cursor: pointer" data-toggle="tooltip" data-placement="top" title="Detalhar"></i>';
                objFaturamentoList.push(_faturamento);
            });

            var qntResult = objFaturamentoList.length;
            $('#lblTotalResultado').text(qntResult);

            iniciarPaginacaoCustom(objFaturamentoList, 20);
            exibirPaginacaoCustom(1, 20);

            $('#mdLoader').modal('hide');
            $('#dvResultadoFaturamento').css('display', 'block');
        });
    }
}

function validarCamposFaturamento(CPF, Nome, CRM, Produto, Carteirinha, DataInicio, Datafim) {
    if (CPF == '' && Nome == '' && CRM == 0 && isNullOrEmpty(Produto) && isNullOrEmpty(Carteirinha) && DataInicio == '' && Datafim == '') {
        $('#dvResultadoFaturamento').css('display', 'none');
        $('#SPRetornoErroFaturamento').show();
        return false;
    }

    else if (CPF !== '') {
        var retorno = verificarCPF(CPF.replace('.', '').replace('.', '').replace('-', ''));
        if (retorno == false) {
            $('#dvResultadoFaturamento').css('display', 'none');
            $('#SPtxtCPFFaturamento').show();
        }
        return true;
    }
    return true;
}




$('#dv-aba-fat').click(function () {
    //onClickFat();
    var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Faturamento/ConsultaFaturamento.aspx?aba=1';
    window.location.replace(urlLogin);
});
$('#dv-aba-md-faixa').click(function () {
    //onClickMF();
    var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Faturamento/ConsultaFaturamento.aspx?aba=2';
    window.location.replace(urlLogin);
});

function onClickFat() {
    // Este procedimento é necessário, pois existem 2 tabelas c as classes .tbody, logo só poderia existir uma.
    $('#tblMFaixa').html('');
    $('#tblFaturamento').html('');
    $('#tblFaturamento').html(`
            <table class="table table-hover">
                <tr>   
					<td align="left"><b>Titular</b></td>	                 
					<td align="center"><b>Produto</b></td>
                    <td align="center"><b>Plano</b></td>
                    <td align="center"><b>Vencimento</b></td>
                    <td align="center"><b>Data de Pagamento</b></td>
                    <td align="center"><b>Prêmio</b></td>
                    <td align="center"><b>Nr. de dependentes</b></td>
                    <td align="center"><b>Forma de Pagamento</b></td>
                </tr>
                <tbody class="tbody">
                    <tr>                        
                        <td class="NomeSegurado" align="left"></td>
                        <td class="Produto" align="center"></td>
                        <td class="Plano" align="center"></td>
                        <td class="DtVencimento" align="center"></td>
                         <td class="DtPagamento" align="center"></td>
                        <td class="VlPremio" align="center"></td>
                        <td class="NrDependentes" align="center"></td>
                        <td class="FPagamento" align="center"></td>
                    </tr>
                </tbody>
            </table>
            <div class="col-md-12 btns-nav" style="margin: 0;">
                <div class="row">
                    <button type="button" onclick="voltarPag()" id="btnVoltar" class="page-link">&laquo; Anterior</button>
                    <ul class="pagination pag-1"></ul>
                    <ul class="pagination pag-2"></ul>
                    <button type="button" onclick="avancarPag()" id="btnProximo" class="page-link">Próximo &raquo;</button>
                </div>
            </div>
`);

    ShowFaturamento();
}

function onClickMF() {
    ShowPagamentoMFaixa();
    $('#tblFaturamento').html('');

    $('#tblMFaixa').html('');

    $('#tblMFaixa').html(`
    <table class="table table-hover">
        <tr>
            <td align="left"><b></b></td>
            <td align="left"><b>Nome</b></td>
            <td align="center"><b>CPF</b></td>
            <td align="center"><b>Plano</b></td>
            <td align="center"><b>Prêmio Anterior</b></td>
            <td align="center"><b>Prêmio Atual</b></td>
            <td align="center"><b>Aniversário</b></td>
            <td align="center"><b>Idade</b></td>
        </tr>
        <tbody class="tbody">
            <tr>
                <td class="Acao"></td>
                <td class="Nome" align="left"></td>
                <td class="Cpf" align="center"></td>
                <td class="Plano" align="center"></td>
                <td class="VlPremioAnterior" align="center"></td>
                <td class="VlPremio" align="center"></td>
                <td class="DtAniversario" align="center"></td>
                <td class="Idade" align="center"></td>   
            </tr>                                        
        </tbody>
    </table>
    <div class="col-md-12 btns-nav" style="margin: 0;">
        <div class="row">
            <button type="button" onclick="voltarPag()" id="btnVoltar" class="page-link">&laquo; Anterior</button>
            <ul class="pagination pag-1"></ul>
            <ul class="pagination pag-2"></ul>
            <button type="button" onclick="avancarPag()" id="btnProximo" class="page-link">Próximo &raquo;</button>
        </div>
    </div>
`); 
}

function ShowFaturamento() {

    $('#dv-aba-fat').addClass('active');
    $('#dv-aba-md-faixa').removeClass('active');

    $('#dv-content-md-faixa').hide();
    $('#dv-content-fat').show();
    $('#dvResultadoFaturamento').css('display', 'block');
    $('#dvResultadoMudancaFaixa').css('display', 'none');
}
function ShowPagamentoMFaixa() {
    $('#dv-aba-fat').removeClass('active');
    $('#dv-aba-md-faixa').addClass('active');

    $('#dv-content-md-faixa').show();
    $('#dv-content-fat').hide();
    $('#dvResultadoFaturamento').css('display', 'none');
    $('#dvResultadoMudancaFaixa').css('display', 'block');
}
// #endregion
// ######################################################################### ConfiguraBoleto.aspx #########################################################################################
// #region ConfiguraBoletoPage
$(document).ready(function () {

    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)

    // Pré-carregamentos da página
    if (url.includes('ConfiguraBoleto.aspx')) {
        CarregaArquivosConfigurados();
        CarregaComboArquivosAGerarBoleto();
        CarregaComboMesReferenciaBoleto();
        //ShowAbaGerarBoleto();
        ListarHistoricoExportacao();
    }

    $('#dv-aba-gerar-boleto, #dv-aba-config-boleto').click(function () {
        $('#SPRetornoSuccessConfigBoleto').hide();
        $('#box-msg-boleto-sucesso').hide();
    });


    $('#btnSalvarLayout').click(function () {
        $('#SPRetornoSuccessConfigBoleto').hide();
        if (ValidarSalvarLayout()) {
            SalvarLayout();
        }
    });

    $('#btnGerarBoleto').click(function () {
        $('#box-msg-boleto-sucesso').hide();
        $('#box-msg-boleto-erro').hide();
        
        GerarArquivoBoleto();
    });

    $('#mainContent_inputNomeConfiguracaoBoleto').keypress(function () {
        $('#SPRetornoErroNomeArquivo').hide();
        $('#SPRetornoSuccessConfigBoleto').hide();
    });

    $('#dv-aba-gerar-boleto').click(function () {
        CarregaComboArquivosAGerarBoleto();
        ShowAbaGerarBoleto();
    });

    $('#dv-aba-config-boleto').click(function () {
        ShowAbaConfiguracaoBoleto();
    });

});

// #region functions
var arrOrdemBoletoDisponivel = [];
var arrIdsLayoutsCarregados = [];

function ListarHistoricoExportacao() {
    $.ajax({
        method: "POST",
        url: `ConfiguraBoleto.aspx/ListarHistoricoExportacao`,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);
        let GridHistorico = [];
        retorno.d.forEach(function (value) {
            let Hist = class {
                constructor() {
                    this.DataEvento;
                    this.Usuario;
                    this.MesRef;
                    this.Status;
                }
            };

            Hist.DataEvento = value.DataEvento;
            Hist.Usuario = value.Usuario;
            Hist.MesRef = value.MesRef;
            Hist.Status = value.Status;

            GridHistorico.push(Hist);
        });

        iniciarPaginacaoCustom(GridHistorico, 10);
        exibirPaginacaoCustom(1, 10);
    });
}

function LimpaGerarBoletoForm() {    
    $('#mainContent_txtNrDocumento_gerarBoleto, #mainContent_txtCpf_gerarBoleto, #mainContent_txtFiliacao_gerarBoleto').val('');
    let dtNow = new Date();
    let MonthNow = dtNow.getMonth();
    $('#mainContent_cmbPeriodo_gerarBoleto').val(MonthNow + 1);
    $('#box-msg-boleto-erro').hide();
    $('#box-msg-boleto-sucesso').hide();
    $('#dv-btn-limpar').html('<button id="btn-limpar-gerar-boleto" type="button" onclick="LimpaGerarBoletoForm()" style="float: right;" class="btn btn-primary">Limpar</button>');    
}

function ShowAbaGerarBoleto() {
    $('#dv-aba-config-boleto').removeClass('active');
    $('#dv-aba-gerar-boleto').addClass('active');

    $('#dv-content-gerar-boleto').show();
    $('#dv-content-config').hide();
    //$('#dvResultadoPagamento').css('display', 'none');
}

function CarregaComboMesReferenciaBoleto() {
    let arrMesRef = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];

    var dt = new Date();
    let CurrentMounth = dt.getMonth() + 1;


    $('#mainContent_cmbPeriodo_gerarBoleto').html('');

    arrMesRef.forEach(function (value, index) {
        if (CurrentMounth === (index + 1))
            $('#mainContent_cmbPeriodo_gerarBoleto').append(`<option value=${index + 1} selected>${value}</option>`);
        else
            $('#mainContent_cmbPeriodo_gerarBoleto').append(`<option value=${index + 1}>${value}</option>`);
    });


}

function CarregaComboArquivosAGerarBoleto() {
    $.ajax({
        method: "POST",
        url: `ConfiguraBoleto.aspx/ConsultarTemplatesConfigurados`,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);
        $('#mainContent_cmbNomeArquivo_gerarBoleto').html('');
        retorno.d.forEach(function (value) {
            if (value.Padrao)
                $('#mainContent_cmbNomeArquivo_gerarBoleto').append(`<option value='${value.IdTemplate}' selected>${value.NomeTemplate}</option>`);
            else
                $('#mainContent_cmbNomeArquivo_gerarBoleto').append(`<option value='${value.IdTemplate}'>${value.NomeTemplate}</option>`);
        });
    });
}

function ExibirLoaderGerarBoleto() {
    $('#md_gerarBoleto').modal({ backdrop: 'static', keyboard: false });
    $('#md_gerarBoleto').modal('show');
}

function GerarArquivoBoleto() {
    ExibirLoaderGerarBoleto();

    let IdTemplate = $('#mainContent_cmbNomeArquivo_gerarBoleto').val();
    let MesReferencia = $('#mainContent_cmbPeriodo_gerarBoleto').val();
    MesReferencia = isNullOrEmpty(MesReferencia) ? parseInt(MesReferencia) : MesReferencia;
    let cpf = $('#mainContent_txtCpf_gerarBoleto').val();
    let filiacao = $('#mainContent_txtFiliacao_gerarBoleto').val();

    if (!isNullOrEmpty(cpf))
        cpf = cpf.replace(/[^\d]+/g, ''); 

    let data = {
        IdTemplate,
        MesReferencia,
        cpf,
        filiacao
    }

    $.ajax({
        method: "POST",
        url: `ConfiguraBoleto.aspx/GerarArquivoBoleto`,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d.includes('erro')) {
            $('#md_gerarBoleto').modal('hide');            
            $('#sp-msg-boleto-erro').text(retorno.d);
            $('#box-msg-boleto-erro').show();
        } else {
            let nomeArquivoCliente = retorno.d.substring(0, 29);
            $('#md_gerarBoleto').modal('hide');
            $('#msg-boleto-link').html('');
            $('#msg-boleto-link').append(`Para realizar o download, <a href="../TempFiles/${retorno.d}.csv" download="${retorno.d}.csv">Clique aqui</a>`);
            $('#box-msg-boleto-sucesso').show();
        } 

        ListarHistoricoExportacao();
    });
}

function ShowAbaConfiguracaoBoleto() {
    $('#dv-aba-config-boleto').addClass('active');
    $('#dv-aba-gerar-boleto').removeClass('active');

    $('#dv-content-gerar-boleto').hide();
    $('#dv-content-config').show();
    //$('#dvResultadoPagamento').css('display', 'none');
}

function FiltrarCombosPorOrdens() {
    let OrdensSelecionadas = new Array();
    OrdensSelecionadas.push(parseInt($('#combo-1-ordem').val()));

    let NovasOrdensDisponiveis = new Array();
    NovasOrdensDisponiveis = arrOrdemBoletoDisponivel.filter(x => !OrdensSelecionadas.includes(x));
    CarregaCombosOrdem(NovasOrdensDisponiveis);
}

function CarregaCombosOrdem(novasOrdensDisponiveis = arrOrdemBoletoDisponivel) {
    let vlrComboNrDoc = $('#combo-nrDoc-ordem').val();
    $('.cmb-campo-boleto-ordem').html("");
    $('.cmb-campo-boleto-ordem').append("<option value='0'>0</option>");

    novasOrdensDisponiveis.forEach(function (value) {
        $('.cmb-campo-boleto-ordem').append("<option value='" + value + "'>" + value + "</option>");
    })

    if (vlrComboNrDoc != undefined)
        $('#combo-nrDoc-ordem').append("<option value='" + vlrComboNrDoc + "' selected>" + vlrComboNrDoc + "</option>");
}

function ValidarSalvarLayout() {
    if ($('#mainContent_inputNomeConfiguracaoBoleto').val() == '') {
        $('#SPRetornoErroNomeArquivo').show();
        return false;
    }
    else
        $('#SPRetornoErroNomeArquivo').hide();

    return true;
}

function SalvarLayout() {
    let layouts = new Array();
    // percorre os ids carregados dos campos do layout, para serem enviados para atualização.
    arrIdsLayoutsCarregados.forEach(function (valueId) {
        let ordem = $(`#combo-${valueId}-ordem`).val();
        let inputAtivo = $(`#chk-ativa-layout-${valueId}`).is(':checked');
        let nomeCampo = $(`#lbl-cmp-${valueId}`).text();
        let layout = {
            lay_id: valueId,
            lay_tpl_id: $('#hdd-id-template-selecionado').val(),
            lay_posicao_campo: ordem,
            lay_ativo: inputAtivo,
            lay_nome_campo: nomeCampo
        };

        layouts.push(layout);
    });


    let data = {
        NomeTemplate: $('#mainContent_inputNomeConfiguracaoBoleto').val(),
        Layout: layouts
    };

    $.ajax({
        method: "POST",
        url: `ConfiguraBoleto.aspx/SalvarLayoutTemplate`,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null)
            if (retorno.d.includes('erro')) {
                alert('erro durante a atualização do layout. Vide dev tools log.');
                console.log(retorno.d);
                return;
            }

        RefreshArquivosConfigurados();
        $('#SPRetornoSuccessConfigBoleto').show();
    });
}

function AtualizaDadosLayout(id) {
    $('#SPRetornoErroNomeArquivo').hide();
    $('#SPRetornoSuccessConfigBoleto').hide();

    $('#hdd-id-template-selecionado').val(id);
    CarregaCamposLayoutPorTemplate(id);
}


function AtivarDesativarOrdenacaoCampos(idLayout) {
    $('#SPRetornoSuccessConfigBoleto').hide();

    // verifica ordem nr. doc.
    if ($(`#chk-ativa-layout-${idLayout}`).is(':checked')) {
        let qtdChecked = $('.chk-cmp-ordem:checked').length;
        if ($(`#combo-${idLayout}-ordem`).val() == '0')
            $(`#combo-${idLayout}-ordem`).val(qtdChecked);
        $(`#combo-${idLayout}-ordem`).show();
    }
    else {
        $(`#combo-${idLayout}-ordem`).hide();
        $(`#combo-${idLayout}-ordem`).val('0');
    }

    // Após ativar/desativar ordenação, verifica novamente se existe repetição.
    VerificaRepeticaoOrdem(idLayout);    
}

function ZerarEstadoConfigCampos() {
    // Zera estado da config. campos
    $('.chk-cmp-ordem').prop('checked', false);
    $('.cmb-campo-boleto-ordem').val('0');
    $('.cmb-campo-boleto-ordem').hide();
    CarregaCombosOrdem();
    $('#mainContent_inputNomeConfiguracaoBoleto').val('');
}

function InsereCheckCampo(campo1, campo2 = null) {
    let InputSelect = $(`<select class='cmb-campo-boleto-ordem' onchange='VerificaRepeticaoOrdem(${campo1.id})' id='combo-${campo1.id}-ordem'></select>`).append('<option></option>');
    let InputChk = $(`<input type='checkbox' class='checkbox-template chk-cmp-ordem' onChange='AtivarDesativarOrdenacaoCampos(${campo1.id});' id='chk-ativa-layout-${campo1.id}' />`);
    let Label = $(`<label for="chk-${campo1.id}" id='lbl-cmp-${campo1.id}'>${campo1.nome}</label>`);
    let divIChecks = $(`<div class='i-checks'></div>`).append(InputSelect);
    divIChecks.append(InputChk);
    divIChecks.append(Label);
    let divCol = $(`<div class='col-md-5'></div>`).append(divIChecks);
    let divRow = $(`<div class='row'></div>`).append(divCol);

    // campo 2 - (segunda coluna)
    if (campo2 != null) {
        let InputSelect2 = $(`<select class='cmb-campo-boleto-ordem' onchange='VerificaRepeticaoOrdem(${campo2.id})' id='combo-${campo2.id}-ordem'></select>`).append('<option></option>');
        let InputChk2 = $(`<input type='checkbox' class='checkbox-template chk-cmp-ordem' onChange='AtivarDesativarOrdenacaoCampos(${campo2.id});' id='chk-ativa-layout-${campo2.id}' />`);
        let Label2 = $(`<label for="chk-${campo2.id}" id='lbl-cmp-${campo2.id}'>${campo2.nome}</label>`);
        let divIChecks2 = $(`<div class='i-checks'></div>`).append(InputSelect2);
        divIChecks2.append(InputChk2);
        divIChecks2.append(Label2);
        let divCol2 = $(`<div class='col-md-6'></div>`).append(divIChecks2);
        divRow.append(divCol2);
    }

    $('.checks-campos-boleto').append(divRow);
}

function CarregaCamposLayoutPorTemplate(id) {
    // zera bloco do layout do template
    $('.checks-campos-boleto').html('');

    $.ajax({
        method: "POST",
        url: `ConfiguraBoleto.aspx/ConsultarLayoutTemplatePorId`,
        data: '{Id: ' + id + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);        
        for (var i = 0; i <= retorno.d.length; i++) {
            if (i == retorno.d.length - 1) {
                let campo1 = { id: retorno.d[i].IdLayout, nome: retorno.d[i].NomeCampo, ativo: retorno.d[i].Ativo };
                InsereCheckCampo(campo1);
                break;
            }
            if (i < retorno.d.length) {
                let campo1 = { id: retorno.d[i].IdLayout, nome: retorno.d[i].NomeCampo, ativo: retorno.d[i].Ativo };
                let campo2 = { id: retorno.d[i + 1].IdLayout, nome: retorno.d[i + 1].NomeCampo, ativo: retorno.d[i + 1].Ativo };
                InsereCheckCampo(campo1, campo2);
                i += 1;
                if (i == retorno.d.length) {
                    let campo1 = { id: retorno.d[i].IdLayout, nome: retorno.d[i].NomeCampo, ativo: retorno.d[i].Ativo };
                    InsereCheckCampo(campo1);
                    break;
                }
            }
        }
        // Carrega qtde de posições disponíveis e armazenar os Ids dos layouts para futura atualização.
        arrOrdemBoletoDisponivel = [];
        arrIdsLayoutsCarregados = [];
        for (var i = 0; i < retorno.d.length; i++) {
            arrOrdemBoletoDisponivel.push(i + 1);
            arrIdsLayoutsCarregados.push(retorno.d[i].IdLayout);
        }


        CarregaCombosOrdem(arrOrdemBoletoDisponivel);
        AtualizaTelaConfig(retorno.d);
    });
}

function AtualizaTelaConfig(layout) {
    //ZerarEstadoConfigCampos();
    $('#mainContent_inputNomeConfiguracaoBoleto').val(layout[0].NomeTemplate).focus();

    layout.forEach(function (value) {
        // se layout estiver ativo, deixar checkbox checado
        if (value.Ativo) {
            $(`#chk-ativa-layout-${value.IdLayout}`).click();
            $(`#combo-${value.IdLayout}-ordem`).val(value.PosicaoCampo);
        }
    });
}

function VerificaRepeticaoOrdem(idLayout) {
    $('#SPRetornoSuccessConfigBoleto').hide();
    let vlrOrdemSelecionada = $(`#combo-${idLayout}-ordem`).val();
    let auxContRep = 0;
    $(".cmb-campo-boleto-ordem").each(function () {
        if ($(this).val() == vlrOrdemSelecionada)
            auxContRep++;
    })
    // se repetir mais q 2x, significa q existe ordem repetida nos campos => exibir critica
    if (auxContRep > 1) {
        $('#SPRetornoErroConfigBoleto').show();
        $('#btnSalvarLayout').attr('disabled', true);
    }
    else {
        $('#SPRetornoErroConfigBoleto').hide();
        $('#btnSalvarLayout').attr('disabled', false);
    }
}

function CarregaArquivosConfigurados() {
    $('.container-radio-boleto').html('');
    $.ajax({
        method: "POST",
        url: `ConfiguraBoleto.aspx/ConsultarTemplatesConfigurados`,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {        
        if (retorno.d != null) {
            let IdTemplatePadrao = 0;
            retorno.d.forEach(function (value) {
                InserirRowRadioConfig(value.IdTemplate, value.NomeTemplate, value.Padrao);
                if (value.Padrao)
                    IdTemplatePadrao = value.IdTemplate;
            });
            $(`#rd-config-${IdTemplatePadrao}`).click();
        }
    });
}

function RefreshArquivosConfigurados() {
    $('.container-radio-boleto').html('');
    $.ajax({
        method: "POST",
        url: `ConfiguraBoleto.aspx/ConsultarTemplatesConfigurados`,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d != null) {
            let IdTemplatePadrao = 0;
            retorno.d.forEach(function (value) {
                InserirRowRadioConfig(value.IdTemplate, value.NomeTemplate, value.Padrao);
                if (value.Padrao)
                    IdTemplatePadrao = value.IdTemplate;
            });
            //$(`#rd-config-${IdTemplatePadrao}`).click();
        }
    });
}

function InserirRowRadioConfig(id, nomeConfig, padrao = false) {
    let RdConfig = $(`<input type='radio' name='rd-config' value='${id}' onClick='AtualizaDadosLayout(${id})' class='radio-template rd-config' id='rd-config-${id}' />`);
    if (padrao)
        RdConfig = $(`<input type='radio' name='rd-config' value='${id}' onClick='AtualizaDadosLayout(${id})' checked class='radio-template rd-config' id='rd-config-${id}' />`);
    let divIchecks = $("<div class='i-checks'>").prepend(RdConfig);
    divIchecks.append(`<label for="rd-config-${id}">${nomeConfig}</label>`);
    let = divRow = $("<div class='row'>").prepend(divIchecks);
    $('.container-radio-boleto').append(divRow);
}

// #endregion


// #endregion
// ######################################################################### PainelGerencial.aspx #########################################################################################
// #region PainelGerencial
$(document).ready(function () {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)

    if (url.includes('PainelGerencial.aspx')) {
        CarregaConsolidadoSegurados();
        CarregaConsolidadoPagamento();
        CarregaConsolidadoFaturamento();
        CarregaDetalhadoGrafico();
		CarregaConsolidadoPremio();
    }
});

function CarregaDetalhadoGrafico() {
    const saude = [];
    const odonto = [];
    $.ajax({
        method: "POST",
        url: "PainelGerencial.aspx/CarregarDetalhadoGraficoAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        console.log(retorno);
        if (retorno.d == null)
            return;

        // dummy data
        //let data = [];
        //data.push({ Mes: 1, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 2, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 3, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 4, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 5, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 6, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 7, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 8, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 9, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 10, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 11, QtdeSaude: 80, QtdeOdonto: 10 });
        //data.push({ Mes: 12, QtdeSaude: 80, QtdeOdonto: 10 });
        
        retorno.d.forEach(function (item) {
            saude.push(item.QtdeSaude);
            odonto.push(item.QtdeOdonto);
        });
        barsChart( saude,odonto);
        renderGraph(retorno.d);
    });
}
function barsChart(saude, odonto) {
    Chart.defaults.global.animation = false;
    Chart.defaults.global.responsive = false;


    var ctx = document.getElementById("barsChart");

    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
            datasets: [
                {
                    data: saude,
                    label: 'Saúde',
                    backgroundColor: "rgba(4,85,49,0.5)",
                    borderColor: "rgba(4,85,49,1)",
                    borderWidth: 3
                },
                {
                    data: odonto,
                    label: 'Odonto',
                    backgroundColor: "rgba(255,126,43,0.5)",
                    borderColor: "rgba(255,126,43,1)",
                    borderWidth: 3
                }
            ]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }],
                xAxes: [{
                    ticks: {
                        autoSkip: false
                    }
                }]

            },
            responsive: true,
            maintainAspectRatio: false,
            legend: {
                position: 'top',
                labels: {
                    fontColor: 'rgb(255, 99, 132)'
                },
                onClick: function (e, legendItem) {
                    var index = legendItem.datasetIndex;
                    var ci = this.chart;
                    var alreadyHidden = (ci.getDatasetMeta(index).hidden === null) ? false : ci.getDatasetMeta(index).hidden;
                    console.log(legendItem);

                    ci.data.datasets.forEach(function (e, i) {
                        var meta = ci.getDatasetMeta(i);

                        if (i !== index) {
                            if (!alreadyHidden) {
                                meta.hidden = meta.hidden === null ? !meta.hidden : null;
                            } else if (meta.hidden === null) {
                                meta.hidden = true;
                            }
                        } else if (i === index) {
                            meta.hidden = false;
                        }
                    });

                    ci.update();
                },
                labels: {
                    padding: 20
                }
            }
        },
        showTooltips: false,
        onAnimationComplete: function () {

            var ctx = this.chart.ctx;
            ctx.font = this.scale.font;
            ctx.fillStyle = this.scale.textColor
            ctx.textAlign = "center";
            ctx.textBaseline = "bottom";

            this.datasets.forEach(function (dataset) {
                dataset.bars.forEach(function (bar) {
                    ctx.fillText(bar.value, bar.x, bar.y - 5);
                });
            })
        },

        plugins: [{
            afterDatasetsDraw: function (chartController, options) {
                var ctx = chartController.chart.ctx;

                ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                ctx.textAlign = 'center';
                ctx.textBaseline = 'bottom';

                chartController.data.datasets.forEach(function (dataset, i) {
                    var meta = chartController.getDatasetMeta(i);

                    var isHidden = meta.hidden; //'hidden' property of dataset
                    if (!isHidden) {
                        meta.data.forEach(function (bar, index) {
                            var data = dataset.data[index];
                            ctx.fillText(data, bar._model.x, bar._model.y - 5);
                        });
                    }
                });
            },
            beforeInit: function (chart, options) {
                chart.legend.afterFit = function () {
                    this.height = this.height + 10;
                };
            }
        }]
    });

   // legend(document.getElementById("barsLegend"), data);
}

function CarregaConsolidadoSegurados() {
    $.ajax({
        method: "POST",
        url: "PainelGerencial.aspx/CarregarConsolidadoSeguradoAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno);        
        $('#sp-timer-QtdTitularSaude').html(retorno.d.QtdTitularSaude);        
        $('#sp-timer-QtdDependenteSaude').html(retorno.d.QtdDependenteSaude);        
        $('#sp-timer-QtdTitularOdonto').html(retorno.d.QtdTitularOdonto);
        $('#sp-timer-QtdDependenteOdonto').html(retorno.d.QtdDependenteOdonto);     
        let current_datetime = new Date();
        let formatted_date = current_datetime.getDate() + "/" + (current_datetime.getMonth() + 1) + "/" +current_datetime.getFullYear() + " " + current_datetime.getHours() + ":" + current_datetime.getMinutes();
        $('#sp-qtd-saude-atualizado').html(formatted_date);
        $('#sp-qtd-odonto-atualizado').html(formatted_date);
    });
}

function CarregaConsolidadoPagamento() {
    $.ajax({
        method: "POST",
        url: "PainelGerencial.aspx/CarregarConsolidadoPagamentoAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
       // console.log(retorno);                
        $('#sp-pag-recebido-saude').html(retorno.d.ConsolidPagamentoRecebidoSaude).priceFormat({ prefix: 'R$ ', centsSeparator: ',', thousandsSeparator: '.' });
        $('#sp-pag-pendente-saude').html(retorno.d.ConsolidPagamentoPendenteSaude).priceFormat({ prefix: 'R$ ', centsSeparator: ',', thousandsSeparator: '.' });        
        $('#sp-pag-recebido-odonto').html(retorno.d.ConsolidPagamentoRecebidoOdonto).priceFormat({ prefix: 'R$ ', centsSeparator: ',', thousandsSeparator: '.' });
        $('#sp-pag-pendente-odonto').html(retorno.d.ConsolidPagamentoPendenteOdonto).priceFormat({ prefix: 'R$ ', centsSeparator: ',', thousandsSeparator: '.' });   
        let current_datetime = new Date();
        let formatted_date = current_datetime.getDate() + "/" + (current_datetime.getMonth() + 1) + "/" +current_datetime.getFullYear() + " " + current_datetime.getHours() + ":" + current_datetime.getMinutes();
        $('#sp-pag-saude-atualizado').html(formatted_date);
        $('#sp-pag-pendente-atualizado').html(formatted_date);
        
    });
}

function CarregaConsolidadoFaturamento() {
    $.ajax({
        method: "POST",
        url: "PainelGerencial.aspx/CarregarConsolidadoFaturamentoAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno);
        $('#sp-fat-saude').html(retorno.d.ConsolidFaturamentoSaude).priceFormat({ prefix: 'R$ ', centsSeparator: ',', thousandsSeparator: '.' });
        $('#sp-fat-odonto').html(retorno.d.ConsolidFaturamentoOdonto).priceFormat({ prefix: 'R$ ', centsSeparator: ',', thousandsSeparator: '.' });      
        
        let current_datetime = new Date();
        let formatted_date = current_datetime.getDate() + "/" + (current_datetime.getMonth() + 1) + "/" +current_datetime.getFullYear() + " " + current_datetime.getHours() + ":" + current_datetime.getMinutes();
        $('#sp-pag-faturamento-atualizado').html(formatted_date);
    });
}

function CarregaConsolidadoPremio() {
    $.ajax({
        method: "POST",
        url: "PainelGerencial.aspx/CarregarConsolidadoPremioAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno);

        var tabela = $("#PremioConsolidado");
        var rows = "";
      
        retorno.d.forEach(function (item) {

            if(item.ConsolidPremioMes == "1")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes1").show();
                tabela = $("#mes1");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Janeiro</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }

            if(item.ConsolidPremioMes == "2")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes2").show();
                tabela = $("#mes2");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Feveriro</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }
            if(item.ConsolidPremioMes == "3")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes3").show();
                tabela = $("#mes3");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Março</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }
            if(item.ConsolidPremioMes == "4")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes4").show();
                tabela = $("#mes4");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Abril</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }
            if(item.ConsolidPremioMes == "5")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes5").show();
                tabela = $("#mes5");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Maio</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }
            if(item.ConsolidPremioMes == "6")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes6").show();
                tabela = $("#mes6");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Junho</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }
            if(item.ConsolidPremioMes == "7")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes7").show();
                tabela = $("#mes7");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Julho</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }
            if(item.ConsolidPremioMes == "8")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes8").show();
                tabela = $("#mes8");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Agosto</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }
            if(item.ConsolidPremioMes == "9")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes9").show();
                tabela = $("#mes9");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Setembro</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }
            if(item.ConsolidPremioMes == "10")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes10").show();
                tabela = $("#mes10");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Outubro</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }
            if(item.ConsolidPremioMes == "11")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes11").show();
                tabela = $("#mes11");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Novembro</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            }
            if(item.ConsolidPremioMes == "12")
            {
                var valorPremioSaude = item.ConsolidValorPremioSaude == "" ? 0 : item.ConsolidValorPremioSaude;
                var valorPremioOdonto = item.ConsolidValorPremioOdonto == "" ? 0 : item.ConsolidValorPremioOdonto;
                var quantidadeSaude = item.ConsolidPremioQuantidadeSaude == "" ? 0 : item.ConsolidPremioQuantidadeSaude;
                var quantidadeOdonto = item.ConsolidPremioQuantidadeOdonto == "" ? 0 : item.ConsolidPremioQuantidadeOdonto;

                $("#mes12").show();
                tabela = $("#mes12");
                tabela.find("td").remove();
                rows += " <th scope='row' class='text-center'>Dezembro</th>";
                rows += " <td class='text-center'>" + quantidadeSaude + "</td>";
                rows += " <td class='text-center'>" + quantidadeOdonto + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioSaude) + "</td>";
                rows += " <td class='text-center'>" + numberToReal(valorPremioOdonto) + "</td>";
                tabela.html(rows);
                rows = "";
            } 
        });
    });
}

function numberToReal(numero) {
    var numero = parseFloat(numero).toFixed(2).split('.');
    numero[0] = "R$ " + numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}