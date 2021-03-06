﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="scrum_Grupo2_website.html.Administrador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Pagina Administrador</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="~/css/StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server" style="margin: auto">
        <ul class="topnav">
            <asp:Button class="button" ID="Button1" runat="server" Text="Home" Width="150px" PostBackUrl="~/html/homepage.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_paginaInicial" runat="server" Text="Página Inicial" PostBackUrl="~/html/Administrador.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_faq" runat="server" Text="F.A.Q" PostBackUrl="~/html/Faq.aspx" UseSubmitBehavior="False"/>
            <asp:Button class="button" ID="btn_contactos" runat="server" Text="Contactos" PostBackUrl="~/html/Contactos.aspx" UseSubmitBehavior="False"/>
        </ul>
        <ul class="leftbar">
            <asp:Button class="buttonleftbar" ID="btn_RegistarMedico" runat="server" Text="Registar Médico" PostBackUrl="~/html/RegistoMedico.aspx" Width ="150px" UseSubmitBehavior="False"/>
            <asp:Button class="buttonleftbar" ID="btn_RegistoDoente" runat="server" Text="Registar Doente" PostBackUrl="~/html/RegistoUtente.aspx" Width ="150px" UseSubmitBehavior="False"/>
            <asp:Button class="buttonleftbar" ID="btn_Questionario" runat="server" Text="Questionários" Width ="150px" UseSubmitBehavior="False" OnClick="btn_Questionario_Click"/>
            <asp:Button class="buttonleftbar" ID="ButtonProcurar" runat="server" Text="Procurar" Width ="150px" OnClick="ButtonProcurar_Click"/> 
            <asp:TextBox type="text" ID="TextBox_Procurar" runat="server" placeholder="Número Procurar" name="Numero_Procurar" Width ="150px"></asp:TextBox>   
            <asp:label ID="labelProcurar" runat="server" Width ="150px" style="text-align:center" ForeColor="Red"><b></b></asp:label>          
        </ul>
        <div style="height: 590px; margin-left:150px; padding: 20px; border-color: lightseagreen; width: auto;">
            <asp:Panel ID ="panelDoente" runat ="server">
                <asp:Button class="button" ID="ButtonEditar_Doente" runat="server" Text="Editar Dados" Width="217px" OnClick="ButtonEditar_Doente_Click"/>
                &nbsp;
                <asp:Button class="button" ID="ButtonRemover_Doente" runat="server" Text="Remover Doente" Width="217px" OnClick="ButtonRemover_Doente_Click"/>
                &nbsp;
                <asp:Button class="button" ID="ButtonInfo_Socio" runat="server" Text="Socioedemográfica" Width="217px" OnClick="ButtonInfo_Socio_Click"/>
                <p></p>
                <label for="nome"><b>Nome:</b></label>
                <asp:TextBox type="text" ID="txtbox_nome" runat="server" placeholder="Nome" name="nome"></asp:TextBox>

                <label><b>Número de Utente:</b></label>
                <asp:TextBox type="text" ID="TextBox_Utente" runat="server" placeholder="Número de Utente" name="numero_utente"></asp:TextBox>

                <label><b>Data de Nascimento:</b></label>
                <asp:TextBox type="text" ID="TextBoxNascimento_Doente" runat="server" placeholder="Data de Nascimento" name="data_nascimento"></asp:TextBox>
           
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
            </asp:Panel>
            <asp:Panel ID ="panelMedico" runat="server">
                <asp:Button class="button" ID="ButtonEditar_Medico" runat="server" Text="Editar Dados" Width="217px" OnClick="ButtonEditar_Medico_Click"/>
                &nbsp;
                <asp:Button class="button" ID="ButtonRemover_Medico" runat="server" Text="Remover Doente" Width="217px" OnClick="ButtonRemover_Medico_Click"/>
                <p></p>
                <label for="nome"><b>Nome:</b></label>
                <asp:TextBox type="text" ID="TextBox_Nome_Medico" runat="server" placeholder="Nome" name="nome"></asp:TextBox>

                <label><b>Número de Contribuinte:</b></label>
                <asp:TextBox type="text" ID="TextBox_Contribuinte_Medico" runat="server" placeholder="Número Contribuinte" name="numero_utente"></asp:TextBox>

                <label><b>Data de Nascimento:</b></label>
                <asp:TextBox type="text" ID="TextBoxNascimento_Medico" runat="server" placeholder="Data de Nascimento" name="data_nascimento"></asp:TextBox>

                <label><b>Género:</b></label>
                <p>
                    <asp:DropDownList ID="DropDownList_Sexo_Medico" runat="server">
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
                </asp:Panel>
            <asp:Panel ID ="panelInfo_Socio" runat ="server">
                
                <label><b>Nome:</b></label>
                <asp:TextBox type="text" ID="TextBox_Nome_Socio" runat="server" placeholder="Nome" name="nome"></asp:TextBox>

                <label><b>Data de Nascimento:</b></label>
                <asp:TextBox type="text" ID="TextBox_DataNascimento_Socio" runat="server" placeholder="Data de Nascimento" name="data_nascimento"></asp:TextBox>

                <label><b>Morada:</b></label>
                <asp:TextBox type="text" ID="TextBox_Morada_Socio" runat="server" placeholder="Morada" name="morada"></asp:TextBox>

                <label><b>Nacionalidade:</b></label>
                <asp:TextBox type="text" ID="TextBox_Nacionalidade_Socio" runat="server" placeholder="Nacionalidade" name="nacionalidade"></asp:TextBox>

                <label><b>Naturalidade:</b></label>
                <asp:TextBox type="text" ID="TextBox_Naturalidade_Socio" runat="server" placeholder="Naturalidade" name="naturalidade"></asp:TextBox>
           
                <label><b>Estado Civil:</b></label>
                <p>
                <asp:DropDownList ID="DropDownList_EstadoCivil_Socio" runat="server">
                    <asp:ListItem>Solteiro(a)</asp:ListItem>
                    <asp:ListItem>Casado(a)</asp:ListItem>
                    <asp:ListItem>Divorciado(a)</asp:ListItem>
                    <asp:ListItem>Viuvo(a)</asp:ListItem>
                </asp:DropDownList>
                </p>
               
                <label><b>Número de filhos:</b></label>
                <p><asp:TextBox ID="Textbox_NumeroFilhos_Socio" type="text" TextMode="Number" runat="server" min="0" max="20" step="1"></asp:TextBox></p>

                <label><b>Agregado familiar (Número de pessoas):</b></label>
                <p><asp:TextBox ID="Textbox_Agregado_Socio" type="text" TextMode="Number" runat="server" min="0" max="20" step="1"></asp:TextBox></p>

                <label><b>Profissão:</b></label>
                <asp:TextBox type="text" ID="TextBox_Profissão_Socio" runat="server" placeholder="Profissão" name="profissão"></asp:TextBox>

                <label><b>Grau de Escolaridade:</b></label>
                <p>
                <asp:DropDownList ID="DropDownList_Escolaridade_Socio" runat="server">
                    <asp:ListItem>Nenhum</asp:ListItem>
                    <asp:ListItem>1º Ciclo</asp:ListItem>
                    <asp:ListItem>2º Ciclo</asp:ListItem>
                    <asp:ListItem>3º Ciclo</asp:ListItem>
                    <asp:ListItem>Ensino Secundário</asp:ListItem>
                    <asp:ListItem>Ensino Superior (Licenciatura)</asp:ListItem>
                    <asp:ListItem>Ensino Superior (Mestrado)</asp:ListItem>
                    <asp:ListItem>Ensino Superior (Doutoramento)</asp:ListItem>
                </asp:DropDownList>
                </p>
                <asp:Button type="submit" ID="Button_Guardar_Socio" class="registerbtn" runat="server" Text="Guardar" OnClick="Button_Guardar_Socio_Click" />
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
            <asp:Panel ID ="panel_QuestionarioOpcoes" runat ="server">
                <p><asp:Button class="button" ID="ButtonAddQuestionario" runat="server" Text="Adicionar Questionário" Width="217px" OnClick="ButtonAddQuestionario_Click"/></p>
                <label><b>Questionários Disponiveis:</b></label>
                <p><asp:ListBox ID="ListBox_Questionarios" runat="server" Height="150px" Width="217px"></asp:ListBox></p>    
                <p><asp:Button class="button" ID="ButtonRemoverQuestionario" runat="server" Text="Remover Questionário" Width="217px" OnClick="ButtonRemoverQuestionario_Click"/></p>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
