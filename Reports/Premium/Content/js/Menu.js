$(document).ready(function () {
    $(".logout").click(function () {
        $.ajax({
            method: "POST",
            url: "_Orcamento.aspx/RealizaLogoutAsync",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //Redirect
            var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Acesso/ManterUsuario.aspx';
            window.location.replace(urlLogin);
        })
    });

    //Preenche a variavel com o passo do menu clicado.
    $('#ulNav li a').click(function () {
        localStorage.setItem('linkMenu', $(this).parent().attr("id"));
    });

    //Seta classe active para o menu.
    var linkMenu = localStorage.getItem('linkMenu');
    if (linkMenu) {
        $('#ulNav li').removeClass();
        $('#' + linkMenu).addClass('active');
    }

    //Limpa variavel para finalizar sessão.
    linkMenu = localStorage.setItem('linkMenu', '');
});


VerificaSession();

function VerificaSession() {
    $.ajax({
        method: "POST",
        url: "_Orcamento.aspx/VerificaSessionAsync",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //se retornar false, a session está expirada. Encaminhar para login.
        //console.log(retorno.d);
        if (!retorno.d) {
            //Redirect
            var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Acesso/ManterUsuario.aspx';
            window.alert('Sessão expirada. Favor realizar novo login.');
            window.location.replace(urlLogin);
        } else {
            //colocar apenas o primeiro nome no login.
            var arrNome = retorno.d.Nome.split(" ");
            $('#lblUser').text(arrNome[0]);            
        }
    });
}