<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Doente.aspx.cs" Inherits="scrum_Grupo2_website.QuestionarioDoente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Questionário</title>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="~/css/StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server" style="margin: auto">
    <ul class="topnav">
            <asp:Button class="button" ID="Button1" runat="server" Text="Home" Width="150px" PostBackUrl="~/html/homepage.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_paginaInicial" runat="server" Text="Página Inicial" PostBackUrl="~/html/Doente.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_faq" runat="server" Text="F.A.Q" PostBackUrl="~/html/Faq.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_contactos" runat="server" Text="Contactos" PostBackUrl="~/html/Contactos.aspx" UseSubmitBehavior="False"/>
     </ul>
     <ul class="leftbar">
            <asp:Button class="buttonleftbar" ID="btn_Questionario" runat="server" Text="Questionários" Width ="150px" UseSubmitBehavior="False" OnClick="btn_Questionario_Click"/>        
    </ul>
        <div style="height: 590px; margin-left:150px; padding: 20px; border-color: lightseagreen; width: auto;">
       <asp:Panel ID ="panel_procura" runat ="server">
           <asp:ListBox ID="ListBox_Questionarios" runat="server" Height="150px" Width="217px"></asp:ListBox>
           <p><asp:Button class="button" ID="button_Comecar" runat="server" Width="217px" Text="Começar Questionario" OnClick="button_Comecar_Click"/></p>
       </asp:Panel>
       <asp:Panel ID ="panel_questionario" runat ="server">
           <asp:label ID="labelNome_Questionario" runat="server"><b>Nome-Questionario</b></asp:label>
           <p><asp:label ID="labelQuestao" runat="server"><b>Questão</b></asp:label></p>
           <p><asp:DropDownList ID="DropDownListSimNao" runat="server">
               <asp:ListItem>Sim</asp:ListItem>
               <asp:ListItem>Não</asp:ListItem>
           </asp:DropDownList></p>
           <p><asp:DropDownList ID="DropDownListSatisfacao" runat="server">
               <asp:ListItem>Nada Satisfeito</asp:ListItem>
               <asp:ListItem>Pouco Satisfeito</asp:ListItem>
               <asp:ListItem>Satisfeito</asp:ListItem>
               <asp:ListItem>Muito Satisfeito</asp:ListItem>
               <asp:ListItem>Completamente Satisfeito</asp:ListItem>
           </asp:DropDownList></p>
           <p><asp:DropDownList ID="DropDownListFrequencia" runat="server">
               <asp:ListItem>Nunca</asp:ListItem>
               <asp:ListItem>Raramente</asp:ListItem>
               <asp:ListItem>Algumas Vezes</asp:ListItem>
               <asp:ListItem>Quase Sempre</asp:ListItem>
               <asp:ListItem>Sempre</asp:ListItem>
           </asp:DropDownList></p>
           <asp:TextBox type="text" ID="TextBoxAberta" runat="server" placeholder="Resposta" name="resposta"></asp:TextBox>
           <p><asp:Button class="button" ID="buttonSubmeter" runat="server" Width="217px" Text="Submeter Questão" OnClick="buttonSubmeter_Click"/></p>
           <p><asp:Button class="button" ID="button_Seguinte" runat="server" Width="217px" Text="Próxima Questão" OnClick="button_Seguinte_Click"/></p>
       </asp:Panel>
    </div>
    </form>
</body>
</html>
