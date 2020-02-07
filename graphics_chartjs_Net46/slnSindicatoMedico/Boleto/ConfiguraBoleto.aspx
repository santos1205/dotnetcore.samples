<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="ConfiguraBoleto.aspx.cs" Inherits="slnSindicatoMedico.MasterPage.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
        a {
            text-decoration: underline;
            color: blue;
        }

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
           #dvConfiguracaoBoleto
            {
                padding:0px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">

    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Boletos</h2>
            </div>
        </div>
    </header>
     <div class="breadcrumb-holder container-fluid ">
        <ul class="breadcrumb">
            <li class="breadcrumb-item" id="nav-form2">Gerar Boleto</li>
        </ul>
    </div>
    <div id="dvConfiguracaoBoleto" class="container-fluid">
        <section style="padding: 10px 0 170px;">
            <div class="container-fluid">
                <div class="row col-lg-12 col-12">
                    <div class="card-header card-menu" style="margin-left: 1px !important;">
                        <ul class="nav nav-tabs card-header-tabs">
                            <li class="nav-item" style="margin-left: -6px">
                                <span class="nav-link active" style="cursor: pointer" id="dv-aba-config-boleto">Configuração de Boleto</span>
                            </li>
                            <li class="nav-item" style="margin-right: -6px; cursor: pointer">
                                <span class="nav-link" id="dv-aba-gerar-boleto">Gerar Arquivo Boleto</span>
                            </li>
                        </ul>
                    </div>
                    <%--Div Configuração boleto--%>
                    <div class="card-body bg-white col-md-12" id="dv-content-config" style="height:500px">
                        <div class="form-group-material config-boleto-container">
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="row form-group-material">
                                        <div class="col-md-12">
                                            <asp:TextBox ID="inputNomeConfiguracaoBoleto" runat="server" MaxLength="100" type="text" CssClass="input-material" value=""></asp:TextBox>
                                            <label for="inputNomeConfiguracaoBoleto" class="label-material">Nome do Arquivo de Remessa</label>
                                            <span id="SPRetornoErroNomeArquivo" class="required-error" style="float: left; font-size: 0.9em; display: none;">Nome do arquivo obrigatório!</span>
                                        </div>
                                        <%--<div class="col-md-2">
                                            <button type="button" style="float: left;" class="btn btn-primary">Incluir</button>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <%--CHECKBOXs CONFIGURAÇÕES--%>
                                    <h3>Arquivos</h3>
                                    <input type="hidden" id="hdd-id-template-selecionado" />
                                    <div class="container-radio-boleto">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="row">
                                        <div class="col-md-9">
                                            <h3>Parâmetros de configuração do boleto</h3>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                </div>
                            </div>
                            <%--CHECKBOXs CAMPOS--%>
                            <div class="row">
                                <div class="col-md-7 checks-campos-boleto">
                                    <%--<div class="row">
                                        <div class="col-md-5">                                                                                        
                                            <div class="i-checks">
                                                <select class="cmb-campo-boleto-ordem" id="combo-nrDoc-ordem">
                                                    
                                                    
                                                </select>
                                              <input id="chk-nr-doc" type="checkbox" value="" class="checkbox-template chk-cmp-ordem" />
                                              <label for="chk-nr-doc">Nº Doc</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">                                            
                                            <div class="i-checks">
                                                <select class="cmb-campo-boleto-ordem" id="combo-idFiliacao-ordem">
                                                    
                                                    
                                                </select>
                                              <input id="chk-id-filiacao" type="checkbox" value="" class="checkbox-template chk-cmp-ordem" />
                                              <label for="chk-id-filiacao">ID. Filiação</label>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="col-md-5">
                                </div>
                            </div>
                        </div>                        
                        <div class="form-group-material">
                            <div class="row">
                                <button id="btnSalvarLayout" type="button" style="float: left;" class="btn btn-primary">Salvar e definir como padrão</button>                            
                            </div>                            
                        </div>
                        <div class="form-group-material">
                            <div class="row">
                                <div class="col-md-12 box-msg-boleto-sucesso" id="SPRetornoSuccessConfigBoleto" style="display: none">
                                    <span>
                                        <i class="fa fa-check fa-2x" style="margin-top: -3px"></i>
                                        <span style="font-weight: 600; vertical-align: top; margin: 1px 8px;">Configuração salva com sucesso</span>                                         
                                    </span>
                                </div>

                                <div class="col-md-12 " style="text-align: center;">                                    
                                    <span id="SPRetornoErroConfigBoleto" class="required-error" style="float: left; font-size: 0.9em; display: none;">Não é permitido ordens repedidas!</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--Div de gerar boleto--%>
                    <div class="card-body bg-white col-md-12" id="dv-content-gerar-boleto" style="display: none">
                        <%--Div de gerar boleto - Form. consulta--%>
                        <div class="form-group-material">
                            <div class="row">
                                <div class="col-md-12">
                                    <font color="#0d408f" size="2px">Nome Arquivo</font>
                                    <asp:DropDownList ID="cmbNomeArquivo_gerarBoleto" CssClass="dropdown-material" runat="server">
                                        <asp:ListItem Value="" Text="-- Selecione --"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <font color="#0d408f" size="2px">Período</font>
                                    <asp:DropDownList ID="cmbPeriodo_gerarBoleto" CssClass="dropdown-material" runat="server">                                        
                                        <asp:ListItem Value="1" Text="Janeiro" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Fevereiro"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Março"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Abril"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Maio"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Junho"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Julho"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Setembro"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="Outubro"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="Novembro"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="Dezembro"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6" style="margin-top: 25px">
                                    <asp:TextBox ID="txtNrDocumento_gerarBoleto" runat="server" type="text" onKeyPress="mascaraInteiro()" CssClass="input-material"
                                        MaxLength="10" value="" />
                                    <label for="txtNrDocumento" class="label-material">Nº Documento</label>
                                    <span id="SPtxtNrDocumento" class="required-error" style="display: none;"></span>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCpf_gerarBoleto" runat="server" type="text" onKeyPress="MascaraCPF(this)" CssClass="input-material"
                                        MaxLength="14" value=""></asp:TextBox>
                                    <label for="txtCpf_gerarBoleto" class="label-material">CPF</label>
                                    <span id="SPtxtCpf_gerarBoleto" class="required-error" style="display: none;"></span>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtFiliacao_gerarBoleto" runat="server" type="text" onKeyPress="mascaraInteiro()" CssClass="input-material"
                                        MaxLength="15" value="" />
                                    <label for="txtFiliacao_gerarBoleto" class="label-material">Nº Filiação</label>
                                    <span id="SPtxtFiliacao_gerarBoleto" class="required-error" style="display: none;"></span>
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
                            <div class="row">
                                <div class="col-md-12">
                                    <button id="btnGerarBoleto" type="button" style="float: left;" class="btn btn-primary">Gerar</button>
                                    <div id="dv-btn-limpar">
                                        <button id="btn-limpar-gerar-boleto" type="button" onclick="LimpaGerarBoletoForm()" style="float: right;" class="btn btn-primary">Limpar</button>
                                    </div>                                    
                                </div>
                            </div>                            
                            <div class="row">
                                <div class="col-md-12 box-msg-boleto-sucesso" id="box-msg-boleto-sucesso" style="display: none; margin-top: 20px">
                                    <span>
                                        <i class="fa fa-check fa-2x" style="margin-top: -3px"></i>
                                        <span style="font-weight: 600; vertical-align: top; margin: 1px 8px;">Arquivo gerado com sucesso.</span> 
                                        <span style="vertical-align: top; margin: 1px -5px;" id='msg-boleto-link'>Para realizar o download, <a href="../TempFiles/epx_boleto_0719.txt" download="boleto_XXXX.txt">Clique aqui</a></span>
                                    </span>
                                </div>
                                <div class="col-md-12 box-msg-boleto-sucesso" id="box-msg-boleto-erro" style="display: none; margin-top: 20px">
                                    <span>
                                        <i class="fa fa-times fa-2x" style="color: red"></i>
                                        <span style="font-weight: 600; color: red; vertical-align: top; margin: 3px 8px;" id="sp-msg-boleto-erro">Erro ao gerar o Arquivo</span>                                         
                                    </span>
                                </div>
                            </div>
                            
                            <%--Div Histórico de exportação--%>
                            <div id="dv-grid-historico-exportacao" class="col-md-12">
                                <div class="form-group-material">
                                    <div class="">
                                        <div class="col-md-12" style="margin-left: -17px">
                                            <div class="card-header d-flex">
                                                <div class="col-md-6" style="margin-left: -31px;">
                                                    <h3>Histórico de exportação</h3>
                                                </div>
                                                <div class="col-md-6" style="float: right; text-align: right;">
                                                    <%--<h6>Total retornado: <span id="lblTotalResultado"></span></h6>--%>
                                                </div>
                                            </div>
                                            <div class="card-body no-padding">
                                                <table class="table table-hover">
                                                    <tr>
                                                        <td align="center"><b>Data Geração</b></td>
                                                        <td align="center"><b>Responsável</b></td>
                                                        <td align="center"><b>Mês referência</b></td>
                                                        <td align="center"><b>Status</b></td>
                                                    </tr>
                                                    <tbody class="tbody">
                                                        <tr>
                                                            <td class="DataEvento" align="center"></td>
                                                            <td class="Usuario" align="center"></td>
                                                            <td class="MesRef" align="center"></td>
                                                            <td class="Status" align="center"></td>
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </section>
    </div>
    <!-- /.modal -->
    <div class="modal" id="md_gerarBoleto" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="text-center" style="padding: 5%;">
                            <h5 style="font-size: 1.2em;">Aguarde... O arquivo está sendo gerado</h5>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <div class="medium-loader"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">    
    <script src="../ContentAdm/jquery/jquery.priceformat.js"></script>
    <script src="../ContentAdm/jquery/jquery-ui.min.js"></script>
</asp:Content>
