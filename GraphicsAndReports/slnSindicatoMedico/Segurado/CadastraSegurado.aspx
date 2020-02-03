<%@ Page Title="Cadastrar Segurado" Language="C#" MasterPageFile="~/MasterPage/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="CadastraSegurado.aspx.cs" Inherits="slnSindicatoMedico.Segurado.CadastraSegurado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style type="text/css">
        #dvCadTitular, dvCadDependentes {
            margin: 0 auto;
        }

        #dvCadastraSegurado {
            padding: 0;
        }

        input:checked + .small-slider {
            background-color: green;
        }

        #dvCadastraSegurado > section {
            padding-top: 1%;
        }

        .card-menu > ul > li > .nav-link active {
            width: 300px;
            color: #495057;
            text-decoration: none;
            border: none;
        }

        .card-menu > ul > li > .nav-link {
            color: #fff;
            width: 300px;
            text-decoration: none;
            border: none !important;
        }

        .card-menu {
            margin-left: 15px;
            margin-bottom: -5px;
            background: rgba(4, 85, 49,0.7);
        }

            .card-menu > ul > .nav-item > .active:hover {
                color: rgb(4, 85, 49);
                text-decoration: none;
            }

            .card-menu > ul > .nav-item > a:hover {
                color: #ffc099;
                text-decoration: none;
                border-color: rgb(4, 85, 49);
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Cadastrar Segurado</h2>
            </div>
        </div>
    </header>

    <%-- Subtitulo - diretorio de navegação --%>
    <div class="breadcrumb-holder container-fluid ">
        <ul class="breadcrumb">
            <li class="breadcrumb-item" id="nav-form1">Segurado</li>
            <li class="breadcrumb-item" id="nav-form2">Cadastrar Segurado</li>
        </ul>
    </div>

    <div id="dvCadastraSegurado" class="container-fluid">
        <section>
            <div class="container-fluid">
                <div class="row">
                    <div class="card-header card-menu">
                        <ul class="nav nav-tabs card-header-tabs">
                            <li class="nav-item" style="margin-left: -6px">
                                <a class="nav-link active" href="#" id="btncadastrarSegurado">Cadastrar Segurado</a>
                            </li>
                            <li class="nav-item" style="margin-right: -6px">
                                <a class="nav-link" href="#" id="btncadastrarDependentes">Cadastrar Dependentes</a>
                            </li>
                        </ul>
                    </div>

                    <%--Dados do Titular--%>
                    <div id="dvCadTitular" class="col-lg-12 col-12">
                        <div class="card-body bg-white">
                            <div class="row col-md-12" style="margin: 0px; padding: 0px;">
                                <h3 class="col-md-6" style="padding: 0px;">Dados do Titular</h3>
                            </div>
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtCPFCadSegurado" runat="server" type="text"
                                            CssClass="input-material" MaxLength="14" onkeypress="MascaraCPF(this)" onkeydown="LimparDadosSegurado()" value=""></asp:TextBox>
                                        <label for="txtCPFCadSegurado" class="label-material">CPF</label>
                                        <span id="SPCPFCadSegurado" class="required-error" style="display: none">CPF inválido</span>
                                    </div>
                                    <div class="col-md-3" style="margin-top: 15px">
                                        <asp:TextBox ID="txtNumFiliacaoCadSegurado" runat="server" type="text" Style="width: 97.5%"
                                            CssClass="input-material" onkeypress="mascaraInteiro()" MaxLength="9" value=""></asp:TextBox>
                                        <label for="txtNumFiliacaoCadSegurado" class="label-material">Número da Filiação</label>
                                    </div>
                                    <div class="col-md-3" style="margin-top: 15px">
                                        <asp:TextBox ID="txtPropostaCadSegurado" runat="server" type="text" Style="width: 97.5%"
                                            CssClass="input-material" onkeypress="mascaraInteiro()" MaxLength="9" value=""></asp:TextBox>
                                        <label for="txtPropostaCadSegurado" class="label-material">Número da Proposta</label>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" style="margin-top: 25px">
                                        <asp:TextBox ID="txtNomeCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPNomeCadSegurado')"
                                            CssClass="input-material" value=""></asp:TextBox>
                                        <label for="txtNomeCadSegurado" class="label-material">Nome do Segurado</label>
                                        <span id="SPNomeCadSegurado" class="required-error" style="display: none">Nome obrigatório</span>
                                    </div>
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Origem do Segurado</font>
                                        <select id="cmbSeguradora" class="dropdown-material">
                                            <option value="" selected="selected">-- Selecione --</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3" style="margin-top: 25px">
                                        <asp:TextBox ID="txtCRMCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPCRMCadSegurado')"
                                            CssClass="input-material" onkeypress="mascaraInteiro()" MaxLength="10" value=""></asp:TextBox>
                                        <label for="txtCRMCadSegurado" class="label-material">CRM (número)</label>
                                        <span id="SPCRMCadSegurado" class="required-error" style="display: none">CMR obrigatório</span>
                                    </div>
                                    <div class="col-md-3">
                                        <font color="#0d408f" size="2px">CRM (estado)</font>
                                        <asp:DropDownList ID="cmbUFCRMCadSegurado" CssClass="dropdown-material" runat="server">
                                            <asp:ListItem Value="" Text="-- Selecione --" Selected></asp:ListItem>
                                            <asp:ListItem Value="AC" Text="AC"></asp:ListItem>
                                            <asp:ListItem Value="AL" Text="AL"></asp:ListItem>
                                            <asp:ListItem Value="AP" Text="AP"></asp:ListItem>
                                            <asp:ListItem Value="AM" Text="AM"></asp:ListItem>
                                            <asp:ListItem Value="BA" Text="BA"></asp:ListItem>
                                            <asp:ListItem Value="CE" Text="CE"></asp:ListItem>
                                            <asp:ListItem Value="DF" Text="DF"></asp:ListItem>
                                            <asp:ListItem Value="ES" Text="ES"></asp:ListItem>
                                            <asp:ListItem Value="GO" Text="GO"></asp:ListItem>
                                            <asp:ListItem Value="MA" Text="MA"></asp:ListItem>
                                            <asp:ListItem Value="MT" Text="MT"></asp:ListItem>
                                            <asp:ListItem Value="MS" Text="MS"></asp:ListItem>
                                            <asp:ListItem Value="MG" Text="MG"></asp:ListItem>
                                            <asp:ListItem Value="PA" Text="PA"></asp:ListItem>
                                            <asp:ListItem Value="PB" Text="PB"></asp:ListItem>
                                            <asp:ListItem Value="PR" Text="PR"></asp:ListItem>
                                            <asp:ListItem Value="PE" Text="PE"></asp:ListItem>
                                            <asp:ListItem Value="PI" Text="PI"></asp:ListItem>
                                            <asp:ListItem Value="RJ" Text="RJ"></asp:ListItem>
                                            <asp:ListItem Value="RN" Text="RN"></asp:ListItem>
                                            <asp:ListItem Value="RS" Text="RS"></asp:ListItem>
                                            <asp:ListItem Value="RO" Text="RO"></asp:ListItem>
                                            <asp:ListItem Value="RR" Text="RR"></asp:ListItem>
                                            <asp:ListItem Value="SC" Text="SC"></asp:ListItem>
                                            <asp:ListItem Value="SP" Text="SP"></asp:ListItem>
                                            <asp:ListItem Value="SE" Text="SE"></asp:ListItem>
                                            <asp:ListItem Value="TO" Text="TO"></asp:ListItem>
                                        </asp:DropDownList>
                                        <span id="SPcmbUFCRMCadSegurado" class="required-error" style="display: none">UF do CRM obrigatória</span>
                                    </div>
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Especialidade</font>
                                        <select id="cmbEspecCadSegurado" class="dropdown-material">
                                            <option value="" selected="selected">-- Selecione --</option>
                                        </select>
                                        <span id="SPEspecCadSegurado" class="required-error" style="display: none">Especialidade obrigatória</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Sexo</font>
                                        <select id="cmbSexoCadSegurado" class="dropdown-material">
                                            <option value="" selected="selected">-- Selecione --</option>
                                            <option value="M">Masculino</option>
                                            <option value="F">Feminino</option>
                                        </select>
                                        <span id="SPSexoCadSegurado" class="required-error" style="display: none">Sexo obrigatório</span>
                                    </div>
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Estado Civil</font>
                                        <select id="cmbCivilCadSegurado" class="dropdown-material">
                                        </select>
                                        <span id="SPCivilCadSegurado" class="required-error" style="display: none">Estado Civil obrigatório</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" style="margin-top: 10px">
                                        <asp:TextBox ID="txtNomeMaeCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPNomeMaeCadSegurado')"
                                            CssClass="input-material" MaxLength="100" value=""></asp:TextBox>
                                        <label for="txtNomeMaeCadSegurado" class="label-material">Nome da Mãe</label>
                                        <span id="SPNomeMaeCadSegurado" class="required-error" style="display: none">Nome da Mãe obrigatório</span>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 10px">
                                        <asp:TextBox ID="txtDataNascCadSegurado" runat="server" type="text" onKeyPress="MascaraData(this)" CssClass="input-material"
                                            MaxLength="10" onkeydown="CampoValido(this,'SPDataNascCadSegurado');" value="" />
                                        <label for="txtDtNascimento" class="label-material">Data de Nascimento</label>
                                        <span id="SPDataNascCadSegurado" class="required-error" style="display: none;">Data de nascimento obrigatória</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2" style="margin-top: 15px">
                                        <asp:TextBox ID="txtCEPCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPCEPCadSegurado')"
                                            CssClass="input-material" MaxLength="10" onkeypress="MascaraCep(this)" value=""></asp:TextBox>
                                        <label for="txtCEPCadSegurado" class="label-material lblEndereco">CEP</label>
                                        <span id="SPCEPCadSegurado" class="required-error" style="display: none">CEP inválido</span>
                                    </div>
                                    <div class="col-md-10" style="margin-top: 15px">
                                        <asp:TextBox ID="txtLogradouroCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPLogradouroCadSegurado')"
                                            CssClass="input-material" value=""></asp:TextBox>
                                        <label for="txtLogradouroCadSegurado" class="label-material lblEndereco">Endereço</label>
                                        <span id="SPLogradouroCadSegurado" class="required-error" style="display: none">Endereço obrigatório</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4" style="margin-top: 25px">
                                        <asp:TextBox ID="txtBairroCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPBairroCadSegurado')"
                                            CssClass="input-material" MaxLength="100" value=""></asp:TextBox>
                                        <label for="txtBairroCadSegurado" class="label-material lblEndereco">Bairro</label>
                                        <span id="SPBairroCadSegurado" class="required-error" style="display: none">Bairro obrigatório</span>
                                    </div>
                                    <div class="col-md-5" style="margin-top: 25px">
                                        <asp:TextBox ID="txtCidadeCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPCidadeCadSegurado')"
                                            CssClass="input-material" MaxLength="100" value=""></asp:TextBox>
                                        <label for="txtCidadeCadSegurado" class="label-material lblEndereco">Cidade</label>
                                        <span id="SPCidadeCadSegurado" class="required-error" style="display: none">Cidade obrigatória</span>
                                    </div>
                                    <div class="col-md-3">
                                        <font color="#0d408f" size="2px">UF</font>
                                        <asp:DropDownList ID="cmbUFCadSegurado" CssClass="dropdown-material" runat="server">
                                            <asp:ListItem Value="" Text="-- Selecione --" Selected></asp:ListItem>
                                            <asp:ListItem Value="AC" Text="AC"></asp:ListItem>
                                            <asp:ListItem Value="AL" Text="AL"></asp:ListItem>
                                            <asp:ListItem Value="AP" Text="AP"></asp:ListItem>
                                            <asp:ListItem Value="AM" Text="AM"></asp:ListItem>
                                            <asp:ListItem Value="BA" Text="BA"></asp:ListItem>
                                            <asp:ListItem Value="CE" Text="CE"></asp:ListItem>
                                            <asp:ListItem Value="DF" Text="DF"></asp:ListItem>
                                            <asp:ListItem Value="ES" Text="ES"></asp:ListItem>
                                            <asp:ListItem Value="GO" Text="GO"></asp:ListItem>
                                            <asp:ListItem Value="MA" Text="MA"></asp:ListItem>
                                            <asp:ListItem Value="MT" Text="MT"></asp:ListItem>
                                            <asp:ListItem Value="MS" Text="MS"></asp:ListItem>
                                            <asp:ListItem Value="MG" Text="MG"></asp:ListItem>
                                            <asp:ListItem Value="PA" Text="PA"></asp:ListItem>
                                            <asp:ListItem Value="PB" Text="PB"></asp:ListItem>
                                            <asp:ListItem Value="PR" Text="PR"></asp:ListItem>
                                            <asp:ListItem Value="PE" Text="PE"></asp:ListItem>
                                            <asp:ListItem Value="PI" Text="PI"></asp:ListItem>
                                            <asp:ListItem Value="RJ" Text="RJ"></asp:ListItem>
                                            <asp:ListItem Value="RN" Text="RN"></asp:ListItem>
                                            <asp:ListItem Value="RS" Text="RS"></asp:ListItem>
                                            <asp:ListItem Value="RO" Text="RO"></asp:ListItem>
                                            <asp:ListItem Value="RR" Text="RR"></asp:ListItem>
                                            <asp:ListItem Value="SC" Text="SC"></asp:ListItem>
                                            <asp:ListItem Value="SP" Text="SP"></asp:ListItem>
                                            <asp:ListItem Value="SE" Text="SE"></asp:ListItem>
                                            <asp:ListItem Value="TO" Text="TO"></asp:ListItem>
                                        </asp:DropDownList>
                                        <span id="SPUFCadSegurado" class="required-error" style="display: none">UF obrigatória</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" style="margin-top: -11px">
                                        <font color="#0d408f" size="2px">Nacionalidade</font>
                                        <select id="cmbNacionalidadeCadSegurado" class="dropdown-material">
                                            <option value="" selected="selected">-- Selecione --</option>
                                            <option value="brasileira">Brasileira</option>
                                            <option value="estrangeira">Estrangeira</option>
                                        </select>
                                        <%--<asp:TextBox ID="txtNacionalidadeCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPNacionalidadeCadSegurado')"
                                            CssClass="input-material" MaxLength="100" onkeypress="return (event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123)" value=""></asp:TextBox>--%>
                                        <span id="SPNacionalidadeCadSegurado" class="required-error" style="display: none">Nacionalidade obrigatória</span>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtPISPASEPCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPPISPASEPCadSegurado')"
                                            CssClass="input-material" MaxLength="14" onkeypress="MascaraPis(this)" value=""></asp:TextBox>
                                        <label for="txtPISPASEPCadSegurado" class="label-material">Número do PISPASEP</label>
                                        <span id="SPPISPASEPCadSegurado" class="required-error" style="display: none">Número PISPASEP inválido</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtCNSCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPCNSCadSegurado')"
                                            CssClass="input-material" onkeypress="MascaraCNS(this)" MaxLength="18" value=""></asp:TextBox>
                                        <label for="txtCNSCadSegurado" class="label-material">Nº do CNS</label>
                                        <span id="SPCNSCadSegurado" class="required-error" style="display: none">CNS inválido</span>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtDNCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPDNCadSegurado')"
                                            CssClass="input-material" MaxLength="13" onkeypress="MascaraDN(this)" value=""></asp:TextBox>
                                        <label for="txtDNCadSegurado" class="label-material">DN</label>
                                        <span id="SPDNCadSegurado" class="required-error" style="display: none">DN inválido</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Plano de saúde</font>
                                        <select name="cmbplanoSaude_cadSegurado" id="cmbplanoSaude_cadSegurado" class="dropdown-material">
                                            <option value="" selected>-- Selecione -- </option>
                                        </select>
                                        <span id="SPcmbplanoSaude_cadSegurado" class="required-error" style="display: none">Plano de saúde é obrigatório</span>
                                    </div>
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Plano odontológico</font>
                                        <select name="cmbplanoOdonto_cadSegurado" id="cmbplanoOdonto_cadSegurado" class="dropdown-material">
                                        </select>

                                        <span id="SPcmbplanoOdonto_cadSegurado" class="required-error" style="display: none"></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4" style="margin-top: 50px">
                                        <asp:TextBox ID="txtNrCarteirinhaCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPCidadeCadSegurado')"
                                            onkeypress="mascaraInteiro()" CssClass="input-material" MaxLength="20" value=""></asp:TextBox>
                                        <label for="txtNrCarteirinhaCadSegurado" class="label-material">Nº da Carteirinha</label>
                                        <span id="SPtxtNrCarteirinhaCadSegurado" class="required-error" style="display: none">Cidade obrigatória</span>
                                    </div>
                                    <div class="col-md-4" style="margin-top: 25px">
                                        <font color="#0d408f" size="2px">Data filiação</font>

                                        <asp:TextBox ID="txtDataFiliacaoCadSegurado" runat="server" type="text" MaxLength="10"
                                            CssClass="input-material calendario" onkeypress="MascaraData(this)" value=""></asp:TextBox>
                                        <span id="SPDataFiliacaoCadSegurado" class="required-error" style="display: none">Data de Filiação obrigatória</span>
                                    </div>
                                    <div class="col-md-4" style="margin-top: 25px">
                                        <font color="#0d408f" size="2px">Início de Vigência</font>
                                        <asp:TextBox ID="txtInicioVigenciaCadSegurado" runat="server" type="text" MaxLength="10"
                                            CssClass="input-material calendario" onkeypress="MascaraData(this)" value=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card-body bg-white" style="margin-top: 30px">
                            <h3 style="padding: 0px;">Contatos</h3>
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtTelefoneCadSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPTelefoneCadSegurado')"
                                            CssClass="input-material" onkeypress="MascaraTelefone(this)" autocomplete="false" MaxLength="14" value=""></asp:TextBox>
                                        <label for="txtTelefoneCadSegurado" style="margin: 0px" class="label-material">Telefone</label>
                                        <span id="SPTelefoneCadSegurado" class="required-error" style="display: none">Telefone Inválido</span>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtCelularCadSegurado" runat="server" type="text" onkeypress="MascaraCelular(this)" AutoCompleteType="None"
                                            CssClass="input-material" MaxLength="15" onkeydown="CampoValido(this, 'SPCelularCadSegurado')" value=""></asp:TextBox>
                                        <label for="txtCelularCadSegurado" style="margin: 0px" class="label-material">Celular</label>
                                        <span id="SPCelularCadSegurado" class="required-error" style="display: none">Celular Inválido</span>
                                    </div>
                                </div>

                                <div class="col-md-12" style="padding: 0px; margin-top: 35px">
                                    <asp:TextBox ID="txtEmailCadSegurado" runat="server" type="text"
                                        CssClass="input-material" onkeydown="CampoValido(this, 'SPmainContent_txtEmailCadSegurado')" AutoCompleteType="None" value=""></asp:TextBox>
                                    <label for="txtEmailCadSegurado" style="margin-left: -15px" class="label-material">E-mail</label>
                                    <span id="SPmainContent_txtEmailCadSegurado" class="required-error" style="display: none"></span>
                                </div>
                            </div>
                        </div>

                        <div class="card-body bg-white" style="margin-top: 30px">
                            <div class="row">
                                <div class="col-md-8" style="padding: 0;">
                                    <h3 style="padding: 0px;">Forma de Pagamento</h3>
                                    <nav class="md-forma-pag">
                                        <ul>
                                            <li style="cursor: pointer" class="li-forma-pagamento">
                                                <img id="imgBoletoCadSegurado" src="../ContentAdm/img/boleto_icon.png" style="width:100px;"/>
                                            </li> 
                                            <span id="SPBoletoCadSegurado" style="font-weight: bold; margin-left:115px !important; margin-top:20px;">Boleto<br /> Bancário</span>
                                            <%--<li style="cursor: pointer" id="lifpagCCCadSegurado">
                                                    <div class="dv-fpag-border-CadSegurado" style="border-style: none; margin-left: 13%"></div>
                                                    <img class="md-forma-pag-img-CadSegurado" style="margin-left: 14%; opacity: 0.5" src="../ContentAdm/img/credit-card-icon.png" />
                                                    <span class="md-creditcard-label" id="SPCCCadSegurado" style="display: inline; opacity: 0.5; margin-left: 20px">Cartão<br />
                                                        de Crédito</span>
                                                </li>
                                                <li style="cursor: pointer" id="lifpagDebitoCadSegurado">
                                                    <div class="dv-fpag-border-CadSegurado" style="margin-left: 13%"></div>
                                                    <img class="md-forma-pag-img-CadSegurado" style="margin-left: 14%; width: 100px" src="../ContentAdm/img/debito_icon.png" />
                                                    <span class="md-debito-label" id="SPDebitoCadSegurado" style="display: inline">Débito<br />
                                                        em Conta</span>
                                                </li>--%>
                                        </ul>
                                    </nav>
                                </div>
                                <div class="col-md-1" ></div>
                                <div class="col-md-2" style="padding: 0; margin-top: 10px;">
                                    <font color="#0d408f" size="2px">Melhor dia Pagamento</font>
                                    <select id="cmbMelhorDiaPag_cadSegurado" class="dropdown-material">
                                        <option value="">-- Selecione --</option>
                                    </select>
                                    <span id="SPcmbMelhorDiaPag_cadSegurado" class="required-error" style="display: none">Melhor dia de Pagamento é obrigatório</span>
                                </div>
                            </div>
                        </div>

                        <div class="card-body bg-white fp-debito-cad-segurado" style="margin-top: 30px; display: none">
                            <h3 style="padding: 0px;">Dados da Conta Bancária</h3>
                            <div class="row">
                                <div class="col-md-5">
                                    <div id="dv_cmbBanco_cadSegurado">
                                        <select id="cmbBanco_cadSegurado" class="dropdown-material" style="margin-top: 23px">
                                            <option value="0">Banco</option>
                                        </select>
                                    </div>
                                    <span id="SPCMBBancoCadSegurado" class="required-error" style="display: none">Banco obrigatório</span>
                                </div>
                                <div class="col-md-3" style="margin-top: 25px">
                                    <asp:TextBox ID="txtAgencia_cadSeguro" runat="server" type="text" onkeyPress="mascaraInteiro()"
                                        CssClass="input-material" MaxLength="10" value=""></asp:TextBox>
                                    <label for="txtAgencia_cadSeguro" class="label-material">Agência</label>
                                    <span id="SPtxtAgencia_cadSeguro" class="required-error" style="display: none">Agência obrigatória</span>
                                </div>
                                <div class="col-md-4" style="margin-top: 25px">
                                    <asp:TextBox ID="txtConta_cadSegurado" runat="server" type="text" MaxLength="20" onkeypress="mascaraInteiro()"
                                        onkeydown="CampoValido(this, 'SPtxtConta_cadSegurado')" CssClass="input-material" value=""></asp:TextBox>
                                    <label for="txtConta_cadSegurado" class="label-material">Conta</label>
                                    <span id="SPtxtConta_cadSegurado" class="required-error" style="display: none">Conta obrigatória</span>
                                </div>
                            </div>
                        </div>

                        <div class="card-body bg-white" style="margin-top: 30px">
                            <button id="btnSalvarCadSegurado" type="button" style="position: relative; left: 45%; right: 55%" class="btn btn-primary">Salvar</button>
                        </div>
                    </div>

                    <%--Dados dos Dependentes--%>
                    <div id="dvCadDependentes" class="col-lg-12 col-12" style="display: none">
                        <div class="card-body bg-white">
                            <h3 class="col-md-6" style="padding: 0px;">Dados dos Dependentes</h3>
                            <div class="form-group-material">
                                <input id="hddCpfTitular" type="hidden" />
                                <div class="row">
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtCPFCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPCPFCadDependente')"
                                            onkeypress="MascaraCPF(this)" MaxLength="14" CssClass="input-material" value=""></asp:TextBox>
                                        <label for="txtCPFCadDependente" class="label-material">CPF</label>
                                        <span id="SPCPFCadDependente" class="required-error" style="display: none">CPF Inválido</span>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtDataNascCadDependente" runat="server" type="text" onKeyPress="MascaraData(this)" CssClass="input-material input-dependente"
                                            MaxLength="10" onkeydown="CampoValido(this,'SPDataNascCadDependente');" value="" />
                                        <label for="txtDataNascCadDependente" class="label-material">Data de Nascimento</label>
                                        <span id="SPDataNascCadDependente" class="required-error" style="display: none;">Data de nascimento obrigatória</span>
                                    </div>
                                </div>

                                <div class="col-md-12" style="padding: 0px; margin-top: 25px">
                                    <asp:TextBox ID="txtNomeCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPNomeCadDependente')"
                                        CssClass="input-material input-dependente" value=""></asp:TextBox>
                                    <label for="txtNomeCadDependente" style="margin-left: -15px" class="label-material">Nome do Dependente</label>
                                    <span id="SPNomeCadDependente" class="required-error" style="display: none">Nome obrigatório</span>
                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <font color="#0d408f" size="2px">Sexo</font>
                                        <select id="cmbSexoCadDependente" class="dropdown-material cmb-dependente">
                                            <option value="" selected="selected">-- Selecione --</option>
                                            <option value="M">Masculino</option>
                                            <option value="F">Feminino</option>
                                        </select>
                                        <span id="SPSexoCadDependente" class="required-error" style="display: none">Sexo obrigatório</span>
                                    </div>
                                    <div class="col-md-4">
                                        <font color="#0d408f" size="2px">Estado Civil</font>
                                        <select id="cmbCivilCadDependente" class="dropdown-material cmb-dependente">
                                        </select>
                                        <span id="SPCivilCadDependente" class="required-error" style="display: none">Estado Civil obrigatório</span>
                                    </div>
                                    <div class="col-md-4">
                                        <font color="#0d408f" size="2px">Plano odontológico</font>
                                        <select name="cmbplanoOdonto_CadDependente" id="cmbplanoOdonto_CadDependente" class="dropdown-material">
                                        </select>
                                        <span id="SPcmbplanoOdonto_CadDependente" class="required-error" style="display: none"></span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Profissão</font>
                                        <div id="dv-cmbCadDependentes">
                                            <select id="cmbProfissaoCadDependente" class="dropdown-material cmb-dependente">
                                                <option></option>
                                            </select>
                                        </div>
                                        <span id="SPProfissaoCadDependente" class="required-error" style="display: none">Profissão obrigatória</span>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 25px">
                                        <asp:TextBox ID="txtNomeMaeCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPNomeMaeCadDependente')"
                                            CssClass="input-material input-dependente" value=""></asp:TextBox>
                                        <label for="txtNomeMaeCadDependente" class="label-material">Nome da Mãe</label>
                                        <span id="SPNomeMaeCadDependente" class="required-error" style="display: none">Nome da Mãe obrigatório</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2" style="margin-top: 15px">
                                        <asp:TextBox ID="txtCEPCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPCEPCadDependente')"
                                            CssClass="input-material input-dependente" MaxLength="10" onkeypress="MascaraCep(this)" value=""></asp:TextBox>
                                        <label for="txtCEPCadDependente" class="label-material lblEndereco">CEP</label>
                                        <span id="SPCEPCadDependente" class="required-error" style="display: none">CEP inválido</span>
                                    </div>
                                    <div class="col-md-10" style="margin-top: 15px">
                                        <asp:TextBox ID="txtLogradouroCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPLogradouroCadDependente')"
                                            CssClass="input-material input-dependente" value=""></asp:TextBox>
                                        <label for="txtLogradouroCadDependente" class="label-material lblEndereco">Endereço</label>
                                        <span id="SPLogradouroCadDependente" class="required-error" style="display: none">Endereço obrigatório</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4" style="margin-top: 25px">
                                        <asp:TextBox ID="txtBairroCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPBairroCadDependente')"
                                            CssClass="input-material input-dependente" MaxLength="100" value=""></asp:TextBox>
                                        <label for="txtBairroCadDependente" class="label-material lblEndereco">Bairro</label>
                                        <span id="SPBairroCadDependente" class="required-error" style="display: none">Bairro obrigatório</span>
                                    </div>
                                    <div class="col-md-5" style="margin-top: 25px">
                                        <asp:TextBox ID="txtCidadeCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPCidadeCadDependente')"
                                            CssClass="input-material input-dependente" MaxLength="100" value=""></asp:TextBox>
                                        <label for="txtCidadeCadDependente" class="label-material lblEndereco">Cidade</label>
                                        <span id="SPCidadeCadDependente" class="required-error" style="display: none">Cidade obrigatória</span>
                                    </div>
                                    <div class="col-md-3">
                                        <font color="#0d408f" size="2px">UF</font>
                                        <asp:DropDownList ID="cmbUFCadDependente" CssClass="dropdown-material cmb-dependente" runat="server">
                                            <asp:ListItem Value="" Text="-- Selecione --" Selected></asp:ListItem>
                                            <asp:ListItem Value="AC" Text="AC"></asp:ListItem>
                                            <asp:ListItem Value="AL" Text="AL"></asp:ListItem>
                                            <asp:ListItem Value="AP" Text="AP"></asp:ListItem>
                                            <asp:ListItem Value="AM" Text="AM"></asp:ListItem>
                                            <asp:ListItem Value="BA" Text="BA"></asp:ListItem>
                                            <asp:ListItem Value="CE" Text="CE"></asp:ListItem>
                                            <asp:ListItem Value="DF" Text="DF"></asp:ListItem>
                                            <asp:ListItem Value="ES" Text="ES"></asp:ListItem>
                                            <asp:ListItem Value="GO" Text="GO"></asp:ListItem>
                                            <asp:ListItem Value="MA" Text="MA"></asp:ListItem>
                                            <asp:ListItem Value="MT" Text="MT"></asp:ListItem>
                                            <asp:ListItem Value="MS" Text="MS"></asp:ListItem>
                                            <asp:ListItem Value="MG" Text="MG"></asp:ListItem>
                                            <asp:ListItem Value="PA" Text="PA"></asp:ListItem>
                                            <asp:ListItem Value="PB" Text="PB"></asp:ListItem>
                                            <asp:ListItem Value="PR" Text="PR"></asp:ListItem>
                                            <asp:ListItem Value="PE" Text="PE"></asp:ListItem>
                                            <asp:ListItem Value="PI" Text="PI"></asp:ListItem>
                                            <asp:ListItem Value="RJ" Text="RJ"></asp:ListItem>
                                            <asp:ListItem Value="RN" Text="RN"></asp:ListItem>
                                            <asp:ListItem Value="RS" Text="RS"></asp:ListItem>
                                            <asp:ListItem Value="RO" Text="RO"></asp:ListItem>
                                            <asp:ListItem Value="RR" Text="RR"></asp:ListItem>
                                            <asp:ListItem Value="SC" Text="SC"></asp:ListItem>
                                            <asp:ListItem Value="SP" Text="SP"></asp:ListItem>
                                            <asp:ListItem Value="SE" Text="SE"></asp:ListItem>
                                            <asp:ListItem Value="TO" Text="TO"></asp:ListItem>
                                        </asp:DropDownList>
                                        <span id="SPUFCadDependente" class="required-error" style="display: none">UF obrigatória</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" style="margin-top: -11px">
                                        <font color="#0d408f" size="2px">Nacionalidade</font>
                                        <select id="cmbNacionalidadeCadDependente" class="dropdown-material">
                                            <option value="" selected="selected">-- Selecione --</option>
                                            <option value="brasileira">Brasileira</option>
                                            <option value="estrangeira">Estrangeira</option>
                                        </select>
                                        <%--<asp:TextBox ID="txtNacionalidadeCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPNacionalidadeCadDependente')"
                                            CssClass="input-material input-dependente" onkeypress="return (event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123)" MaxLength="100" value=""></asp:TextBox>--%>
                                        <span id="SPNacionalidadeCadDependente" class="required-error" style="display: none">Nacionalidade obrigatória</span>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtPISPASEPCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPPISPASEPCadDependente')"
                                            CssClass="input-material input-dependente" MaxLength="14" onkeypress="MascaraPis(this)" value=""></asp:TextBox>
                                        <label for="txtPISPASEPCadDependente" class="label-material">Número do PISPASEP</label>
                                        <span id="SPPISPASEPCadDependente" class="required-error" style="display: none">Número PISPASEP inválido</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtCNSCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPCNSCadDependente')"
                                            CssClass="input-material input-dependente" onkeypress="MascaraCNS(this)" MaxLength="18" value=""></asp:TextBox>
                                        <label for="txtCNSCadDependente" class="label-material">Nº do CNS</label>
                                        <span id="SPCNSCadDependente" class="required-error" style="display: none">CNS inválido</span>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtDNCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPDNCadDependente')"
                                            CssClass="input-material input-dependente" MaxLength="13" onkeypress="MascaraDN(this)" value=""></asp:TextBox>
                                        <label for="txtDNCadDependente" class="label-material">DN</label>
                                        <span id="SPDNCadDependente" class="required-error" style="display: none">DN inválido</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3" style="margin-top: 25px">
                                        <asp:TextBox ID="txtNrCarteirinhaCadDep" runat="server" type="text" onkeydown="CampoValido(this, 'SPCNSCadDependente')"
                                            CssClass="input-material input-dependente" onkeypress="mascaraInteiro()" MaxLength="20" value=""></asp:TextBox>
                                        <label for="txtNrCarteirinhaCadDep" class="label-material">Nº da Carteirinha</label>
                                        <span id="SPtxtNrCarteirinhaCadDep" class="required-error" style="display: none">Nr. Carteirinha inválido</span>
                                    </div>
                                    <div class="col-md-3">
                                        <font color="#0d408f" size="2px">Inicio de Vigência</font>
                                        <asp:TextBox ID="txtInicioVigenciaCadDependente" runat="server" type="text" MaxLength="10"
                                            CssClass="input-material input-dependente" onkeypress="MascaraData(this)" value=""></asp:TextBox>
                                    </div>
                                    <%--<div class="col-md-3">
                                        <font color="#0d408f" size="2px">Fim de Vigência</font>
                                        <asp:TextBox ID="txtFimVigenciaCadDependente" runat="server" type="text" MaxLength="10"
                                            CssClass="input-material input-dependente" onkeypress="MascaraData(this)" value=""></asp:TextBox>
                                    </div>--%>
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Tipo de Parentesco</font>
                                        <asp:DropDownList ID="cmbParentescoCadDependente" CssClass="dropdown-material cmb-dependente" runat="server">
                                            <asp:ListItem Value="" Text="-- Selecione --" Selected></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Avos"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Bisneto(a)"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Cunhado(a)"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Enteado(a)"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="Filho(a)"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Genro/Nora"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="Irmao(a)"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="Neto(a)"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="Pai/Mae"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="Sobrinho(a)"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="Sogro(a)"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="Tio(a)"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="Companheiro(a)"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="Companheiro(a) Homo"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="Conjuge"></asp:ListItem>
                                            <asp:ListItem Value="16" Text="Enteado(a)"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="Filho(a)"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="Menor sob guarda"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="Titular"></asp:ListItem>
                                        </asp:DropDownList>
                                        <span id="SPParentescoCadDependente" class="required-error" style="display: none">Parentesco obrigatório</span>
                                    </div>
                                </div>

                                <div style="height: 1px; background-color: #e9ebee; margin: 37px"></div>

                                <h3 style="padding: 0px">Contatos</h3>
                                <div class="form-group-material">
                                    <div class="row">
                                        <div class="col-md-6" style="margin-top: 15px">
                                            <asp:TextBox ID="txtTelefoneCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPTelefoneCadDependente')"
                                                CssClass="input-material input-dependente" onkeypress="MascaraTelefone(this)" MaxLength="14" value=""></asp:TextBox>
                                            <label for="txtTelefoneCadDependente" style="margin: 0px" class="label-material">Telefone</label>
                                            <span id="SPTelefoneCadDependente" class="required-error" style="display: none">Telefone Inválido</span>
                                        </div>
                                        <div class="col-md-6" style="margin-top: 15px">
                                            <asp:TextBox ID="txtCelularCadDependente" runat="server" type="text" onkeypress="MascaraCelular(this)"
                                                CssClass="input-material input-dependente" MaxLength="15" onkeydown="CampoValido(this, 'SPCelularCadDependente')" value=""></asp:TextBox>
                                            <label for="txtCelularCadDependente" style="margin: 0px" class="label-material">Celular</label>
                                            <span id="SPCelularCadDependente" class="required-error" style="display: none">Celular Inválido</span>
                                        </div>
                                    </div>

                                    <div class="col-md-12" style="padding: 0px; margin-top: 25px">
                                        <asp:TextBox ID="txtEmailCadDependente" runat="server" type="text" onkeydown="CampoValido(this, 'SPmainContent_txtEmailCadDependente')"
                                            CssClass="input-material input-dependente" value=""></asp:TextBox>
                                        <label for="txtEmailCadDependente" style="margin-left: -15px" class="label-material">E-mail</label>
                                        <span id="SPmainContent_txtEmailCadDependente" class="required-error" style="display: none"></span>
                                    </div>

                                    <div style="margin-top: 10px">
                                        <button id="btnAdicionarCadDependente" type="button" style="float: right" class="btn btn-primary">Adicionar</button>
                                        <button id="btnLimparCadDependente" type="button" style="float: right; margin-right: 15px" class="btn btn-primary">Limpar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card-body bg-white" style="margin-top: 30px">
                            <h3 style="padding: 0px;">Dependentes Cadastrados</h3>
                            <div id="dvDependentesCadastrados" class="col-lg-12 col-12" style="padding: 50px 0; display: none">
                                <div class="form-group-material">
                                    <div class="col-md-12">
                                        <div class="card-body no-padding">
                                            <table class="table table-hover">
                                                <tr>
                                                    <td align="left"><b>Nome</b></td>
                                                </tr>
                                                <tbody class="tbody">
                                                    <tr>
                                                        <%--<td><i class="far fa-user"></i></td>--%>
                                                        <td class="Nome" align="left"></td>
                                                        <td class="Acao"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card-body bg-white" style="margin-top: 30px">
                            <button id="btnSairCadDependente" type="button" style="float: right; margin-top: -15px" class="btn btn-primary">Sair</button>
                            <%--<button id="btnSalvarCadDependente" type="button" style="float: right; margin-right: 15px; margin-top: -15px" class="btn btn-primary">Salvar</button>--%>
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>

    <%--Modal--%>
    <div id="mdCadSegurado" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body" id="mdMensagem">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>

    <%--Modal Cnfirmação Excluir Dep--%>
    <div id="mdCadDependente" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body" id="mdMensagemDep">
                    Deseja excluir o dependente?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                    <button type="button" class="btn btn-primary" id="btnExcluirCadDependente">Sim</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">    
    <script src="../ContentAdm/jquery/jquery.priceformat.js"></script>    
</asp:Content>
