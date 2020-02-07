$(document).ready(function () {
    $('#txtSenhaUser').focus();
    $('#txtLoginUser').focus();


    $('#txtNomeUsuario').blur(function () {
        let nome = $(this).val();
        let Titlelized = PrimeiraLetraMaiuscula(nome);
        $(this).val(Titlelized);
    });

    //Corrige rota em prod.
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)
    if (url.includes('www.sindicatomedico.vertce.com.br'))
        if (!url.includes('login.aspx') && !url.includes('Login.aspx')) {
            var urlLogin = 'http://www.sindicatomedico.vertce.com.br/Usuario/Login.aspx';
            window.location.replace(urlLogin);
        }

        
    //Se veio valor no token, verifica se está valido para abrir a tela de recuperar senha.    
    VerificaTokenRecupSenha();
    //Verifica se upload da logo foi feito com sucesso, afim de exibir a msg. de sucesso.
    VerificaUploadLogo();


    //Verifica se cpf existe na base quando informado. Caso exista, bloqueia o formulário.
    $("#txtCPF").change(function () {
        VerificarCpfNaBase();
    });
    
    $("#btnLogin").click(function () {
        EfetuarLogin();
    });
});


$("#txtEmail").blur(function () {
    //console.log('verificando email')
    VerificaEmailCadastro();
});


$("#myfile").change(function () {
    //console.log('alterou');
    $('#dvFileReturn').show();
});


//Consulta a empresa pelo cnpj, caso exista preenche o formulário com os dados.
$("#txtEmpCNPJ").blur(function () {
    //retira a máscara
    var vlrCnpj = $("#txtEmpCNPJ").val().replace(/[^\d]+/g, '');
    $.ajax({
        method: "POST",
        url: "Login.aspx/ConsultaEmpresaPorCnpjAsync",
        data: '{cnpj: "' + vlrCnpj + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);
        //Verifica se tem erro
        if (retorno.d.Erro != null) {
            alert('Ocorreu erro na verificação do cnpj.');
            console.log(retorno.d.Erro);
        }
        //Caso exista a empresa, preenche o formulário com os dados e bloqueia os campos para não serem editados.
        if (retorno.d.NomeFantasia) {
            $('#hddEmpresaId').val(retorno.d.IdEmpresa);
            $('#txtEmpNomeFt').val(retorno.d.NomeFantasia);
            $("#txtEmpNomeFt").prop("readonly", true);
            $('#txtEmpNomeFt').focus();
            $('#txtEmpRazaoSc').val(retorno.d.RazaoSocial);
            $("#txtEmpRazaoSc").prop("readonly", true);
            $('#txtEmpRazaoSc').focus();
            $('#txtEmCel').val(retorno.d.Celular);
            $("#txtEmCel").prop("readonly", true);
            $('#txtEmCel').focus();
            $('#txtEmpCom').val(retorno.d.Telefone);
            $("#txtEmpCom").prop("readonly", true);
            $('#txtEmpCom').focus();
            $('#txtEmpLogr').val(retorno.d.Logradouro);
            $("#txtEmpLogr").prop("readonly", true);
            $('#txtEmpLogr').focus();
            $('#txtEmpNum').val(retorno.d.LogradouroNumero);
            $("#txtEmpNum").prop("readonly", true);
            $('#txtEmpNum').focus();
            $('#txtEmpComp').val(retorno.d.ComplementoEndereco);
            $("#txtEmpComp").prop("readonly", true);
            $('#txtEmpComp').focus();
            $('#txtEmpCidade').val(retorno.d.Cidade);
            $("#txtEmpCidade").prop("readonly", true);
            $('#txtEmpCidade').focus();
            $('#txtEmpBairro').val(retorno.d.Bairro);
            $("#txtEmpBairro").prop("readonly", true);
            $('#txtEmpBairro').focus();
            $('#txtEmpUF').val(retorno.d.Uf);
            $("#txtEmpUF").prop("readonly", true);
            $('#txtEmpUF').focus();
            $('#txtEmpCEP').val(retorno.d.Cep);
            $("#txtEmpCEP").prop("readonly", true);
            $('#txtEmpCEP').focus();
        } else {
            //caso o cnpj não exista na base, abilita o form. para cadastro.
            $('.cmpsEmpresa').prop("readonly", false);
            $('.cmpsEndereco').prop("readonly", false);
        }
    })
});


$("#dpdDepto").change(function () {
    $("#SPtxtDepto").hide();
    $('#dpdDepto').css("border-color", "");
    $("#SPtxtDepto").innerHTML = "";
});

//Consulta endereço pelo cep, caso exista preenche o formulário com os dados.
$("#txtEmpCEP").change(function () {
    //Se campo estiver somente leitura, não atualiza, pois endereço já veio da entidade empresa.
    if ($('#txtEmpCEP').is('[readonly]'))
        return;

    //retira a máscara
    var vlrcep = $("#txtEmpCEP").val().replace(/[^\d]+/g, '');
    $.ajax({
        method: "POST",
        url: "Login.aspx/ConsultaEnderecoEmpresaAsync",
        data: '{cep: "' + vlrcep + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);
        //Caso exista a empresa, preenche o formulário com os dados e bloqueia os campos para não serem editados.
        if (retorno.d.Logradouro) {
            $('#txtEmpLogr').val(retorno.d.Logradouro);
            $('#txtEmpLogr').focus();
            $('#txtEmpCidade').val(retorno.d.Cidade);
            $('#txtEmpCidade').focus();
            $('#txtEmpUF').val(retorno.d.Uf);
            $('#txtEmpUF').focus();
            $('#txtEmpBairro').val(retorno.d.Bairro);
            $('#txtEmpBairro').focus();
        }
    });
});


$("#btnCadastrar").click(function () {    
    if (validaForm()) 
        CadastrarUsuario();    
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


    $('.mascaraCPF2').on('keydown.mask').focusout(function () {
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


    //Funções anônimas em jQuery que são ativadas de acordo com a ação/evento acionados.
    $(document).on('click', "#btnMasc", function () {

        $('#btnFem').removeClass("active");
        $('#btnMasc').addClass("active");

    });

    $(document).on('click', "#btnFem", function () {
        $('#btnMasc').removeClass("active");
        $('#btnFem').addClass("active");
    });

    $(document).on('click', "#lkConfSenha", function () {
        var validarPassou = true;

        var Nvsenha = $('#txtNvSenha').val();
        var confNvSenha = $('#txtCnfNvSenha').val();
        if (Nvsenha == "" && confNvSenha == "" || Nvsenha.length < 2 && confNvSenha.length < 2) {
            $("#SPNvSenha").text("Insira uma senha!");
            $("#SPCfNvSenha").text("Insira uma senha!");
            $("#SPCfNvSenha").show();
            $("#SPNvSenha").show();
            $("#txtNvSenha").css("border-color", "red");
            $("#txtCnfNvSenha").css("border-color", "red");
            validarPassou = false;
        } else if (confNvSenha != Nvsenha) {
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


function validaLogin() {
    var validarPassou = true;

    var txtLogin = $('#txtLoginUser').val();
    var txtSenha = $('#txtSenhaUser').val();

    if (txtLogin == "" || txtLogin.length < 2) {
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
    }
    else {
        $("#SPtxtSenhaUser").text("");
        $("#SPtxtSenhaUser").hide();
        $("#txtSenhaUser").css("border-color", "");
    }

    return validarPassou;
}

function validaCadastro()
{
    var validarPassou = true;
    var txtSenha = $('#txtSenha').val();

    if (txtSenha.length < 6) {
        $("#SPTxtConfSenha").text("A senha deve ter no minímo 6 caracteres, incluindo números, ao menos uma letra maiúscula e ao menos um caractere especial");
        $("#SPTxtConfSenha").show();
        $("#SPTxtConfSenha").css("border-color", "red");
        validarPassou = false;
    }

    if (!txtSenha.match(/[A-Z]+/)) {
        $("#SPTxtConfSenha").text("A senha deve ter no minímo 6 caracteres, incluindo números, ao menos uma letra maiúscula e  menos um caractere especial");
        $("#SPTxtConfSenha").show();
        $("#SPTxtConfSenha").css("border-color", "red");
        validarPassou = false;
    }
    if (!txtSenha.match(/[0-9]+/)) {
        $("#SPTxtConfSenha").text("A senha deve ter no minímo 6 caracteres, incluindo números, ao menos uma letra maiúscula e  menos um caractere especial");
        $("#SPTxtConfSenha").show();
        $("#SPTxtConfSenha").css("border-color", "red");
        validarPassou = false;
    }

    if (!txtSenha.match(/[\^\"!#$%&'()*+,-./:;?@[\\\]_`´{|}~¨¿♥]/)) {
        $("#SPTxtConfSenha").text("A senha deve ter no minímo 6 caracteres, incluindo números, ao menos uma letra maiúscula e  menos um caractere especial");
        $("#SPTxtConfSenha").show();
        $("#SPTxtConfSenha").css("border-color", "red");
        validarPassou = false;
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


function SalvarSenha() {
    var validarPassou = true;

    var Nvsenha = $('#txtNvSenha').val();
    var confNvSenha = $('#txtCnfNvSenha').val();
    if (Nvsenha == "" && confNvSenha == "" || Nvsenha.length < 2 && confNvSenha.length < 2) {
        $("#SPNvSenha").text("Insira uma senha!");
        $("#SPCfNvSenha").text("Insira uma senha!");
        $("#SPCfNvSenha").show();
        $("#SPNvSenha").show();
        $("#txtNvSenha").css("border-color", "red");
        $("#txtCnfNvSenha").css("border-color", "red");
        validarPassou = false;
    } else if (confNvSenha != Nvsenha) {
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

        if (Nvsenha.length < 6) {
            $("#SPCfNvSenha").text("A senha deve ter no minímo 6 caracteres, incluindo números, ao menos uma letra maiúscula e ao menos um caractere especial");
            $("#SPCfNvSenha").show();
            $("#txtCnfNvSenha").css("border-color", "red");
            validarPassou = false;
        }

        if (!Nvsenha.match(/[A-Z]+/)) {
            $("#SPCfNvSenha").text("A senha deve ter no minímo 6 caracteres, incluindo números, ao menos uma letra maiúscula e ao menos um caractere especial");
            $("#SPCfNvSenha").show();
            $("#txtCnfNvSenha").css("border-color", "red");
            validarPassou = false;
        }
        if (!Nvsenha.match(/[0-9]+/)) {
            $("#SPCfNvSenha").text("A senha deve ter no minímo 6 caracteres, incluindo números, ao menos uma letra maiúscula e ao menos um caractere especial");
            $("#SPCfNvSenha").show();
            $("#txtCnfNvSenha").css("border-color", "red");
            validarPassou = false;
        }

        if (!Nvsenha.match(/[\^\"!#$%&'()*+,-./:;?@[\\\]_`´{|}~¨¿♥]/)) {
            $("#SPCfNvSenha").text("A senha deve ter no minímo 6 caracteres, incluindo números, ao menos uma letra maiúscula e ao menos um caractere especial");
            $("#SPCfNvSenha").show();
            $("#txtCnfNvSenha").css("border-color", "red");
            validarPassou = false;
        }
    }
   
    if (validarPassou == true) {
        $.ajax({
            method: "POST",
            url: "Login.aspx/SalvarRecuperacaoSenhaAsync",
            data: '{cpf: "' + $('#hddCpfUsuario').val() + '", novaSenha: "' + $('#txtCnfNvSenha').val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);  
            ExibeMsgFinal(retorno.d);
        })
    }
}

function VerificaEmailCadastro() {
    $.ajax({
        method: "POST",
        url: "Login.aspx/VerificaEmailCadastradoAsync",
        data: '{email: "' + $('#txtEmail').val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        //console.log(retorno.d);          
        //Se email é cadastrado, exibe msg. de erro
        if (retorno.d) {
            $("#txtEmail").css("border-color", "red");
            $("#SPTxtEmail").text("Ops, identificamos que este e-mail já está cadastrado.");
            $("#SPTxtEmail").show();
                        
            $("#txtSenha").val('');
            $("#txtSenha").prop("readonly", true);
            $("#txtConfSenha").val('');
            $("#txtConfSenha").prop("readonly", true);
        } else {
            $("#SPTxtEmail").hide();            
            $("#txtSenha").prop("readonly", false);
            $("#txtConfSenha").prop("readonly", false);
        }
    })
}

function RecuperarSenha() {
    var vlrCpf = $('#txtCPFRec').val();
    vlrCpf = vlrCpf.replace(/[^\d]+/g, '');
    if (verificarCPF(vlrCpf)) {
        ExibeLoading();
        $.ajax({
            method: "POST",
            url: "Login.aspx/RecuperarSenhaAsync",
            data: '{cpf: "' + vlrCpf + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);              
            if (retorno.d != null) {
                if (retorno.d.includes('Erro')) {
                    //alert(retorno.d);
                    ExibeMsgFinal('Erro durante a redefinição de senha.');
                    console.log(retorno.d);                        
                    return;
                }

                if (retorno.d == 'Não foi encontrado registro com o cpf informado.') {
                    mudaPassos(7);
                    $('#txtCPFRec').css("border-color", "red");
                    $('#SPtxtCPFRec').text(retorno.d);
                    $("#SPtxtCPFRec").show();
                    return;
                }                
                ExibeMsgFinal(retorno.d);   
            }
        })
    } else {
        $('#txtCPFRec').css("border-color", "red");
        $('#SPtxtCPFRec').text("Insira um CPF válido!");
        $("#SPtxtCPFRec").show();
    }
}


function VerificaUploadLogo() {
    url = new URL(window.location);
    url = String(url);
    var idLogo = '';
    var erroLogo = '';

    var spltUrl = url.split("?");
    if (spltUrl.length > 1) {
        if (spltUrl[1].includes('uploadErro')) {
            //método getUrlParameter() encontra-se no arquivo ValidacoesCamposForm.js
            erroLogo = getUrlParameter('uploadErro');
            idEmpresa = getUrlParameter('idEmpresa');
            nomeEmpresa = getUrlParameter('nomeEmpresa');
            $('#hddEmpresaId').val(idEmpresa);
            $('#hddNomeEmpresa').val(nomeEmpresa);
            //erroLogo = spltUrl[1].replace('uploadErro=', '');
            //erroLogo = encodeURIComponent(erroLogo);
            $('#dvmsgSucesso').html('<font color="red">Ocorreu um erro ao enviar a logo: ' + erroLogo + '</font>');
            mudaPassos(11);
            return;
        }
        idLogo = getUrlParameter('idLogo');
    }


    if (idLogo) {
        $.ajax({
            method: "POST",
            url: "Login.aspx/VerificaUploadLogoAsync",
            data: '{idLogo: "' + idLogo + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            if (retorno.d.IdLogo) {
                //console.log(retorno.d);                
                mudaPassos(10);
            }
        });
    }
}

function VerificaTokenRecupSenha() {
    url = new URL(window.location);
    url = String(url);
    var token = '';

    var spltUrl = url.split("?");
    if (spltUrl.length > 1)
        token = spltUrl[1].replace('solicitRec=', '');

    if (token.length == 15) {           // significa a redefinição de senha por solicitação do usuário
        //VERIFICA SE TOKEN É VALIDO.
        $.ajax({
            method: "POST",
            url: "Login.aspx/VerificaTokenRecupSenhaAsync",
            data: '{token: "' + token + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            console.log(retorno.d);
            //Se verifficação retorna verdadeiro, carrega os campos para a recuperação de senha.
            if (retorno.d.Cpf) {
                $('.cmpRecEmail').hide();
                $('.cmpsRecAcesso').show();
                document.getElementById("lblLogin").style.display = 'none';
                document.getElementById("lblLogoRec").style.display = 'inline-block';
                mudaPassos(7);
                //carrega o cpf do usuario, para a futura atualização da senha.
                $('#hddCpfUsuario').val(retorno.d.Cpf);
            } else {
                $('.cmpRecEmail').show();
                $('.cmpsRecAcesso').hide();
            }
            });
    } else if (token.length == 32) {      // significa a redefinição de senha por padronização de segurança
        //VERIFICA SE TOKEN É VALIDO.
        $.ajax({
            method: "POST",
            url: "Login.aspx/VerificaTokenPadraoSenhaAsync",
            data: '{token: "' + token + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            console.log(retorno.d);
            //Se verifficação retorna verdadeiro, carrega os campos para a recuperação de senha.
            if (retorno.d.length > 0) {
                $('.cmpRecEmail').hide();
                $('.cmpsRecAcesso').show();
                document.getElementById("lblLogin").style.display = 'none';
                document.getElementById("lblLogoRec").style.display = 'inline-block';
                mudaPassos(7);
                //carrega o cpf do usuario, para a futura atualização da senha.
                $('#hddCpfUsuario').val(retorno.d);
            } else {
                $('.cmpRecEmail').show();
                $('.cmpsRecAcesso').hide();
            }
        });
    } else {
        $('.cmpRecEmail').show();
        $('.cmpsRecAcesso').hide();
    }
}

function validarCNPJ() {

    var c = document.getElementById("txtEmpCNPJ").value;
    var b = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    if ((c = c.replace(/[^\d]/g, "")).length != 14)
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


function validaForm() {
    var validarPassou = true;
    var cpf = document.getElementById("txtCPF");

    if (cpf.value == "" || cpf.value.length < 2) {
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

    var txtEmail = document.getElementById("txtEmail");
    var posP = txtEmail.value.indexOf(".");
    var posA = txtEmail.value.indexOf("@");

    if (txtEmail.value == "" || txtEmail.value.length < 2 || txtEmail.value == "usuario@dominio.com.br") {
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
    
    var dpdDepto = document.getElementById("dpdDepto");
    if (dpdDepto.value === '0') {
        $("#dpdDepto").css("border-color", "red");
        $("#SPtxtDepto").text("Selecione o departamento!");
        $("#SPtxtDepto").show();
        validarPassou = false;
    } else {
        $("#SPtxtDepto").hide();
        $('#dpdDepto').css("border-color", "");
        $("#SPtxtDepto").innerHTML = "";
    }

    
    var nome = document.getElementById("txtNomeUsuario");
    if (nome.value == "" || nome.value.length < 2) {
        $("#txtNomeUsuario").css("border-color", "red");
        $("#SPtxtNomeUsuario").show();
        nome.focus();
        validarPassou = false;
    } else {
        $("#SPtxtNomeUsuario").hide();
        $('#txtNomeUsuario').css("border-color", "");
        $("#SPtxtNomeUsuario").innerHTML = "";
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


    var senha = document.getElementById("txtSenha");
    var confSenha = document.getElementById("txtConfSenha");
    if (senha.value == "" && confSenha.value == "" || senha.value.length < 1 && confSenha.value.length < 1) {
        $("#SPTxtSenha").text = "Insira uma senha!";
        $("#SPTxtConfSenha").text = "Insira uma senha!";
        $("#SPTxtConfSenha").show();
        $("#SPTxtSenha").show();
        $("#txtSenha").css("border-color", "red");
        $("#txtConfSenha").css("border-color", "red");
        validarPassou = false;
    } else if (confSenha.value != senha.value) {
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


function ExibeLoading() {
    dvCadastro.style.display = 'none';
    dvLogin.style.display = 'none';    
    dvRecSenha.style.display = 'none';
    $('.loader').show();
}

function EscondeLoader() {
    dvCadastro.style.display = 'block';    
    $('.loader').hide();
}



function ExibeMsgFinal(mensagem) {
    var dvCadastro = document.getElementById("dvCadastro");
    var dvLogin = document.getElementById("dvLogin");    
    var dvRecSenha = document.getElementById("dvRecSenha");
    var dvSucesso = document.getElementById("dvSucesso");

    var lblUser = document.getElementById("lblLogoUser");
    var lblCadEmp = document.getElementById("lblLogoEmp");
    var lblEndEmp = document.getElementById("lblLogoEnd");
    var lblLogin = document.getElementById("lblLogin");
    var lblRec = document.getElementById("lblLogoRec");

    EscondeLoader();    
    dvCadastro.style.display = 'none';
    dvLogin.style.display = 'none';    
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

function CadastrarUsuario() {
    //retira a máscara
    var vlrCPF = $('#txtCPF').val().replace(/[^\d]+/g, '');
    var vlrGenero = $("#rdMasc").is(":checked") ? 'M' : 'F';
    var cadValido = false;

    cadValido = validaCadastro();

    if (cadValido) {
        //As propriedades dos objetos (json/c#) tem que ser iguais.
        var data = {
            objUsuario: {
                usr_nome: $('#txtNomeUsuario').val(),
                usr_cpf: vlrCPF,
                usr_email: $('#txtEmail').val(),
                usr_genero: vlrGenero,
                usr_senha: $('#txtSenha').val(),
                usr_nvl_id: $('#dpdDepto').val(),
            }
        };

        ExibeLoading();

        $.ajax({
            method: "POST",
            url: "Login.aspx/CadastraUsuario",
            crossDomain: true,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (resp) {
            //console.log(resp.d);
            if (resp.d.MsgErro != null) {
                ExibeMsgFinal('Ocorreu um erro durante o cadastro do usuário. Tente novamente mais tarde');
                console.log(resp.d.Msg);
            }
            if (resp.d.MsgSenha != null) {
                ExibeMsgFinal('Senha deve ter 6 caracteres no minimo, incluindo números, ao menos uma letra maiúscula ,ao menos um caractere especial, retorne e tente fazer o cadastro novamente');
                console.log(resp.d.Msg);
            }
            else
                ExibeMsgFinal('Pronto! Agora é só aguardar que enviaremos um e-mail para você com o acesso ao sistema Sindicato Médico.');
        });
    }
}

function EfetuarLogin() {
    vlrUser = $('#txtLoginUser').val();
    vlrSenha = $('#txtSenhaUser').val();
    vlrUser = vlrUser.replace(/[^\d]+/g, '');
    $('#spErroLogin').hide();
    if (!validaLogin())
        return;
    ExibeLoading();
    $.ajax({
        method: "POST",
        url: "Login.aspx/VerificaLoginAsync",
        crossDomain: true,
        data: '{user: "' + vlrUser + '", senha: "' + vlrSenha + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        if (retorno.d.includes('erro')) {
            alert('Ocorreu um erro na tentativa de login.');
            console.log(retorno.d);
        }
        //se retorna true, é que foi logado.
        var str = retorno.d;
        var usuarioAprovado = str.split(":")[0];
        var acessoUsuario = str.split(":")[1];

        if (usuarioAprovado == 'A') {
            $('#form1 input[type = text]').val("");
            //Redirect
            if (acessoUsuario == 1) {
                var urlConsultaSegurado = 'http://' + window.location.hostname + ':' + window.location.port + '/Segurado/ConsultaSegurado.aspx';
                window.location.replace(urlConsultaSegurado);
            }
            if(acessoUsuario == 2)
            {
                var urlConsultaSegurado = 'http://' + window.location.hostname + ':' + window.location.port + '/Pagamentos/Pagamentos.aspx';
                window.location.replace(urlConsultaSegurado);
            }
            if (acessoUsuario == 3)
            {
                var urlConsultaSegurado = 'http://' + window.location.hostname + ':' + window.location.port + '/Gerencial/PainelGerencial.aspx';
                window.location.replace(urlConsultaSegurado);
            }
            
        } else if (usuarioAprovado == 'N' || usuarioAprovado == 'R') {
            dvCadastro.style.display = 'none';
            dvLogin.style.display = 'block';            
            dvRecSenha.style.display = 'none';
            dvSucesso.style.display = 'none';
            $('.loader').hide();
            $('#spErroLogin').text('Usuário pendente de aprovação.');
            $('#spErroLogin').show();
        } else if (vlrUser != "" && vlrSenha != "") {
            dvCadastro.style.display = 'none';
            dvLogin.style.display = 'block';            
            dvRecSenha.style.display = 'none';
            dvSucesso.style.display = 'none';
            $('.loader').hide();
            $('#spErroLogin').text(retorno.d);
            $('#spErroLogin').show();            
        }
    });
}


function VerificarCpfNaBase() {        
    $("#SPTxtCPF").text('Cpf inválido.');
    $("#SPTxtCPF").hide();

    var vlrCpf = $("#txtCPF").val();
    //retira a máscara
    vlrCpf = vlrCpf.replace(/[^\d]+/g, '');

    $.ajax({
        method: "POST",
        url: "Login.aspx/ConsultaUsuarioPorCpfAsync",
        data: '{cpf: "' + vlrCpf + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (retorno) {
        console.log(retorno.d);
        //Caso exista usuario cadastrado, exibir msg. e bloquear formulario.
        if (retorno.d != null) {
            $("#SPTxtCPF").show();
            $("#SPTxtCPF").text('Ops, identificamos que este CPF já está cadastrado.');
            $(".cmpsUsuario").val('');
            $(".cmpsUsuario").prop("readonly", true);
        } else {
            $(".cmpsUsuario").prop("readonly", false);
        }
    });
}


//Troca tela dos forms.
function mudaPassos(passo) {

    var dvCadastro = document.getElementById("dvCadastro");
    var dvLogin = document.getElementById("dvLogin");    
    var dvRecSenha = document.getElementById("dvRecSenha");
    var dvSucesso = document.getElementById("dvSucesso");

    var lblUser = document.getElementById("lblLogoUser");
    var lblCadEmp = document.getElementById("lblLogoEmp");
    var lblEndEmp = document.getElementById("lblLogoEnd");
    var lblLogin = document.getElementById("lblLogin");
    var lblRec = document.getElementById("lblLogoRec");
    $('.loader').hide();

    $(".input-material").css("border-color", "");
    $('.required-error').hide();
    validarPassou = true;
    switch (passo) {

        //Passo 1 Tela de Cadastro do usuário        
        case 1:
            dvCadastro.style.display = 'block';
            dvLogin.style.display = 'none';            
            dvSucesso.style.display = 'none';

            lblUser.style.display = 'inline-block';
            lblCadEmp.style.display = 'none';
            lblEndEmp.style.display = 'none';
            lblLogin.style.display = 'none';
            $('#txtCPF').focus();
            break;

        // Passo 2 Tela de Cadastro da Empresa        
        case 2:
            if (validaPasso2()) {
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'none';                
                dvSucesso.style.display = 'none';

                lblUser.style.display = 'none';
                lblCadEmp.style.display = 'inline-block';
                lblEndEmp.style.display = 'none';
                lblLogin.style.display = 'none';
            } else {
                dvCadastro.style.display = 'block';
                dvLogin.style.display = 'none';                
                dvSucesso.style.display = 'none';

                lblUser.style.display = 'inline-block';
                lblCadEmp.style.display = 'none';
                lblEndEmp.style.display = 'none';
                lblLogin.style.display = 'none';
            }
            break;

        //Passo 3 Tela de Cadastro do Endereço Empresa        
        case 3:
            if (validaPasso3()) {
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'none';                
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
                dvSucesso.style.display = 'none';
                lblUser.style.display = 'none';
                lblCadEmp.style.display = 'inline-block';
                lblEndEmp.style.display = 'none';
                lblLogin.style.display = 'none';
                break;
            }

        //Passo 4 Tela de Login usuário.        
        case 4:
            if (validaPasso4()) {
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'block';                
                dvSucesso.style.display = 'none';
                lblUser.style.display = 'none';
                lblCadEmp.style.display = 'none';
                lblEndEmp.style.display = 'none';
                lblLogin.style.display = 'inline-block';
                break;
            } else {
                dvCadastro.style.display = 'none';
                dvLogin.style.display = 'none';                
                dvSucesso.style.display = 'block';
                lblUser.style.display = 'none';
                lblCadEmp.style.display = 'none';
                lblEndEmp.style.display = 'inline-block';
                lblLogin.style.display = 'none';
                break;
            }
        //Exibe tela de msg. final!        
        case 5:
            // Se check enviar logo estiver marcado, abre view do upload
            if ($('#chkUpload').prop('checked')) {
                mudaPassos(9);
                break;
            }

            dvCadastro.style.display = 'none';
            dvLogin.style.display = 'none';            
            vlrEmail = $('#txtEmail').val();
            $('#dvmsgSucesso').html('Pronto! Agora é só aguardar que enviaremos um e-mail para <b>' + vlrEmail + '</b> com o acesso ao sistema Seguro Energia Solar.');
            dvSucesso.style.display = 'block';

            lblUser.style.display = 'none';
            lblCadEmp.style.display = 'none';
            lblEndEmp.style.display = 'none';
            lblLogin.style.display = 'none';
            $('#form1 input[type = text]').val("");
            break;

        //Volta Login        
        case 6:
            var url = 'http://' + window.location.hostname + ':' + window.location.port + '/Usuario/Login.aspx';
            window.location.replace(url);
            break;

        //Exibe rec. senha        
        case 7:
            dvCadastro.style.display = 'none';
            dvLogin.style.display = 'none';            
            dvRecSenha.style.display = 'block';
            lblUser.style.display = 'none';
            lblCadEmp.style.display = 'none';
            lblEndEmp.style.display = 'none';
            lblLogin.style.display = 'none';
            lblRec.style.display = 'inline-block';
            break;

        case 8:
            var url = 'http://' + window.location.hostname + ':' + window.location.port + '/Site/SiteIndex.aspx';
            window.location.replace(url);
            break;
        case 9:
            //Tratando o nome da empresa para ser enviado ao bkend, afim de registrar o nome da logo
            let nomeEmp = $('#txtEmpNomeFt').val().split(' ');
            nomeEmp = nomeEmp[0];
            $('#hddNomeEmpresa').val(nomeEmp);
            dvCadastro.style.display = 'none';
            dvLogin.style.display = 'none';            
            dvSucesso.style.display = 'none';
            dvUpload.style.display = 'block';

            lblUser.style.display = 'none';
            lblCadEmp.style.display = 'none';
            lblEndEmp.style.display = 'none';
            lblLogin.style.display = 'none';
            break;
        case 10:
            dvCadastro.style.display = 'none';
            dvLogin.style.display = 'none';            
            vlrEmail = $('#txtEmail').val();
            $('#dvmsgSucesso').html('Sua logo foi enviada com sucesso.<br>Pronto! Agora é só aguardar que enviaremos um e-mail com o acesso ao sistema Seguro Energia Solar.');
            dvSucesso.style.display = 'block';

            lblUser.style.display = 'none';
            lblCadEmp.style.display = 'none';
            lblEndEmp.style.display = 'none';
            lblLogin.style.display = 'none';
            $('#form1 input[type = text]').val("");
            break;
        case 11:
            dvCadastro.style.display = 'none';
            dvLogin.style.display = 'none';            
            vlrEmail = $('#txtEmail').val();
            dvSucesso.style.display = 'block';
            dvUpload.style.display = 'block';
            lblUser.style.display = 'none';
            lblCadEmp.style.display = 'none';
            lblEndEmp.style.display = 'none';
            lblLogin.style.display = 'none';
            $('#form1 input[type = text]').val("");
            break;
    }
}

const FocaCamposPreenchidos = () => {
    if ($('#txtEmpLogr').val())
        $('#txtEmpLogr').focus();
    if ($('#txtEmpNum').val())
        $('#txtEmpNum').focus();
    if ($('#txtEmpComp').val())
        $('#txtEmpComp').focus();
    if ($('#txtEmpCidade').val())
        $('#txtEmpCidade').focus();
    if ($('#txtEmpBairro').val())
        $('#txtEmpBairro').focus();
    if ($('#txtEmpUF').val())
        $('#txtEmpUF').focus();
    if ($('#txtEmpCEP').val())
        $('#txtEmpCEP').focus();
};


function mudaSubPassos(passo) {

    var dvCadastro1 = document.getElementById("dvCadastro1");
    var dvCadastro2 = document.getElementById("dvCadastro2");
       

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
            dvCadastro2.style.display = 'block';

            break;
    }
}
