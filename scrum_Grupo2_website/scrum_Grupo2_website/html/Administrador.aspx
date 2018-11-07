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
            <asp:Button class="button" ID="Button1" runat="server" Text="Home" Width="150px" PostBackUrl="~/html/homepage.aspx"/>
            <asp:Button class="button" ID="btn_paginaInicial" runat="server" Text="Página Inicial" PostBackUrl="~/html/Administrador.aspx"/>
            <asp:Button class="button" ID="btn_faq" runat="server" Text="F.A.Q" PostBackUrl="~/html/Faq.aspx" />
            <asp:Button class="button" ID="btn_contactos" runat="server" Text="Contactos" PostBackUrl="~/html/Contactos.aspx" />
        </ul>
        <ul class="leftbar">
            <asp:Button class="buttonleftbar" ID="btn_RegistarMedico" runat="server" Text="Registar Médico" PostBackUrl="~/html/RegistoMedico.aspx" Width ="150px"/>
            <asp:Button class="buttonleftbar" ID="btn_RegistoDoente" runat="server" Text="Registar Doente" PostBackUrl="~/html/RegistoUtente.aspx" Width ="150px"/>
            <asp:Button class="buttonleftbar" ID="ButtonProcurar" runat="server" Text="Procurar" Width ="150px" OnClick="ButtonProcurar_Click"/> 
            <asp:TextBox type="text" ID="TextBox_Procurar" runat="server" placeholder="Número de Utente" name="numero_utente" Width ="150px"></asp:TextBox>   
            <asp:label ID="labelProcurar" runat="server" Width ="150px" style="text-align:center" ForeColor="Red"><b></b></asp:label>          
        </ul>
        <div style="height: 590px; margin-left:150px; padding: 20px; border-color: lightseagreen; width: auto;">
            <asp:Button class="button" ID="ButtonEditar" runat="server" Text="Editar Dados" Width="217px" OnClick="ButtonEditar_Click" />
            &nbsp;
            <asp:Button class="button" ID="ButtonRemover" runat="server" Text="Remover Doente" Width="217px" OnClick="ButtonRemover_Click" />
            <asp:Panel ID ="panelDoente" runat ="server">
                <label for="nome"><b>Nome:</b></label>
                <asp:TextBox type="text" ID="txtbox_nome" runat="server" placeholder="Nome" name="nome"></asp:TextBox>

                <label><b>Número de Utente:</b></label>
                <asp:TextBox type="text" ID="TextBox_Utente" runat="server" placeholder="Número de Utente" name="numero_utente"></asp:TextBox>

                <p><label><b>Data de Nascimento:</b></label> 
                <asp:Calendar ID="Calendar_datanascimento" runat="server" AutoPostBack = "false"></asp:Calendar>
                </p>
           
                <label><b>Género:</b></label>
                <p>
                <asp:DropDownList ID="DropDownList_Sexo" runat="server">
                    <asp:ListItem>Masculino</asp:ListItem>
                    <asp:ListItem>Feminino</asp:ListItem>
                </asp:DropDownList>
                </p>
                <label for="morada"><b>Morada:</b></label>
                <asp:TextBox type="text" ID="TextBox_morada" runat="server" placeholder="Morada" name="morada"></asp:TextBox>

                <label for="email"><b>Email:</b></label>
                <asp:TextBox type="text" ID="txtbox_email" runat="server" placeholder="Email" name="email"></asp:TextBox>

                <asp:Button type="submit" ID="btn_Guardar_Doente" class="registerbtn" runat="server" Text="Guardar"/>
            </asp:Panel>
            <asp:Panel ID ="panelMedico" runat="server">
                <label for="nome"><b>Nome:</b></label>
                <asp:TextBox type="text" ID="TextBox_Nome_Medico" runat="server" placeholder="Nome" name="nome"></asp:TextBox>

                <label><b>Número de Contribuinte:</b></label>
                <asp:TextBox type="text" ID="TextBox_Contribuinte_Medico" runat="server" placeholder="Número Contribuinte" name="numero_utente"></asp:TextBox>

                <p>
                <label><b>Data de Nascimento:</b></label>
                <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                </p>

                <label><b>Género:</b></label>
                <p>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>Masculino</asp:ListItem>
                        <asp:ListItem>Feminino</asp:ListItem>
                    </asp:DropDownList>
                </p>

                <label><b>Cédula Profissional:</b></label>
                <asp:TextBox ID="txtbox_cedula" runat="server" placeholder="Número de Cédula" name="cedula"></asp:TextBox>

                <label for="morada"><b>Morada:</b></label>
                <asp:TextBox type="text" ID="TextBox_Morada_Medico" runat="server" placeholder="Morada" name="morada"></asp:TextBox>

                <label><b>Email:</b></label>
                <asp:TextBox type="text" ID="TextBox_Email_Medico" runat="server" placeholder="Email" name="email"></asp:TextBox>

                <asp:Button type="submit" ID="Button_Guardar_Medico" class="registerbtn" runat="server" Text="Guardar"/>   
                </asp:Panel>
        </div>
    </form>
</body>
</html>
