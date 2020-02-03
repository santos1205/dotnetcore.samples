<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="Pagamentos.aspx.cs" Inherits="slnSindicatoMedico.MasterPage.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../ContentAdm/css/jquery.range.css" rel="stylesheet" />

    <style type="text/css">
        #dvPagamento {
            padding: 0;
        }

        #dvResultadoPagamento {
            margin: 0 auto;
        }

        /*override datapicker style*/
        .DataPagamentoModal > .ui-widget.ui-widget-content {
            border: 1px solid #d3d3d3;
            margin-top: 7%;
        }

        input:checked + .small-slider {
            background-color: green;
        }

        .aba-manual h3, .aba-automatico h3 {
            cursor: pointer;
        }


        #dvPagamento > section {
            padding-top: 1%;
        }

        .DataPagamentoModal {
            top: 50% !important;
        }

        .DatePikerEN > .ui-datepicker-calendar {
            display: none;
        }

        .DatePikerEN > .ui-datepicker-header > .ui-datepicker-prev {
            display: none;
        }

        .DatePikerEN > .ui-datepicker-buttonpane > button.ui-datepicker-current {
            display: none !important;
        }

        .DatePikerEN > .ui-datepicker-header > .ui-datepicker-next {
            display: none;
        }

        .DatePikerEN {
            top: 42% !important;
        }

        @media (min-width: 1920px) {
            .DataPagamentoModal {
                top: 48% !important;
            }

            .DatePikerEN {
                top: 35% !important;
            }
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
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Pagamento</h2>
            </div>
        </div>
    </header>

    <div class="breadcrumb-holder container-fluid ">
        <ul class="breadcrumb">
            <li class="breadcrumb-item" id="nav-form2">Consultar Baixa de Pagamento</li>
        </ul>
    </div>

    <div id="dvPagamento" class="container-fluid">
        <section>
            <div class="container-fluid">

                <div class="row col-lg-12 col-12">
                    <div class="card-header card-menu" style="margin-left: 1px !important;">
                        <ul class="nav nav-tabs card-header-tabs">
                            <li class="nav-item" style="margin-left: -6px">
                                <a class="nav-link active" id="dv-aba-manual" href="#">Manual</a>
                            </li>
                            <li class="nav-item" style="margin-right: -6px">
                                <a class="nav-link" href="#" id="dv-aba-automatico">Automático</a>
                            </li>
                        </ul>
                    </div>
                    <%--Manual--%>
                    <div class="card-body bg-white col-md-12" id="dv-content-manual" style="height:500px">
                        <div class="form-group-material">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCPFeditPagamento" runat="server" type="text" onKeyPress="MascaraCPF(this)" CssClass="input-material"
                                        MaxLength="14" onkeydown="CampoValido(this, 'SPtxtCPF')" value=""></asp:TextBox>
                                    <label for="editPagamento" class="label-material">CPF</label>
                                    <span id="SPtxtCPF" class="required-error" style="display: none;">CPF inválido!</span>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtNomeeditPagamento" runat="server" type="text" CssClass="input-material" value=""></asp:TextBox>
                                    <label for="txtNomeeditPagamento" class="label-material">Nome</label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6" style="margin-top: 25px">
                                    <asp:TextBox ID="txtCRMeditPagamento" runat="server" type="text" onkeydown="CampoValido(this, 'SPtxtDtFinalCalc')"
                                        CssClass="input-material input-consultar-segurado" onkeypress="mascaraInteiro()" MaxLength="10" value=""></asp:TextBox>
                                    <label for="txtCRMeditPagamento" class="label-material">CRM</label>
                                </div>
                                <div class="col-md-6">
                                    <font color="#0d408f" size="2px">Produto</font>
                                    <asp:DropDownList ID="cmbProdutoeditPagamento" CssClass="dropdown-material" runat="server">
                                        <asp:ListItem Value="" Text="-- Selecione --"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Saúde"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Odonto"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Saúde / Odonto"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6" style="margin-top: 25px">
                                    <asp:TextBox ID="txtNrCarteirinhaeditPagamento" runat="server" type="text"
                                        CssClass="input-material" MaxLength="20" value=""></asp:TextBox>
                                    <label for="txtNrCarteirinhaeditPagamento" class="label-material">N° de Carterinha</label>
                                </div>
                                <div class="col-md-6">
                                    <font color="#0d408f" size="2px">Status</font>
                                    <asp:DropDownList ID="cmbStatuseditPagamento" CssClass="dropdown-material input-consultar-segurado" runat="server">
                                        <asp:ListItem Value="" Text="-- Selecione --"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Aberto"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Vencido"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Recebido"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-6">
                                    <input name="txtDataInicioeditPagamento" maxlength="10" id="txtDataInicioeditPagamento" class="input-material calendario"
                                        type="text" onkeypress="MascaraData(this)" onkeydown="CampoValido(this, 'SPtxtDataInicio')" value="" />
                                    <label for="txtDataInicioeditPagamento" class="label-material">Data Inicio</label>
                                    <span id="SPtxtDataInicio" class="required-error" style="display: none;"></span>

                                </div>
                                <div class="col-md-6">
                                    <input name="txtDataFimeditPagamento" maxlength="10" id="txtDataFimeditPagamento" class="input-material calendario"
                                        type="text" onkeypress="MascaraData(this)" onkeydown="CampoValido(this, 'SPtxtDataFim')" value="" />
                                    <label for="txtDataFimeditPagamento" class="label-material">Data Fim</label>
                                    <span id="SPtxtDataFim" class="required-error" style="display: none;"></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group-material">
                            <div class="row">
                                <div class="col-md-12 " style="text-align: center;">
                                    <span id="SPRetornoErro" class="required-error" style="display: none;">É obrigatório preencher pelo menos um campo para realizar a consulta de Baixa de pagamento.</span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group-material">
                            <button id="btnConsultarPagamento" type="button" style="float: left;" class="btn btn-primary">Consultar</button>
                            <button id="btnLimparPagamento" type="button" style="float: right;" class="btn btn-primary">Limpar</button>
                        </div>
                    </div>

                    <%--Automatico--%>
                    <div class="card-body bg-white col-md-12" id="dv-content-automatico" style="display: none; height:500px" >
                        <div class="form-group-material">
                            <div class="row">
                                <div class="col-md-6" style="border: none !important">
                                    <div class="row dv-baixa-automatica">
                                        <div class="col-md-12">
                                            <input name="txtPeriodoBaixaAutomatica" maxlength="10" id="txtPeriodoBaixaAutomatica" class="input-material calendarioMesAno"
                                                type="text" onkeypress="MascaraDataMesAno(this)" onkeydown="CampoValido(this, 'SPtxtDataFim')" value="" />

                                            <label for="txtPeriodoBaixaAutomatica" style="font-size: 0.8em; top: -10px; color: #0d408f;">Período</label>
                                            <span id="SPtxtPeriodoBaixaAutomatica" class="required-error" style="display: none;"></span>
                                        </div>
                                    </div>
                                    <div class="row dv-baixa-automatica">
                                        <div class="col-md-12">
                                            <label for="UploadBaixaManual" class="label-material active">Documento/Arquivo</label>
                                            <asp:FileUpload ID="UploadBaixaManual" runat="server" onchange="getFileData(this)" />
                                            <span id="SPUploadBaixaManualValida" class="required-error" style="display: none; margin-top: 25px;"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 10px;">
                                    <asp:Button ID="btnValidarArquivo" OnClientClick="return ValidacaoImportacao();" runat="server" Text="Validar" class="btn btn-primary" />

                                </div>
                            </div>
                        </div>
                    </div>
                    <%--Grid com o resultado da consulta feita Pagamento Manual--%>
                    <div id="dvResultadoPagamento" class="col-lg-12 col-12" style="padding: 50px 0;" style="padding: 50px 0;">
                        <div class="form-group-material">
                            <div class="card-header d-flex">
                                <div class="col-md-6">
                                    <h3>Resultado consulta</h3>
                                </div>
                                <div class="col-md-6" style="float: right; text-align: right;">
                                    <h6>Total retornado: <span id="lblTotalResultado"></span></h6>

                                </div>
                            </div>
                            <div class="card-body no-padding" id="tblPagManual">
                            </div>
                        </div>
                    </div>

                    <%--Grid com o resultado da consulta feita Pagamento Automatico--%>
                    <div id="dvResultadoPagamentoAutomatico" class="col-lg-12 col-12" style="display: none; padding: 50px 0;" style="padding: 50px 0;">
                        <div class="form-group-material">

                            <div class="card-header d-flex">
                                <div class="col-md-6">
                                    <h3>Resultado consulta</h3>
                                </div>
                                <div class="col-md-6" style="float: right; text-align: right;">
                                    <h6>Total retornado: <span id="lblTotalResultadoAutomatico"></span></h6>

                                </div>
                            </div>
                            <div class="card-body no-padding" id="tblPagAuto">
                            </div>
                        </div>
                    </div>


                </div>
        </section>
    </div>

    <!-- Modal Manual-->
    <div id="mdPagamento" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" style="width: 100%">
            <!-- Modal content-->
            <div class="modal-content" style="background: #e9e9e9;">
                <i id="faClosePagamento" class="fa fa-window-close" style="position: absolute; top: 4px; right: 5px; z-index: 1; font-size: 25px; color: red; cursor: pointer"></i>
                <div style="border-radius: 10px; border: 1px solid #045531; margin: 50px 15px 100px 15px; background: #fff;">
                    <div class="row" style="width: 90%; margin-left: 5%">
                        <div class="col-lg-12">
                            <span class="modal-title-pag">
                                <h3>Baixa de Pagamento Manual</h3>
                            </span>
                            <input type="hidden" id="hddId" />
                            <hr />
                        </div>
                    </div>
                    <div class="row" style="width: 90%; margin-left: 5%">
                        <hr />
                        <div class="col-lg-12">
                            <div class="content">
                                <div>
                                    <span class="modal-label lbl-pag">Nome:</span><span class="modal-data" id="sp-nome"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="align-items-center">
                                <div class="content">
                                    <div>
                                        <span class="modal-label lbl-pag">CPF:</span><span class="modal-data" id="sp-cpf"></span>
                                    </div>

                                    <div>
                                        <span class="modal-label lbl-pag">Data Vencimento:</span><span class="modal-data" id="sp-data-vencimento"></span>
                                    </div>

                                    <div>
                                        <span class="modal-label lbl-pag">CRM:</span><span class="modal-data" id="sp-crm"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="align-items-center">
                                <div class="content">
                                    <div>
                                        <span class="modal-label lbl-pag">Plano:</span><span class="modal-data" id="sp-plano"></span>
                                    </div>

                                    <div>
                                        <span class="modal-label lbl-pag">Forma de Pagamento:</span><span class="modal-data" id="sp-forma-pagamento"></span>
                                    </div>

                                    <div>
                                        <span class="modal-label lbl-pag">Valor Prêmio:</span><span class="modal-data" id="sp-valor-premio"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="width: 90%; margin-left: 5%; margin-bottom: 70px;">
                        <div class="col-lg-12">
                            <hr />
                            <div class="row">
                                <div class="col-md-3"><span class="modal-label lbl-pag"><a class="campo-obrigatorio" style="color: red;">*</a>Data de Pagamento:</span></div>
                                <div class="col-md-3"><span class="modal-label lbl-pag"><a class="campo-obrigatorio" style="color: red;">*</a>Número do Documento:</span></div>
                                <div class="col-md-3"><span class="modal-label lbl-pag"><a class="campo-obrigatorio" style="color: red;">*</a>Prêmio Corrigido:</span></div>
                                <div class="col-md-3"><span class="modal-label lbl-pag">Juros:</span></div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <input name="txtDtPagamento-editSegurado" maxlength="10" id="txtDtPagamento-editSegurado" class="input-material-titular input-modal-pag data calendarioPag" type="text"
                                        onkeypress="MascaraData(this)" onkeydown="CampoValido(this, 'SPtxtDtInicioObra')" value="" />
                                    <span id="SPRetornoErroModalDtPagamento" class="required-error" style="display: none;">É obrigatório preencher o Nº do documento</span>
                                </div>
                                <div class="col-md-3">
                                    <input name="txtnrDocumento-editPagamento" class="input-material-titular input-modal-pag" id="txtnrDocumento-editSegurado" type="text" value="" />
                                    <span id="SPRetornoErroModalTxtnrDocumento" class="required-error" style="display: none;">É obrigatório preencher o Nº do documento</span>
                                </div>
                                <div class="col-md-3">
                                    <input name="txtpremioCorrigido-editPagamento" class="input-material-titular input-modal-pag" onkeypress="mascaraMoeda(this)" id="txtpremioCorrigido-editSegurado" type="text" value="" />
                                    <span id="SPRetornoErroModaltxtpremioCorrigido" class="required-error" style="display: none;">É obrigatório preencher o Nº do documento</span>
                                </div>
                                <div class="col-md-3">
                                    <input name="txtJurosPagamento-editPagamento" class="input-material-titular input-modal-pag" onkeypress="mascaraMoeda(this)" id="txtjuros-editSegurado" type="text" value="" />
                                    <span id="SPRetornoErroModaltxtJuros" class="required-error" style="display: none;">É obrigatório preencher o Nº do documento</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="width: 100%; margin-top: -1%">
                        <div id="btnBaixaPag">
                            <button id="btnSalvar" type="button" onclick="SalvarDadosManterBaixa()" class="btn btn-primary btn-concluir">Salvar e Sair</button>
                        </div>
                    </div>
                    <div class="form-group-material" style="width: 100%; margin-top: -5%">
                        <div class="row">
                            <div class="col-md-12 " style="text-align: center;">
                                <span id="SPRetornoErroModal" class="required-error" style="display: none;">É obrigatório preencher o Nº do documento</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <div class="modal" id="mdPagamentoAutomatico" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <%--<i id="faClosePagamentoAutomatico" class="fa fa-window-close" style="position: absolute; top: 4px; right: 5px; z-index: 1; font-size: 25px; color: red; cursor: pointer"></i>--%>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 text-center" style="padding-bottom: 10px; margin-top: 15px;">
                            <span id="SPUploadBaixaManual" style="display: none; font-size: 1.2em;"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-sm-12 text-center" style="padding-bottom: 10px;">
                            <h5 style="font-size: 1.2em;">Deseja importar o arquivo? </h5>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5%;">
                        <div class="col-md-6 col-sm-12" style="padding-bottom: 10px;">
                            <asp:Button ID="btnImportar" OnClientClick="return ImportarArquivoBaixaAutomatica();" runat="server" Text="Importar" Width="100%" class="btn btn-primary" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <button type="button" class="btn btn-danger" id="btnFecharImportacaoBaixaAutomatica" data-dismiss="modal" style="width: 100%">Cancelar</button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-sm-12 text-center" style="padding-bottom: 10px;">
                            <span id="SPRetornoImportacao" class="required-error" style="display: none; width: 190px">O arquivo foi importado com sucesso</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="mdPagamentoAutomaticoSucesso" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <%--<i id="faClosePagamentoAutomaticoSucesso" class="fa fa-window-close" style="position: absolute; top: 4px; right: 5px; z-index: 1; font-size: 25px; color: red; cursor: pointer"></i>--%>
                <div class="modal-body">
                    <div class="row" style="padding-left: 25px; padding-right:25px;">
                        <div class="col-md-12 col-sm-12 text-center" style="padding-bottom: 10px; margin-top: 15px;">
                            <span id="SPUploadBaixaManualSucesso" style="font-size: 1.2em;">Arquivo importado</span>
                        </div>
                       
                    </div>
                     <div class="row" style="margin-top: 5%;">
                             <div class="col-md-2 col-sm-12" style="padding-bottom: 10px;"></div>
                            <div class="col-md-8 col-sm-12 text-center" style="padding-bottom: 10px;">
                                 <button type="button" class="btn btn-primary" id="faClosePagamentoAutomaticoSucesso" data-dismiss="modal" style="width: 100%">Ok</button>
                            </div>
                            <div class="col-md-2 col-sm-12" style="padding-bottom: 10px;"></div>
                        </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">    
    <script src="../ContentAdm/jquery/jquery.priceformat.js"></script>    
</asp:Content>

