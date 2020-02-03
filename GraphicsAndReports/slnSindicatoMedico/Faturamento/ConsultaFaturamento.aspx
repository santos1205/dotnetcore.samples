<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="ConsultaFaturamento.aspx.cs" Inherits="slnSindicatoMedico.MasterPage.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../ContentAdm/css/jquery.range.css" rel="stylesheet" />

    <style type="text/css">
        #dvFaturamento {
            padding: 0;
        }

        #dvResultadoFaturamento {
            margin: 0 auto;
        }


        input:checked + .small-slider {
            background-color: green;
        }

        .aba-manual h3, .aba-automatico h3 {
            cursor: pointer;
        }

        .card-menu > ul > li >.nav-link active {
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

     .card-menu > ul >  .nav-item > .active:hover {
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
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Faturamento</h2>
            </div>
        </div>
    </header>
    <div class="breadcrumb-holder container-fluid ">
        <ul class="breadcrumb">
            <li class="breadcrumb-item" id="nav-form2">Consultar Faturamento</li>
        </ul>
    </div>
    <div id="dvFaturamento" class="container-fluid">
        <section style="padding: 10px 0 170px;">
            <div class="container-fluid">

                <div class="row col-lg-12 col-12">
                    <div class="card-header card-menu" style="margin-left: 1px !important;">
                        <ul class="nav nav-tabs card-header-tabs">
                            <li class="nav-item" style="margin-left: -6px">
                                <a class="nav-link active" id="dv-aba-fat" href="#">Consultar Faturamento</a>
                            </li>
                            <li class="nav-item" style="margin-right: -6px">
                                <a class="nav-link" href="#" id="dv-aba-md-faixa">Consultar Mudança de Faixa</a>
                            </li>
                        </ul>
                    </div>
                    <div class="card-body bg-white col-md-12" id="dv-content-fat" style="height:500px;">
                        <div class="form-group-material">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCPFeditFaturamento" runat="server" type="text" onKeyPress="MascaraCPF(this)" CssClass="input-material"
                                        MaxLength="14" onkeydown="CampoValido(this, 'SPtxtCPF')" value=""></asp:TextBox>
                                    <label for="txtCPFeditFaturamento" class="label-material">CPF</label>
                                    <span id="SPtxtCPF" class="required-error" style="display: none;">CPF inválido!</span>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtNomeeditFaturamento" runat="server" type="text" CssClass="input-material" MaxLength="100" value=""></asp:TextBox>
                                    <label for="txtNomeeditFaturamento" class="label-material">Nome</label>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 25px">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txteditCarteirinha" runat="server" type="text" CssClass="input-material" onkeypress="mascaraInteiro()" MaxLength="50" value=""></asp:TextBox>
                                    <label for="txteditCarteirinha" class="label-material">Nº carteirinha</label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCRMeditFaturamento" runat="server" type="text" CssClass="input-material" onkeypress="mascaraInteiro()" value=""></asp:TextBox>
                                    <label for="txtCRMeditFaturamento" class="label-material">CRM</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <font color="#0d408f" size="2px">Produto</font>
                                    <asp:DropDownList ID="cmbProdutoeditFaturamento" CssClass="dropdown-material" runat="server">
                                        <asp:ListItem Value="" Text="-- Selecione --" Selected></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Saúde"></asp:ListItem>
                                         <asp:ListItem Value="3" Text="Odonto"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Saúde / Odonto"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtDataInicioeditFaturamento" runat="server" type="text" onKeyPress="MascaraData(this)" CssClass="input-material calendario"
                                        MaxLength="10" value="" />
                                    <label for="txtDataInicioeditFaturamento" class="label-material">Data Início</label>
                                    <span id="SPtxtDataInicioF" class="required-error" style="display: none;"></span>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtDataFimeditFaturamento" runat="server" type="text" onKeyPress="MascaraData(this)" CssClass="input-material calendario"
                                        MaxLength="10" value="" />
                                    <label for="txtDataFimeditFaturamento" class="label-material">Data Fim</label>
                                    <span id="SPtxtDataFimF" class="required-error" style="display: none;"></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group-material">
                            <div class="row">
                                <div class="col-md-12 " style="text-align: center;">
                                    <span id="SPRetornoErroFaturamento" class="required-error" style="display: none;">É obrigatório preencher pelo menos um campo para realizar a consulta do Faturamento.</span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group-material">
                            <button id="btnConsultarFaturamento" type="button" style="float: left;" class="btn btn-primary">Consultar</button>
                            <button id="btnLimparFaturamento" type="button" style="float: right;" class="btn btn-primary">Limpar</button>
                        </div>
                    </div>

                    <%--Grid com o resultado da consulta feita--%>
                    <div id="dvResultadoFaturamento" class="col-lg-12 col-12" style=" height:500px; display:none; padding: 50px 0;">
                        <div class="form-group-material">
                            <div class="card-header d-flex">
                                <div class="col-md-6">
                                    <h3>Resultado consulta</h3>
                                </div>
                                <div class="col-md-6" style="float: right; text-align: right;">
                                    <h6>Total retornado: <span id="lblTotalResultado"></span></h6>
                                </div>
                            </div>
                            <div class="card-body no-padding" id="tblFaturamento">
                                <table class="table table-hover">
                                    <tr>
                                        <td align="left"><b>Nome</b></td>
                                        <td align="center"><b>CPF</b></td>
                                        <td align="center"><b>Plano</b></td>
                                        <td align="center"><b>Vencimento</b></td>
                                        <td align="center"><b>Prêmio</b></td>
                                        <td align="center"><b>Nr. de dependentes</b></td>
                                        <td align="center"><b>Forma de Pagamento</b></td>
                                    </tr>
                                    <tbody class="tbody">
                                        <tr>
                                            <td class="NomeSegurado" align="left"></td>
                                            <td class="CPF" align="center"></td>
                                            <td class="Plano" align="center"></td>
                                            <td class="DtVencimento" align="center"></td>
                                            <td class="VlPremio" align="center"></td>
                                            <td class="NrDependentes" align="center"></td>
                                            <td class="FPagamento" align="center"></td>
                                            <td class="Acao" align="center"></td>
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
                            </div>
                        </div>
                    </div>
                    <%--Div de mudanca de faixa--%>
                    <div class="card-body bg-white col-md-12" id="dv-content-md-faixa" style="display: none; height:425px;">
                        <div class="form-group-material">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtNomeeditMudancaFaixa" runat="server" type="text" CssClass="input-material" value=""></asp:TextBox>
                                    <label for="txtNomeeditMudancaFaixa" class="label-material">Nome</label>
                                </div>
                                <div class="col-md-6">

                                    <asp:TextBox ID="txtCPFeditMudancaFaixa" runat="server" type="text" onKeyPress="MascaraCPF(this)" CssClass="input-material"
                                        MaxLength="14" onkeydown="CampoValido(this, 'SPtxtCPF')" value=""></asp:TextBox>
                                    <label for="editMudancaFaixa" class="label-material">CPF</label>
                                    <span id="SPtxtCPF" class="required-error" style="display: none;">CPF inválido!</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6" style="margin-top: 25px">
                                    <asp:TextBox ID="txtCRMeditMudancaFaixa" runat="server" type="text" CssClass="input-material" value=""></asp:TextBox>
                                    <label for="txtCRMeditMudancaFaixa" class="label-material">CRM</label>
                                </div>
                                <div class="col-md-6">
                                    <font color="#0d408f" size="2px">Produto</font>
                                    <asp:DropDownList ID="cmbProdutoeditMudancaFaixa" CssClass="dropdown-material" runat="server">
                                        <asp:ListItem Value="" Text="-- Selecione --"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Saúde"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Odonto"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Saúde/Odonto"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtDataInicioeditMudancaFaixa" runat="server" type="text" onKeyPress="MascaraData(this)" CssClass="input-material calendario"
                                        MaxLength="10" value="" />
                                    <label for="txtDataInicioeditMudancaFaixa" class="label-material">Data Inicio</label>
                                    <span id="SPtxtDataInicio" class="required-error" style="display: none;"></span>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtDataFimeditMudancaFaixa" runat="server" type="text" onKeyPress="MascaraData(this)" CssClass="input-material calendario"
                                        MaxLength="10" value="" />
                                    <label for="txtDataFimeditMudancaFaixa" class="label-material">Data Fim</label>
                                    <span id="SPtxtDataFim" class="required-error" style="display: none;"></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group-material">
                            <div class="row">
                                <div class="col-md-12 " style="text-align: center;">
                                    <span id="SPRetornoErroMF" class="required-error" style="display: none;">É obrigatório preencher pelo menos um campo para realizar a consulta de Baixa de Mudanca de Faixa.</span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group-material">
                            <button id="btnConsultarMudancaFaixa" type="button" style="float: left;" class="btn btn-primary">Consultar</button>
                            <button id="btnLimparMudancaFaixa" type="button" style="float: right;" class="btn btn-primary">Limpar</button>
                        </div>
                    </div>

                    <%--Grid com o resultado da consulta feita--%>
                    <div id="dvResultadoMudancaFaixa" class="col-lg-12 col-12" style="padding: 50px 0; display: none">
                        <div class="form-group-material">

                            <div class="card-header d-flex">
                                <div class="col-md-6">
                                    <h3>Resultado consulta</h3>
                                </div>
                                <%--<div class="col-md-6" style="float: right; text-align: right;">
                                    <h6>Total retornado: <span id="lblTotalResultadoMF"></span></h6>
                                </div>--%>
                            </div>
                            <div class="card-body no-padding" id="tblMFaixa">
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

                                <%--<table class="table table-hover">
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
                                            <td class="Acao"><i style="cursor: pointer" data-toggle="tooltip" data-placement="top" title="Detalhar"></i></td>                                            
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
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <!-- Modal -->
    <div id="mdFaturamento" class="modal fade" style="margin-left: -46%" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 100%">
            <!-- Modal content-->
            <div class="modal-content modal-resolution-pag">
                <i id="faCloseFaturamento" class="fa fa-window-close" style="position: absolute; top: 4px; right: 5px; z-index: 1; font-size: 25px; color: red; cursor: pointer"></i>
                <div style="border-radius: 10px; border: 1px solid #0d408f; margin: 50px 15px 100px 15px;">
                    <div class="row" style="width: 90%; margin-left: 5%">
                        <div class="col-lg-12">
                            <span class="modal-title-pag">
                                <h3>Faturamento</h3>
                            </span>
                            <hr />
                        </div>
                    </div>
                    <div class="row" style="width: 90%; margin-left: 5%">
                        <hr />
                        <div class="col-lg-6">
                            <div class="align-items-center">
                                <div class="content">
                                    <div>
                                        <span class="modal-label lbl-pag">Nome:</span><span class="modal-data" id="sp-nome"></span>
                                    </div>

                                    <div>
                                        <span class="modal-label lbl-pag">CPF:</span><span class="modal-data" id="sp-cpf"></span>
                                    </div>

                                    <div>
                                        <span class="modal-label lbl-pag">Produto:</span><span class="modal-data" id="sp-produto"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="align-items-center">
                                <div class="content">
                                    <div>
                                        <span class="modal-label lbl-pag">Data Vencimento:</span><span class="modal-data" id="sp-data-vencimento"></span>
                                    </div>
                                    <div>
                                        <span class="modal-label lbl-pag">Premio:</span><span class="modal-data" id="sp-premio"></span>
                                    </div>
                                    <div>
                                        <span class="modal-label lbl-pag">Forma de Pagamento:</span><span class="modal-data" id="sp-forma-pagamento"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="width: 100%; margin-top: -1%">
                    <div>
                        <button type="button" onclick="SalvarTudo()" class="btn btn-primary btn-concluir">Salvar e Sair</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">
    <script src="../ContentAdm/js/SindMedico.js"></script>    
    <script src="../ContentAdm/jquery/jquery.priceformat.js"></script>
    <script src="../ContentAdm/jquery/jquery-ui.min.js"></script>
</asp:Content>
