<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultTravelAceIFrame.aspx.cs" Inherits="Proseg.calculo.ModuloViagem.DefaultTravelAceIFrame" %>

<%@ Register Src="../Controls/padraoMenu.ascx" TagName="padraoMenu" TagPrefix="uc1" %>
<%@ Register Src="../Controls/padraoRodape.ascx" TagName="padraoRodape" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" href="../Estilos/css/estilos.aspx" rel="stylesheet"/>
    <script type="text/javascript" src="../Include/funcoes_1.0.0.js"></script>
    <script type="text/javascript" src="../Include/jquery.min.js"></script>
    <script type="text/javascript" src="../Include/jquery.tokenize.js"></script>
    <link rel="stylesheet" type="text/css" href="../Estilos/css/jquery.tokenize.css" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <script language="javascript" type="text/javascript">
        function resizeIframe(obj) {
            obj.style.height = obj.contentWindow.document.body.scrollHeight + 'px';
        }
</script>
    <style type="text/css">
        input, select {
            float: none;
        }

        fieldset li {
            clear: none;
        }

        fieldset ol {
            width: inherit;
        }

        .tokenize-sample {
            width: 200px;
        }

        label {
            WIDTH: 10.42em;
        }
        </style>
</head>
<body onkeypress="checkForEnter(event);">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerViagem" runat="server" EnablePageMethods="true"
            EnableScriptGlobalization="true">
        </asp:ScriptManager>
        <div id="telafundo" runat="server" class="PopupPanelModalArea" style="display: none;">
        </div>
        <!-- linha de passos -->
        <div style="width: 100%; float: left; height: 100%;">
            <uc1:padraoMenu ID="padraoMenu1" runat="server" />
            <div id="divIFrame">                
			<!--BugID 5917 - Guilherme Almeida-->
                <iframe runat="server" id="iframeGTA" name="iframeGTA" style="width: 100%; height: 800px" src="http://www.gtawlabel.com.br/FHE-POUPEX" ></iframe>
            </div>
            <asp:Timer ID="timerIFrameGTA" runat="server" Enabled="False">
            </asp:Timer>
            <uc2:padraoRodape float="footnote" ID="padraoRodape1" runat="server" />
        </div>       
    </form>
</body>
</html>
