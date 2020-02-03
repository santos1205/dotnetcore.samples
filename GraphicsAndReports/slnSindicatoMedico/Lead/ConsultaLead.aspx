<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="ConsultaLead.aspx.cs" Inherits="slnSindicatoMedico.MasterPage.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

    <link href="../ContentAdm/css/jquery.range.css" rel="stylesheet" />
    <link href="../ContentAdm/css/jquery-ui.css" rel="stylesheet" />

    <style type="text/css">
        #dvLeads,
        #dvResultadoLeads {
            margin: 0 auto;
        }

        #dvConsultaLeads {
            padding: 0;
        }

        input:checked + .small-slider {
            background-color: green;
        }

        #dvConsultaLeads > section {
            padding-top: 1%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
     <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Consultar Leads</h2>
            </div>
        </div>
    </header>

    <%-- Subtitulo - diretorio de navegação --%>
    <div class="breadcrumb-holder container-fluid ">
        <ul class="breadcrumb">
            <li class="breadcrumb-item" id="nav-form1">Leads</li>
            <li class="breadcrumb-item" id="nav-form2">Consultar Leads</li>
        </ul>
    </div>

    <div id="dvConsultaLeads" class="container-fluid">
        <section>
            <div class="container-fluid">
                <div class="row">
                    <div id="dvLeads" class="col-lg-12 col-12">
                        <div class="card-header d-flex">
                            <h3>Consultar Leads</h3>
                        </div>
                        <div class="card-body bg-white">
                            <div class="form-group-material">
                                <div class="form-group-material">                                
                                <div class="row">
                                    <div class="col-md-6">
                                        <input name="txtPeriodoInicio-lead" maxlength="10" id="txtPeriodoInicio-lead" class="input-material calendario" type="text" 
                                                onkeypress="MascaraData(this)" onkeydown="CampoValido(this, 'SPtxtDtInicioObra')" value="" />
                                        <label for="txtPeriodoInicio-lead" style="font-size: 0.8em;top: -10px;color: #0d408f;" >Período Início</label>
                                        <span id="SPtxtPeriodoInicio-leads" class="required-error" style="display: none;"></span>
                                    </div>
                                    <div class="col-md-6">
                                        <input name="txtPeriodoFim-lead" maxlength="10" id="txtPeriodoFim-lead" class="input-material calendario" type="text" 
                                                onkeypress="MascaraData(this)" onkeydown="CampoValido(this, 'SPtxtDtInicioObra')" value="" />
                                        <label for="txtPeriodoFim-lead" style="font-size: 0.8em;top: -10px;color: #0d408f;">Período Fim</label>
                                        <span id="SPtxtPeriodoFim-leads" class="required-error" style="display: none;"></span>
                                    </div>
                                </div>
                            </div>

                            </div>
                            
                            <div class="form-group-material">
                                <button id="btnConsultarLeads" type="button" style="float: left;" class="btn btn-primary">Consultar</button>
                                <button id="btnLimpar-leads" type="button" style="float: right;" class="btn btn-primary">Limpar</button>
                            </div>
                        </div>
                    </div>
                    <div id="dvResultadoLeads" class="col-lg-12 col-12" style="display: block; padding: 50px 0;">
                        <div class="form-group-material">
                            <div class="">
                                <div class="col-md-12">
                                    <div class="card-header d-flex">
                                        <div class="col-md-6">
                                            <h3>Resultado consulta</h3>
                                        </div>
                                        <div class="col-md-6" style="float: right; text-align: right;">
                                            <h6>Total retornado: <span id="lblTotalResultado"></span></h6>

                                        </div>
                                    </div>
                                    <div class="card-body no-padding">
                                        <table class="table table-hover">
                                            <tr>
                                                <td align="center"><b>Nome</b></td>
                                                <td align="center"><b>CRM</b></td>
                                                <td align="center"><b>Data de Cadastro</b></td>                                                
                                                <td align="center"><b>Celular</b></td>
                                                <td align="center"><b>E-mail</b></td>
                                            </tr>
                                            <tbody class="tbody">
                                                <tr>
                                                    <td class="Nome" align="center"></td>
                                                    <td class="CRM" align="center"></td>
                                                    <td class="DtCadastro" align="center"></td>
                                                    <td class="Celular" align="center"></td>
                                                    <td class="Email" align="center"></td>
                                                    <%--<td class="Acao"></td>--%>
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
        </section>
    </div>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">    
    <script src="../ContentAdm/jquery/jquery.priceformat.js"></script>
    <script src="../ContentAdm/jquery/jquery-ui.min.js"></script>    
</asp:Content>
