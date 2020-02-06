<%@ Page Title="" Language="C#" MasterPageFile="~/Premium.Master" AutoEventWireup="true" CodeBehind="_ConsultaRelatorio.aspx.cs" ClientIDMode="Static"
    Inherits="Premium.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Content/css/jquery-ui.css" rel="stylesheet" />

    <style type="text/css">
        #dvCotacao,
        #dvResultadoCotacao {
            margin: 0 auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Consultar cotação</h2>
            </div>
        </div>
    </header>


    <div id="dvConsultaCotacao" class="container-fluid">
        <section>
            <div class="container-fluid">
                <div class="row">
                    <div id="dvCotacao" class="col-lg-10 col-12">
                        <div class="card-header d-flex">
                            <h3>Consultar Relatório Voucher</h3>
                        </div>
                        <div class="card-body bg-white">
                            <div class="form-group-material">

                                <div class="row">
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Ano:</font>
                                        <select id="cmbAno" class="dropdown-material"></select>
                                        <span id="SPcmbUsuario" class="required-error" style="display: none;">Ano inválido, por favor verifique!</span>
                                    </div>
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Mês:</font>
                                        <select id="cmbMes" class="dropdown-material"></select>
                                        <span id="SpcmbMes" class="required-error" style="display: none;">Usuário inválido, por favor verifique!</span>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-12 " style="text-align: center;">
                                        <span id="SPRetornoErro" class="required-error" style="display: none;">Preencha ao menos um campo para pesquisar uma cotação!</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group-material">
                                <button id="btnConsultar" type="button" style="float: left;" class="btn btn-primary">Consultar</button>
                                <button id="btnImpressao" type="button" style="float: right; display: none;" class="btn btn-primary" onclick="impressaoRelatorio();">Imprimir</button>
                            </div>
                        </div>
                    </div>
                    <div id="dvResultadoCotacao" class="col-lg-10 col-12" style="display: inline; padding: 50px 0;">
                        <div class="form-group-material">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card-header d-flex">
                                        <div class="col-md-6">
                                            <h3 style="font-weight: bold;">Resultado consulta</h3>
                                        </div>
                                        <div class="col-md-6" style="float: right; text-align: right;">
                                            <h6>Total retornado: <span id="lblTotalResultado"></span></h6>

                                        </div>
                                    </div>
                                    <div class="card-body no-padding">
                                        <table class="table table-hover">
                                            <tr>
                                                <td align="center"><b>Passageiro</b></td>
                                                <td align="center"><b>CPF</b></td>
                                                <td align="center"><b>Voucher</b></td>
                                                <td align="center"><b>Diárias</b></td>
                                                <td align="center"><b>Destino</b></td>
                                                <td align="center"><b>Prêmio</b></td>
                                                <td align="center"><b>Status</b></td>
                                                <td align="center"><b>Ação</b></td>
                                                <td></td>
                                            </tr>
                                            <tbody class="tbody">
                                                <tr>
                                                    <td class="NomePassageiro" align="center"></td>
                                                    <td class="CpfPassageiro" align="center"></td>
                                                    <td class="NumVoucher" align="center"></td>
                                                    <td class="Diaria" align="center"></td>
                                                    <td class="Destino" align="center"></td>
                                                    <td class="vlrPremio" align="center"></td>
                                                    <td class="Status" align="center"></td>
                                                    <td class="Acao" align="center"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <div class="col-md-12 btns-nav" style="margin: 0;">
                                            <div class="row">
                                                <button type="button" onclick="voltarPag()" id="btnVoltar" class="page-link">&laquo; Anterior</button>
                                                <ul class="pagination">
                                                </ul>
                                                <button type="button" onclick="avancarPag()" id="btnProximo" class="page-link">Próximo &raquo;</button>
                                            </div>
                                        </div>
                                        <!-- Modal -->
                                        <div id="mdlAlteraVoucher" class="modal fade" role="dialog">
                                            <div class="modal-dialog modal-lg" style="width: 100%">
                                                <!-- Modal content-->
                                                <div class="modal-content detailBack">
                                                    <div class="modal-title modalTitulo">
                                                        <div class="row">
                                                            <div class="col-md-10">
                                                                <h4 class="">Detalhamento Voucher</h4>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <button type="button" data-dismiss="modal" style="float: right; background-color: transparent; border: 0; color: #fff;">
                                                                    <i class="fa fa-close"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-body" style="text-align: justify">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="">
                                                                        <input type="hidden" style="display: none;" id="hddIdVoucher" />
                                                                        <input type="hidden" style="display: none;" id="hddstrDtInicioVig" />
                                                                        <input type="hidden" style="display: none;" id="hddstrDtFimVig" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group modalBox">
                                                                <div class="col-md-12">
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="row">
                                                                                <label class="lblModal" style="color: #000;">Nome do Passageiro:</label>&nbsp;<label id="lblNomePassageiro" class="label-material" style="font-size: 1.0em"></label>
                                                                            </div>
                                                                            <div class="row">
                                                                                <label class="lblModal" style="color: #000;"><b>Nº do Voucher/Bilhete:</b></label>&nbsp;<label id="lblNumVoucher" class="label-material" style="font-size: 1.0em"></label>
                                                                            </div>
                                                                            <div class="row">
                                                                                <label class="lblModal" style="color: #000;">Inicio da Vigência:</label>&nbsp;<label id="lblIniVig" class="label-material" style="font-size: 1.0em"></label>
                                                                            </div>
                                                                            <div class="row">
                                                                                <label class="lblModal" style="color: #000;">Diárias:</label>&nbsp;<label id="lblDiarias" class="label-material" style="font-size: 1.0em"></label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="row">
                                                                                <label class="lblModal" style="color: #000;">CPF:</label>&nbsp;<label id="lblCPF" class="label-material" style="font-size: 1.0em"></label>
                                                                            </div>
                                                                            <div class="row">
                                                                                <label class="lblModal" style="color: #000;"><b>Destino:</b></label>&nbsp;<label id="lblDestino" class="label-material" style="font-size: 1.0em"></label>
                                                                            </div>
                                                                            <div class="row">
                                                                                <label class="lblModal" style="color: #000;">Fim da Vigência:</label>&nbsp;<label id="lblFimVig" class="label-material" style="font-size: 1.0em"></label>
                                                                            </div>
                                                                            <div class="row">
                                                                                <label class="lblModal" style="color: #000;">Prêmio Licitação:</label>&nbsp;<label id="lblPremioLic" class="label-material" style="font-size: 1.0em"></label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group modalBox">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <label class="lblModal" style="color: #000;">Status:</label>
                                                                        <div class="row">
                                                                            <div class="col-md-4">
                                                                                <input id="rdEmitido" type="radio" value="1" name="radioStatus" class="radio-template" />
                                                                                <label for="rdEmitido">Emitido</label>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <input id="rdDescontinuado" type="radio" value="3" name="radioStatus" class="radio-template" />
                                                                                <label for="rdDescontinuado">Descontinuado</label>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <input id="rdCancelado" type="radio" value="2" name="radioStatus" class="radio-template" />
                                                                                <label for="rdCancelado">Cancelado</label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4 modalBox" id="dvDtCancelamento" style="float:right;">
                                                                <input name="txtCancelamento" maxlength="14" id="txtCancelamento" class="input-material calendario" type="text" style="background-color:transparent"
                                                                    onclick="CampoValido(this, 'SPtxtCancelamento');" value="" />
                                                                <label for="txtCancelamento" class="label-material">Data de Cancelamento</label>
                                                                <span id="SPtxtCancelamento" class="required-error" style="display: none;">Data de Cancelamento é obrigatório!</span>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12 text-center" style="padding-bottom: 10px; margin: auto">
                                                                    <span id="SPMsgRetorno" class="required-error" style="font-size: 1.1em; padding: 30px;"></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="dvFooterStatus" class="modal-footer" style="padding-bottom: 10px; margin: 0 auto">
                                                        <button type="button" class="btn btn-primary" id="btnConfirmar" style="margin-right: 30px" onclick="validaDetalhamento();">Alterar</button>
                                                        <button type="button" class="btn btn-default" id="btnFechar" data-dismiss="modal">Fechar</button>
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
            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">
    <script src="Content/js/ValidacoesCamposForm.js"></script>
    <script src="Content/js/ConsultaRelatorio.js"></script>
    <script src="Content/js/paginacao.js"></script>
    <script src="Content/jquery/jquery-ui.min.js"></script>
</asp:Content>
