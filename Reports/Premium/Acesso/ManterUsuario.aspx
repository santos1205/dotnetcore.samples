<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManterUsuario.aspx.cs" Inherits="Premium.Acesso.ManterUsuario" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Login</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="robots" content="all,follow">

    <link rel="stylesheet" href="../Content/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="../Content/css/fontastic.css">
    <link rel="stylesheet" href="../Content/font-awesome/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,700">
    <link rel="stylesheet" href="../Content/css/style.default.css" id="theme-stylesheet">
    <link rel="stylesheet" href="../Content/css/custom.css">
    <link rel="shortcut icon" href="../Content/img/favicon.png" />

    <!-- Tweaks for older IEs-->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div class="page login-page">
            <div class="container d-flex align-items-center">
                <div class="form-holder has-shadow">
                    <div class="row">

                        <%-- Logo --%>
                        <div class="col-lg-6" id="dvLogo">
                            <div class="info d-flex align-items-center">
                                <div class="content">
                                    <div class="col-md-12 footer-logo">
                                        <img src="../Content/img/logoPremiumBranca2.png" alt="" />
                                        <div class="form-group" style="width: 100%;">
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
                                    <div id="loader" style="display: none;"></div>
                                    <div id="dvBemVindo"></div>

                                    <div id="dvCadastro" style="display: none;">
                                        <div id="dvCadastro1">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtNomeC" runat="server" type="text" name="registerUsername" onKeyPress="somenteLetras()" CssClass="input-material" MaxLength="80"
                                                    onkeydown="CampoValido(this,'SPTxtNomeC');" value="" />
                                                <label for="txtNomeUser" class="label-material">Nome Completo</label>
                                                <span id="SPTxtNomeC" class="required-error" style="display: none;">Nome completo obrigatório!</span>
                                            </div>
                                            <div class="form-group">
                                                <div class="row" style="padding: 2%;">
                                                    <div class="col-md-6" style="padding: 1%;">
                                                        <asp:TextBox ID="txtCPF" runat="server" type="text" name="registerCpf" onKeyPress="MascaraCPF(this)" CssClass="input-material" MaxLength="14"
                                                            onkeydown="CampoValido(this,'SPTxtCPF');" value="" />
                                                        <label for="txtCPF" class="label-material">Cpf</label>
                                                        <span id="SPTxtCPF" class="required-error" style="display: none;">Cpf inválido</span>
                                                    </div>
                                                    <div class="col-md-6" style="padding: 1%;">
                                                        <asp:TextBox ID="txtDtNascimento" runat="server" type="text" name="registerDteNasc" CssClass="input-material cmpsUsuario"
                                                            MaxLength="10" onkeydown="CampoValido(this,'SPTxtDtNascimento');" value="" />
                                                        <label for="txtDtNascimento" class="label-material">Data de Nascimento</label>
                                                        <span id="SPTxtDtNascimento" class="required-error" style="display: none;">Data de nascimento obrigatório</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row" style="padding: 2%;">
                                                    <div class="col-md-6" style="padding: 1%;">
                                                        <asp:TextBox ID="txtEmail" runat="server" type="text" name="registerEmail" CssClass="input-material cmpsUsuario" MaxLength="50"
                                                            onkeydown="CampoValido(this,'SPTxtEmail');" value="" />
                                                        <label for="txtEmail" class="label-material">E-mail</label>
                                                        <span id="SPTxtEmail" class="required-error" style="display: none;">E-mail obrigatório</span>
                                                    </div>
                                                    <div class="col-md-6" style="padding: 1%;">
                                                        <asp:TextBox ID="txtTelefone" runat="server" type="text" name="registerEmpCel" CssClass="input-material celular cmpsUsuario" MaxLength="14" value="" />
                                                        <label for="txtTelefone" class="label-material">Telefone</label>
                                                        <span id="SPTxtTelefone" class="required-error" style="display: none;">Telefone é obrigatório.</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="label-material">Gênero</label>
                                                <div class="row" style="padding: 2%;">
                                                    <div class="col-md-6" style="padding: 1%;">
                                                        <label class="btn btn-default" id="btnMasc" style="width: 100%;" onclick="CampoValido(this,'SPGen');">
                                                            <input type="radio" id="rdMasc" name="rdGenero" style="display: none;" value="1" />
                                                            <img class="padrao-iconeM" src="../Content/img/icons/iconHOMEM.png" style="width: 40px; height: 30px;" />
                                                            <img class="padrao-invertM" src="../Content/img/icons/iconHOMEM-invert.png" style="width: 40px; height: 30px;" />
                                                            Masculino
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="padding: 1%;">
                                                        <label class="btn btn-default" id="btnFem" style="width: 100%;" onclick="CampoValido(this,'SPGen');">
                                                            <input type="radio" id="rdFem" name="rdGenero" style="display: none;" value="2" />
                                                            <img class="padrao-iconeF" src="../Content/img/icons/iconmulher.png" style="width: 40px; height: 30px;" />
                                                            <img class="padrao-invertF" src="../Content/img/icons/iconmulher-invert.png" style="width: 40px; height: 30px;" />
                                                            Feminino
                                                        </label>
                                                    </div>
                                                    <span id="SPGen" class="required-error" style="display: none">Selecione o gênero!</span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row" style="padding: 2%;">
                                                    <div class="col-md-6" style="padding: 1%;">
                                                        <asp:TextBox ID="txtSenha" runat="server" type="password" name="registerSenha" CssClass="input-material cmpsUsuario" MaxLength="50"
                                                            onkeydown="CampoValido(this,'SPTxtSenha');" value="" />
                                                        <label for="txtSenha" class="label-material">Senha</label>
                                                        <span id="SPTxtSenha" class="required-error" style="display: none;">Senha obrigatório</span>
                                                    </div>
                                                    <div class="col-md-6" style="padding: 1%;">
                                                        <asp:TextBox ID="txtConfSenha" runat="server" type="password" name="registerConfSenha" CssClass="input-material cmpsUsuario" MaxLength="50"
                                                            onkeydown="CampoValido(this,'SPTxtConfSenha');" value="" />
                                                        <label for="txtConfSenha" class="label-material">Confirmar Senha</label>
                                                        <span id="SPTxtConfSenha" class="required-error" style="display: none;">Senha não confere</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <a class="signup" style="float: left; cursor: pointer;" onclick="mudaPassos(6)">Voltar à tela de login</a>
                                                <a class="signup" id="btnCadastrar" style="float: right; cursor: pointer;">Cadastrar</a>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="dvLogin" style="display: block">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtLoginUser" runat="server" type="text" name="registerLoginUser" onKeyPress="MascaraCPF(this)" CssClass="input-material" MaxLength="14"
                                                onkeydown="CampoValido(this, 'SPtxtLoginUser')" value="" />
                                            <label for="txtLoginUser" class="label-material">Cpf</label>
                                            <span id="SPtxtLoginUser" class="required-error" style="display: none;">Login do usuário é obrigatório!</span>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtSenhaUser" runat="server" type="password" name="registerSenhaUser" CssClass="input-material"
                                                onkeydown="CampoValido(this, 'SPtxtSenhaUser')" MaxLength="50" value="" />
                                            <label for="txtSenhaUser" class="label-material">Senha</label>
                                            <span id="SPtxtSenhaUser" class="required-error" style="display: none;">Senha é obrigatório!</span>
                                            <span id="spErroLogin" class="required-error" style="display: none">Login ou Senha inválidos.</span>
                                        </div>
                                        <div class="form-group">
                                            <button type="button" id="btnLogin" class="btn btn-primary">Entrar</button>
                                        </div>
                                        <div class="form-group">
                                            <a class="signup" style="cursor: pointer;" onclick="mudaPassos(7)">Esqueci minha senha</a><br />
                                            <small>Não tem uma conta ainda? </small><a class="signup" style="cursor: pointer;" onclick="mudaPassos(1)">Cadastre-se</a>
                                        </div>
                                    </div>

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
                                        <div class="form-group">
                                            <a class="signup" style="float: left; cursor: pointer;" onclick="mudaPassos(6)">Voltar a tela de login</a>
                                            <a class="signup cmpsRecAcesso" style="float: right; cursor: pointer;" onclick="SalvarSenha()">Confirmar</a>
                                            <a class="signup cmpRecEmail" style="float: right; cursor: pointer;" onclick="RecuperarSenha()">Enviar</a>
                                        </div>
                                    </div>

                                    <div id="dvSucesso" style="display: none;">
                                        <div id="dvmsgSucesso">
                                            Pronto! Agora é só aguardar que enviaremos um e-mail para você com o acesso ao sistema Seguro Energia Solar.
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <a class="signup" style="float: left; cursor: pointer;" onclick="mudaPassos(6)">Voltar a tela de login</a>
                                        </div>
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
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="../Content/jquery.cookie/jquery.cookie.js"> </script>
    <script src="../Content/jquery-validation/jquery.validate.min.js"></script>
    <script src="../Content/js/front.js"></script>
    <script src="../Content/jquery/jquery.maskedinput.js"></script>
    <script src="../Content/jquery/jquery.cpf-validate.1.0.min.js"></script>
    <script src="../Content/js/ValidacoesCamposForm.js"></script>

    <script src="../Content/js/ManterUsuario.js"></script>
</body>
</html>
