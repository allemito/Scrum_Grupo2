<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistoMedico.aspx.cs" Inherits="scrum_Grupo2_website.html.RegistoMedico1" %>

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
            <asp:Button class="button" ID="btn_paginaInicial" runat="server" Text="Página Inicial" PostBackUrl="~/html/Administrador.aspx" />
            <asp:Button class="button" ID="btn_faq" runat="server" Text="F.A.Q" PostBackUrl="~/html/Faq.aspx" />
            <asp:Button class="button" ID="btn_contactos" runat="server" Text="Contactos" PostBackUrl="~/html/Contactos.aspx" />
        </ul>
        <div style="height: 590px; margin: 0 auto; width: 520px;">
            <img src="../image/heart-health-main.jpg" class="background-image" />

            <h1>Registo Médico</h1>

            <p>Insira os dados para adicionar um Médico.</p>
            <label for="nome"><b>Nome:</b></label>
            <asp:TextBox type="text" ID="txtbox_nome" runat="server" placeholder="Nome" name="nome"></asp:TextBox>

            <label><b>Número de Contribuinte:</b></label>
            <asp:TextBox type="text" ID="TextBox_Contribuinte" runat="server" placeholder="Número Contribuinte" name="numero_utente"></asp:TextBox>

            <p>
            <label><b>Data de Nascimento:</b></label>
            <asp:Calendar ID="Calendar_datanascimento" runat="server"></asp:Calendar>
            </p>

            <label><b>Género:</b></label>
            <p>
                <asp:DropDownList ID="DropDownList_Sexo" runat="server">
                    <asp:ListItem>Masculino</asp:ListItem>
                    <asp:ListItem>Feminino</asp:ListItem>
                </asp:DropDownList>
            </p>

            <label><b>Cédula Profissional:</b></label>
            <asp:TextBox ID="txtbox_cedula" runat="server" placeholder="Número de Cédula" name="cedula"></asp:TextBox>

            <label for="morada"><b>Morada:</b></label>
            <asp:TextBox type="text" ID="TextBox_morada" runat="server" placeholder="Morada" name="morada"></asp:TextBox>

            <label><b>Email:</b></label>
            <asp:TextBox type="text" ID="txtbox_email" runat="server" placeholder="Email" name="email"></asp:TextBox>

            <asp:Button type="submit" ID="btn_registar" class="registerbtn" runat="server" Text="Criar" OnClick="btn_registar_Click" />
        </div>
    </form>
</body>
</html>
