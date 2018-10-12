<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistoPrincipal.aspx.cs" Inherits="scrum_Grupo2_website.html.RegistoPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="~/css/StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="../image/heart-health-main.jpg" class="background-image" />
        <p>
            <asp:Button ID="button_utente" runat="server" class="registerbtn" PostBackUrl="~/html/RegistoUtente.aspx" Text="Utente" Width="282px" />
        </p>
        <p>
            <asp:Button ID="button_medico" runat="server" class="registerbtn" PostBackUrl="~/html/RegistoMedico.aspx" Text="Médico" Width="283px" />
        </p>
    </div>
    </form>
</body>
</html>
