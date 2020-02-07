$(document).ready(function () {
    VerificaSession();
    VerificaQuantidadeUsuariosPendentes();
    VerificaPadraoSenha();
    GerenciaMenu();
});
// #################   LISTENERS
$('#lkSair').click(function () {
    $('#mdlSair').modal('show');
});

$('#btnRdSenha').click(function () {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)

    if (url.includes('ConsultaSegurado.aspx')) {
        let url = "ConsultaSegurado.aspx/RedefinirPadraoSenha";
        padronizacaoRedefinicaoSenha(url);
    }

    if (url.includes('Pagamentos.aspx')) {
        let url = "Pagamentos.aspx/RedefinirPadraoSenha";
        padronizacaoRedefinicaoSenha(url);
    }

    if (url.includes('PainelGerencial.aspx')) {
        let url = "PainelGerencial.aspx/RedefinirPadraoSenha";
        padronizacaoRedefinicaoSenha(url);
    }
});

$("#btnSair").click(function () {
    Logout();
});


function redirecionaUsuariosPendentes() {
    var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Segurado/aprovaUsuario.aspx';
    window.location.replace(urlLogin);
}

function padronizacaoRedefinicaoSenha (url) {
    $.ajax({
        method: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //    //Redirect
        var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Usuario/Login.aspx?solicitRec=' + retorno.d;
        window.location.replace(urlLogin);
        //})
    });

}

function Logout() {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)


    if (url.includes('Planos.aspx')) {
        let url = "Planos.aspx/RealizaLogoutAsync";
        ExecutarLogout(url);
    }

    if (url.includes('aprovaUsuario.aspx')) {
        let url = "aprovaUsuario.aspx/RealizaLogoutAsync";
        ExecutarLogout(url);
    }

    if (url.includes('CadastraSegurado.aspx')) {
        let url = "CadastraSegurado.aspx/RealizaLogoutAsync";
        ExecutarLogout(url);
    }

    if (url.includes('ConsultaSegurado.aspx')) {
        let url = "ConsultaSegurado.aspx/RealizaLogoutAsync";
        ExecutarLogout(url);
    }

    if (url.includes('ConsultaLog.aspx')) {
        let url = "ConsultaLog.aspx/RealizaLogoutAsync";
        ExecutarLogout(url);
    }

    if (url.includes('ConsultaLead.aspx')) {
        let url = "ConsultaLead.aspx/RealizaLogoutAsync";
        ExecutarLogout(url);
    }

    if (url.includes('Pagamentos.aspx')) {
        let url = "Pagamentos.aspx/RealizaLogoutAsync";
        ExecutarLogout(url);
    }

    if (url.includes('ConsultaFaturamento.aspx')) {
        let url = "ConsultaFaturamento.aspx/RealizaLogoutAsync";
        ExecutarLogout(url);
    }

    if (url.includes('ConfiguraBoleto.aspx')) {
        let url = "ConfiguraBoleto.aspx/RealizaLogoutAsync";
        ExecutarLogout(url);
    }

    if (url.includes('PainelGerencial.aspx')) {
        let url = "PainelGerencial.aspx/RealizaLogoutAsync";
        ExecutarLogout(url);
    }
    
}
function ExecutarLogout(url) {
    $.ajax({
        method: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //Redirect
        var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Usuario/Login.aspx';
        window.location.replace(urlLogin);        
    });
}

function VerificaPadraoSenha() {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)

    if (url.includes('ConsultaSegurado.aspx')) 
        url = "ConsultaSegurado.aspx/VerificaPadraoSenha";

    if (url.includes('Pagamentos.aspx'))
        url = "Pagamentos.aspx/VerificaPadraoSenha";

    if (url.includes('PainelGerencial.aspx'))
        url = "PainelGerencial.aspx/VerificaPadraoSenha";

    $.ajax({
        method: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);  
        if (retorno.d != null) {
            if (!retorno.d) {        // Caso a verificação retorne false, necessário redefinir senha.
                $('#mdlPadraoSenha').modal({ backdrop: 'static', keyboard: false });
                $('#mdlPadraoSenha').modal('show');
            }
        }
        else {
            redirecionaLogin();
        }
    });
}

function VerificaQuantidadeUsuariosPendentes() {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)


    if (url.includes('Planos.aspx')) {
        let url = "Planos.aspx/ListarUsuariosPendentes";
        ListarUsuariosPedentes(url);
    }

    if (url.includes('aprovaUsuario.aspx')) {
        let url = "aprovaUsuario.aspx/ListarUsuariosPendentes";
        ListarUsuariosPedentes(url);
    }

    if (url.includes('CadastraSegurado.aspx')) {
        let url = "CadastraSegurado.aspx/ListarUsuariosPendentes";
        ListarUsuariosPedentes(url);
    }

    if (url.includes('ConsultaSegurado.aspx')) {
        let url = "ConsultaSegurado.aspx/ListarUsuariosPendentes";
        ListarUsuariosPedentes(url);
    }

    if (url.includes('ConsultaLog.aspx')) {
        let url = "ConsultaLog.aspx/ListarUsuariosPendentes";
        ListarUsuariosPedentes(url);
    }

    if (url.includes('ConsultaLead.aspx')) {
        let url = "ConsultaLead.aspx/ListarUsuariosPendentes";
        ListarUsuariosPedentes(url);
    }

    if (url.includes('Pagamentos.aspx')) {
        let url = "Pagamentos.aspx/ListarUsuariosPendentes";
        ListarUsuariosPedentes(url);
    }

    if (url.includes('ConsultaFaturamento.aspx')) {
        let url = "ConsultaFaturamento.aspx/ListarUsuariosPendentes";
        ListarUsuariosPedentes(url);
    }

    if (url.includes('ConfiguraBoleto.aspx')) {
        let url = "ConfiguraBoleto.aspx/ListarUsuariosPendentes";
        ListarUsuariosPedentes(url);
    }

    if (url.includes('PainelGerencial.aspx')) {
        let url = "PainelGerencial.aspx/ListarUsuariosPendentes";
        ListarUsuariosPedentes(url);
    }
}

function GerenciaMenuNivelAcesso(nvlAcessoUsuario) {
    if (nvlAcessoUsuario == 1) {        // Comercial
        $('#liTitulo').show();
        $('#liTitulotext').text('Comercial');        
        $('#liPasso2').show();
        $('#liPasso4').show();
        $('#liPasso1').show();
    }
    if (nvlAcessoUsuario == 2) {        // Financeiro
        $('#liTitulo').show();
        $('#liTitulotext').text('Financeiro');
        $('#liPasso7').show();
        $('#liPasso8').show();        
    }
    if (nvlAcessoUsuario == 3) {        // Gestor / Administrativo
        $('#liTitulo').show();
        $('#liTitulotext').text('Administrativo');
        $('#liPasso1').show();
        $('#liPasso2').show();
        $('#liPasso3').show();
        $('#liPasso4').show();
        $('#liPasso5').show();
        $('#liPasso6').show();
        $('#liPasso7').show();
        $('#liPasso8').show();
        $('#liPasso10').show();
    }
}

function GerenciaMenu() {
    let url = new URL(window.location)
    url = String(url)
    if (url.includes('Planos.aspx')) {
        $('.btnMenu').removeClass('active');
        $('.btnMenu').removeClass('disabled');

        $('#liPasso1').addClass('disabled');
        $('#liPasso2').addClass('disabled');
        $('#liPasso3').addClass('active');
        $('#liPasso4').addClass('disabled');
        $('#liPasso5').addClass('disabled');
        $('#liPasso6').addClass('disabled');
        $('#liPasso7').addClass('disabled');
        $('#liPasso8').addClass('disabled');
        $('#liPasso9').addClass('disabled');
        $('#liPasso10').addClass('disabled');
    }
    if (url.includes('ConsultaSegurado.aspx')) {
        $('.btnMenu').removeClass('active');
        $('.btnMenu').removeClass('disabled');

        $('#liPasso1').addClass('disabled');
        $('#liPasso2').addClass('active');
        $('#liPasso3').addClass('disabled');
        $('#liPasso4').addClass('disabled');
        $('#liPasso5').addClass('disabled');
        $('#liPasso6').addClass('disabled');
        $('#liPasso7').addClass('disabled');
        $('#liPasso8').addClass('disabled');
        $('#liPasso9').addClass('disabled');
        $('#liPasso10').addClass('disabled');
    }
    if (url.includes('CadastraSegurado.aspx')) {
        $('.btnMenu').removeClass('active');
        $('.btnMenu').removeClass('disabled');

        $('#liPasso1').addClass('disabled');
        $('#liPasso2').addClass('disabled');
        $('#liPasso3').addClass('disabled');
        $('#liPasso4').addClass('active');
        $('#liPasso5').addClass('disabled');
        $('#liPasso6').addClass('disabled');
        $('#liPasso7').addClass('disabled');
        $('#liPasso8').addClass('disabled');
        $('#liPasso9').addClass('disabled');
        $('#liPasso10').addClass('disabled');
    }
    if (url.includes('aprovaUsuario.aspx')) {
        $('.btnMenu').removeClass('active');
        $('.btnMenu').removeClass('disabled');

        $('#liPasso1').addClass('active');
        $('#liPasso2').addClass('disabled');
        $('#liPasso3').addClass('disabled');
        $('#liPasso4').addClass('disabled');
        $('#liPasso5').addClass('disabled');
        $('#liPasso6').addClass('disabled');
        $('#liPasso7').addClass('disabled');
        $('#liPasso8').addClass('disabled');
        $('#liPasso9').addClass('disabled');
        $('#liPasso10').addClass('disabled');
    }
    if (url.includes('ConsultaLog.aspx')) {
        $('.btnMenu').removeClass('active');
        $('.btnMenu').removeClass('disabled');

        $('#liPasso1').addClass('disabled');
        $('#liPasso2').addClass('disabled');
        $('#liPasso3').addClass('disabled');
        $('#liPasso4').addClass('disabled');
        $('#liPasso5').addClass('active');
        $('#liPasso6').addClass('disabled');
        $('#liPasso7').addClass('disabled');
        $('#liPasso8').addClass('disabled');
        $('#liPasso9').addClass('disabled');
        $('#liPasso10').addClass('disabled');
    }
    if (url.includes('ConsultaLead.aspx')) {
        $('.btnMenu').removeClass('active');
        $('.btnMenu').removeClass('disabled');

        $('#liPasso1').addClass('disabled');
        $('#liPasso2').addClass('disabled');
        $('#liPasso3').addClass('disabled');
        $('#liPasso4').addClass('disabled');
        $('#liPasso5').addClass('disabled');
        $('#liPasso6').addClass('active');
        $('#liPasso7').addClass('disabled');
        $('#liPasso8').addClass('disabled');
        $('#liPasso9').addClass('disabled');
        $('#liPasso10').addClass('disabled');
    }

    if (url.includes('Pagamentos.aspx')) {
        $('.btnMenu').removeClass('active');
        $('.btnMenu').removeClass('disabled');

        $('#liPasso1').addClass('disabled');
        $('#liPasso2').addClass('disabled');
        $('#liPasso3').addClass('disabled');
        $('#liPasso4').addClass('disabled');
        $('#liPasso5').addClass('disabled');
        $('#liPasso6').addClass('disabled');
        $('#liPasso7').addClass('active');
        $('#liPasso8').addClass('disabled');
        $('#liPasso9').addClass('disabled');
        $('#liPasso10').addClass('disabled');
    }

    if (url.includes('ConsultaFaturamento.aspx')) {
        $('.btnMenu').removeClass('active');
        $('.btnMenu').removeClass('disabled');

        $('#liPasso1').addClass('disabled');
        $('#liPasso2').addClass('disabled');
        $('#liPasso3').addClass('disabled');
        $('#liPasso4').addClass('disabled');
        $('#liPasso5').addClass('disabled');
        $('#liPasso6').addClass('disabled');
        $('#liPasso7').addClass('disabled');
        $('#liPasso8').addClass('active');
        $('#liPasso9').addClass('disabled');
        $('#liPasso10').addClass('disabled');
    }

    if (url.includes('ConfiguraBoleto.aspx')) {
        $('.btnMenu').removeClass('active');
        $('.btnMenu').removeClass('disabled');

        $('#liPasso1').addClass('disabled');
        $('#liPasso2').addClass('disabled');
        $('#liPasso3').addClass('disabled');
        $('#liPasso4').addClass('disabled');
        $('#liPasso5').addClass('disabled');
        $('#liPasso6').addClass('disabled');
        $('#liPasso7').addClass('disabled');
        $('#liPasso8').addClass('disabled');
        $('#liPasso9').addClass('active');
        $('#liPasso10').addClass('disabled');
    }

    if (url.includes('PainelGerencial.aspx')) {
        $('.btnMenu').removeClass('active');
        $('.btnMenu').removeClass('disabled');

        $('#liPasso1').addClass('disabled');
        $('#liPasso2').addClass('disabled');
        $('#liPasso3').addClass('disabled');
        $('#liPasso4').addClass('disabled');
        $('#liPasso5').addClass('disabled');
        $('#liPasso6').addClass('disabled');
        $('#liPasso7').addClass('disabled');
        $('#liPasso8').addClass('disabled');
        $('#liPasso9').addClass('disabled');
        $('#liPasso10').addClass('active');
    }
}
function ListarUsuariosPedentes(url) {
    //Setado os parametros vazio para na proc realizar a pesquisa default.
    var nmUsuario = '';
    var cmbStatus = 'N';        // Setar parâmetro como pendentes
    $.ajax({
        method: "POST",
        url: url,
        data: '{NomeUsuario: "' + nmUsuario + '", Status: "' + cmbStatus + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);
        $('#qtdePend').text(retorno.d.length);
        if (retorno.d.length > 1)
            $('#msgPendentes').html('<i class="fa fa-users bg-red"></i>' + retorno.d.length + ' usuário(s) pendente(s) de aprovação.');
        else
            $('#msgPendentes').html('<i class="fa fa-users bg-red"></i>Você tem ' + retorno.d.length + ' usuário(s) pendente(s) de aprovação.');
    });
}
function VerificaSession() {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)
    if (url.includes('Planos.aspx'))
        ExecutaVerificacaoSession("Planos.aspx/VerificaSessionAsync");
    if (url.includes('aprovaUsuario.aspx'))
        ExecutaVerificacaoSession("aprovaUsuario.aspx/VerificaSessionAsync");
    if (url.includes('ConsultaSegurado.aspx'))
        ExecutaVerificacaoSession("ConsultaSegurado.aspx/VerificaSessionAsync");
    if (url.includes('CadastraSegurado.aspx'))
        ExecutaVerificacaoSession("CadastraSegurado.aspx/VerificaSessionAsync");
    if (url.includes('ConsultaLog.aspx'))
        ExecutaVerificacaoSession("ConsultaLog.aspx/VerificaSessionAsync");
    if (url.includes('ConsultaLead.aspx'))
        ExecutaVerificacaoSession("ConsultaLead.aspx/VerificaSessionAsync");
    if (url.includes('Pagamentos.aspx'))
        ExecutaVerificacaoSession("Pagamentos.aspx/VerificaSessionAsync");
    if (url.includes('ConsultaFaturamento.aspx'))
        ExecutaVerificacaoSession("ConsultaFaturamento.aspx/VerificaSessionAsync");
    if (url.includes('ConfiguraBoleto.aspx'))
        ExecutaVerificacaoSession("ConfiguraBoleto.aspx/VerificaSessionAsync");
    if (url.includes('PainelGerencial.aspx'))
        ExecutaVerificacaoSession("PainelGerencial.aspx/VerificaSessionAsync");
}
function ExecutaVerificacaoSession(url) {
    $.ajax({
        method: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        console.log(retorno);
        if (retorno.d != null) {
            GerenciaMenuNivelAcesso(retorno.d.NvlAcesso);
        }
        else {
            redirecionaLogin();
        }
    });
}
function redirecionaLogin() {
    var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Usuario/Login.aspx';
    window.location.replace(urlLogin);
   // alert('Sua sessão expirou.');
}