<%@ Page Title="" Language="C#" MasterPageFile="~/Premium.Master" AutoEventWireup="true" CodeBehind="_DownloadApp.aspx.cs" Inherits="Premium.WebForm4" %>

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
                <h2>Download App</h2>
            </div>
        </div>
    </header>

    <div class="container-fluid">
        <section>
            <div class="container-fluid">
                <div class="row">
                    <div id="dvInfoApp" class="col-lg-10 col-12">
                        <div class="card-header d-flex">
                            <h3>Informativos</h3>
                        </div>
                        <div class="card-header">
                            <div style="text-align: justify; text-justify: inter-word;margin-right:36px;margin-left:36px">
                                <div class="form-group-material">
                                    <div class="row">
                                        <b>Atenção:</b> É muito importante que o passageiro / segurado seja instruído a manter uma cópia do voucher sempre em mãos, pois se ocorrer qualquer imprevisto durante a viagem terá as informações e instruções para o contato / acionamento da Central Operativa.
                                    </div>
                                </div>
                                <div class="form-group-material" style="padding-left: 5%;">
                                    <div class="row">
                                        •             Sugerimos que imprima o voucher e deixe uma cópia junto com documento pessoal, pois assim sempre terá em mãos as informações e instruções de procedimentos em caso de emergência.
                                        <br />
                                        <br />
                                        •             Não é necessário imprimir as condições gerais.
                                    </div>
                                    <div class="form-group-material" style="padding-top: 4%;">
                                        <div class="row" style="width: 125%">
                                            <div class="col-md-2" style="text-align: center">
                                                <a href="Content/orientacao acionamento da assistencia.pdf" download>
                                                    <img src="Content/img/icon_pdf.png" alt="" style="width: 50px;" />
                                                    <div class="row">
                                                        <span class="informativo"><i></i>Orientações para uso da assistência</span>
                                                    </div>
                                                </a>
                                            </div>
                                            <div class="col-md-2" style="text-align: center">
                                                <a href="Content/linhas de assistencia.pdf" download>
                                                    <img src="Content/img/icon_pdf.png" alt="" style="width: 50px;" />
                                                    <div class="row">
                                                        <span class="informativo"><i></i>Linhas de assistência</span>
                                                    </div>
                                                </a>
                                            </div>
                                            <div class="col-md-2" style="text-align: center">
                                                <a href="Content/condicoes gerais nacional.pdf" download>
                                                    <img src="Content/img/icon_pdf.png" alt="" style="width: 50px;" />
                                                    <div class="row">
                                                        <span class="informativo"><i></i>Condições gerais nacional</span>
                                                    </div>
                                                </a>
                                            </div>
                                            <div class="col-md-2" style="text-align: center">
                                                <a href="Content/condicoes gerais internacional.pdf" download>
                                                    <img src="Content/img/icon_pdf.png" alt="" style="width: 50px;" />
                                                    <div class="row">
                                                        <span class="informativo"><i></i>Condições gerais internacional</span>
                                                    </div>
                                                </a>
                                            </div>
                                            <div class="col-md-2" style="text-align: center">
                                                <a href="Content/manual do sistema.pdf" download>
                                                    <img src="Content/img/icon_pdf.png" alt="" style="width: 50px;" />
                                                    <div class="row">
                                                        <span class="informativo"><i></i>Manual do sistema</span>
                                                    </div>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="dvDownloadApp" class="col-lg-10">
                                <div class="heading d-flex download-title">
                                    <h3 class="">Download APP</h3>
                                </div>
                                <h6 style="padding: 5px;">Sugerimos ainda que faça download do aplicativo para dispositivos móveis do seu Seguro Viagem.</h6>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-4" style="text-align: left;padding-left:  18px;">
                                            <a href="https://itunes.apple.com/us/app/travel-ace-assistance/id1005784416" target="_blank">
                                                <img src="Content/img/img_app.png" alt="" style="width: 45px;" />
                                                <div class="row">
                                                    <span class="informativo"><i></i>Apple</span>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="col-md-4" style="text-align: left;padding-left:  18px;">
                                            <a href="https://play.google.com/store/apps/details?id=com.mobilenik.travelaceassistance"  target="_blank">
                                                <img src="Content/img/img_app.png" alt="" style="width: 45px;" />
                                                <div class="row"></div>
                                                <span class="informativo"><i></i>Android</span>
                                            </a>
                                        </div>
                                        <div class="col-md-4" style="text-align: left;padding-left:  18px;">
                                            <a href="https://www.microsoft.com/pt-br/store/p/travel-ace-assistance/9nblggh1drs7"  target="_blank">
                                                <img src="Content/img/img_app.png" alt="" style="width: 45px;" />
                                                <div class="row">
                                                    <span class="informativo"><i></i>Windows</span>
                                                </div>
                                            </a>
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
