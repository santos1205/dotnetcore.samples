<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Premium.Acesso.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
<script src="../Content/jquery/jquery.min.js"></script>
<script>
    var urlCalculo = 'http://' + window.location.hostname + ':' + window.location.port + '/Acesso/ManterUsuario.aspx';
    window.location.replace(urlCalculo);
</script>