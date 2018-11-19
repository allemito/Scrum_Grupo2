<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="scrum_Grupo2_website.html.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign in</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="~/css/StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server" style="margin: auto">
        <ul class="topnav">
            <asp:Button class="button" ID="btn_home" runat="server" Text="Home" PostBackUrl="~/html/homepage.aspx" UseSubmitBehavior="False" />
            <asp:Button class="button" ID="btn_login" runat="server" Text="Login" PostBackUrl="~/html/Login.aspx" UseSubmitBehavior="False" />
            <asp:Button class="button" ID="btn_faq" runat="server" Text="F.A.Q" PostBackUrl="~/html/Faq.aspx" UseSubmitBehavior="False" />
            <asp:Button class="button" ID="btn_contactos" runat="server" Text="Contactos" PostBackUrl="~/html/Contactos.aspx" UseSubmitBehavior="False" />
        </ul>
        <img src="../image/heart-health-main.jpg" class="background-image" />   
        <div style="height: 336px; width: 528px; margin:auto;">
            <h1>Login</h1>
            <label for="username"><b>Username</b></label>
            <asp:TextBox type="text" ID="txtbox_username" runat="server" placeholder="Insira username" name="username"></asp:TextBox>

            <label for="psw_login"><b>Password</b></label>
            <asp:TextBox type="password" ID="txtbox_password" runat="server" placeholder="Insira Password" name="psw_login"></asp:TextBox>

            <asp:Button ID="btn_login_account" runat="server" Text="Login" class="registerbtn" OnClick="btn_login_account_Click" />
        </div>
    </form>
</body>
</html>
