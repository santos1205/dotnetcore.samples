<%@ Page Title="" Language="C#" MasterPageFile="~/Premium.Master" AutoEventWireup="true" CodeBehind="_Orcamento.aspx.cs" Inherits="Premium.WebForm1" %>

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
                <h2>Cotação Seguro Viagem</h2>
            </div>
        </div>
    </header>
    
    <iframe id="iframeGTA" name="iframeGTA" style="width: 100%; height: 800px" src="Cotacao/TravelAcordoProseg.aspx"></iframe>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">

    <script src="Content/js/ValidacoesCamposForm.js"></script>
    <script src="Content/js/list.min.js"></script>    
    <script src="Content/js/Orcamento.js"></script>
    <script src="Content/jquery/jquery.priceformat.js"></script>
    <script src="Content/jquery/jquery-ui.min.js"></script>
    <script src="Content/js/Menu.js"></script>    
    <%--<script src="Content/jquery/jquery.range.js"></script>--%>    
</asp:Content>
