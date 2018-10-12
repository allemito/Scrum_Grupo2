<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrativo.aspx.cs" Inherits="scrum_grupo2_website.html.Administrativo" %>



<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title></title>

    <style type="text/css">

        .auto-style1 {

            width: 190px;

            height: 134px;

        }



.button {

    background-color: #333; /* Green */

    border: none;

    color: white;

    padding: 15px 32px;

    text-align: center;

    text-decoration: none;

    display: inline-block;

    font-size: 14px;

}



    * {

    box-sizing: border-box;

}



        .auto-style3 {

            width: 1102px;

        }

        .auto-style4 {

            height: 25px;

        }

    </style>

</head>

<body>

    <form id="form1" runat="server">

        <p class="auto-style3">

            <img alt="Imagem relacionada" class="auto-style1" src="http://www.correiofraterno.com.br/images/stories/2013/450/saude.jpg" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Button class="button" ID="btn_home1" runat="server" Text="Lista de Doentes" PostBackUrl="~/html/homepage.aspx" OnClick="btn_listdoente_Click" Width="217px" />

            &nbsp;<asp:Button class="button" ID="btn_home" runat="server" Text="Lista de Médicos" PostBackUrl="~/html/homepage.aspx" OnClick="btn_listmedico_Click" Width="217px" />

            &nbsp;<asp:Button class="button" ID="btn_home0" runat="server" Text="Registar Médicos" PostBackUrl="~/html/RegistoMedico.aspx" OnClick="btn_registmedico_Click" Width="217px" />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </p>

        <p class="auto-style4">

            &nbsp;</p>

    </form>

</body>

</html>


