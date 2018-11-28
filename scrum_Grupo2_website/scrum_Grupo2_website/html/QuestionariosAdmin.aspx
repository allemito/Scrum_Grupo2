<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionariosAdmin.aspx.cs" Inherits="scrum_Grupo2_website.Questionarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Questionário</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="~/css/StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server" style="margin: auto;">
    <ul class="topnav">
            <asp:Button class="button" ID="Button1" runat="server" Text="Home" Width="150px" PostBackUrl="~/html/homepage.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_paginaInicial" runat="server" Text="Página Inicial" PostBackUrl="~/html/Administrador.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_faq" runat="server" Text="F.A.Q" PostBackUrl="~/html/Faq.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_contactos" runat="server" Text="Contactos" PostBackUrl="~/html/Contactos.aspx" UseSubmitBehavior="False"/>
     </ul>
     <ul class="leftbar">
            <asp:Button class="buttonleftbar" ID="btn_RegistarMedico" runat="server" Text="Registar Médico" PostBackUrl="~/html/RegistoMedico.aspx" Width ="150px" UseSubmitBehavior="False"/>
            <asp:Button class="buttonleftbar" ID="btn_RegistoDoente" runat="server" Text="Registar Doente" PostBackUrl="~/html/RegistoUtente.aspx" Width ="150px" UseSubmitBehavior="False"/>
            <asp:Button class="buttonleftbar" ID="btn_Questionario" runat="server" Text="Questionários" Width ="150px" UseSubmitBehavior="False"/>
            <asp:Button class="buttonleftbar" ID="ButtonProcurar" runat="server" Text="Procurar" Width ="150px" /> 
            <asp:TextBox type="text" ID="TextBox_Procurar" runat="server" placeholder="Número Procurar" name="Numero_Procurar" Width ="150px"></asp:TextBox>   
            <asp:label ID="labelProcurar" runat="server" Width ="150px" style="text-align:center" ForeColor="Red"><b></b></asp:label>          
    </ul>
    </form>
</body>
</html>
