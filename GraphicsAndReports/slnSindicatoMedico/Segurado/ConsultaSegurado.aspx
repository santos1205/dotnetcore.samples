 <%@ Page Title="Consultar Segurado" Language="C#" MasterPageFile="~/MasterPage/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="ConsultaSegurado.aspx.cs" Inherits="slnSindicatoMedico.Segurado.ConsultaSegurado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../ContentAdm/css/jquery.range.css" rel="stylesheet" />
    <style type="text/css">
        #dvSegurado,
        #dvResultadoSegurado {
            margin: 0 auto;
        }

        #dvConsultaSegurado {
            padding: 0;
        }

        input:checked + .small-slider {
            background-color: green;
        }

        #dvConsultaSegurado > section {
            padding-top: 1%;
        }
         @media (min-width: 992px) {
            .modal-lg {
                max-width: 1400px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <%-- Header da página --%>
    <header class="page-header">
        <div class="container-fluid">
            <div class="row">
                <h2>Consultar Segurado</h2>
            </div>
        </div>
    </header>

    <%-- Subtitulo - diretorio de navegação --%>
    <div class="breadcrumb-holder container-fluid ">
        <ul class="breadcrumb">
            <li class="breadcrumb-item" id="nav-form1">Segurado</li>
            <li class="breadcrumb-item" id="nav-form2">Consultar Segurado</li>
        </ul>
    </div>

    <div id="dvConsultaSegurado" class="container-fluid">
        <section>
            <div class="container-fluid">
                <div class="row">
                    <div id="dvSegurado" class="col-lg-12 col-12">
                        <div class="card-header d-flex">
                            <h3>Consultar Segurado</h3>
                        </div>
                        <div class="card-body bg-white">
                            <div class="form-group-material">
                                <div class="row">
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">&nbsp;</font>
                                        <asp:TextBox ID="txtCPF" runat="server" type="text" onKeyPress="MascaraCPF(this)" CssClass="input-material input-consultar-segurado"
                                            MaxLength="14" onkeydown="CampoValido(this, 'SPtxtCPF')" value=""></asp:TextBox>
                                        <label for="txtCPF" class="label-material">CPF</label>
                                        <span id="SPtxtCPF" class="required-error" style="display: none;">CPF inválido!</span>
                                    </div>
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">&nbsp;</font>
                                        <asp:TextBox ID="txtSegurado" runat="server" type="text" onkeydown="CampoValido(this, 'SPtxtSegurado')"
                                            CssClass="input-material input-consultar-segurado" value=""></asp:TextBox>
                                        <label for="txtSegurado" class="label-material">Nome do Segurado</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <font color="#0d408f" size="2px">Status</font>
                                        <asp:DropDownList ID="cmbStatusAprov" CssClass="dropdown-material input-consultar-segurado" runat="server">
                                            <asp:ListItem Value="" Text="-- Selecione --" Selected></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Ativo"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Inativo"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 25px">
                                        <asp:TextBox ID="txtCRM" runat="server" type="text" onkeydown="CampoValido(this, 'SPtxtDtFinalCalc')"
                                            CssClass="input-material input-consultar-segurado" onkeypress="mascaraInteiro()" MaxLength="10" value=""></asp:TextBox>
                                        <label for="txtCRM" class="label-material">CRM</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtCelular_ConsultarSegurado" runat="server" type="text" onkeypress="mascaraInteiro()" onkeyup="MascaraTelefone_E_Celular(this)"
                                            CssClass="input-material input-consultar-segurado" MaxLength="15" value=""></asp:TextBox>
                                        <label for="txtCelular-CadSegurado" style="margin: 0px" class="label-material">Telefone</label>
                                        <span id="SPtxtCelular-CadSegurado" class="required-error" style="display: none">Telefone Inválido</span>
                                    </div>

                                    <div class="col-md-6" style="margin-top: 15px">
                                        <asp:TextBox ID="txtEmail_ConsultarSegurado" runat="server" type="text" CssClass="input-material input-consultar-segurado" MaxLength="100"
                                            onkeydown="CampoValido(this, 'SPCelularCadSegurado')" value=""></asp:TextBox>
                                        <label for="txtEmail_ConsultarSegurado-CadSegurado" style="margin: 0px" class="label-material">E-mail</label>
                                        <span id="SPtxtEmail_ConsultarSegurado" class="required-error" style="display: none">E-mail Inválido</span>
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
                                <button id="btnConsultarSegurado" type="button" style="float: left;" class="btn btn-primary">Consultar</button>
                                <button id="btnLimpar_consultarSegurado" type="button" style="float: right;" class="btn btn-primary">Limpar</button>
                            </div>
                        </div>
                    </div>

                    <div id="dvResultadoSegurado" class="col-lg-12 col-12" style="display: block; padding: 50px 0;">
                        <div class="form-group-material">
                            <div class="">
                                <div class="col-md-12" >
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
                                                <td align="center"><b>CRM</b></td>
                                                <td align="center"><b>CPF</b></td>
                                                <td align="center"><b>Nome</b></td>
                                                <td align="center"><b>Parentesco</b></td>
                                                <td align="center"><b>Status</b></td>
                                            </tr>
                                            <tbody class="tbody">
                                                <tr>
                                                    <td class="Crm" align="center"></td>
                                                    <td class="Cpf" align="center"></td>
                                                    <td class="Nome" align="center"></td>
                                                    <td class="Parentesco" align="center"></td>
                                                    <td class="Status" align="center"></td>
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
    <!-- Modal -->
    <div id="mdSegurado" class="modal fade"  role="dialog">
        <div class="modal-dialog modal-lg" style="width: 95%">
            <!-- Modal content-->
            <div class="modal-content content-titular-consulta">
                <div class="row" style="width: 100%; margin-left: 0%">
                    <div class="col-lg-6">
                        <div class="content titular-content">
                            <div class="container-titular-resolution">
                                <span class="modal-title">
                                    <h3 style="font-weight: bold;">Dados do titular</h3>
                                </span>
                                <input type="hidden" id="hddIdSegurado" />
                                <div class="scroll-titular">
                                    <div id="lkCollapseSegurado" class="segurado-border" style="margin-top: 1%">
                                        <div class="segurado-grid" style="padding-left: 4%">
                                            <label class="switch">
                                                <input onclick="" type="checkbox" id="tglStatusTitular" />
                                                <span class="small-slider round" style="top: 4px; left: -15px"></span>
                                            </label>
                                            <span class="modal-label" id="sp-nome-titular" style="margin: 11px 0 -2px -20px"></span>
                                            <a href="#" style="float: right; color: black">
                                                <i class="fa fa-sort-down titular-icon-collapse" style="margin-top: 5px"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="row detalhe-segurado" id="detalheSegurado">
                                        <div style="padding-left: 5%; width: 100%">
                                            <div class="segurado-container" id="dv-collapse-segurado-container-1">
                                                <div class="detalhe-titular-read-edit" id="detalheTitular-read">
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Número de filiação:</span><span class="modal-data" id="sp-nr-filiacao"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Número da proposta:</span><span class="modal-data" id="sp-nr-proposta"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">CRM:</span><span class="modal-data" id="sp-crm"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Especialidade:</span><span class="modal-data" id="sp-especialidade-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Data de Nasc:</span><span class="modal-data" id="sp-dt-nascimento-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Sexo:</span><span class="modal-data" id="sp-sexo-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Estado Civil:</span><span class="modal-data" id="sp-estado-civil-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">CPF:</span><span class="modal-data" id="sp-cpf-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Nome da Mãe:</span><span class="modal-data" id="sp-nome-mae-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">CEP:</span><span class="modal-data" id="sp-cep-titular"></span>
                                                    </div>
                                                    <table class="titular-table" style="width: 100%;">
                                                        <tr style="width: 100%">
                                                            <td style="width: 50%"><span class="modal-label">Endereço:</span><span class="modal-data" id="sp-endereco-titular"></span></td>
                                                            <td style="width: 50%"><span class="modal-label">Bairro:</span><span class="modal-data" id="sp-bairro-titular"></span></td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td style="width: 50%"><span class="modal-label">Cidade:</span><span id="sp-cidade-titular" class="modal-data"></span></td>
                                                            <td style="width: 50%"><span class="modal-label">UF:</span><span id="sp-uf-titular" class="modal-data"></span></td>
                                                        </tr>
                                                    </table>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Nacionalidade:</span><span class="modal-data" id="sp-nacionalidade-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Número do PIS/PASEP:</span><span class="modal-data" id="sp-pispasep-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Nº do CNS:</span><span class="modal-data" id="sp-cns-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">DN:</span><span class="modal-data" id="sp-dn-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Nrº da Carteirinha:</span><span class="modal-data" id="sp-nr-carteirinha-titular"></span>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Data de Filiação:</span><span class="modal-data" id="sp-dt-filiacao-titular"></span>
                                                    </div>

                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Inicio de Vigência:</span><span class="modal-data" id="sp-inicio-vigencia-titular"></span>
                                                    </div>
                                                    <%--<div class="segurado-grid-collapse">
                                                        <span class="modal-label">Fim de Vigência:</span><span class="modal-data" id="sp-fim-vigencia-titular"></span>
                                                    </div>--%>
                                                    <div class="row-edit-save">
                                                        <span id="lkEditarTitular" class="lk-save-edit">Editar</span>
                                                    </div>
                                                    <p></p>
                                                </div>
                                                <div class="detalhe-titular-read-edit" id="detalheTitular-edit" style="display: none">
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Número de filiação:</span>
                                                        <input name="txtNRFiliacao-editSegurado" maxlength="15" id="txtNRFiliacao-editSegurado"
                                                            onkeypress="mascaraInteiro()" class="input-material-titular nr-filiacao-editSegurado" type="text" value="" />
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Número da proposta:</span>
                                                        <input name="txtNRProposta-editSegurado" maxlength="15" id="txtNRProposta-editSegurado"
                                                            onkeypress="mascaraInteiro()" class="input-material-titular nr-proposta-editSegurado" type="text" value="" />
                                                    </div>

                                                    <div class="segurado-grid-collapse" style="margin-top: 6px">
                                                        <span class="modal-label">Origem do segurado:</span>
                                                        <select id="cmbOrigemSegurado-editSegurado" class="dropdown-material-titular cmbOrigemSegurado-editSegurado">
                                                            <option>Selecione</option>
                                                        </select>
                                                    </div>

                                                    <table class="dependente-table" style="width: 100%; margin-top: 9px">
                                                        <tr style="width: 100%">
                                                            <td style="width: 50%">
                                                                <span class="modal-label">CRM (número):</span>
                                                                <input name="txtEndereco" maxlength="10" onkeypress="mascaraInteiro()" id="txtNRCRM-editSegurado"
                                                                    class="input-material-titular txtNRCRM-editSegurado" type="text" />
                                                            </td>
                                                            <td style="width: 50%">
                                                                <span class="modal-label">CRM (estado):</span>
                                                                <select class="dropdown-material-titular cmbUF-editSegurado" id="cmbCRMestado-editSegurado">
                                                                    <option value="AC">AC</option>
                                                                    <option value="AL">AL</option>
                                                                    <option value="AP">AP</option>
                                                                    <option value="AM">AM</option>
                                                                    <option value="BA">BA</option>
                                                                    <option value="CE">CE</option>
                                                                    <option value="DF">DF</option>
                                                                    <option value="ES">ES</option>
                                                                    <option value="GO">GO</option>
                                                                    <option value="MA">MA</option>
                                                                    <option value="MT">MT</option>
                                                                    <option value="MS">MS</option>
                                                                    <option value="MG">MG</option>
                                                                    <option value="PA">PA</option>
                                                                    <option value="PB">PB</option>
                                                                    <option value="PR">PR</option>
                                                                    <option value="PE">PE</option>
                                                                    <option value="PI">PI</option>
                                                                    <option value="RJ">RJ</option>
                                                                    <option value="RN">RN</option>
                                                                    <option value="RS">RS</option>
                                                                    <option value="RO">RO</option>
                                                                    <option value="RR">RR</option>
                                                                    <option value="SC">SC</option>
                                                                    <option value="SP">SP</option>
                                                                    <option value="SE">SE</option>
                                                                    <option value="TO">TO</option>
                                                                </select>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <div class="segurado-grid-collapse" style="margin-top: 3px">
                                                        <span class="modal-label">Especialidade:</span>
                                                        <select class="dropdown-material-titular esp-editSegurado" id="cmbEspecialidade-edit-titular">
                                                            <option id="0">Selecione</option>
                                                        </select>
                                                    </div>
                                                    <div class="segurado-grid-collapse" style="margin-top: 2px">
                                                        <span class="modal-label">*Data de Nasc:</span>
                                                        <input id="txtDtNasc-editSegurado" type="text" class="input-material-titular data calendario dtnasc-editSegurado" onkeypress="MascaraData(this)" maxlength="10" />
                                                    </div>
                                                    <div class="segurado-grid-collapse" style="margin-top: 5px">
                                                        <span class="modal-label">Sexo:</span>
                                                        <select class="dropdown-material-titular sexo-editSegurado" id="cmbSexo-editSegurado">
                                                            <option value="0">Selecione </option>
                                                            <option value="M">Masculino</option>
                                                            <option value="F">Feminino</option>
                                                        </select>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Estado Civil:</span>
                                                        <select class="dropdown-material-titular ecivil-editSegurado" id="cmbEstadoCivilSegurado">
                                                            <option value="0">Selecione </option>
                                                            <option value="1">Solteiro(a)</option>
                                                            <option value="2">Casado(a)</option>
                                                            <option value="3">Viúvo(a)</option>
                                                            <option value="4">Desquitado(a)</option>
                                                            <option value="5">Divorciado(a)</option>
                                                            <option value="6">Outros</option>
                                                        </select>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">*CPF:</span>
                                                        <input name="txtTelefone" maxlength="14" id="txtCPF-editSegurado" class="input-material-titular txtCPF-editSegurado"
                                                            type="text" onkeypress="MascaraCPF(this)" value="" />
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Nome da Mãe:</span>
                                                        <input name="txtNomeMae" maxlength="100" id="txtNomeMae-editSegurado" class="input-material-titular txtNomeMae-editSegurado"
                                                            type="text" value="" />
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">CEP:</span>
                                                        <input name="txtCEP" maxlength="10" id="txtCEP-editSegurado" class="input-material-titular txtCEP-editSegurado"
                                                            type="text" onkeypress="MascaraCep(this)" value="" />
                                                    </div>
                                                    <table class="dependente-table" style="width: 100%;">
                                                        <tr style="width: 100%">
                                                            <td style="width: 50%">
                                                                <span class="modal-label">Endereço:</span>
                                                                <input name="txtEndereco" maxlength="100" id="txtEndereco-editSegurado" class="input-material-titular txtEndereco-editSegurado"
                                                                    type="text" value="" />
                                                            </td>
                                                            <td style="width: 50%">
                                                                <span class="modal-label">Bairro:</span>
                                                                <input name="txtBairro" maxlength="100" id="txtBairro-editSegurado" class="input-material-titular txtBairro-editSegurado"
                                                                    type="text" value="" />
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td style="width: 50%"><span class="modal-label">Cidade:</span>
                                                                <input name="txtCidade" maxlength="100" id="txtCidade-editSegurado" class="input-material-titular txtCidade-editSegurado"
                                                                    type="text" value="" />
                                                            </td>
                                                            <td style="width: 50%; padding-top: 10px"><span class="modal-label">UF:</span>
                                                                <select class="dropdown-material-titular cmbUF-editSegurado" id="cmbUF-editSegurado">
                                                                    <option value="AC">AC</option>
                                                                    <option value="AL">AL</option>
                                                                    <option value="AP">AP</option>
                                                                    <option value="AM">AM</option>
                                                                    <option value="BA">BA</option>
                                                                    <option value="CE">CE</option>
                                                                    <option value="DF">DF</option>
                                                                    <option value="ES">ES</option>
                                                                    <option value="GO">GO</option>
                                                                    <option value="MA">MA</option>
                                                                    <option value="MT">MT</option>
                                                                    <option value="MS">MS</option>
                                                                    <option value="MG">MG</option>
                                                                    <option value="PA">PA</option>
                                                                    <option value="PB">PB</option>
                                                                    <option value="PR">PR</option>
                                                                    <option value="PE">PE</option>
                                                                    <option value="PI">PI</option>
                                                                    <option value="RJ">RJ</option>
                                                                    <option value="RN">RN</option>
                                                                    <option value="RS">RS</option>
                                                                    <option value="RO">RO</option>
                                                                    <option value="RR">RR</option>
                                                                    <option value="SC">SC</option>
                                                                    <option value="SP">SP</option>
                                                                    <option value="SE">SE</option>
                                                                    <option value="TO">TO</option>
                                                                </select>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div class="segurado-grid-collapse" style="margin-top: 3px; margin-bottom: 8px">
                                                        <span class="modal-label">Nacionalidade:</span>
                                                        <select class="dropdown-material-titular cmbNacionalidade-editSegurado" id="cmbNacionalidade-editSegurado">
                                                            <option value="">Selecione </option>
                                                            <option value="brasileira">Brasileira</option>
                                                            <option value="estrangeira">Estrangeira</option>
                                                        </select>
                                                        <%--                                                        <input name="txtNacionalidade" maxlength="20" id="txtNacionalidade-editSegurado" class="input-material-titular txtNacionalidade-editSegurado"
                                                            type="text" value="" />--%>
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Número do PIS/PASEP:</span>
                                                        <input name="txtPIS" maxlength="14" id="txtPIS-editSegurado" class="input-material-titular txtPIS-editSegurado"
                                                            type="text" onkeypress="MascaraPis(this)" value="" />
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Nº do CNS:</span>
                                                        <input name="txtCNS" maxlength="18" id="txtCNS-editSegurado" class="input-material-titular txtCNS-editSegurado"
                                                            type="text" onkeypress="MascaraCNS(this)" value="" />
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">DN:</span>
                                                        <input name="txtDN" maxlength="11" id="txtDN-editSegurado" onkeypress="mascaraInteiro()" class="input-material-titular txtDN-editSegurado" type="text"
                                                            value="" />
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Nrº de Carteirinha:</span>
                                                        <input name="txtNrCarteirinha-editSegurado" maxlength="20" id="txtNrCarteirinha-editSegurado" onkeypress="mascaraInteiro()"
                                                            class="input-material-titular txtNrCarteirinha-editSegurado" type="text" value="" />
                                                    </div>
                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Data de Filiação:</span>
                                                        <input id="txtDtFiliacao-editSegurado" type="text" class="input-material-titular data calendario txtDtFiliacao-editSegurado"
                                                            onkeypress="MascaraData(this)" maxlength="10" />
                                                    </div>

                                                    <div class="segurado-grid-collapse">
                                                        <span class="modal-label">Inicio de Vigência:</span>
                                                        <input id="txtDtInicioVig-editSegurado" type="text" class="input-material-titular data calendario txtDtInicioVig-editSegurado"
                                                            onkeypress="MascaraData(this)" maxlength="10" />
                                                    </div>
                                                    <%--<div class="segurado-grid-collapse">
                                                        <span class="modal-label">Fim de Vigência:</span>
                                                        <input id="txtDtFimVig-editSegurado" type="text" class="input-material-titular data calendario txtDtFimVig-editSegurado"
                                                            onkeypress="MascaraData(this)" maxlength="10" />
                                                    </div>--%>
                                                    <div class="row-edit-save">
                                                        <span></span>
                                                        <i id="icon-saved-titular" class="fa fa-check-circle" style="display: none; margin-bottom: -33px;"></i>
                                                        <i id="icon-wait-titular" class="fas fa-hourglass-half" style="display: none; margin-bottom: -33px;"></i>
                                                        <span id="lkSaveTitular" class="lk-save-edit">Salvar</span>
                                                    </div>
                                                    <p></p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="segurado-border" id="lkCollapseContatos" style="margin-top: 1%">
                                        <div class="segurado-grid">
                                            <h3 style="font-weight: bold; margin-left: 2%; margin-top: 2%">Contatos</h3>
                                            <a href="#" style="float: right; color: black">
                                                <i class="fa fa-sort-down titular-icon-collapse" style="margin-top: -35px"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div id="detalheContatos" class="detalhe-segurado" style="display: none">
                                        <div class="detalhe-contatos-read-edit" id="detalheContatos-edit" style="display: none">
                                            <div style="margin-left: 5%; padding-top: 5px">
                                                <span class="modal-label">Telefone:</span>
                                                <input name="txtTelefone" maxlength="14" id="txtTelefone-editSegurado" class="input-material-titular" type="text"
                                                    onkeypress="MascaraTelefone(this)" style="width: 80%" />
                                            </div>
                                            <div style="margin-left: 5%">
                                                <span class="modal-label">Celular:</span>
                                                <input name="txtCelular" maxlength="15" id="txtCelular-editSegurado" style="width: 82%" class="input-material-titular" type="text"
                                                    onkeypress="MascaraCelular(this)" value="" />
                                            </div>
                                            <div style="margin-left: 5%">
                                                <span class="modal-label">Email:</span>
                                                <input id="txtEmail-editSegurado" type="text" name="registerEmail" style="width: 84%" class="input-material-titular" maxlength="100"
                                                    onkeydown="CampoValido(this,'SPTxtEmail');" value="" />
                                            </div>
                                            <div class="row-edit-save">
                                                <span class="msg-error-deps" id="sp-msg-error-contato-titular" style="display: none">Campo obrigatório</span>
                                                <i id="icon-saved-contato" class="fa fa-check-circle" style="display: none"></i>
                                                <i id="icon-wait-contato" class="fas fa-hourglass-half" style="display: none"></i>
                                                <span id="lkSaveCotato" class="lk-save-edit">Salvar</span>
                                            </div>
                                        </div>
                                        <div class="detalhe-contatos-read-edit" id="detalheContatos-read">
                                            <div style="margin-left: 5%; padding-top: 5px">
                                                <span class="modal-label">Telefone:</span><span id="sp-telefone-titular" class="modal-data"></span>
                                            </div>
                                            <div style="margin-left: 5%">
                                                <span class="modal-label">Celular:</span><span id="sp-celular-titular" class="modal-data"></span>
                                            </div>
                                            <div style="margin-left: 5%">
                                                <span class="modal-label">Email:</span><span id="sp-email-titular" class="modal-data"></span>
                                            </div>

                                            <div class="row-edit-save">
                                                <span id="lk-editar-contato" class="lk-save-edit">Editar</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="segurado-border" id="lkCollapsePlano" style="margin-top: 1%">
                                        <div class="segurado-grid">
                                            <h3 style="font-weight: bold; margin-left: 2%; margin-top: 2%">Planos</h3>
                                            <a href="#" style="float: right; color: black">
                                                <i class="fa fa-sort-down titular-icon-collapse" style="margin-top: -36px"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div id="detalhePlano" class="detalhe-segurado" style="display: none">
                                        <%--<div style="margin-left: 8%; padding-top: 5px;">
                                            <span class="modal-label">Plano A:</span><span id="sp-plano" class="modal-data">Básico I</span>
                                        </div>--%>


                                        <div class="segurado-grid-collapse" style="margin-top: -25px; margin-bottom: 25px;">
                                            <input type="hidden" id="hdd-plano-saude-atual" />
                                            <span class="modal-label" style="margin: 33px 0px 0px; margin-left: 5%;">Plano de Saúde:</span>
                                            <div id="dv-cmbPlanoSaude-editSegurado" style="display: inline">
                                                <select class="dropdown-material-titular cmb-plano-saude" id="cmbPlanoSaude-editSegurado" onchange="AlterouPlanoSaude()" style="width: 45%;">
                                                </select>
                                            </div>
                                             <div id="dv-plano-error" style="margin-left: 5%;margin-top: 2%; margin-bottom:-30px; display: none">
                                                <span style="color: red; font-size: 0.8em;">Não é possível a diminuição do plano de saúde.</span>
                                            </div>
                                            <br />
                                            <span class="modal-label" style="margin:33px 0px 0px 0px; margin-left: 5%;">Plano de Odontológico:</span>
                                            <div id="dv-cmbPlanoSaudeOdonto-editSegurado" style="display: inline">
                                                <select class="dropdown-material-titular cmb-plano-odonto" id="cmbPlanoOdonto-editSegurado" onchange="AlterouPlanoOdonto()" style="width: 45%">
                                                </select>
                                            </div>
                                           
                                        </div>

                                    </div>
                                    <div class="segurado-border" id="lkCollapseFP" style="margin-top: 1%; margin-bottom: 2%">
                                        <div class="segurado-grid">
                                            <h3 style="font-weight: bold; margin-left: 2%; margin-top: 2%">Forma de Pagamento</h3>
                                            <a href="#" style="float: right; color: black">
                                                <i class="fa fa-sort-down titular-icon-collapse" style="margin-top: -36px"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div id="detalheFP" class="detalhe-segurado" style="display: none; margin-top: -14px">
                                        <div class="row">
                                            <div style="margin-bottom: 20px; margin-left:20px" class="col-md-5">
                                                <nav class="md-forma-pag">
                                                    <ul>
                                                        <li style="cursor: pointer" class="li-forma-pagamento">
                                                            <img class="md-forma-pag-img" style="width: 100px;" src="../ContentAdm/img/boleto_icon.png" />  
                                                        </li>
                                                        <span  style="font-weight: bold; margin-left:115px !important; margin-top:20px;">Boleto<br/>Bancário</span>
                                                    </ul>
                                                </nav>
                                            </div>
                                            <div class="col-md-1" style="border-left: 2px solid #e9ebee;"></div>
                                            <div class="col-md-4" style="padding: 0; margin-top: 10px;">
                                                <font color="#0d408f" size="2px">Melhor dia Pagamento</font>
                                                <div id="dv-cmbMelhorDia-editSegurado">
                                                    <select  id="cmbMelhorDia-editSegurado" style="margin: 8px 0 0; width: 65%;">
                                                        <option value="0">Selecione</option>
                                                    </select>
                                                </div>
                                                <span class="required-error" id="SPCMBBanco_debito" style="display: none">Campo obrigatório</span>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="align-items-center">
                            <div class="content">
                                <i id="faClose" class="fa fa-window-close" style="position: absolute; top: 4px; right: 5px; z-index: 1; font-size: 25px; color: red; cursor: pointer"></i>
                                <div class="container-dependente-resolution">
                                    <div class="form-group " style="margin-top: 15px; border-radius: 5px; margin-bottom: 10%">
                                        <div style="margin-top: -15px;">
                                            <span class="modal-title">
                                                <h3 style="font-weight: bold;">Dados dos dependentes</h3>
                                            </span>
                                        </div>
                                        <div id="dv-dependente" class="scroll-dependentes">
                                            <h5 id="msgSemDeps" style="margin-left: 8%; display: none;">Titular não possui dependentes.</h5>
                                            <%-- COMPONENTE DINÂMICO - JQUERY - DV-DEPENDENTE --%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="width: 100%; margin-top: -1%">
                    <div>
                        <button type="button" onclick="SalvarTudo()" class="btn btn-primary btn-concluir">Salvar e Sair</button>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">    
    <script src="../ContentAdm/jquery/jquery.priceformat.js"></script>    
</asp:Content>
