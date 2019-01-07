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
            <asp:Button class="button" ID="ButtonHome" runat="server" Text="Home" Width="150px" PostBackUrl="~/html/homepage.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_paginaInicial" runat="server" Text="Página Inicial" PostBackUrl="~/html/Medico.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_faq" runat="server" Text="F.A.Q" PostBackUrl="~/html/Faq.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_contactos" runat="server" Text="Contactos" PostBackUrl="~/html/Contactos.aspx" UseSubmitBehavior="False"/>
        </ul>
        <ul class="med">
            <asp:Button class="btnmed" ID="ButtonRecolha" runat="server" Text="Recolha Dados" Width ="150px" UseSubmitBehavior="False" OnClick="ButtonRecolha_Click"/>
            <asp:Button class="btnmed" ID="btn_Questionario" runat="server" Text="Questionários" Width ="150px" UseSubmitBehavior="False" OnClick="btn_Questionario_Click"/>
            <asp:Button class="btnmed" ID="ButtonProcurar" runat="server" Text="Procurar" Width ="150px" OnClick="ButtonProcurar_Click"/> 
            <asp:TextBox type="text" ID="TextBox_Procurar" runat="server" placeholder="Número de Utente" name="numero_utente" Width ="150px"></asp:TextBox>   
            <asp:label ID="labelProcurar" runat="server" Width ="150px" style="text-align:center" ForeColor="Red"><b></b></asp:label>          
        </ul>
        <div style="height: 590px; margin-left:150px; padding: 20px; border-color: lightseagreen; width: auto;">
            <asp:Panel ID ="panelDoente" runat ="server">
                <asp:Button class="button" ID="ButtonVoltar1" runat="server" Text="Voltar" Width="217px" OnClick="ButtonVoltar1_Click"/>
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
            <asp:Panel ID ="panelExcel" runat ="server">
                <asp:FileUpload ID="FileUpload1" runat="server"/>
                <p><asp:Label ID="LabelTexto" runat="server"><b></b></asp:Label></p>

                <asp:Button ID="ButtonAdicionarInfo" runat="server" class="btnmed" Text="Guardar Dados Biometricos" Width="217px" OnClick="ButtonAdicionarInfo_Click"/>
                <asp:Button ID="ButtonExcel" runat="server" class="btnmed" Text="Upload" Width="217px" ValidateRequestGroup="vg" OnClick="ButtonExcel_Click" />
                
                <p><asp:GridView ID="GridView1" runat="server" EmptyDataText="No record found!">
                <RowStyle Width="175px" />
                <EmptyDataRowStyle BackColor="Silver" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" ForeColor="#003300" />
                <HeaderStyle BackColor="#6699ff" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Top" Wrap="true" />
                </asp:GridView></p>          
            </asp:Panel>
             <asp:Panel ID ="panelBotoes" runat ="server">
                 <asp:Button class="button" ID="Button_dados" runat="server" Text="Dados Biométricos" OnClick="Button_dados_Click" Width="217px" />
                 &nbsp;&nbsp;
                 <asp:Button class="button" ID="ButtonInformacaoCli" runat="server" Text="Informação Clínica" Width="217px" OnClick="ButtonInformacaoCli_Click"/>              
            </asp:Panel>
             <asp:Panel ID ="panelDadosBiometricos" runat ="server">
                 <asp:Button class="button" ID="ButtonVoltar2" runat="server" Text="Voltar" Width="217px" OnClick="ButtonVoltar2_Click" />
                 
                 <p><label><b>Nome:</b></label>
                 <asp:TextBox type="text" ID="TextBoxNome1" runat="server" placeholder="Nome" name="nome"></asp:TextBox></p>
                 <p>
                    <asp:ListBox ID="ListBox_valores" runat="server" Height="200px"></asp:ListBox>
                    <asp:Label ID="Label_verificacao_valores" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                </p>                 
            </asp:Panel>
            <asp:Panel ID ="panel_QuestionarioOpcoes" runat ="server">
                <p><asp:Button class="button" ID="ButtonAddQuestionario" runat="server" Text="Adicionar Questionário" Width="217px" OnClick="ButtonAddQuestionario_Click"/></p>
                <label><b>Questionários Disponiveis:</b></label>
                <p><asp:ListBox ID="ListBox_Questionarios" runat="server" Height="150px" Width="217px"></asp:ListBox></p>    
                <p><asp:Button class="button" ID="ButtonRemoverQuestionario" runat="server" Text="Remover Questionário" Width="217px" OnClick="ButtonRemoverQuestionario_Click"/></p>
            </asp:Panel>
            <asp:Panel ID ="panelQuestionario" runat ="server">
                 <h1>Inserção de Questionarios</h1>
                <label><b>Nome do Questionário:</b></label>
                <asp:TextBox type="text" ID="TextBoxNomeQuestionario" runat="server" placeholder="Adicione um nome ao questionário" name="nome"></asp:TextBox>
                 <p><asp:label ID="labelQuestionario" runat="server"><b>Questão</b></asp:label></p>
                 <asp:TextBox type="text" ID="TextBoxPergunta" runat="server" placeholder="Adicione uma pergunta" name="Pergunta"></asp:TextBox>
                 <label><b>Selecione o tipo de resposta pretendida:</b></label>
                 <p><asp:DropDownList ID="DropDownListTipoPergunta" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListTipoPergunta_SelectedIndexChanged">
                     <asp:ListItem>Resposta Aberta</asp:ListItem>   
                     <asp:ListItem>Resposta Sim/Não</asp:ListItem>    
                     <asp:ListItem>Resposta Frequência</asp:ListItem> 
                     <asp:ListItem>Resposta Satisfação</asp:ListItem>   
                     <asp:ListItem>Adicionar Manualmente</asp:ListItem>
                 </asp:DropDownList></p>
                 <asp:TextBox type="text" ID="TextBoxAdicionarResposta" runat="server" placeholder="Adicione uma resposta" name="nome"></asp:TextBox>
                 <asp:Button class="button" ID="ButtonAddResp" runat="server" Text="Adicionar" Width="217px" OnClick="ButtonAddResp_Click"/>
                 &nbsp;
                 <asp:Button ID="ButtonRemoveResp" runat="server" class="button" OnClick="ButtonRemoveResp_Click" Text="Remover" Width="217px" />
                 <p>
                     <asp:ListBox ID="ListBoxOpcoesResposta" runat="server" Height="150px" Width="217px"></asp:ListBox>
                 </p>
                 <asp:Button ID="ButtonEditarQuestao" runat="server" class="button" OnClick="ButtonEditarQuestao_Click" Text="Questão Anterior" Width="217px" />
                 &nbsp;
                <asp:Button class="button" ID="ButtonAdicionarQuestao" runat="server" Text="Próxima Questão" Width="217px" OnClick="ButtonAdicionarQuestionario_Click"/>
                <p><asp:Label ID="LabelConfirmacao" runat="server" Text=""></asp:Label></p>  
            </asp:Panel>           
        </div>
    </form>
</body>
</html>
