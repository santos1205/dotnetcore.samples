<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="ConsultaLog.aspx.cs" Inherits="slnSindicatoMedico.MasterPage.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../ContentAdm/css/jquery.range.css" rel="stylesheet" />
    <link href="../ContentAdm/css/jquery-ui.css" rel="stylesheet" />

    <style type="text/css">
        #dvSegurado,
        #dvResultadoSegurado {
            margin: 0 auto;
             margin-top: 1%;
        }

        #dvConsultaLog {
            padding: 0;
        }

        input:checked + .small-slider {
            background-color: green;
        }

        #dvConsultaLog > section {
            padding-top: 1%;
            padding: 0px 0 170px !important;
        }
 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Consultar Log</h2>
            </div>
        </div>
    </header>

    <%-- Subtitulo - diretorio de navegação --%>
    <div class="breadcrumb-holder container-fluid ">
        <ul class="breadcrumb">
            <li class="breadcrumb-item" id="nav-form1">Log</li>
            <li class="breadcrumb-item" id="nav-form2">Consultar Logs</li>
        </ul>
    </div>

    <div id="dvConsultaLog" class="container-fluid">
        <section>
            <div class="container-fluid">
                <div class="row">
                    <div id="dvSegurado" class="col-lg-12 col-12">
                        <div class="card-header d-flex">
                            <h3>Consultar Logs</h3>
                        </div>
                        <div class="card-body bg-white">
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Usuário</font>
                                        <div id="dv-cmb-usuario">
                                            <select id="cmbUsuario" class="dropdown-material">
                                                <option value="0">-- Selecione --</option>
                                            </select>
                                        </div>                                        
                                    </div>
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Ação</font>
                                        <div id="dv-cmb-acoes">
                                            <select id="cmbAcoes" class="dropdown-material">
                                                <option value="0">-- Selecione --</option>
                                            </select>                                            
                                        </div>                                        
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <input name="txtPeriodoInicio" maxlength="10" id="txtPeriodoInicio" class="input-material calendario" type="text" 
                                                onkeypress="MascaraData(this)" onkeydown="CampoValido(this, 'SPtxtDtInicioObra')" value="" />
                                        <label for="txtPeriodoInicio" style="font-size: 0.8em;top: -10px;color: #0d408f;" >Período Início</label>
                                        <span id="SPtxtPeriodoInicio" class="required-error" style="display: none;"></span>
                                    </div>
                                    <div class="col-md-6">
                                        <input name="txtPeriodoFim" maxlength="10" id="txtPeriodoFim" class="input-material calendario" type="text" 
                                                onkeypress="MascaraData(this)" onkeydown="CampoValido(this, 'SPtxtDtInicioObra')" value="" />
                                        <label for="txtPeriodoFim" style="font-size: 0.8em;top: -10px;color: #0d408f;">Período Fim</label>
                                        <span id="SPtxtPeriodoFim" class="required-error" style="display: none;"></span>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-12 " style="text-align: center;">
                                        <span id="SPRetornoErro" class="required-error" style="display: none;">Preencha ao menos um campo para pesquisar um Segurado!</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group-material">
                                <button id="btnConsultarLog" type="button" style="float: left;" class="btn btn-primary">Consultar</button>
                                <button id="btnLimpar" type="button" style="float: right;" class="btn btn-primary">Limpar</button>
                            </div>
                        </div>
                    </div>
                    <div id="dvResultadoLogs" class="col-lg-12 col-12" style="display: block; padding: 50px 0;overflow: auto;">
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
                                    <div class="card-body no-padding" style="overflow: auto; height: 1200px">
                                        <table class="table table-hover">
                                            <tr>
                                                <td align="center"><b>Nome</b></td>
                                                <td align="center"><b>Departamento</b></td>
                                                <td align="center"><b>Ação</b></td>
                                                <td align="center"><b>Nome do Segurado</b></td>
                                                <td align="center"><b>Data</b></td>
                                            </tr>
                                            <tbody class="tbody">
                                                <tr>
                                                    <td class="Nome" align="center"></td>
                                                    <td class="Departamento" align="center"></td>
                                                    <td class="ActLog" align="center"></td>
                                                    <td class="NomeSegurado" align="center"></td>
                                                    <td class="Data" align="center"></td>
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
</asp:Content>
