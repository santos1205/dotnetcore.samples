<%@ Page Title="Planos" Language="C#" MasterPageFile="~/MasterPage/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="Planos.aspx.cs" Inherits="slnSindicatoMedico.Planos.Planos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <link href="../ContentAdm/css/jquery.range.css" rel="stylesheet" />
    <link href="../ContentAdm/css/jquery-ui.css" rel="stylesheet" />

    <style type="text/css">
        #dvPlanos {
            padding: 0;
        }

            #dvPlanos > section {
                padding-top: 1%;
            }

        a.list-group-item {
            height: auto;
            min-height: 145px;
            border-left: 10px solid transparent;
            border-right: 10px solid transparent;
        }

            a.list-group-item:hover, a.list-group-item:focus {
                border-left: 10px solid #045531;
                border-right: 10px solid #045531;
                background-color: #e3e9f2;
            }

        .list-group-item-heading {
            color: black;
            font-size: 20px;
        }

        .list-group-item-text {
            color: black;
            font-size: 13px;
        }

        .icon {
            height: 117px;
            width: 112px;
            margin-left: 10px;
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

        .card-menu > ul >  .nav-item > .active:hover {
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
    <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Planos</h2>
            </div>
        </div>
    </header>

    <%-- Subtitulo - diretorio de navegação --%>
    <div class="breadcrumb-holder container-fluid ">
        <ul class="breadcrumb">
            <li class="breadcrumb-item" id="nav-form1">Planos</li>
            <li class="breadcrumb-item" id="nav-form2">Gerenciar Planos</li>
        </ul>
    </div>

    <div id="dvPlanos" class="container-fluid">
        <section>
            <div class="container-fluid">
                <div class="row col-lg-12 col-12">
                    <div class="card-header card-menu" style="margin-left:1px !important;" >
                        <ul class="nav nav-tabs card-header-tabs" >
                            <li class="nav-item" style="margin-left:-6px">
                                <a class="nav-link active" href="#" id="dv-aba-saude">Saúde</a>
                            </li>
                            <li class="nav-item" style="margin-right:-6px">
                                <a class="nav-link" href="#" id="dv-aba-odonto">Odonto</a>
                            </li>
                        </ul>
                    </div>
                    <%--SAÚDE--%>
                    <div class="card-body bg-white" id="dv-content-saude" style="height: 610px;">
                        <div class="row">
                            <a href="#" class="list-group-item col-md-6">
                                <div class="row">
                                    <div class="media col-md-4">
                                        <figure class="pull-left">
                                            <i>
                                                <img class="icon" src="../ContentAdm/img/icon01Planos.png" />
                                            </i>
                                        </figure>
                                    </div>
                                    <div class="col-md-8" style="padding-left: 0px; padding-top: 10px">
                                        <h4 class="list-group-item-heading">Compacto Enfermaria</h4>
                                        <p class="list-group-item-text">
                                            É o nosso plano de entrada, com custo baixo do mercado, tabela de reembolso padrão, e acomodação
                                            hospitalar na modalidade enfermaria. Sua abrangência é nacional e possui uma rede de hospitais adequada.
                                        </p>
                                    </div>
                                </div>
                            </a>
                            <a href="#" class="list-group-item col-md-6">
                                <div class="row">
                                    <div class="media col-md-4">
                                        <figure class="pull-left">
                                            <i>
                                                <img class="icon" src="../ContentAdm/img/icon04Planos.png" />
                                            </i>
                                        </figure>
                                    </div>
                                    <div class="col-md-8" style="padding-left: 0px; padding-top: 10px">
                                        <h4 class="list-group-item-heading">Superior Apartamento</h4>
                                        <p class="list-group-item-text">
                                            Neste plano, a tabela de reembolso mais atrativa, e inclui praticamente todos os hospitais de referência
                                            em sua rede.
                                        </p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="row">
                            <a href="#" class="list-group-item col-md-6">
                                <div class="row">
                                    <div class="media col-md-4">
                                        <figure class="pull-left">
                                            <i>
                                                <img class="icon" src="../ContentAdm/img/icon02Planos.png" />
                                            </i>
                                        </figure>
                                    </div>
                                    <div class="col-md-8" style="padding-left: 0px; padding-top: 10px">
                                        <h4 class="list-group-item-heading">Efetivo Apartamento</h4>
                                        <p class="list-group-item-text">
                                            Possui tudo o que o plano compacto oferece, com o diferencial da acomodação hospitalar ser na modalidade apartamento.
                                        </p>
                                    </div>
                                </div>
                            </a>
                            <a href="#" class="list-group-item col-md-6">
                                <div class="row">
                                    <div class="media col-md-4">
                                        <figure class="pull-left">
                                            <i>
                                                <img class="icon" src="../ContentAdm/img/icon05Planos.png" />
                                            </i>
                                        </figure>
                                    </div>
                                    <div class="col-md-8" style="padding-left: 0px; padding-top: 10px">
                                        <h4 class="list-group-item-heading">Sênior Apartamento</h4>
                                        <p class="list-group-item-text">
                                            Este é o nosso plano TOP, com tabela de reembolso agressiva, e disponibilizando em sua rede todos os hospitais
                                            de referência a nível nacional.
                                        </p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="row">
                            <a href="#" class="list-group-item col-md-6">
                                <div class="row">
                                    <div class="media col-md-4">
                                        <figure class="pull-left">
                                            <i>
                                                <img class="icon" src="../ContentAdm/img/icon03Planos.png" />
                                            </i>
                                        </figure>
                                    </div>
                                    <div class="col-md-8" style="padding-left: 0px; padding-top: 10px">
                                        <h4 class="list-group-item-heading">Completo Apartamento</h4>
                                        <p class="list-group-item-text">
                                            Também de abrangência nacional, possui a tabela de reembolso diferenciada, com acomodação
                                            na modalidade apartamento e como diferencial, sua rede hospitalar é mais abrangente.
                                        </p>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>

                    <%--ODONTO--%>
                    <div class="card-body bg-white" id="dv-content-odonto" style="display: none; height: 610px;">
                        <div class="row">
                            <a href="#" class="list-group-item col-md-6">
                                <div class="row">
                                    <div class="media col-md-4">
                                        <figure class="pull-left">
                                            <i>
                                                <img class="icon" src="../ContentAdm/img/icons/iconOdontoEssencial.png" />
                                            </i>
                                        </figure>
                                    </div>
                                    <div class="col-md-8" style="padding-left: 0px; padding-top: 10px">
                                        <h4 class="list-group-item-heading">Essencial</h4>
                                        <p class="list-group-item-text">
                                            195 procedimentos.
                                            <br />
                                            Coberturas do rol da ANS vigente.
                                        </p>
                                    </div>
                                </div>
                            </a>
                            <a href="#" class="list-group-item col-md-6">
                                <div class="row">
                                    <div class="media col-md-4">
                                        <figure class="pull-left">
                                            <i>
                                                <img class="icon" src="../ContentAdm/img/icons/iconOdontoPleno.png" />
                                            </i>
                                        </figure>
                                    </div>
                                    <div class="col-md-8" style="padding-left: 0px; padding-top: 10px">
                                        <h4 class="list-group-item-heading">Pleno</h4>
                                        <p class="list-group-item-text">
                                            225 procedimentos.<br />
                                            Coberturas do Essencial Plus + Complementares de prótese.
                                        </p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="row">
                            <a href="#" class="list-group-item col-md-6">
                                <div class="row">
                                    <div class="media col-md-4">
                                        <figure class="pull-left">
                                            <i>
                                                <img class="icon" src="../ContentAdm/img/icons/iconOdontoPlus.png" />
                                            </i>
                                        </figure>
                                    </div>
                                    <div class="col-md-8" style="padding-left: 0px; padding-top: 10px">
                                        <h4 class="list-group-item-heading">Essencial Plus</h4>
                                        <p class="list-group-item-text">
                                            214 procedimentos.
                                        </p>
                                    </div>
                                </div>
                            </a>
                            <a href="#" class="list-group-item col-md-6">
                                <div class="row">
                                    <div class="media col-md-4">
                                        <figure class="pull-left">
                                            <i>
                                                <img class="icon" src="../ContentAdm/img/icons/iconOdontoPlenoOrt.png" />
                                            </i>
                                        </figure>
                                    </div>
                                    <div class="col-md-8" style="padding-left: 0px; padding-top: 10px">
                                        <h4 class="list-group-item-heading">Pleno Ortodontia</h4>
                                        <p class="list-group-item-text">
                                            Este é o nosso plano TOP, com todos os procedimentos do produto pleno mais instalação e manutenção do aparelho.
                                        </p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="row">
                            <a href="#" class="list-group-item col-md-6">
                                <div class="row">
                                    <div class="media col-md-4">
                                        <figure class="pull-left">
                                            <i>
                                                <img class="icon" src="../ContentAdm/img/icons/iconOdontoDoc.png" />
                                            </i>
                                        </figure>
                                    </div>
                                    <div class="col-md-8" style="padding-left: 0px; padding-top: 10px">
                                        <h4 class="list-group-item-heading">Essencial Plus Doc</h4>
                                        <p class="list-group-item-text">
                                            227 procedimentos.
                                            <br />
                                            Coberturas do Essencial Plus + Documentação ortodôntica.
                                        </p>
                                    </div>
                                </div>
                            </a>
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
