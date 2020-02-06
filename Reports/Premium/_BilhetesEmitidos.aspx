<%@ Page Title="" Language="C#" MasterPageFile="~/Premium.Master" AutoEventWireup="true" CodeBehind="_BilhetesEmitidos.aspx.cs" ClientIDMode="Static"
    Inherits="Premium._BilhetesEmitidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/css/jquery.range.css" rel="stylesheet" />
    <link href="Content/css/jquery-ui.css" rel="stylesheet" />

    <style type="text/css">
        #dvBilhetes {
            margin: 0 auto !important;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">

    <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Bilhetes Emitidos</h2>
            </div>
        </div>
    </header>
    <div id="dvPessoaFisica" class="container-fluid">
        <section>
            <div class="container-fluid">
                <div class="row">
                    <input type="hidden" id="hddIdSegurado" />
                    <div id="dvBilhetes" class="col-lg-10 col-12" style="display: block">
                        <div class="card-header d-flex">
                            <h3>Registro de Voucher</h3>
                        </div>
                        <div class="card-body bg-white">
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6 ">
                                        <input name="txtNome" maxlength="50" id="txtNome" class="input-material" type="text" onkeypress="somenteLetras()" onkeydown="CampoValido(this, 'SPtxtNome');CampoValido(this, 'SPVoucher')" value="" />
                                        <label for="txtNome" class="label-material">Nome do Passageiro</label>
                                        <span id="SPtxtNome" class="required-error" style="display: none;">Nome do passageiro é obrigatório!</span>
                                    </div>
                                    <div class="col-md-6" style="padding-right: 0px;">
                                        <input name="txtCPF" id="txtCPF" class="input-material" type="text" value="" onkeypress="MascaraCPF(this);"
                                           maxlength="14" onkeydown="CampoValido(this, 'SPtxtCPF');CampoValido(this, 'SPVoucher')" />
                                        <label for="txtCPF" class="label-material">CPF</label>
                                        <span id="SPtxtCPF" class="required-error" style="display: none;">CPF obrigatório!</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6 ">
                                        <input name="nrVoucher" id="nrVoucher" class="input-material"  onkeypress="mascaraInteiro(this)" type="text" value="" />
                                        <label for="nrVoucher" class="label-material">Nº do Voucher/Bilhete</label>
                                        <span id="SPnrVoucher" class="required-error" style="display: none;">Voucher obrigatório!</span>
                                    </div>
                                    <div class="col-md-6">
                                        <select id="cmbDestino" class="form-control" style="height:48px;border: 0;border-bottom: 1px solid #e6e6e6;padding:0;font-size:1.2em;background-color: #fff;">
                                                <option value="">-- Selecione Destino --</option>
                                            <option value="Nacional">Nacional</option>
                                            <option value="Schengen">Schengen</option>
                                            <option value="Não Schengen">Não Schengen</option>
                                        </select>




                                        <span id="SPcmbDestino" class="required-error" style="display: none;">Destino obrigatório!</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6 ">
                                        <input name="txtIniVigencia" maxlength="9" id="txtIniVigencia" class="input-material calendario" type="text" onkeydown="CampoValido(this, 'SPtxtIniVigencia');CampoValido(this, 'SPVoucher')" value="" />
                                        <label for="txtIniVigencia" class="label-material">Início da Vigência</label>
                                        <span id="SPtxtIniVigencia" class="required-error" style="display: none;">Início da vigência obrigatório!</span>
                                    </div>
                                    <div class="col-md-6" style="padding-right: 0px;">
                                        <input name="txtFimVigencia" maxlength="2" id="txtFimVigencia" class="input-material calendario" type="text" 
                                             onkeydown="CampoValido(this, 'SPtxtFimVigencia');CampoValido(this, 'SPVoucher')" value="" />
                                        <label for="txtFimVigencia" class="label-material">Fim da Vigência</label>
                                        <span id="SPtxtFimVigencia" class="required-error" style="display: none;">Fim da vigência é obrigatório!</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6" style="padding-right: 0px;">
                                        <input name="txtDiarias" maxlength="2" id="txtDiarias" class="input-material" type="text" onclick="populaDiarias(); populaPremio($('#txtDiarias').val());"
                                             onkeydown="CampoValido(this, 'SPtxtDiarias');CampoValido(this, 'SPVoucher')" value="" readonly="true" />
                                        <label for="txtDiarias" class="label-material">Diárias</label>
                                        <span id="SPtxtDiarias" class="required-error" style="display: none;">Quantidade de diárias é obrigatório!</span>
                                    </div>
                                    <div class="col-md-6 ">
                                        <input name="txtPremioLic" id="txtPremioLic" class="input-material valor-formato" type="text" value="" 
                                             readonly="true"  onkeydown="CampoValido(this, 'SPtxtPremioLic');CampoValido(this, 'SPVoucher')" />
                                        <label for="txtPremioLic" class="label-material">Prêmio Licitação</label>
                                        <span id="SPtxtPremioLic" class="required-error" style="display: none;">Prêmio Licitação obrigatório!</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6" style="padding-right: 0px;">
                                        <font color="#0d408f" size="2px">Status:</font>
                                        <select id="cmbStatus" class="form-control" disabled="disabled" style="height:48px;border: 0;border-bottom: 1px solid #e6e6e6;padding:0;font-size:1.2em;background-color: #fff;">
                                            <option value="">Selecione</option>
                                            <option value="1">Emitido</option>
                                            <option value="2">Cancelado</option>
                                            <option value="3">Descontinuado</option>
                                        </select>                                        
                                        <span id="SPcmbStatus" class="required-error" style="display: none;">Status é obrigatório!</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group-material" style="text-align:center;">
                                <button type="button" style="float: left;" class="btn btn-primary" onclick="validaBilhete();">Salvar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <!-- Modal -->
    <div class="modal" id="mdlConfirma" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 30%">
            <div class="modal-content">
                <div class="modal-body" style="padding:60px;">
                    <div class="row">
                        <div class="text-center" style="padding-bottom: 10px; margin: auto">
                            <span id="spMsgpSucesso"><h5 style="color:#1e5099; font-size:1.1em;">Voucher cadastrado com sucesso!</h5></span>
                        </div>
                    </div>
                    <div class="row">
                        <div style="margin: auto">
                            <button class="btn btn-primary" id="btnConfirmar" data-dismiss="modal">Ok</button>                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">

    <script src="Content/js/ValidacoesCamposForm.js"></script>
    <script src="Content/js/list.min.js"></script>
    <script src="Content/js/bilheteEmitido.js"></script>
    <script src="Content/jquery/jquery.priceformat.js"></script>
    <script src="Content/jquery/jquery-ui.min.js"></script>
</asp:Content>

