﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace scrum_Grupo2_website
{
    public partial class QuestionarioDoente : System.Web.UI.Page
    {
        OracleConnection conexao = new OracleConnection("DATA SOURCE=25.15.145.193:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;
        ClasseQuestao questao = new ClasseQuestao();
        int numero_max = 1;
        int numero_questao = 1;
        string questionario_selectionado;
        string id_questionario;
        string username;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Recebe o valor do numero de utente logado
            try
            {
                username = (string)(Session["username"]);
            }
            catch
            {

            }
            //conexao base dados
            comando.Connection = conexao;

            panel_procura.Visible = false;
            panel_questionario.Visible = false;
            DropDownListFrequencia.Visible = false;
            DropDownListSatisfacao.Visible = false;
            DropDownListSimNao.Visible = false;
            TextBoxAberta.Visible = false;

            if (IsPostBack)
            {
                if (ViewState["numero_max"] != null)
                {
                    numero_max = (int)ViewState["numero_max"];
                }
                if (ViewState["numero_questao"] != null)
                {
                    numero_questao = (int)ViewState["numero_questao"];
                }
                if (ViewState["nome_questionario"] != null)
                {
                    questionario_selectionado = (string)ViewState["nome_questionario"];
                }
                if (ViewState["id_questionario"] != null)
                {
                    id_questionario = (string)ViewState["id_questionario"];
                }
            }
        }

        protected void btn_Questionario_Click(object sender, EventArgs e)
        {
            panel_procura.Visible = true;
            DropDownListFrequencia.Visible = false;
            DropDownListSatisfacao.Visible = false;
            DropDownListSimNao.Visible = false;
            TextBoxAberta.Visible = false;

            numero_questao = 1;
            ViewState["numero_questao"] = numero_questao;
            numero_max = 1;
            ViewState["numero_max"] = numero_max;

            // Mete na listBox os questionarios disponiveis para responder
            conexao.Open();
            comando.CommandText = "SELECT Nome_Questionario from QUESTIONARIO_PERGUNTAS group by NOME_QUESTIONARIO";
            dataReader = comando.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    ListBox_Questionarios.Items.Clear();
                    ListBox_Questionarios.Items.Add(dataReader[0].ToString());
                }
            }
            conexao.Close();
        }

        protected void button_Comecar_Click(object sender, EventArgs e)
        {
            button_Seguinte.Visible = false;
            panel_procura.Visible = false;
            panel_questionario.Visible = true;
            DropDownListFrequencia.Visible = false;
            DropDownListSatisfacao.Visible = false;
            DropDownListSimNao.Visible = false;
            TextBoxAberta.Visible = false;

            //Obtem o nome do questionario selecionado        
            questionario_selectionado = ListBox_Questionarios.SelectedItem.Value.ToString();
            ViewState["nome_questionario"] = questionario_selectionado;

             //Encontra o valor maximo de perguntas do questionario 
            conexao.Open();
            comando.CommandText = "SELECT count(Numero_Questao) from QUESTIONARIO_PERGUNTAS where NOME_QUESTIONARIO ='" + questionario_selectionado + "'";
            numero_max = Convert.ToInt32(comando.ExecuteScalar());
            ViewState["numero_max"] = numero_max;
            comando.CommandText = "SELECT ID_Questionario from QUESTIONARIO_PERGUNTAS where NOME_QUESTIONARIO ='" + questionario_selectionado + "'";
            id_questionario = Convert.ToString(comando.ExecuteScalar());
            ViewState["id_questionario"] = id_questionario;
            conexao.Close();

             // Começa a receber as perguntas para serem respondidas
             if (numero_questao <= numero_max)
             {
                 string tipoResposta;
                 conexao.Open();
                 labelNome_Questionario.Text = "Nome do Questionário: "+ questionario_selectionado;
                 comando.CommandText = "SELECT QUESTAO from QUESTIONARIO_PERGUNTAS where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and NOME_QUESTIONARIO = '" + questionario_selectionado + "'";
                 labelQuestao.Text = "Questão " + numero_questao + ": " + Convert.ToString(comando.ExecuteScalar());
                 comando.CommandText = "SELECT Tipo_Resposta from QUESTIONARIO_PERGUNTAS where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and NOME_QUESTIONARIO = '" + questionario_selectionado + "'";
                 tipoResposta = Convert.ToString(comando.ExecuteScalar());
                 if (tipoResposta == "Resposta Satisfação")
                 {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = true;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;
                }
                 else if (tipoResposta == "Resposta Aberta")
                 {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = true;
                }
                 else if (tipoResposta == "Resposta Sim/Não")
                 {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = true;
                    TextBoxAberta.Visible = false;
                }
                 else if (tipoResposta == "Resposta Frequência")
                 {
                    DropDownListFrequencia.Visible = true;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;
                }
                 conexao.Close();
             }
        }

        protected void button_Seguinte_Click(object sender, EventArgs e)
        {
            buttonSubmeter.Visible = true;
            button_Seguinte.Visible = false;

            //Incrementa o numero da resposta
            numero_questao++;
            ViewState["numero_questao"] = numero_questao;
            labelNome_Questionario.Text = "Nome do Questionário: " + questionario_selectionado;
            panel_procura.Visible = false;
            panel_questionario.Visible = true;

            // Começa a receber as perguntas para serem respondidas
            if (numero_questao <= numero_max)
            {
                string tipoResposta;
                conexao.Open();
                comando.CommandText = "SELECT QUESTAO from QUESTIONARIO_PERGUNTAS where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and NOME_QUESTIONARIO = '" + questionario_selectionado + "'";
                labelQuestao.Text = "Questão " + numero_questao + ": " + Convert.ToString(comando.ExecuteScalar());
                comando.CommandText = "SELECT Tipo_Resposta from QUESTIONARIO_PERGUNTAS where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and NOME_QUESTIONARIO = '" + questionario_selectionado + "'";
                tipoResposta = Convert.ToString(comando.ExecuteScalar());

                if (tipoResposta == "Resposta Satisfação")
                {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = true;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;
                }
                else if (tipoResposta == "Resposta Aberta")
                {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = true;
                }
                else if (tipoResposta == "Resposta Sim/Não")
                {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = true;
                    TextBoxAberta.Visible = false;
                }
                else if (tipoResposta == "Resposta Frequência")
                {
                    DropDownListFrequencia.Visible = true;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;
                }
                conexao.Close();
            }
        }

        protected void buttonSubmeter_Click(object sender, EventArgs e)
        {
            panel_procura.Visible = false;
            panel_questionario.Visible = true;
            string tipoResposta;
            conexao.Open();
            comando.CommandText = "SELECT Tipo_Resposta from QUESTIONARIO_PERGUNTAS where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and NOME_QUESTIONARIO = '" + questionario_selectionado + "'";
            tipoResposta = Convert.ToString(comando.ExecuteScalar());

            if(numero_questao == numero_max)
            {
                buttonSubmeter.Visible = false;
                button_Seguinte.Visible = false;

                if (tipoResposta == "Resposta Frequência")
                {
                    DropDownListFrequencia.Visible = true;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;

                    comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListFrequencia.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    //Limpar 
                    DropDownListFrequencia.ClearSelection();
                    DropDownListFrequencia.Visible = false;
                    labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";
                }
                else if (tipoResposta == "Resposta Sim/Não")
                {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = true;
                    TextBoxAberta.Visible = false;

                    comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListSimNao.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    //Limpar
                    DropDownListSimNao.ClearSelection();
                    DropDownListSimNao.Visible = false;
                    labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";

                }
                else if (tipoResposta == "Resposta Aberta")
                {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = true;

                    comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + TextBoxAberta.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    //Limpar
                    TextBoxAberta.Text = "";
                    TextBoxAberta.Visible = false;
                    labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";
                }
                else if (tipoResposta == "Resposta Satisfação")
                {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = true;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;

                    comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListSatisfacao.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    //Limpar
                    DropDownListSatisfacao.ClearSelection();
                    DropDownListSatisfacao.Visible = false;
                    labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";
                }
            }
            else
            {
                buttonSubmeter.Visible = false;
                button_Seguinte.Visible = true;

                if (tipoResposta == "Resposta Frequência")
                {
                    DropDownListFrequencia.Visible = true;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;

                    comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListFrequencia.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    //Limpar 
                    DropDownListFrequencia.ClearSelection();
                    DropDownListFrequencia.Visible = false;
                    labelQuestao.Text = "Resposta Submetida";
                }
                else if (tipoResposta == "Resposta Sim/Não")
                {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = true;
                    TextBoxAberta.Visible = false;

                    comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListSimNao.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    //Limpar
                    DropDownListSimNao.ClearSelection();
                    DropDownListSimNao.Visible = false;
                    labelQuestao.Text = "Resposta Submetida";

                }
                else if (tipoResposta == "Resposta Aberta")
                {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = true;

                    comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + TextBoxAberta.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    //Limpar
                    TextBoxAberta.Text = "";
                    TextBoxAberta.Visible = false;
                    labelQuestao.Text = "Resposta Submetida";
                }
                else if (tipoResposta == "Resposta Satisfação")
                {
                    DropDownListFrequencia.Visible = false;
                    DropDownListSatisfacao.Visible = true;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;

                    comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListSatisfacao.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    //Limpar
                    DropDownListSatisfacao.ClearSelection();
                    DropDownListSatisfacao.Visible = false;
                    labelQuestao.Text = "Resposta Submetida";
                }

            }

            

            
        }
    }
}