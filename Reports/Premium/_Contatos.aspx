<%@ Page Title="" Language="C#" MasterPageFile="~/Premium.Master" AutoEventWireup="true" CodeBehind="_Contatos.aspx.cs" Inherits="Premium.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/css/jquery.range.css" rel="stylesheet" />
    <link href="Content/css/jquery-ui.css" rel="stylesheet" />

    <style type="text/css">
        /*#dvInfoApp,
       #dvDownloadApp {
            margin: 0 auto;
        }*/
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">

    <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Contatos</h2>
            </div>
        </div>
    </header>

    <div class="container-fluid">
        <section>
            <div class="container-fluid">
                <div class="row">
                    <div id="dvInfoApp" class="col-lg-10 col-12">
                        <div class="card-header d-flex">
                            <h3>Fale conosco</h3>
                        </div>
                        <div class="card-body bg-white">
                            <div class="form-group-material">
                                <div class="col-md-12">
                                    <div class="row contato-linha">
                                        <div class="col-md-4">
                                            <span style="padding-left: 24px;"><i class="fa fa-phone" aria-hidden="true" style="padding-right: 15px; font-size: 40px;"></i>(61) 3223-5711</span>
                                        </div>
                                        <div class="col-md-8" style="text-align: center;">
                                            <span><i class="fa fa-envelope-o" aria-hidden="true" style="padding-right: 15px; font-size: 40px;"></i>atendimento@corretorapremium.com.br</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="padding-top: 10%;">
                                <div class="row contato-footer">
                                    * A Premium é uma corretora associada à Proseg.
                                </div>
                            </div>
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
    <div style="padding-top: 130px;"></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">
</asp:Content>

