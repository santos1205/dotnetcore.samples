<%@ Page Title="" Language="C#" MasterPageFile="~/Premium.Master" AutoEventWireup="true" CodeBehind="_Cancelamento.aspx.cs" Inherits="Premium.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/css/jquery.range.css" rel="stylesheet" />
    <link href="Content/css/jquery-ui.css" rel="stylesheet" />

    <style type="text/css">
        #dvProponentesPF1,
        #dvProponentesPF2,
        #dvSeguroPF,
        #dvProponentesPJ,
        #dvSeguroPJ,
        #dvProponentePJSoc,
        #dvCotacaoPF,
        #dvCotacaoPJ,
        #dvValorTotalPF,
        #dvValorTotalPJ {
            margin: 0 auto;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">

    <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Cancelamento</h2>
            </div>
        </div>
    </header>

    <div id="dvPessoaFisica" class="container-fluid">
        <section>
            <div class="container-fluid">
                <div class="row">
                    <input type="hidden" id="hddIdSegurado" />
                    <div id="dvCancelamento" class="col-lg-10 col-12" style="display: block">
                        <div class="card-header d-flex">
                            <h3>Cancelar Seguro Viagem</h3>                            
                            <a style="margin-left: 480px;" href="Content/img/termo%20de%20cancelamento.pdf" download>
                                <img src="Content/img/pdf 22x22.png" />&nbsp;Termo de Cancelamento</a>
                        </div>
                        <div class="card-body bg-white">
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6 ">
                                        <input name="txtNome" maxlength="50" id="txtNome" class="input-material" type="text"  onKeyPress="somenteLetras()" onkeydown="CampoValido(this, SPtxtNome)" value="" />
                                        <label for="txtNome" class="label-material">Nome Completo</label>
                                        <span id="SPtxtNome" class="required-error" style="display: none;">Nome obrigatório!</span>
                                    </div>
                                    <div class="col-md-6" style="padding-right: 0px;">
                                        <input name="txtEmail" maxlength="50" id="txtEmail" class="input-material" type="text" value="" />
                                        <label for="txtEmail" class="label-material">Email</label>
                                        <span id="SPTxtEmail" class="required-error" style="display: none;">telefone obrigatório!</span>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6 ">
                                        <input name="txtCPF" maxlength="14" id="txtCPF" class="input-material" type="text" onkeydown="CampoValido(this, SPTxtCPF)" onkeypress="MascaraCPF(this)" value="" />
                                        <label for="txtCPF" class="label-material">CPF</label>
                                        <span id="SPTxtCPF" class="required-error" style="display: none;">CPF obrigatório!</span>
                                    </div>
                                    <div class="col-md-6" style="padding-right: 0px;">
                                        <input name="txtRG" maxlength="11" id="txtRG" class="input-material" type="text" onkeypress="mascaraInteiro(this)" onkeydown="CampoValido(this, &#39;SPtxtRiscoUF&#39;)" value="" />
                                        <label for="txtRG" class="label-material">RG</label>
                                        <span id="SPtxtRG" class="required-error" style="display: none;">RG obrigatório!</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6 ">
                                        <input name="nrVoucher" maxlength="9" id="nrVoucher" class="input-material" onkeypress="mascaraInteiro(this)" type="text" value="" />
                                        <label for="nrVoucher" class="label-material">Nº do Voucher/Bilhete</label>
                                        <span id="SPnrVoucher" class="required-error" style="display: none;">Voucher obrigatório!</span>
                                    </div>
                                    <div class="col-md-6 ">
                                        <input name="txtTelefone" maxlength="15" id="txtTelefone" onkeypress="MascaraTelefone(this)" class="input-material" type="text" value="" />
                                        <label for="txtTelefone" class="label-material">Telefone</label>
                                        <span id="SPtxtTelefone" class="required-error" style="display: none;">telefone obrigatório!</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group-material">
                                <button type="button" style="float: right;" class="btn btn-primary" onclick="enviarSolicitacao()">Enviar Cancelamento</button>
                            </div>
                        </div>
                    </div>


                    <div id="dvMsgCancelamento" class="col-lg-10 col-12" style="display: none">
                        <div class="card-header d-flex">
                            <h3>Cancelar Seguro Viagem123</h3>
                        </div>
                        <br />
                        <br />
                        <div align="center">
                            <h3>CANCELAMENTO DO SEGURO VIAGEM</h3>
                        </div>
                        <br />
                        <div style="text-align: justify; text-justify: inter-word;">
                            Direito de Arrependimento: O Segurado poderá desistir do seguro contratado, desde que antes da viagem, no prazo de 7 (sete) dias corridos a contar do efetivo pagamento do prêmio ou da emissão do voucher e bilhete do seguro, o que ocorrer por último. Ultrapassado o prazo de arrependimento, o Segurado somente poderá cancelar o produto adquirido no prazo de até 48 horas de antecedência ao de início da vigência do Seguro. O não cumprimento desta condição automaticamente elimina o direito ao reembolso do prêmio pago pelo produto adquirido. A devolução do valor pago se dará pelo mesmo meio e forma de efetivação do pagamento utilizado pelo Segurado ou por outros meios disponibilizados, mediante escolha do Segurado.
                        </div>
                    </div>                    
                </div>
            </div>
        </section>
    </div>
    <!-- /.modal -->
    <div class="modal" id="mdMsgSucesso" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <p>Solicitação de cancelamento enviada!</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <div style="padding-top:130px;"></div>                        
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">

    <script src="Content/js/ValidacoesCamposForm.js"></script>
    <script src="Content/js/list.min.js"></script>
    <script src="Content/js/Cancelamento.js"></script>
    <script src="Content/jquery/jquery.priceformat.js"></script>
    <script src="Content/jquery/jquery-ui.min.js"></script>    
</asp:Content>
