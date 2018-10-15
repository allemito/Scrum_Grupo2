<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="scrum_Grupo2_website.html.Administrador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="~/css/StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server" style="margin: auto">
        <ul class="topnav">
            <asp:Button class="button" ID="Button1" runat="server" Text="Home" PostBackUrl="~/html/homepage.aspx" />
            <asp:Button class="button" ID="btn_registo" runat="server" Text="Registo" PostBackUrl="~/html/RegistoUtente.aspx" />
            <asp:Button class="button" ID="btn_login" runat="server" Text="Login" PostBackUrl="~/html/Login.aspx" />
            <asp:Button class="button" ID="btn_faq" runat="server" Text="F.A.Q" PostBackUrl="~/html/Faq.aspx" />
            <asp:Button class="button" ID="btn_contactos" runat="server" Text="Contactos" PostBackUrl="~/html/Contactos.aspx" />
        </ul>
        <img src="../image/saude.jpg" class="background-image"/>
        <div style="margin:auto;">
            <p>
            <asp:Button class="button" ID="btn_ListarDoentes" runat="server" Text="Lista de Doentes" PostBackUrl="~/html/homepage.aspx" Width="217px" /> <!-- ainda falta atribuir uma pagina para quando o botao for clicado no 'postbackurl'-->
            </p>
            <p>
            <asp:Button class="button" ID="btn_ListarMedicos" runat="server" Text="Lista de Médicos" PostBackUrl="~/html/homepage.aspx" Width="217px" /> <!-- ainda falta atribuir uma pagina para quando o botao for clicado no 'postbackurl'-->
            </p>
            <p>
            <asp:Button class="button" ID="btn_RegistarMedico" runat="server" Text="Registar Médicos" PostBackUrl="~/html/RegistoMedico.aspx" Width="217px" />
            </p>
        </div>
    </form>
</body>
</html>
