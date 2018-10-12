<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistoUtente.aspx.cs" Inherits="scrum_Grupo2_website.html.RegistoUtente1" %>

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
    <div style="height: 590px; margin: 0 auto; width: 520px;">
    
        <div style="height: 590px; margin: 0 auto; width: 520px;">
            <h1>
        <img src="../image/heart-health-main.jpg" class="background-image" /></h1>
            <h1>&nbsp;</h1>
            <h1>Registo Utente</h1>
            <p>Insira os dados para criar a sua conta.</p>
            <label for="nome"><b>Nome:</b></label>
            <asp:TextBox type="text" ID="txtbox_nome" runat="server" placeholder="Insira Nome" name="nome"></asp:TextBox>

            <label><b>
            <br />
            Numero de Utente:</b></label>
            <asp:TextBox type="text" ID="TextBox_numeroutente" runat="server" placeholder="Insira Numero de Utente" name="numero_utente"></asp:TextBox>

            <p>
            <label><b>Data de Nascimento:</b></label>
            <asp:Calendar ID="Calendar_datanascimento" runat="server"></asp:Calendar>
            </p>

            <label><b>Sexo:</b></label>
            <p>
                <asp:DropDownList ID="DropDownList_Sexo" runat="server">
                    <asp:ListItem>Masculino</asp:ListItem>
                    <asp:ListItem>Feminino</asp:ListItem>
                </asp:DropDownList>
            </p>
            <label for="email"><b>Email:</b></label>
            <asp:TextBox type="text" ID="txtbox_email" runat="server" placeholder="Insira Email" name="email"></asp:TextBox>
            
            <label for="morada"><b>
            <br />
            Morada:</b></label>
            <asp:TextBox type="text" ID="TextBox_morada" runat="server" placeholder="Insira Morada" name="morada"></asp:TextBox>

            <label for="psw"><b>
            <br />
            Password:</b></label>
            <asp:TextBox type="password" ID="txtbox_pass" runat="server" placeholder="Insira Password" name="psw"></asp:TextBox>

            <label for="psw-repeat"><b>
            <br />
            Repita Password:</b></label>
            <asp:TextBox type="password" ID="txtbox_repass" runat="server" placeholder="Insira Password" name="psw-repeat"></asp:TextBox>

            &nbsp;<br />
            <br />

            <asp:Button type="submit" ID="btn_registar" class="registerbtn" runat="server" Text="Criar" OnClick="btn_registar_Click" />
            <br />
            <br />
        </div>

    </div>
    </form>
        <p>
            &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
