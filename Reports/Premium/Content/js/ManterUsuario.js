$(document).ready(function () {
    $('#txtSenhaUser').focus();
    $('#txtLoginUser').focus();
        

    //Se veio valor no token, verifica se está valido para abrir a tela de recuperar senha.    
    VerificaTokenRecupSenha();    
    

    $("#btnLogin").click(function () {                         
        vlrUser = $('#txtLoginUser').val();
        vlrSenha = $('#txtSenhaUser').val();
        vlrUser = vlrUser.replace(/[^\d]+/g, '');
        $('#spErroLogin').hide();
        if(!validaLogin())
            return;
        ExibeLoading();
        $.ajax({
            method: "POST",
            url: "ManterUsuario.aspx/VerificaLoginAsync",
            data: '{user: "' + vlrUser + '", senha: "' + vlrSenha + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno){
            console.log(retorno.d);
            //se retorna true, é que foi logado.
            if(retorno.d == 'true')
            {                
                //Redirect
                var urlCalculo = 'http://' + window.location.hostname + ':' + window.location.port + '/_Orcamento.aspx';            
                window.location.replace(urlCalculo);
            }else if(retorno.d == 'pendente'){
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'block';                
                dvRecSenha.style.display = 'none';
                dvSucesso.style.display = 'none';
                $('#loader').hide();
                $('#spErroLogin').text('Usuário pendente e aprovação.');
                $('#spErroLogin').show();
            }else{
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'block';                
                dvRecSenha.style.display = 'none';
                dvSucesso.style.display = 'none';
                $('#loader').hide();
                $('#spErroLogin').text('Login ou Senha inválidos.');
                $('#spErroLogin').show();
            }
        });
    });
});

//Verifica se cpf existe na base quando informado. Caso exista, bloqueia o formulário.
$("#txtCPF").change(function () {
    $("#txtNomeC").prop("disabled", false);
    $("#SPTxtCPF").text('Cpf inválido.');
    $("#SPTxtCPF").hide();

    var vlrCpf = $("#txtCPF").val();
    //retira a máscara
    vlrCpf = vlrCpf.replace(/[^\d]+/g, '');
        
    $.ajax({
        method: "POST",
        url: "ManterUsuario.aspx/ConsultaUsuarioPorCpfAsync",
        data: '{cpf: "' + vlrCpf + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //Caso exista usuario cadastrado, exibir msg. e bloquear formulario.
        if (retorno.d.Nome) {
            $("#SPTxtCPF").show();
            $("#SPTxtCPF").text('Ops, identificamos que este CPF já está cadastrado.');
            $(".cmpsUsuario").val('');
            $(".cmpsUsuario").prop("readonly", true);     
        } else {
            $(".cmpsUsuario").prop("readonly", false);
        }
    })
});


$("#btnCadastrar").click(function () {
    if(validaCadastro())
    {
        var vlrGenero = $("#rdMasc").is(":checked") ? 'M' : 'F';

        var data = {
            objUsuario: {
                Nome: $('#txtNomeC').val(),
                Cpf: $('#txtCPF').val().replace(/[^\d]+/g, ''),
                Email: $('#txtEmail').val(),
                Genero: vlrGenero,
                Telefone: $('#txtTelefone').val().replace(/[^\d]+/g, ''),                
                DataNascimento: converteData($('#txtDtNascimento').val()),                                
                Senha: $('#txtSenha').val(),
            }    
        }

        ExibeLoading();
        $.ajax({
            method: "POST",
            url: "ManterUsuario.aspx/CadastraUsuario",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json"

        }).done(function (retorno) {        
            //console.log(retorno.d);       
            if(retorno.d == 'ok')
                telaMsgFinal('Pronto! Seu cadastro foi realizado com sucesso. Clique no link abaixo para realizar o login.');
            else{
                telaMsgFinal('Erro na tentativa de cadastro do usuário.');
                //console.log(retorno.d);                
            }                
        })
    }
});


$(function () {
    // Máscara CPF
    $('.mascaraCPF').on('keydown.mask', function (e) {
        var target, cpf, elemento;
        target = $(this);
        cpf = target.val().replace(/\D/g, ''); //Remove tudo que não é digito
        elemento = $(target);
        if (cpf.length > 10) {
            elemento.mask("9?99.999.999-99");
        } else {
            elemento.mask("9?99.999.999-99");
        }
    });


    $('.mascaraCPF2').on('keydown.mask').focusout(function(){ 
        var target, cpf, elemento;
        target = $(this);
        cpf = target.val().replace(/\D/g, ''); //Remove tudo que não é digito
        elemento = $(target);
        if (cpf.length > 11) {
            elemento.mask("9?99.999.999-99");
        } else {
            elemento.mask("9?99.999.999-99");
        }    
    });

    // Máscara Dte Nascimento
    $('#txtDtNascimento').on('keydown.mask', function (e) {
        var target, nascimento, elemento;
        target = $(this);
        nascimento = target.val().replace(/\D/g, ''); //Remove tudo que não é digito
        elemento = $(target);
        if (nascimento.length > 10) {
            elemento.mask("9?9/99/9999");
        } else {
            elemento.mask("9?9/99/9999");
        }
    });

    //Máscara CNPJ
    $('#txtEmpCNPJ').on('keydown.mask').focusout(function (e) {
        var target, cnpj, elemento;
        target = $(this);
        cnpj = target.val().replace(/\D/g, ''); //Remove tudo que não é digito
        elemento = $(target);
        if (cnpj.length > 10) {
            elemento.mask("9?9.999.999/9999-99");
        } else {
            elemento.mask("9?9.999.999/9999-99");
        }
    });
    
    //Máscara CEP
    $('#txtEmpCEP').on('keydown.mask').focusout(function (e) {
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

    //Funções anônimas em jQuery que são ativadas de acordo com a ação/evento acionados.
    $(document).on('click', "#btnMasc", function () {

        $('#btnFem').removeClass("active");
        $('#btnMasc').addClass("active");

    });

    $(document).on('click', "#btnFem", function () {
        $('#btnMasc').removeClass("active");
        $('#btnFem').addClass("active");
    });

    $(document).on('click', "#lkConfSenha", function() {
        var validarPassou = true;

        var Nvsenha = $('#txtNvSenha').val();
        var confNvSenha = $('#txtCnfNvSenha').val();
        if(Nvsenha == "" && confNvSenha ==  "" || Nvsenha.length < 2 && confNvSenha.length < 2 ) {
            $("#SPNvSenha").text("Insira uma senha!");
            $("#SPCfNvSenha").text("Insira uma senha!");
            $("#SPCfNvSenha").show();
            $("#SPNvSenha").show();
            $("#txtNvSenha").css("border-color", "red");
            $("#txtCnfNvSenha").css("border-color", "red");
            validarPassou = false;
        } else if (confNvSenha != Nvsenha){
            $("#SPCfNvSenha").text("As senhas não conferem, por favor verifique!");
            $("#SPCfNvSenha").show();
            $("#txtCnfNvSenha").css("border-color", "red");
            validarPassou = false;
        } else {
            $("#SPNvSenha").text = "";
            $("#SPCfNvSenha").text = "";
            $("#SPCfNvSenha").hide();
            $("#SPNvSenha").hide();
            $("#txtNvSenha").css("border-color", "");
            $("#txtCnfNvSenha").css("border-color", "");
        } 

        if (validarPassou == true) {
            return true;
        }
    });

});


function validaCadastro(){
    var validarPassou = true;
    var cpf = document.getElementById("txtCPF");
    
    var txtEmail = document.getElementById("txtEmail");
    var posP = txtEmail.value.indexOf(".");
    var posA = txtEmail.value.indexOf("@");

    if (txtEmail.value == "" || txtEmail.value.length < 2 || txtEmail.value == "usuario@dominio.com.br"){
        $("#txtEmail").css("border-color", "red");
        $("#SPTxtEmail").show();
        $("#SPTxtEmail").text("Insira um endereço de email!");
        validarPassou = false;
    } else if (posA == -1) {
        $("#txtEmail").css("border-color", "red");
        $("#SPTxtEmail").text("Email Inválido, por favor verifique!");
        $("#SPTxtEmail").show();
        validarPassou = false;
    } else if (posP == -1) {
        $("#txtEmail").css("border-color", "red");
        $("#SPTxtEmail").text("Email Inválido, por favor verifique!");
        $("#SPTxtEmail").show();
        validarPassou = false;
    } else {
        $("#txtEmail").css("border-color", "");
        $("#SPTxtEmail").hide();
        $("#SPTxtEmail").text("");
    }   

    var txtTelefone = document.getElementById("txtTelefone").value.replace(/[^\d]+/g, '');
    if(txtTelefone == "" || txtTelefone.length < 1 ) {
        $("#SPTxtTelefone").text = "Insira um número de Telefone!";
        $("#SPTxtTelefone").show();
        $("#txtTelefone").css("border-color", "red");
        validarPassou = false;
    } else if (txtTelefone.length < 9){
        $("#SPTxtTelefone").text("Número inválido, verifique por favor!");
        $("#SPTxtTelefone").show();
        $("#txtTelefone").css("border-color", "red");
        validarPassou = false;
    } 
    else {
        $("#SPTxtTelefone").text = "";
        $("#SPTxtTelefone").hide();
        $("#txtTelefone").css("border-color", "");
    }


    if(cpf.value == "" || cpf.value.length < 2){
        $('#txtCPF').css("border-color", "red");
        $('#SPTxtCPF').text("Insira um CPF válido!");
        $("#SPTxtCPF").show();
        cpf.focus();
        validarPassou = false;
    } else if (validarCPF() == false) {
        $('#txtCPF').css("border-color", "red");
        $("#SPTxtCPF").show();
        cpf.focus();
        validarPassou = false;
    } else {
        $("#SPTxtCPF").hide();
        $('#txtCPF').css("border-color", "");
        $("#SPTxtCPF").innerHTML = "";
    }

    var nome = document.getElementById("txtNomeC");
    if (nome.value == "" || nome.value.length < 2) {
        $("#txtNomeC").css("border-color", "red");
        $("#SPTxtNomeC").show();
        nome.focus();
        validarPassou = false;
    } else {
        $("#SPTxtNomeC").hide();
        $('#txtNomeC').css("border-color", "");
        $("#SPTxtNomeC").innerHTML = "";
    }

    var btnMasc = document.getElementById("rdMasc").checked;
    var btnFem = document.getElementById("rdFem").checked;
    
    if (btnMasc != true && btnFem != true) {
        $("#btnMasc").css("border-color", "red");
        $("#btnFem").css("border-color", "red");
        $("#SPGen").show();
        validarPassou = false;
    } else {
        $("#SPGen").innerHTML = "";
        $("#btnMasc").css("border-color", "");
        $("#btnFem").css("border-color", "");
        $("#SPGen").hide();
    }

    var txtDtNascimento = document.getElementById("txtDtNascimento");
    var date = txtDtNascimento.value;
    var dtAtual = new Date();
    var dia = dtAtual.getDate();
    var mes = dtAtual.getMonth()+1;
    var ano = dtAtual.getFullYear();
    var str_ano = dia + '/' + mes + '/' + ano;
    var arrDT = new Array;
    var arrDT2 = new Array;
    arrDT = date.split("/");
    arrDT2 = str_ano.split("/");
    if(txtDtNascimento.value == "" || txtDtNascimento.value.length < 1){
        $("#txtDtNascimento").css("border-color", "red");
        $("#SPTxtDtNascimento").show();
        $("#SPTxtDtNascimento").text("Insira uma data de nascimento!");
        validarPassou = false;
    } else if (txtDtNascimento != "" ){
        if (arrDT[0] > 31){
            $("#txtDtNascimento").css("border-color", "red");
            $("#SPTxtDtNascimento").show();
            $("#SPTxtDtNascimento").text("Data de nascimento inválida, por favor verifique!");
            validarPassou = false;
        }

        if (arrDT[1] > 12) {
            $("#txtDtNascimento").css("border-color", "red");
            $("#SPTxtDtNascimento").show();
            $("#SPTxtDtNascimento").text("Data de nascimento inválida, por favor verifique!");
            validarPassou = false;
        }

        
        if (arrDT[2] > ano) {
            $("#txtDtNascimento").css("border-color", "red");
            $("#SPTxtDtNascimento").show();
            $("#SPTxtDtNascimento").text("Data de nascimento inválida, por favor verifique!");
            validarPassou = false;
        }

        // Comparação se o mês digitado é maior que o mês atual
        if (arrDT[1] > arrDT2[1]) {
            // Comparação se o ano digitado é maior ou igual ao mês atual.
            if (arrDT[2] >= ano) {
                $("#txtDtNascimento").css("border-color", "red");
                $("#SPTxtDtNascimento").show();
                $("#SPTxtDtNascimento").text("Data de nascimento não pode ser maior ou a mesma de hoje!");
                validarPassou = false;
            }
        } 
        //Comparação se o ano digitado é maior ou o mesmo que o ano atual.
        if (arrDT[2] == ano) {
            //Comparação se o mês digitado é maior ou o mesmo que o mês atual.
            if (arrDT[1] >= arrDT2[1]) {
                //Comparação se o dia digitado é o maior ou o mesmo que o dia atual.
                if (arrDT[0] >= arrDT2[0]) {
                    $("#txtDtNascimento").css("border-color", "red");
                    $("#SPTxtDtNascimento").show();
                    $("#SPTxtDtNascimento").text("Data de nascimento não pode ser maior ou a mesma de hoje!");
                    validarPassou = false;
                }}}
    }
    else {
        $("#txtDtNascimento").css("border-color", "");
        $("#SPTxtDtNascimento").hide();
        $("#SPTxtDtNascimento").text("");
    }


    var senha = document.getElementById("txtSenha");
    var confSenha = document.getElementById("txtConfSenha");
    if(senha.value == "" && confSenha.value ==  "" || senha.value.length < 1 && confSenha.value.length < 1 ) {
        $("#SPTxtSenha").text = "Insira uma senha!";
        $("#SPTxtConfSenha").text = "Insira uma senha!";
        $("#SPTxtConfSenha").show();
        $("#SPTxtSenha").show();
        $("#txtSenha").css("border-color", "red");
        $("#txtConfSenha").css("border-color", "red");
        validarPassou = false;
    } else if (confSenha.value != senha.value){
        $("#SPTxtConfSenha").text = "As senhas não conferem, por favor verifique!";
        $("#SPTxtConfSenha").show();
        $("#txtConfSenha").css("border-color", "red");
        validarPassou = false;
    } else {
        $("#SPTxtSenha").text = "";
        $("#SPTxtConfSenha").text = "";
        $("#SPTxtConfSenha").hide();
        $("#SPTxtSenha").hide();
        $("#txtSenha").css("border-color", "");
        $("#txtConfSenha").css("border-color", "");
    } 


    if (validarPassou == true) {
        return true;
    }
}


function validaLogin(){
    var validarPassou = true;

    var txtLogin = $('#txtLoginUser').val();
    var txtSenha = $('#txtSenhaUser').val();

    if (txtLogin == "" || txtLogin.length < 2){
        $("#SPtxtLoginUser").text("Insira o login do usuário para poder continuar!");
        $("#SPtxtLoginUser").show();
        $("#txtLoginUser").css("border-color", "red");
        validarPassou = false;        
    } else {
        $("#SPtxtLoginUser").text("");
        $("#SPtxtLoginUser").hide();
        $("#txtLoginUser").css("border-color", "");
    } 
    if (txtSenha == "" || txtSenha.length < 2 || txtLogin == "") {
        $("#SPtxtSenhaUser").text("Insira a senha do usuário para poder continuar!");
        $("#SPtxtSenhaUser").show();
        $("#txtSenhaUser").css("border-color", "red");
        validarPassou = false;
    } else {
        $("#SPtxtSenhaUser").text("");
        $("#SPtxtSenhaUser").hide();
        $("#txtSenhaUser").css("border-color", "");
    }
    
    return validarPassou;
}
function validarCPF() {
    if (!$('#txtCPF').validateCPF()) {
        $('#txtCPF').css("border-color", "red");

        return false;
        
    } else {
        $('#txtCPF').css("border-color", "");
        return true;
    }
}


function SalvarSenha()
{
    var validarPassou = true;

    var Nvsenha = $('#txtNvSenha').val();
    var confNvSenha = $('#txtCnfNvSenha').val();
    if(Nvsenha == "" && confNvSenha ==  "" || Nvsenha.length < 2 && confNvSenha.length < 2 ) {
        $("#SPNvSenha").text("Insira uma senha!");
        $("#SPCfNvSenha").text("Insira uma senha!");
        $("#SPCfNvSenha").show();
        $("#SPNvSenha").show();
        $("#txtNvSenha").css("border-color", "red");
        $("#txtCnfNvSenha").css("border-color", "red");
        validarPassou = false;
    } else if (confNvSenha != Nvsenha){
        $("#SPCfNvSenha").text("As senhas não conferem, por favor verifique!");
        $("#SPCfNvSenha").show();
        $("#txtCnfNvSenha").css("border-color", "red");
        validarPassou = false;
    } else {
        $("#SPNvSenha").text = "";
        $("#SPCfNvSenha").text = "";
        $("#SPCfNvSenha").hide();
        $("#SPNvSenha").hide();
        $("#txtNvSenha").css("border-color", "");
        $("#txtCnfNvSenha").css("border-color", "");
    } 

    if (validarPassou == true) {
        $.ajax({
            method: "POST",
            url: "ManterUsuario.aspx/SalvarRecuperacaoSenhaAsync",
            data: '{cpf: "' + $('#hddCpfUsuario').val() + '", novaSenha: "' + $('#txtCnfNvSenha').val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);  
            ExibeMsgFinal(retorno.d);
        })
    }
}


function RecuperarSenha()
{
    var vlrCpf = $('#txtCPFRec').val();
    vlrCpf = vlrCpf.replace(/[^\d]+/g, '');
    if(verificarCPF(vlrCpf))
    {
        ExibeLoading();
        $.ajax({
            method: "POST",
            url: "ManterUsuario.aspx/RecuperarSenhaAsync",
            data: '{cpf: "' + vlrCpf + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);  
            if(retorno.d.includes("erro")){
                console.log('erro: ' + retorno.d);
                ExibeMsgFinal('Houve um erro na tentativa de recuperar a senha');
            }else{
                ExibeMsgFinal(retorno.d);
            }
            
        })
    }else{
        $('#txtCPFRec').css("border-color", "red");
        $('#SPtxtCPFRec').text("Insira um CPF válido!");
        $("#SPtxtCPFRec").show();
    }
}

function VerificaTokenRecupSenha()
{
    url = new URL(window.location);
    url = String(url);
    var token = '';

    var spltUrl = url.split("?");
    if(spltUrl.length > 1)
        token = spltUrl[1].replace('solicitRec=', '');

    if(token.length == 15)
    {
        //VERIFICA SE TOKEN É VALIDO.
        $.ajax({
            method: "POST",
            url: "ManterUsuario.aspx/VerificaTokenRecupSenhaAsync",
            data: '{token: "' + token + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);
            //Se verifficação retorna verdadeiro, carrega os campos para a recuperação de senha.
            if(retorno.d.Cpf)
            {
                $('.cmpRecEmail').hide();
                $('.cmpsRecAcesso').show();
                document.getElementById("lblLogin").style.display = 'none';
                document.getElementById("lblLogoRec").style.display = 'inline-block';
                mudaPassos(7);
                //carrega o cpf do usuario, para a futura atualização da senha.
                $('#hddCpfUsuario').val(retorno.d.Cpf);
            }else
            {
                $('.cmpRecEmail').show();
                $('.cmpsRecAcesso').hide();
            }                
        });
    }else{    
        $('.cmpRecEmail').show();
        $('.cmpsRecAcesso').hide();
    }
}

function validarCNPJ() {
        
    var c = document.getElementById("txtEmpCNPJ").value;
    var b = [6,5,4,3,2,9,8,7,6,5,4,3,2];

    if((c = c.replace(/[^\d]/g,"")).length != 14)
        return false;

    if(/0{14}/.test(c))
        return false;

    for (var i = 0, n = 0; i < 12; n += c[i] * b[++i]);
    if(c[12] != (((n %= 11) < 2) ? 0 : 11 - n))
        return false;

    for (var i = 0, n = 0; i <= 12; n += c[i] * b[i++]);
    if(c[13] != (((n %= 11) < 2) ? 0 : 11 - n))
        return false;

    return true;
}

function CampoValido(campo, nmSpan) {
    $("#" + nmSpan).hide();
    $(campo).css("border-color", "");

}

function validaPasso1() {

    var validarPassou = true;
    var cpf = document.getElementById("txtCPF");
    
    if(cpf.value == "" || cpf.value.length < 2){
        $('#txtCPF').css("border-color", "red");
        $('#SPTxtCPF').text("Insira um CPF válido!");
        $("#SPTxtCPF").show();
        cpf.focus();
        validarPassou = false;
    } else if (validarCPF() == false) {
        $('#txtCPF').css("border-color", "red");
        $("#SPTxtCPF").show();
        cpf.focus();
        validarPassou = false;
    } else {
        $("#SPTxtCPF").hide();
        $('#txtCPF').css("border-color", "");
        $("#SPTxtCPF").innerHTML = "";
    }

    var nome = document.getElementById("txtNomeC");
    if (nome.value == "" || nome.value.length < 2) {
        $("#txtNomeC").css("border-color", "red");
        $("#SPTxtNomeC").show();
        nome.focus();
        validarPassou = false;
    } else {
        $("#SPTxtNomeC").hide();
        $('#txtNomeC').css("border-color", "");
        $("#SPTxtNomeC").innerHTML = "";
    }

    var btnMasc = document.getElementById("rdMasc").checked;
    var btnFem = document.getElementById("rdFem").checked;
    
    if (btnMasc != true && btnFem != true) {
        $("#btnMasc").css("border-color", "red");
        $("#btnFem").css("border-color", "red");
        $("#SPGen").show();
        validarPassou = false;
    } else {
        $("#SPGen").innerHTML = "";
        $("#btnMasc").css("border-color", "");
        $("#btnFem").css("border-color", "");
        $("#SPGen").hide();
    }

    var txtDtNascimento = document.getElementById("txtDtNascimento");
    var date = txtDtNascimento.value;
    var dtAtual = new Date();
    var dia = dtAtual.getDate();
    var mes = dtAtual.getMonth()+1;
    var ano = dtAtual.getFullYear();
    var str_ano = dia + '/' + mes + '/' + ano;
    var arrDT = new Array;
    var arrDT2 = new Array;
    arrDT = date.split("/");
    arrDT2 = str_ano.split("/");
    if(txtDtNascimento.value == "" || txtDtNascimento.value.length < 1){
        $("#txtDtNascimento").css("border-color", "red");
        $("#SPTxtDtNascimento").show();
        $("#SPTxtDtNascimento").text("Insira uma data de nascimento!");
        validarPassou = false;
    } else if (txtDtNascimento != "" ){
        if (arrDT[0] > 31){
            $("#txtDtNascimento").css("border-color", "red");
            $("#SPTxtDtNascimento").show();
            $("#SPTxtDtNascimento").text("Data de nascimento inválida, por favor verifique!");
            validarPassou = false;
        }

        if (arrDT[1] > 12) {
            $("#txtDtNascimento").css("border-color", "red");
            $("#SPTxtDtNascimento").show();
            $("#SPTxtDtNascimento").text("Data de nascimento inválida, por favor verifique!");
            validarPassou = false;
        }

        
        if (arrDT[2] > ano) {
            $("#txtDtNascimento").css("border-color", "red");
            $("#SPTxtDtNascimento").show();
            $("#SPTxtDtNascimento").text("Data de nascimento inválida, por favor verifique!");
            validarPassou = false;
        }

        // Comparação se o mês digitado é maior que o mês atual
        if (arrDT[1] > arrDT2[1]) {
            // Comparação se o ano digitado é maior ou igual ao mês atual.
            if (arrDT[2] >= ano) {
                $("#txtDtNascimento").css("border-color", "red");
                $("#SPTxtDtNascimento").show();
                $("#SPTxtDtNascimento").text("Data de nascimento não pode ser maior ou a mesma de hoje!");
                validarPassou = false;
            }
        } 
        //Comparação se o ano digitado é maior ou o mesmo que o ano atual.
        if (arrDT[2] == ano) {
            //Comparação se o mês digitado é maior ou o mesmo que o mês atual.
            if (arrDT[1] >= arrDT2[1]) {
                //Comparação se o dia digitado é o maior ou o mesmo que o dia atual.
                if (arrDT[0] >= arrDT2[0]) {
                    $("#txtDtNascimento").css("border-color", "red");
                    $("#SPTxtDtNascimento").show();
                    $("#SPTxtDtNascimento").text("Data de nascimento não pode ser maior ou a mesma de hoje!");
                    validarPassou = false;
                }}}
    }
    else {
        $("#txtDtNascimento").css("border-color", "");
        $("#SPTxtDtNascimento").hide();
        $("#SPTxtDtNascimento").text("");
    }
    if (validarPassou == true) {
        return true;
    }
}


function validaPasso2(){

    var validarPassou = true;

    var txtEmail = document.getElementById("txtEmail");
    var posP = txtEmail.value.indexOf(".");
    var posA = txtEmail.value.indexOf("@");

    if (txtEmail.value == "" || txtEmail.value.length < 2 || txtEmail.value == "usuario@dominio.com.br"){
        $("#txtEmail").css("border-color", "red");
        $("#SPTxtEmail").show();
        $("#SPTxtEmail").text("Insira um endereço de email!");
        validarPassou = false;
    } else if (posA == -1) {
        $("#txtEmail").css("border-color", "red");
        $("#SPTxtEmail").text("Email Inválido, por favor verifique!");
        $("#SPTxtEmail").show();
        validarPassou = false;
    } else if (posP == -1) {
        $("#txtEmail").css("border-color", "red");
        $("#SPTxtEmail").text("Email Inválido, por favor verifique!");
        $("#SPTxtEmail").show();
        validarPassou = false;
    } else {
        $("#txtEmail").css("border-color", "");
        $("#SPTxtEmail").hide();
        $("#SPTxtEmail").text("");
    }   

    var txtcelular = document.getElementById("txtCelular").value.replace(/[^\d]+/g, '');
    if(txtcelular == "" || txtcelular.length < 1 ) {
        $("#SPTxtCelular").text = "Insira um número de celular!";
        $("#SPTxtCelular").show();
        $("#txtCelular").css("border-color", "red");
        validarPassou = false;
    } else if (txtcelular.length < 11){
        $("#SPTxtCelular").text("Número inválido, verifique por favor!");
        $("#SPTxtCelular").show();
        $("#txtCelular").css("border-color", "red");
        validarPassou = false;
    } 
    else {
        $("#SPTxtCelular").text = "";
        $("#SPTxtCelular").hide();
        $("#txtCelular").css("border-color", "");
    }

    var senha = document.getElementById("txtSenha");
    var confSenha = document.getElementById("txtConfSenha");
    if(senha.value == "" && confSenha.value ==  "" || senha.value.length < 1 && confSenha.value.length < 1 ) {
        $("#SPTxtSenha").text = "Insira uma senha!";
        $("#SPTxtConfSenha").text = "Insira uma senha!";
        $("#SPTxtConfSenha").show();
        $("#SPTxtSenha").show();
        $("#txtSenha").css("border-color", "red");
        $("#txtConfSenha").css("border-color", "red");
        validarPassou = false;
    } else if (confSenha.value != senha.value){
        $("#SPTxtConfSenha").text = "As senhas não conferem, por favor verifique!";
        $("#SPTxtConfSenha").show();
        $("#txtConfSenha").css("border-color", "red");
        validarPassou = false;
    } else {
        $("#SPTxtSenha").text = "";
        $("#SPTxtConfSenha").text = "";
        $("#SPTxtConfSenha").hide();
        $("#SPTxtSenha").hide();
        $("#txtSenha").css("border-color", "");
        $("#txtConfSenha").css("border-color", "");
    } 

    if (validarPassou == true) {
        return true;
    }
}

function validaPasso3() {
    
    var validarPassou = true;

    var txtNomeFantasia = document.getElementById("txtEmpNomeFt");
    if(txtNomeFantasia.value == "" || txtNomeFantasia.value.length < 2) {
        $("#txtEmpNomeFt").css("border-color", "red");
        $("#SPTxtNomeFT").show();
        $("#SPTxtNomeFT").text("Insira um nome fantasia para a empresa à cadastrar!");
        validarPassou = false;
    } else {
        $("#txtEmpNomeFt").css("border-color", "");
        $("#SPTxtNomeFT").hide();
        $("#SPTxtNomeFT").text("");
    }

    var txtRazaoSocial = document.getElementById("txtEmpRazaoSc");
    if(txtRazaoSocial.value == "" || txtRazaoSocial.value.length < 2) {
        $("#txtEmpRazaoSc").css("border-color", "red");
        $("#SPTxtEmpRazaoSc").show();
        $("#SPTxtEmpRazaoSc").text("Insira a razão social da empresa à cadastrar!");
        validarPassou = false;
    } else {
        $("#txtEmpRazaoSc").css("border-color", "");
        $("#SPTxtEmpRazaoSc").hide();
        $("#SPTxtEmpRazaoSc").text("");
    }

    var txtcnpj = document.getElementById("txtEmpCNPJ");
    if(txtcnpj.value == "" || txtcnpj.value.length < 2) {
        $("#txtEmpCNPJ").css("border-color", "red");
        $("#SPTxtEmpCNPJ").show();
        $("#SPTxtEmpCNPJ").text("Preencha o CNPJ da empresa!");
        validarPassou = false;
    } else {
        if(validarCNPJ() == false) {
            $("#txtEmpCNPJ").css("border-color", "red");
            $("#SPTxtEmpCNPJ").show();
            $("#SPTxtEmpCNPJ").text("CNPJ inválido, por favor verifique!");
            validarPassou = false;
        }
        else {
            $("#txtEmpCNPJ").css("border-color", "");
            $("#SPTxtEmpCNPJ").hide();
            $("#SPTxtEmpCNPJ").text("");
        }
    }

    var txtTelCom = document.getElementById("txtEmpCom").value.replace(/[^\d]+/g, '');

    if(txtTelCom == "" || txtTelCom.length < 1 ) {
        $("#SPTxtEmpCom").text = "Insira um telefone comercial!";
        $("#SPTxtEmpCom").show();
        $("#txtEmpCom").css("border-color", "red");
        validarPassou = false;
    } else if (txtTelCom.length < 9 ) {
        $("#SPTxtEmpCom").text("Número inválido, por favor verifique!");
        $("#SPTxtEmpCom").show();
        $("#txtEmpCom").css("border-color", "red");
        validarPassou = false;
    } 
    else {
        $("#SPTxtEmpCom").text = "";
        $("#SPTxtEmpCom").hide();
        $("#txtEmpCom").css("border-color", "");
    }

    var txtEmpCel = document.getElementById("txtEmCel").value.replace(/[^\d]+/g, '');

    if (txtEmpCel != "") {
        if (txtEmpCel.length < 11 ) {
            $("#SPTxtEmCel").text("Número inválido, por favor verifique!");
            $("#SPTxtEmCel").show();
            $("#txtEmCel").css("border-color", "red");
            validarPassou = false;
        } 
    } else {
        $("#SPTxtEmCel").text = "";
        $("#SPTxtEmCel").hide();
        $("#txtEmCel").css("border-color", "");
    }

    if (validarPassou == true) {
        return true;
    }
}

function validaPasso4(){
    var validarPassou = true;

    var txtCEP = document.getElementById("txtEmpCEP");
    if(txtCEP.value == "" || txtCEP.value.length < 1 ) {
        $("#SPTxtCEP").text = "Insira o CEP!";
        $("#SPTxtCEP").show();
        $("#txtEmpCEP").css("border-color", "red");
        validarPassou = false;
    } else {
        $("#SPTxtCEP").text = "";
        $("#SPTxtCEP").hide();
        $("#txtEmpCEP").css("border-color", "");
    }

    var txtLogradouro = document.getElementById("txtEmpLogr");
    if(txtLogradouro.value == "" || txtLogradouro.value.length < 1 ) {
        $("#SPTxtLogr").text = "Insira o logradouro da empresa!";
        $("#SPTxtLogr").show();
        $("#txtEmpLogr").css("border-color", "red");
        validarPassou = false;
    } else {
        $("#SPTxtLogr").text = "";
        $("#SPTxtLogr").hide();
        $("#txtEmpLogr").css("border-color", "");
    }

    var txtNum = document.getElementById("txtEmpNum");
    if(txtNum.value == "" || txtNum.value.length < 1 ) {
        $("#SPTxtNum").text = "Insira o número do logradouro da empresa!";
        $("#SPTxtNum").show();
        $("#txtEmpNum").css("border-color", "red");
        validarPassou = false;
    } else {
        $("#SPTxtNum").text = "";
        $("#SPTxtNum").hide();
        $("#txtEmpNum").css("border-color", "");
    }

    var txtCidade = document.getElementById("txtEmpCidade");
    if(txtCidade.value == "" || txtCidade.value.length < 1 ) {
        $("#SPTxtCidade").text = "Insira a cidade da empresa!";
        $("#SPTxtCidade").show();
        $("#txtEmpCidade").css("border-color", "red");
        validarPassou = false;
    } else {
        $("#SPTxtCidade").text = "";
        $("#SPTxtCidade").hide();
        $("#txtEmpCidade").css("border-color", "");
    }

    var txtUf = document.getElementById("txtEmpUF");
    if(txtUf.value == "" || txtUf.value.length < 1 ) {
        $("#SpTxtUf").text = "Insira a cidade da empresa!";
        $("#SpTxtUf").show();
        $("#txtEmpUF").css("border-color", "red");
        validarPassou = false;
    } else {
        $("#SpTxtUf").text = "";
        $("#SpTxtUf").hide();
        $("#txtEmpUF").css("border-color", "");
    }

    var txtBairro = document.getElementById("txtEmpBairro");
    if(txtCidade.value == "" || txtCidade.value.length < 1 ) {
        $("#SPTxtBairro").text = "Insira a cidade da empresa!";
        $("#SPTxtBairro").show();
        $("#txtEmpBairro").css("border-color", "red");
        validarPassou = false;
    } else {
        $("#SPTxtBairro").text = "";
        $("#SPTxtBairro").hide();
        $("#txtEmpBairro").css("border-color", "");
    }
    if (validarPassou == true) {
        return true;
    }
}

function ExibeLoading()
{
    dvCadastro.style.display = 'none';
    dvLogin.style.display = 'none';
    //dvCadEmpresa.style.display = 'none';
    //dvEndEmpresa.style.display = 'none';
    dvRecSenha.style.display = 'none';
    $('#loader').show();
}


function ExibeMsgFinal(mensagem)
{
    var dvCadastro = document.getElementById("dvCadastro");
    var dvLogin = document.getElementById("dvLogin");
    //var dvCadEmpresa = document.getElementById("dvCadEmpresa");
    //var dvEndEmpresa = document.getElementById("dvEndEmpresa");
    var dvRecSenha = document.getElementById("dvRecSenha");
    var dvSucesso = document.getElementById("dvSucesso");

    var lblUser = document.getElementById("lblLogoUser");
    var lblCadEmp = document.getElementById("lblLogoEmp");
    var lblEndEmp = document.getElementById("lblLogoEnd");
    var lblLogin = document.getElementById("lblLogin");
    var lblRec = document.getElementById("lblLogoRec");

    $('#loader').hide();
    dvCadastro.style.display = 'none';
    dvLogin.style.display = 'none';
    //dvCadEmpresa.style.display = 'none';
    //dvEndEmpresa.style.display = 'none';
    dvRecSenha.style.display = 'none';
    
    vlrEmail = $('#txtEmail').val();
    $('#dvmsgSucesso').html(mensagem);             
    dvSucesso.style.display = 'block';

    lblUser.style.display = 'none';
    lblCadEmp.style.display = 'none';
    lblEndEmp.style.display = 'none';
    lblLogin.style.display = 'none';
    $('#form1 input[type = text]').val("");
}


//Troca tela dos forms.
function mudaPassos(passo) {

    var dvCadastro = document.getElementById("dvCadastro");
    var dvLogin = document.getElementById("dvLogin");
    //var dvCadEmpresa = document.getElementById("dvCadEmpresa");
    //var dvEndEmpresa = document.getElementById("dvEndEmpresa");
    var dvRecSenha = document.getElementById("dvRecSenha");
    var dvSucesso = document.getElementById("dvSucesso");

    var lblUser = document.getElementById("lblLogoUser");
    var lblCadEmp = document.getElementById("lblLogoEmp");
    var lblEndEmp = document.getElementById("lblLogoEnd");
    var lblLogin = document.getElementById("lblLogin");
    var lblRec = document.getElementById("lblLogoRec");
    $('#loader').hide();

    $(".input-material").css("border-color", "");
    $('.required-error').hide();
    validarPassou = true;
    switch (passo) {

        //Passo 1 Tela de Cadastro do usuário
        case "1":
        case 1:                        
            dvCadastro.style.display = 'block';
            dvLogin.style.display = 'none';            
            dvSucesso.style.display = 'none';
            lblUser.style.display = 'inline-block';
            lblCadEmp.style.display = 'none';
            lblEndEmp.style.display = 'none';
            lblLogin.style.display = 'none';
            
            $('#txtNomeC').focus();
            break;

            // Passo 2 Tela de Cadastro da Empresa
        case "2":
        case 2:
            if (validaPasso2()) {
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'none';
                //dvCadEmpresa.style.display = 'block';
                //dvEndEmpresa.style.display = 'none';
                dvSucesso.style.display = 'none';

                lblUser.style.display = 'none';
                lblCadEmp.style.display = 'inline-block';
                lblEndEmp.style.display = 'none';
                lblLogin.style.display = 'none';
            } else {
                dvCadastro.style.display = 'block';
                dvLogin.style.display = 'none';
                //dvCadEmpresa.style.display = 'none';
                //dvEndEmpresa.style.display = 'none';
                dvSucesso.style.display = 'none';

                lblUser.style.display = 'inline-block';
                lblCadEmp.style.display = 'none';
                lblEndEmp.style.display = 'none';
                lblLogin.style.display = 'none';
            }
            break;

            //Passo 3 Tela de Cadastro do Endereço Empresa
        case "3":
        case 3:
            if(validaPasso3()){
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'none';
                //dvCadEmpresa.style.display = 'none';
                //dvEndEmpresa.style.display = 'block';
                dvSucesso.style.display = 'none';

                FocaCamposPreenchidos();

                lblUser.style.display = 'none';
                lblCadEmp.style.display = 'none';
                lblEndEmp.style.display = 'inline-block';
                lblLogin.style.display = 'none';
                break;
            } else {
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'none';
                //dvCadEmpresa.style.display = 'block';
                //dvEndEmpresa.style.display = 'none';
                dvSucesso.style.display = 'none';

                lblUser.style.display = 'none';
                lblCadEmp.style.display = 'inline-block';
                lblEndEmp.style.display = 'none';
                lblLogin.style.display = 'none';
                break;
            }

            //Passo 4 Tela de Login usuário.
        case "4":
        case 4:
            if(validaPasso4()){
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'block';
                //dvCadEmpresa.style.display = 'none';
                //dvEndEmpresa.style.display = 'none';
                dvSucesso.style.display = 'none';

                lblUser.style.display = 'none';
                lblCadEmp.style.display = 'none';
                lblEndEmp.style.display = 'none';
                lblLogin.style.display = 'inline-block';
                break;
            } else{
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'none';
                //dvCadEmpresa.style.display = 'none';
                //dvEndEmpresa.style.display = 'block';
                dvSucesso.style.display = 'block';

                lblUser.style.display = 'none';
                lblCadEmp.style.display = 'none';
                lblEndEmp.style.display = 'inline-block';
                lblLogin.style.display = 'none';
                break;
            }            
            //Volta Login
        case "6":
        case 6:            
            var urlCalculo = 'http://' + window.location.hostname + ':' + window.location.port + '/Acesso/ManterUsuario.aspx';            
            window.location.replace(urlCalculo);            
            break;

            //Exibe rec. senha
        case "7":
        case 7:
            dvCadastro.style.display = 'none';
            dvLogin.style.display = 'none';
            //dvCadEmpresa.style.display = 'none';
            //dvEndEmpresa.style.display = 'none';
            dvRecSenha.style.display = 'block';

            lblUser.style.display = 'none';
            lblCadEmp.style.display = 'none';
            lblEndEmp.style.display = 'none';
            lblLogin.style.display = 'none';
            lblRec.style.display = 'inline-block';
            break;          
    }
}

function telaMsgFinal(msg){
    dvCadastro.style.display = 'none';
    dvLogin.style.display = 'none';
    //dvCadEmpresa.style.display = 'none';
    //dvEndEmpresa.style.display = 'none';
    vlrEmail = $('#txtEmail').val();
    $('#dvmsgSucesso').html(msg);
    dvSucesso.style.display = 'block';
    lblLogin.style.display = 'none';
    $('#loader').hide();
}

const FocaCamposPreenchidos = () => {
    if($('#txtEmpLogr').val()) 
        $('#txtEmpLogr').focus();
if($('#txtEmpNum').val()) 
    $('#txtEmpNum').focus();
if($('#txtEmpComp').val()) 
    $('#txtEmpComp').focus();
if($('#txtEmpCidade').val()) 
    $('#txtEmpCidade').focus();
if($('#txtEmpBairro').val()) 
    $('#txtEmpBairro').focus();
if($('#txtEmpUF').val()) 
    $('#txtEmpUF').focus();
if($('#txtEmpCEP').val()) 
    $('#txtEmpCEP').focus();
};


function mudaSubPassos(passo) {

    var dvCadastro1 = document.getElementById("dvCadastro1");
    var dvCadastro2 = document.getElementById("dvCadastro2");

    //var dvCadEmpresa1 = document.getElementById("//dvCadEmpresa1");
    //var dvCadEmpresa2 = document.getElementById("//dvCadEmpresa2");

    //var dvEndEmpresa1 = document.getElementById("//dvEndEmpresa1");
    //var dvEndEmpresa2 = document.getElementById("//dvEndEmpresa2");


    switch (passo) {

        //Caso de ida
        case "1":
        case 1:

            dvCadastro1.style.display = 'block';
            dvCadastro2.style.display = 'none';
            break;

        case "2":
        case 2:            
            if (validaPasso1()) {
                dvCadastro1.style.display = 'none';
                dvCadastro2.style.display = 'block';
            } else {
                dvCadastro.style.display = 'block';
                dvLogin.style.display = 'none';
            }
            break;

            //Caso de volta
        case "3":
        case 3:
            //dvCadEmpresa1.style.display = 'none';
            dvCadastro2.style.display= 'block';

            break;
    }
}
