<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Medico.aspx.cs" Inherits="scrum_Grupo2_website.Medico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Pagina Medico</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="~/css/StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
    <link href="~/css/leftbar.css" rel="stylesheet" type="text/css" media="screen" runat="server" />

</head>
<body>
    <form id="form1" runat="server" style="margin: auto">
        <ul class="topnav">
            <asp:Button class="button" ID="Button1" runat="server" Text="Home" Width="150px" PostBackUrl="~/html/homepage.aspx"/>
            <asp:Button class="button" ID="btn_paginaInicial" runat="server" Text="Página Inicial" PostBackUrl="~/html/Medico.aspx"/>
            <asp:Button class="button" ID="btn_faq" runat="server" Text="F.A.Q" PostBackUrl="~/html/Faq.aspx" />
            <asp:Button class="button" ID="btn_contactos" runat="server" Text="Contactos" PostBackUrl="~/html/Contactos.aspx" />
        </ul>
        <ul class="med">
            <asp:Button class="btnmed" ID="ButtonProcurar" runat="server" Text="Procurar" Width ="150px" OnClick="ButtonProcurar_Click"/> 
            <asp:TextBox type="text" ID="TextBox_Procurar" runat="server" placeholder="Número de Utente" name="numero_utente" Width ="150px"></asp:TextBox>   
            <asp:label ID="labelProcurar" runat="server" Width ="150px" style="text-align:center" ForeColor="Red"><b></b></asp:label>          
        </ul>
        <div style="height: 590px; margin-left:150px; padding: 20px; border-color: lightseagreen; width: auto;">
            <asp:Panel ID ="panelDoente" runat ="server">
                <h1>Informação Médica</h1>
                <label><b>Nome:</b></label>
                <asp:TextBox type="text" ID="TextBox_Nome" runat="server" placeholder="Nome" name="nome"></asp:TextBox>

                <label><b>Pressão Arterial:</b></label>
                <asp:TextBox type="text" ID="TextBox_PArterial" runat="server" placeholder="Pressão Arterial" name="Pressão Arterial"></asp:TextBox>

                <label><b>Vacinas:</b></label>
                <p><asp:DropDownList ID="DropDownListVacinas" runat="server"><asp:ListItem>Em Dia</asp:ListItem><asp:ListItem>Em Falta</asp:ListItem></asp:DropDownList></p>    
                
                <label><b>Peso:</b></label>
                <asp:TextBox type="text" ID="TextBox_Peso" runat="server" placeholder="Peso" name="Peso"></asp:TextBox>

                <label><b>Altura:</b></label>
                <asp:TextBox type="text" ID="TextBox_Altura" runat="server" placeholder="Altura" name="Altura"></asp:TextBox>
                
                <p><asp:label ID="labelIMC" runat="server"><b></b></asp:label></p>
                <asp:Button class="button" ID="ButtonIMC" runat="server" Text="Calcular IMC" Width="217px" OnClick="ButtonIMC_Click"/>
                <p></p>
                <p><asp:label ID="label_Vacina_Falta" runat="server"><b>Vacinas em Falta:</b></asp:label></p>
                <p>
                <asp:DropDownList ID="ddl_vacine" runat="server">
                    <asp:ListItem>BCG</asp:ListItem>
                    <asp:ListItem>VHB 1</asp:ListItem>
                    <asp:ListItem>VHB 2</asp:ListItem>
                    <asp:ListItem>VHB 3</asp:ListItem>
                    <asp:ListItem>Hib 1</asp:ListItem>
                    <asp:ListItem>Hib 2</asp:ListItem>
                    <asp:ListItem>Hib 3</asp:ListItem>
                    <asp:ListItem>Hib 4</asp:ListItem>
                    <asp:ListItem>DTPa 1</asp:ListItem>
                    <asp:ListItem>DTPa 2</asp:ListItem>
                    <asp:ListItem>DTPa 3</asp:ListItem>
                    <asp:ListItem>DTPa 4</asp:ListItem>
                    <asp:ListItem>DTPa 5</asp:ListItem>
                    <asp:ListItem>DTPa (10/10 anos)</asp:ListItem>
                    <asp:ListItem>VIP 1</asp:ListItem>
                    <asp:ListItem>VIP 2</asp:ListItem>
                    <asp:ListItem>VIP 3</asp:ListItem>
                    <asp:ListItem>VIP 4</asp:ListItem>
                    <asp:ListItem>Pn13 1</asp:ListItem>
                    <asp:ListItem>Pn13 2</asp:ListItem>
                    <asp:ListItem>Pn13 3</asp:ListItem>
                    <asp:ListItem>MenC</asp:ListItem>
                    <asp:ListItem>VASPR 1</asp:ListItem>
                    <asp:ListItem>VASPR 2</asp:ListItem>
                    <asp:ListItem>HPV 1,2</asp:ListItem>
                </asp:DropDownList>
                </p>
                <asp:Button class="button" ID="Button_Falta" runat="server" Text="Adicionar Vacina" Width="217px" OnClick="Button_Falta_Click"/>
                <p><asp:label ID="label_Falta" runat="server"></asp:label></p>
                <asp:Button type="submit" ID="Button_Adicionar" class="registerbtn" runat="server" Text="Adicionar" OnClick="Button_Adicionar_Click"/>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
