<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="~/Gerencial/PainelGerencial.aspx.cs" Inherits="slnSindicatoMedico.MasterPage.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../ContentAdm/css/jquery.range.css" rel="stylesheet" />
    <link href="../ContentAdm/css/jquery-ui.css" rel="stylesheet" />
    <link href="../ContentAdm/css/demo.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js@2.8.0"></script>
    <style type="text/css">
        #dvPainelGerencial > .card-group > .card > .card-body {
            padding: 0px !important;
        }

        #dvPainelGerencial {
            padding: 0 30px;
        }

            #dvPainelGerencial > .card-group > .card > .card-body > .card-title {
                text-align: center;
                margin-right: 5px;
                padding: 10px 0;
                font-size: 17px;
                border-bottom: 1px #eee solid;
            }

            #dvPainelGerencial > .card-group > .card {
                box-shadow: 0 0px 0px #333;
                border-right: 1px #eee solid;
            }

        .progres {
            margin-top: 10px;
            border-top: 1px solid #eee;
        }

        .icone-graficos {
            width: 40px;
            height: 40px;
            line-height: 40px;
            text-align: center;
            min-width: 40px;
            max-width: 40px;
            color: #fff;
            border-radius: 50%;
            margin-right: 15px;
            margin-top: 30px;
            margin-left: 10px;
        }

        .titulo-card {
            font-size: 1.0em !important;
        }

        .colunas {
            margin-right: 10px;
            border-right: 1px solid #eee;
        }
        @media only screen and (width : 1600px) {
            .numbers{
                width:160px;
            }
        }
          @media only screen and (width : 1280px) {
            .numbers{
                width:112px;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Painel Gerencial</h2>
            </div>
        </div>
    </header>

    <%-- Subtitulo - diretorio de navegação --%>
    <div class="breadcrumb-holder container-fluid ">
        <ul class="breadcrumb">
            <li class="breadcrumb-item" id="nav-form1">Gerencial</li>
            <li class="breadcrumb-item" id="nav-form2">Paineis</li>
        </ul>
    </div>

    <div id="dvPainelGerencial" class="container-fluid">

        <div class="card-deck" style="margin-top: 15px;">
            <div class="card" style="border-radius: 2%; margin-right: 2px;">
                <div class="content" style="padding: 0px 15px 0px 15px;">
                    <div class="row" style="height: 190px;">
                        <div class="row col-lg-12">
                            <div class="col-lg-4">
                                <div class="icon-big icon-warning text-center" style="font-size: 3em; min-height: 64px;">
                                    <i class="fa fa-stethoscope fa-xs" style="color: #045531"></i>
                                </div>
                            </div>
                            <div class="col-lg-8">
                                <div class="numbers">
                                    <p style="font-size: 16px; margin-top: 30px;">Saúde</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="numbers">
                                <p>Titular: <span id="sp-timer-QtdTitularSaude"><i class="fa fa-hourglass-start ic-timer-consolidados"></i></span></p>
                                <p>Dependente: <span id="sp-timer-QtdDependenteSaude"><i class="fa fa-hourglass-start ic-timer-consolidados"></i></span></p>
                            </div>
                        </div>
                    </div>
                    <div class="footer">
                        <hr />
                        <div class="stats" style="color: #a9a9a9; font-weight: 300;">
                            <i class="fa fa-sync"></i><span style="margin-left: 10px;">Atualizado em</span>
                            <span id="sp-qtd-saude-atualizado" style="margin-left: 25px;"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" style="border-radius: 2%; margin-right: 2px;">
                <div class="content" style="padding: 0px 15px 0px 15px;">
                    <div class="row" style="height: 190px;">
                        <div class="row col-lg-12">
                            <div class="col-lg-4">
                                <div class="icon-big icon-warning text-center" style="font-size: 3em; min-height: 64px;">
                                    <i class="fa fa-tooth fa-xs" style="color: #045531"></i>
                                </div>
                            </div>
                            <div class="col-lg-8">
                                <div class="numbers">
                                    <p style="font-size: 16px; margin-top: 30px;">Odonto</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="numbers">
                                <p>Titular: <span id="sp-timer-QtdTitularOdonto"><i class="fa fa-hourglass-start ic-timer-consolidados"></i></span></p>
                                <p>Dependente: <span id="sp-timer-QtdDependenteOdonto"><i class="fa fa-hourglass-start ic-timer-consolidados"></i></span></p>
                            </div>
                        </div>
                    </div>
                    <div class="footer">
                        <hr />
                        <div class="stats" style="color: #a9a9a9; font-weight: 300;">
                            <i class="fa fa-sync"></i><span style="margin-left: 10px;">Atualizado em</span>
                            <span id="sp-qtd-odonto-atualizado" style="margin-left: 25px;"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" style="border-radius: 2%;     margin-right: 2px;">
                <div class="content" style="padding: 0px 15px 0px 15px;">
                    <div class="row" style="height: 190px;">
                        <div class="row col-lg-12">
                            <div class="col-lg-3">
                                <div class="icon-big icon-warning text-center" style="font-size: 3em; min-height: 64px;">
                                    <i class="fa fa-file-invoice-dollar fa-xs" style="color: #045531"></i>
                                </div>
                            </div>
                            <div class="col-lg-9">
                                <div class="numbers">
                                    <p style="font-size: 16px; margin-top: 30px;">Faturamento</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="numbers">
                                <p>Saúde: <span id="sp-fat-saude"><i class="fa fa-hourglass-start ic-timer-consolidados"></i></span></p>
                                <p>Odonto: <span id="sp-fat-odonto"><i class="fa fa-hourglass-start ic-timer-consolidados"></i></span></p>
                            </div>
                        </div>
                    </div>
                    <div class="footer">
                        <hr />
                        <div class="stats" style="color: #a9a9a9; font-weight: 300;">
                            <i class="fa fa-sync"></i><span style="margin-left: 10px;">Atualizado em</span>
                            <span id="sp-pag-faturamento-atualizado" style="margin-left: 25px;"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" style="border-radius: 2%;    margin-right: 2px;">
                <div class="content" style="padding: 0px 15px 0px 15px;">
                    <div class="row" style="height: 190px;">
                        <div class="row col-lg-12">
                            <div class="col-lg-3">
                                <div class="icon-big icon-warning text-center" style="font-size: 3em; min-height: 64px;">
                                    <i class="fa fa-dollar-sign fa-xs" style="color: #045531"></i>
                                </div>
                            </div>
                            <div class="col-lg-9">
                                <div class="numbers">
                                    <p style="font-size: 16px; margin-top: 20px;">Pagamento  Saúde</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="numbers">
                                <p>Recebido: <span id="sp-pag-recebido-saude"><i class="fa fa-hourglass-start ic-timer-consolidados"></i></span></p>
                                <p>Pendente: <span id="sp-pag-pendente-saude"><i class="fa fa-hourglass-start ic-timer-consolidados"></i></span></p>
                            </div>
                        </div>
                    </div>
                    <div class="footer">
                        <hr />
                        <div class="stats" style="color: #a9a9a9; font-weight: 300;">
                            <i class="fa fa-sync"></i><span style="margin-left: 10px;">Atualizado em</span>
                            <span id="sp-pag-saude-atualizado" style="margin-left: 25px;"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" style="border-radius: 2%;">
                <div class="content" style="padding: 0px 15px 0px 15px;">
                    <div class="row" style="height: 190px;">
                        <div class="row col-lg-12">
                            <div class="col-lg-3">
                                <div class="icon-big icon-warning text-center" style="font-size: 3em; min-height: 64px;">
                                    <i class="fa fa-dollar-sign fa-xs" style="color: #045531"></i>
                                </div>
                            </div>
                            <div class="col-lg-9">
                                <div class="numbers" >
                                    <p style="font-size: 16px; margin-top: 20px;">Pagamento Odonto</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="numbers">
                                <p>Recebido: <span id="sp-pag-recebido-odonto"><i class="fa fa-hourglass-start ic-timer-consolidados"></i></span></p>
                                <p>Pendente: <span id="sp-pag-pendente-odonto"><i class="fa fa-hourglass-start ic-timer-consolidados"></i></span></p>
                            </div>
                        </div>
                    </div>
                    <div class="footer">
                        <hr />
                        <div class="stats" style="color: #a9a9a9; font-weight: 300;">
                            <i class="fa fa-sync"></i><span style="margin-left: 10px;">Atualizado em</span>
                            <span id="sp-pag-pendente-atualizado" style="margin-left: 25px;"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="card text-center" style="border-radius: 3px; margin-top: 35px; padding:10px">
            <div>
               <h5 class="card-title" style="margin-top:10px;">2019</h5>
            </div>
            <div class="row">
                <div class="vertical-text col-md-1" style="margin: 10px 0;">
                    <p style="font-size: 20px;">Quantidade de segurados ativos</p>
                </div>
                <div class="col-md-10">
                    <div id="barsLegend" class="row">
                        <div id="legendas"></div>
                    </div>
                    <canvas id="barsChart" width="500" height="500"></canvas>
                </div>
            </div>
        </div>

        <section class="dashboard-counts no-padding-bottom" style="margin: -30px 0; display:none;">
            <div class="card" style="border-radius: 3px;">
                <div class="bg-white" id="container-grafico-qtde-segurados">
                    <div class="row" style="margin: -43px 0px -61px 85px; font-size: 0.8em;">
                        <span>Legendas:</span>
                        <div class="legenda-saude"></div>
                        Saúde
                        <div class="legenda-odonto"></div>
                        Odonto
                    </div>
                    <div class="row" style="margin-bottom: -46px;">
                        <div class="vertical-text">
                            <p>Quantidade de segurados ativos</p>
                        </div>
                        <div class="graph-scales">
                            <ul class="">
                                <li><span id="sp-limit">0</span> -
                                </li>
                                <li><span id="sp-half-limit">0</span> -
                                </li>
                            </ul>
                        </div>
                        <div class="card-body chart graficos">
                            <canvas id="my-canvas" width="625" height="300" style="margin-top: 44px; margin-left: -42px;" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="row card-body graph-label-mes">
                            <ul>
                                <li>JAN</li>
                                <li>FEV</li>
                                <li>MAR</li>
                                <li>ABR</li>
                                <li>MAI</li>
                                <li>JUN</li>
                                <li>JUL</li>
                                <li>AGO</li>
                                <li>SET</li>
                                <li>OUT</li>
                                <li>NOV</li>
                                <li>DEZ</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <div class="bg-white" id="container-grafico-qtde-premio">
            <div class="col-lg-12 col-12">
                <table class="table" id="PremioConsolidado">
                    <thead>
                        <tr>
                            <th scope="col" class="text-center">Mês</th>
                            <th scope="col" class="text-center"> Saúde</th>
                            <th scope="col" class="text-center"> Odonto</th>
                            <th scope="col" class="text-center">Prêmio Saúde</th>
                            <th scope="col" class="text-center">Prêmio Odonto</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="mes1" style="display:none;">
                            <th scope="row" class="text-center">Janeiro</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes2" style="display:none;">
                            <th scope="row" class="text-center">Fevereiro</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes3" style="display:none;">
                            <th scope="row" class="text-center">Março</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes4" style="display:none;">
                            <th scope="row" class="text-center">Abril</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes5" style="display:none;">
                            <th scope="row" class="text-center">Maio</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes6" style="display:none;">
                            <th scope="row" class="text-center">Junho</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes7" style="display:none;">
                            <th scope="row" class="text-center">Julho</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes8" style="display:none;">
                            <th scope="row" class="text-center">Agosto</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes9" style="display:none;">
                            <th scope="row" class="text-center">Setembro</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes10" style="display:none;">
                            <th scope="row" class="text-center">Outubro</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes11" style="display:none;">
                            <th scope="row" class="text-center">Novembro</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                        <tr id="mes12" style="display:none;">
                            <th scope="row" class="text-center">Dezembro</th>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                            <td class="text-center">-</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">
    <script src="../ContentAdm/js/ValidacoesCamposForm.js"></script>
    <script src="../ContentAdm/js/paginacao.js"></script>
    <script src="../ContentAdm/jquery/jquery.priceformat.js"></script>
    <script src="../ContentAdm/jquery/jquery-ui.min.js"></script>

    <script src="../ContentAdm/js/Chart.js"></script>
    <script src="../ContentAdm/js/legend.js"></script>
    <script src="../ContentAdm/js/Graphics.js"></script>
</asp:Content>
