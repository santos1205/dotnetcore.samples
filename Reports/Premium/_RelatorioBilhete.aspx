<%@ Page Title="" Language="C#" MasterPageFile="~/Premium.Master" AutoEventWireup="true" ClientIDMode="Static"
    CodeBehind="_RelatorioBilhete.aspx.cs" Inherits="Premium._RelatorioBilhete" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <rsweb:ReportViewer ID="rptRelatorio" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Relatorio.rdlc">
        </LocalReport>
        <ServerReport ReportServerUrl="http://des.viagem.corretorapremium.com.br/" ReportPath="/Relatotorio.rdlc" />
    </rsweb:ReportViewer>
    <asp:ScriptManager ID="scriptRelatorio" runat="server"></asp:ScriptManager>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footerScript" runat="server">
</asp:Content>
