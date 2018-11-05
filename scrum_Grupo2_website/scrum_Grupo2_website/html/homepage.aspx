<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="scrum_Grupo2_website.homepage" %>

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
            <asp:Button class="button" ID="btn_home" runat="server" Text="Home" PostBackUrl="~/html/homepage.aspx" />
            <asp:Button class="button" ID="btn_login" runat="server" Text="Login" PostBackUrl="~/html/Login.aspx" />
            <asp:Button class="button" ID="btn_faq" runat="server" Text="F.A.Q" PostBackUrl="~/html/Faq.aspx" />
            <asp:Button class="button" ID="btn_contactos" runat="server" Text="Contactos" PostBackUrl="~/html/Contactos.aspx" />
        </ul>
        <img src="../image/heart-health-main.jpg" class="background-image" />

        <div style="height: 590px; margin: 0 auto; width: 520px;">
            <p>
                <asp:Calendar ID="Calendar_Evento" runat="server" Height ="222px" Width="519px"></asp:Calendar>
            </p>
            <p>
                <asp:Label ID ="label_evento" runat="server"></asp:Label>
            </p> 
             <asp:Button type="submit" ID="btn_registar" class="registerbtn" runat="server" Text="Registar" OnClick="btn_registar_Click" />
        </div>
    </form>
</body>
</html>
