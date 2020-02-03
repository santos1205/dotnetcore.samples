<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="slnSindicatoMedico.Usuario.Login" %>

<!DOCTYPE html>

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="robots" content="all,follow" />
    <link rel="shortcut icon" href="../ContentAdm/img/logo/FAVICON.png" />
    <link rel="stylesheet" href="../ContentAdm/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../ContentAdm/css/fontastic.css" />
    <link rel="stylesheet" href="../ContentAdm/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,700" />
    <link rel="stylesheet" href="../ContentAdm/css/style.default.css" id="theme-stylesheet" />
    <link rel="stylesheet" href="../ContentAdm/css/custom.css" />

    <style>
        .input-file-container {
            position: relative;
            width: 150px;
        }

        .form-group-padding {
            position: relative;
            margin-bottom: 14px;
        }

        .js .input-file-trigger {
            display: block;
            padding: 10px 35px;
            background: #0d408f;
            color: #fff;
            font-size: 1em;
            transition: all .4s;
            cursor: pointer;
            border-radius: 10px;
        }

        .js .input-file {
            position: absolute;
            top: 0;
            left: 0;
            width: 225px;
            opacity: 0;
            padding: 14px 0;
            cursor: pointer;
        }

            .js .input-file:hover + .input-file-trigger,
            .js .input-file:focus + .input-file-trigger,
            .js .input-file-trigger:hover,
            .js .input-file-trigger:focus {
                background: #34495E;
                color: #39D2B4;
            }

        .file-return {
            margin: 0;
        }

            .file-return:not(:empty) {
                margin: 0em 0;
            }

        .js .file-return {
            font-style: italic;
            font-size: 15px;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="page login-page">
            <div class="container d-flex align-items-center">
                <div class="form-holder has-shadow" style="margin-top: 3%">
                    <div class="row">
                        <%-- Logo --%>
                        <div class="col-lg-6" id="dvLogo">
                            <div class="info d-flex align-items-center">
                                <div class="content">
                                    <div class="col-md-12 footer-logo">
                                        <img src="../ContentAdm/img/logo/logoSindmedico.png" style="width: 100%; cursor: pointer;" onclick="mudaPassos(8)" alt="" />
                                        <img src="" style="width: 100%; cursor: pointer;" onclick="mudaPassos(8)" alt="" />
                                        <div class="form-group-padding" style="width: 100%;">
                                            <h4 id="lblLogoUser" style="display: none;">Cadastro de usuário</h4>
                                            <h4 id="lblLogoEmp" style="display: none;">Cadastro de empresa</h4>
                                            <h4 id="lblLogoEnd" style="display: none;">Cadastro de endereço</h4>
                                            <h4 id="lblLogoRec" style="display: none;">Recuperação de senha</h4>
                                            <h4 id="lblLogin" style="display: inline-block;">Login</h4>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- Form de cadastro e login --%>
                        <div class="col-lg-6 bg-white">
                            <div class="form d-flex align-items-center">
                                <div class="content">
                                    <div class="loader" style="display: none;"></div>
                                    <div id="dvBemVindo"></div>
                                    <div id="dvCadastro" style="display: none;">
                                        <%--Cadastro --%>
                                        <div id="dvCadastro1">
                                            <div class="form-group-padding">                                                
                                                <div class="row" style="padding: 2%;">
                                                    <div class="col-md-6" style="padding: 1%; margin-top: 2px">
                                                        <asp:TextBox ID="txtCPF" runat="server" type="text" name="registerCpf" onKeyPress="MascaraCPF(this)" CssClass="input-material" MaxLength="14"
                                                            onkeydown="CampoValido(this,'SPTxtCPF');" value="" />
                                                        <label for="txtCPF" class="label-material">Cpf</label>
                                                        <span id="SPTxtCPF" class="required-error" style="display: none;">Cpf inválido</span>
                                                    </div>
                                                    <div class="col-md-6" style="padding: 1%;">                                                        
                                                        <asp:DropDownList ID="dpdDepto" CssClass="dropdown-material" runat="server">
                                                            <asp:ListItem Value="0" Text="Departamento"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Comercial"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Financeiro"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <span id="SPtxtDepto" class="required-error" style="display: none;">Departamento obrigatório!</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group-padding">
                                                <asp:TextBox ID="txtNomeUsuario" runat="server" type="text" name="registerUsername" CssClass="input-material cmpsUsuario" MaxLength="80"
                                                    onkeydown="CampoValido(this,'SPtxtNomeUsuario');" value="" />
                                                <label for="txtNomeUser" class="label-material">Nome Completo</label>
                                                <span id="SPtxtNomeUsuario" class="required-error" style="display: none;">Nome completo obrigatório!</span>
                                            </div>
                                            
                                            <div class="form-group-padding">
                                                <div class="row" style="padding: 2%;">
                                                    <div class="col-md-6" style="padding: 1%;">
                                                        <label class="btn btn-default" id="btnMasc" style="width: 100%;" onclick="CampoValido(this,'SPGen');">
                                                            <input type="radio" id="rdMasc" name="rdGenero" style="display: none;" value="1" />
                                                            <img class="padrao-iconeM" src="../ContentAdm/img/icons/iconHOMEM.png" style="width: 40px; height: 30px;" />
                                                            <img class="padrao-invertM" src="../ContentAdm/img/icons/iconHOMEM-invert.png" style="width: 40px; height: 30px;" />
                                                            Masculino
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="padding: 1%;">
                                                        <label class="btn btn-default" id="btnFem" style="width: 100%;" onclick="CampoValido(this,'SPGen');">
                                                            <input type="radio" id="rdFem" name="rdGenero" style="display: none;" value="2" />
                                                            <img class="padrao-iconeF" src="../ContentAdm/img/icons/iconmulher.png" style="width: 40px; height: 30px;" />
                                                            <img class="padrao-invertF" src="../ContentAdm/img/icons/iconmulher-invert.png" style="width: 40px; height: 30px;" />
                                                            Feminino
                                                        </label>
                                                    </div>
                                                    <span id="SPGen" class="required-error" style="display: none">Selecione o sexo!</span>
                                                </div>
                                            </div>

                                            <div class="form-group-padding">
                                                <asp:TextBox ID="txtEmail" runat="server" type="text" name="registerEmail" CssClass="input-material" MaxLength="50"
                                                    onkeydown="CampoValido(this,'SPTxtEmail');" value="" />
                                                <label for="txtEmail" class="label-material">E-mail</label>
                                                <span id="SPTxtEmail" class="required-error" style="display: none;">E-mail obrigatório</span>
                                            </div>


                                            <div class="form-group-padding">
                                                <asp:TextBox ID="txtSenha" runat="server" type="password" name="registerSenha" CssClass="input-material" MaxLength="50"
                                                    onkeydown="CampoValido(this,'SPTxtSenha');" value="" />
                                                <label for="txtSenha" class="label-material">Senha</label>
                                                <span id="SPTxtSenha" class="required-error" style="display: none;">Senha obrigatório</span>

                                            </div>
                                            <div class="form-group-padding">
                                                <asp:TextBox ID="txtConfSenha" runat="server" type="password" name="registerConfSenha" CssClass="input-material" MaxLength="50"
                                                    onkeydown="CampoValido(this,'SPTxtConfSenha');" value="" />
                                                <label for="txtConfSenha" class="label-material">Confirmar Senha</label>
                                                <span id="SPTxtConfSenha" class="required-error" style="display: none;">Senha não confere</span>
                                            </div>

                                            <div class="form-group-padding">
                                                <a class="signup" style="float: left; cursor: pointer;" onclick="mudaPassos(6)">Voltar à tela de login</a>
                                                <a class="signup" id="btnCadastrar" style="float: right; cursor: pointer;">Cadastrar</a>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Login--%>
                                    <div id="dvLogin" style="display: block">
                                        <div class="form-group-padding">
                                            <input type="text" id="txtLoginUser" class="input-material" MaxLength="14" onKeyPress="MascaraCPF(this)" 
                                                onkeydown="CampoValido(this, 'SPtxtLoginUser')" autocomplete="false" />                                            
                                            <label for="txtLoginUser" class="label-material">Cpf</label>
                                            <span id="SPtxtLoginUser" class="required-error" style="display: none;">Login do usuário é obrigatório!</span>
                                        </div>
                                        <div class="form-group-padding">
                                            <input type="password" id="txtSenhaUser" class="input-material" MaxLength="14" onkeydown="CampoValido(this, 'SPtxtLoginUser')" autocomplete="false" />
                                            <label for="txtSenhaUser" class="label-material">Senha</label>
                                            <span id="SPtxtSenhaUser" class="required-error" style="display: none;">Senha é obrigatório!</span>
                                            <span id="spErroLogin" class="required-error" style="display: none">Login ou Senha inválidos.</span>
                                        </div>
                                        <div class="form-group-padding">
                                            <button type="button" id="btnLogin" class="btn btn-primary" style="background-color: #045531;border-color: #045531;color: white">Entrar</button>
                                        </div>
                                        <div class="form-group-padding">
                                            <a class="signup" style="cursor: pointer;" onclick="mudaPassos(7)">Esqueci minha senha</a><br />
                                            <small>Não tem uma conta ainda? </small><a class="signup" style="cursor: pointer;" onclick="mudaPassos(1)">Cadastre-se</a>
                                        </div>
                                    </div>
                                    <%--Recuperação de Senha--%>
                                    <div id="dvRecSenha" style="display: none">
                                        <div class="form-group cmpRecEmail">
                                            <asp:TextBox ID="txtCPFRec" runat="server" type="text" name="registerCpf" CssClass="input-material mascaraCPF" MaxLength="15"
                                                onkeydown="CampoValido(this,'SPTxtCPF');" value="" />
                                            <label for="txtCPFRec" class="label-material">CPF cadastrado:</label>
                                            <span id="SPtxtCPFRec" class="required-error" style="display: none;">Cpf inválido</span>
                                        </div>
                                        <div class="form-group cmpsRecAcesso">
                                            <input type="hidden" id="hddCpfUsuario" />
                                            <asp:TextBox ID="txtNvSenha" runat="server" type="password" name="registerNvSenha" CssClass="input-material"
                                                onkeydown="CampoValido(this, 'SPNvSenha')" MaxLength="50" value="" />
                                            <label for="txtNvSenha" class="label-material">Nova senha</label>
                                            <span id="SPNvSenha" class="required-error" style="display: none;">Senha é obrigatório!</span>
                                        </div>
                                        <div class="form-group cmpsRecAcesso">
                                            <asp:TextBox ID="txtCnfNvSenha" runat="server" type="password" name="registerCnfNvSenha" CssClass="input-material"
                                                onkeydown="CampoValido(this, 'SPCfNvSenha')" MaxLength="50" value="" />
                                            <label for="txtCnfNvSenha" class="label-material">Confirmar nova senha</label>
                                            <span id="SPCfNvSenha" class="required-error" style="display: none;">Senha não confere!</span>
                                        </div>
                                        <div class="form-group-padding">
                                            <a class="signup" style="float: left; cursor: pointer;" onclick="mudaPassos(6)">Voltar a tela de login</a>
                                            <a class="signup cmpsRecAcesso" style="float: right; cursor: pointer;" onclick="SalvarSenha()">Confirmar</a>
                                            <a class="signup cmpRecEmail" style="float: right; cursor: pointer;" onclick="RecuperarSenha()">Enviar</a>
                                        </div>
                                    </div>
                                    <%--Container de msgs--%>
                                    <div id="dvSucesso" style="display: none;">
                                        <div id="dvmsgSucesso"></div>
                                        <br />
                                        <div class="form-group-padding">
                                            <a class="signup" style="float: left; cursor: pointer;" onclick="mudaPassos(6)">Voltar a tela de login</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>


    <!-- Javascript files-->
    <script>        
        document.querySelector("html").classList.add('js');
        var fileInput = document.querySelector(".input-file"),
            button = document.querySelector(".input-file-trigger"),
            the_return = document.querySelector(".file-return");
    </script>

    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="../ContentAdm/jquery.cookie/jquery.cookie.js"> </script>
    <script src="../ContentAdm/jquery-validation/jquery.validate.min.js"></script>
    <script src="../ContentAdm/js/front.js"></script>
    <script src="../ContentAdm/jquery/jquery.maskedinput.js"></script>
    <script src="../ContentAdm/jquery/jquery.cpf-validate.1.0.min.js"></script>
    <script src="../ContentAdm/js/ValidacoesCamposForm.js"></script>
    <script src="../ContentAdm/js/ManterUsuario.js"></script>
</body>
</html>