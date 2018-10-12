<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistoPrincipal.aspx.cs" Inherits="scrum_Grupo2_website.html.RegistoPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


.background-image {
    max-width: 500px;
    height: auto;
    width: auto\9; /* ie8 */
    display: block;
    margin-left: auto;
    margin-right: auto;
}

* {
    box-sizing: border-box;
}

    </style>
    <link href="~/css/StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <img src="../image/heart-health-main.jpg" class="background-image" /></div>
        
        <p>
            <asp:Button ID="button_utente" runat="server" class="registerbtn" PostBackUrl="~/html/RegistoUtente.aspx" Text="Utente" Width="282px" />
        </p>
        <p>
            <asp:Button ID="button_medico" runat="server" class="registerbtn" PostBackUrl="~/html/RegistoMedico.aspx" Text="Médico" Width="283px" />
        </p>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
