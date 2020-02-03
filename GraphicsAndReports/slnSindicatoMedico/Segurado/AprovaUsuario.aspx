<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="AprovaUsuario.aspx.cs" Inherits="slnSindicatoMedico.MasterPage.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../ContentAdm/css/jquery-ui.css" rel="stylesheet" />

    <style type="text/css">
        #dvCadastro,
        #dvResultadoConsulta {
            margin: 0 auto;
        }
        #dvAprovaCadastro {
            padding:0;
        }

        #dvAprovaCadastro >section {
            padding-top:1%;
        }
            [data-tooltip] {
                display: inline-block;
                position: relative;
            }
            /* Tooltip styling */
            [data-tooltip]:before {
                content: attr(data-tooltip);
                display: none;
                position: absolute;
                background: #045531;
                color: #fff;
                padding: 4px 8px;
                font-size: 14px;
                line-height: 1.4;
                min-width: 100px;
                text-align: center;
                border-radius: 4px;
            }
            /* Dynamic horizontal centering */
            [data-tooltip-position="top"]:before,
            [data-tooltip-position="bottom"]:before {
                left: 50%;
                -ms-transform: translateX(-50%);
                -moz-transform: translateX(-50%);
                -webkit-transform: translateX(-50%);
                transform: translateX(-50%);
            }
            /* Dynamic vertical centering */
            [data-tooltip-position="right"]:before,
            [data-tooltip-position="left"]:before {
                top: 50%;
                -ms-transform: translateY(-50%);
                -moz-transform: translateY(-50%);
                -webkit-transform: translateY(-50%);
                transform: translateY(-50%);
            }
            [data-tooltip-position="top"]:before {
                bottom: 100%;
                margin-bottom: 6px;
            }
            [data-tooltip-position="right"]:before {
                left: 100%;
                margin-left: 6px;
            }
            [data-tooltip-position="bottom"]:before {
                top: 100%;
                margin-top: 6px;
            }
            [data-tooltip-position="left"]:before {
                right: 100%;
                margin-right: 6px;
            }

            /* Tooltip arrow styling/placement */
            [data-tooltip]:after {
                content: '';
                display: none;
                position: absolute;
                width: 0;
                height: 0;
                border-color: transparent;
                border-style: solid;
            }
            /* Dynamic horizontal centering for the tooltip */
            [data-tooltip-position="top"]:after,
            [data-tooltip-position="bottom"]:after {
                left: 50%;
                margin-left: -6px;
            }
            /* Dynamic vertical centering for the tooltip */
            [data-tooltip-position="right"]:after,
            [data-tooltip-position="left"]:after {
                top: 50%;
                margin-top: -6px;
            }
            [data-tooltip-position="top"]:after {
                bottom: 100%;
                border-width: 6px 6px 0;
                border-top-color: #000;
            }
            [data-tooltip-position="right"]:after {
                left: 100%;
                border-width: 6px 6px 6px 0;
                border-right-color: #045531;
            }
            [data-tooltip-position="bottom"]:after {
                top: 100%;
                border-width: 0 6px 6px;
                border-bottom-color: #000;
            }
            [data-tooltip-position="left"]:after {
                right: 100%;
                border-width: 6px 0 6px 6px;
                border-left-color: #000;
            }
            /* Show the tooltip when hovering */
            [data-tooltip]:hover:before,
            [data-tooltip]:hover:after {
                display: block;
                z-index: 50;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Aprovação de Cadastro</h2>
            </div>
        </div>
    </header>

    <%-- Subtitulo - diretorio de navegação --%>
    <div class="breadcrumb-holder container-fluid ">
        <ul class="breadcrumb">
            <li class="breadcrumb-item" id="nav-form1">Cadastro</li>
            <li class="breadcrumb-item" id="nav-form2">Aprovação de Cadastro</li>
        </ul>
    </div>

    <div id="dvAprovaCadastro" class="container-fluid" style="margin-bottom: 10%">
        <section>
            <div class="container-fluid">
                <div class="row">
                    <div id="dvResultadoConsulta" class="col-lg-12 col-12">
                        <div class="card-header d-flex">
                            <h3>Consultar Usuário</h3>
                        </div>
                        <div class="card-body bg-white">
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">&nbsp;</font>
                                        <%--<input type="text" id="txtNomeUsuario" class="input-material" maxlength="80" />--%>
                                        <asp:TextBox ID="txtNomeUsuario" runat="server" type="text" CssClass="input-material mask-cpfcnpj" MaxLength="80" value=""></asp:TextBox>
                                        <label for="txtNomeUsuario" class="label-material">Nome Usuário</label>
                                    </div>
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Status Usuário:</font>
                                        <asp:DropDownList ID="cmbStatusAprov" CssClass="dropdown-material" runat="server">                                            
                                            <asp:ListItem Value="A" Text="Aprovado"></asp:ListItem>
                                            <asp:ListItem Value="R" Text="Reprovado"></asp:ListItem>
                                            <asp:ListItem Value="N" Selected Text="Sem avaliação"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group-material">
                                <button id="btnConsultar" type="button" style="float: left;" class="btn btn-primary" onclick="carregaListaUsuario();">Consultar</button>
                            </div>
                        </div>
                        <div class="form-group-material" id="dvListaUsrPendentes" style="padding-top: 2%;">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card-header d-flex">
                                        <h3>Aprovação de Cadastro</h3>
                                    </div>
                                    <div class="card-body no-padding">
                                        <table class="table table-hover">
                                            <thead>
                                                <tr>                                                    
                                                    <th>Nome</th>                                                    
                                                    <th>CPF</th>                                                    
                                                    <th>E-mail</th>
                                                    <th>Data do Cadastro</th>
                                                    <th style="padding: 0 28px">Perfil</th>
                                                    <th width="20px"><span style="padding-left: 10px; padding-right: 10px">Ação</span></th>
                                                </tr>
                                            </thead>
                                            <tbody class="tbody">
                                                <tr>                                                    
                                                    <td class="Nome"></td>
                                                    <td class="Cpf"></td>
                                                    <td class="Email"></td>
                                                    <td class="DataCadastro"></td>
                                                    <td class="Perfil"></td>
                                                    <td class="Acao"></td>
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
</asp:Content>
