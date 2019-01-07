using Oracle.ManagedDataAccess.Client;
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
            DropDownListRespostaManual.Visible = false;
            DropDownListSatisfacao.Visible = false;
            DropDownListSimNao.Visible = false;
            TextBoxAberta.Visible = false;
            buttonConcluir.Visible = false;

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
            DropDownListRespostaManual.Visible = false;
            DropDownListSatisfacao.Visible = false;
            DropDownListSimNao.Visible = false;
            TextBoxAberta.Visible = false;
            

            numero_questao = 1;
            ViewState["numero_questao"] = numero_questao;
            numero_max = 1;
            ViewState["numero_max"] = numero_max;

            // Mete na listBox os questionarios disponiveis para responder
            conexao.Open();
            comando.CommandText = "select nome_questionario from (select nome_questionario from questionario order by NOME_QUESTIONARIO asc) where nome_questionario not in (select nome_questionario from RESPOSTAS_QUESTIONARIO inner join questionario on respostas_questionario.id_questionario = questionario.ID_QUESTIONARIO inner join doente on doente.ID_DOENTE = RESPOSTAS_QUESTIONARIO.ID_DOENTE where doente.id_doente = '"+username+"')";
            dataReader = comando.ExecuteReader();

            if (dataReader.HasRows)
            {
                ListBox_Questionarios.Items.Clear();
                while (dataReader.Read())
                {
                    
                    ListBox_Questionarios.Items.Add(dataReader[0].ToString());
                }
            }
            conexao.Close();
        }

        protected void button_Comecar_Click(object sender, EventArgs e)
        {
            panel_procura.Visible = false;
            panel_questionario.Visible = true;
            DropDownListFrequencia.Visible = false;
            DropDownListRespostaManual.Visible = false;
            DropDownListSatisfacao.Visible = false;
            DropDownListSimNao.Visible = false;
            TextBoxAberta.Visible = false;
            button_Seguinte.Visible = true;
            button_Anterior.Visible = false;

            //Obtem o nome do questionario selecionado        
            questionario_selectionado = ListBox_Questionarios.SelectedItem.Value.ToString();
            ViewState["nome_questionario"] = questionario_selectionado;

             //Encontra o valor maximo de perguntas do questionario 
            conexao.Open();
            comando.CommandText = "select count(Numero_Questao) from questionario inner join questionario_pergunta on questionario.id_questionario = questionario_pergunta.id_questionario where nome_questionario ='" + questionario_selectionado + "'";
            numero_max = Convert.ToInt32(comando.ExecuteScalar());
            ViewState["numero_max"] = numero_max;
            comando.CommandText = "SELECT ID_Questionario from questionario where NOME_QUESTIONARIO ='" + questionario_selectionado + "'";
            id_questionario = Convert.ToString(comando.ExecuteScalar());
            ViewState["id_questionario"] = id_questionario;
            conexao.Close();

             // Começa a receber as perguntas para serem respondidas
             if (numero_questao <= numero_max)
             {
                 string tipoResposta;
                 conexao.Open();
                 labelNome_Questionario.Text = "Nome do Questionário: "+ questionario_selectionado;
                 comando.CommandText = "SELECT QUESTAO from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                 labelQuestao.Text = "Questão " + numero_questao + " de " + numero_max + ": " + Convert.ToString(comando.ExecuteScalar());
                 comando.CommandText = "SELECT Tipo_Resposta from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                 tipoResposta = Convert.ToString(comando.ExecuteScalar());
                 if (tipoResposta == "Resposta Satisfação")
                 {
                    DropDownListFrequencia.Visible = false;
                    DropDownListRespostaManual.Visible = false;
                    DropDownListSatisfacao.Visible = true;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;
                }
                 else if (tipoResposta == "Resposta Aberta")
                 {
                    DropDownListFrequencia.Visible = false;
                    DropDownListRespostaManual.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = true;
                }
                 else if (tipoResposta == "Resposta Sim/Não")
                 {
                    DropDownListFrequencia.Visible = false;
                    DropDownListRespostaManual.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = true;
                    TextBoxAberta.Visible = false;
                }
                 else if (tipoResposta == "Resposta Frequência")
                 {
                    DropDownListFrequencia.Visible = true;
                    DropDownListRespostaManual.Visible = false;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;
                }
                 else if (tipoResposta == "Adicionar Manualmente")
                {
                    DropDownListFrequencia.Visible = false;
                    DropDownListRespostaManual.Visible = true;
                    DropDownListSatisfacao.Visible = false;
                    DropDownListSimNao.Visible = false;
                    TextBoxAberta.Visible = false;

                    comando.CommandText = "Select Nome_Resposta From TipoResposta Where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                    dataReader = comando.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        DropDownListRespostaManual.Items.Clear();
                        DropDownListRespostaManual.Items.Add("");
                        while (dataReader.Read())
                        {
                            DropDownListRespostaManual.Items.Add(dataReader[0].ToString());
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Não existem valores adicionados!');", true);
                    }
                }
                 conexao.Close();
             }

             if (numero_questao == numero_max)
            {
                button_Seguinte.Text = "Concluir Questionário";
            }

        }

        protected void button_Seguinte_Click(object sender, EventArgs e)
        {
            panel_procura.Visible = false;
            panel_questionario.Visible = true;
            button_Anterior.Visible = true;
            string tipoResposta;
            string verificarquestao;

            conexao.Open();
            comando.CommandText = "SELECT Resposta from Respostas_Questionario where QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '"+username+"'";
            verificarquestao = Convert.ToString(comando.ExecuteScalar());
            conexao.Close();

            if (verificarquestao == "")
            {
                conexao.Open();
                comando.CommandText = "SELECT Tipo_Resposta from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                tipoResposta = Convert.ToString(comando.ExecuteScalar());

                if (numero_questao == numero_max)
                {
                    if (tipoResposta == "Resposta Frequência")
                    {
                        DropDownListFrequencia.Visible = true;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListFrequencia.Text == "")
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '(vazio)')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListFrequencia.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        
                        //Limpar 
                        DropDownListFrequencia.ClearSelection();
                        DropDownListFrequencia.Visible = false;
                        button_Seguinte.Visible = false;
                        buttonConcluir.Visible = true;
                        labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";

                    }
                    else if (tipoResposta == "Resposta Sim/Não")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = true;
                        TextBoxAberta.Visible = false;

                        if (DropDownListSimNao.Text == "")
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '(vazio)')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListSimNao.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListSimNao.ClearSelection();
                        DropDownListSimNao.Visible = false;
                        button_Seguinte.Visible = false;
                        buttonConcluir.Visible = true;
                        labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";

                    }
                    else if (tipoResposta == "Resposta Aberta")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = true;

                        if (TextBoxAberta.Text == "")
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '(vazio)')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + TextBoxAberta.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        TextBoxAberta.Text = "";
                        TextBoxAberta.Visible = false;
                        button_Seguinte.Visible = false;
                        buttonConcluir.Visible = true;
                        labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";
                    }
                    else if (tipoResposta == "Resposta Satisfação")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = true;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListSatisfacao.Text == "")
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '(vazio)')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListSatisfacao.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListSatisfacao.ClearSelection();
                        DropDownListSatisfacao.Visible = false;
                        button_Seguinte.Visible = false;
                        buttonConcluir.Visible = true;
                        labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";
                    }
                    else if (tipoResposta == "Adicionar Manualmente")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = true;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListRespostaManual.Text == "")
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '(vazio)')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListRespostaManual.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListRespostaManual.Items.Clear();
                        DropDownListRespostaManual.Visible = false;
                        button_Seguinte.Visible = false;
                        buttonConcluir.Visible = true;
                        labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";
                    }
                }
                else
                {
                    if (tipoResposta == "Resposta Frequência")
                    {
                        DropDownListFrequencia.Visible = true;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListFrequencia.Text == "")
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '(vazio)')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListFrequencia.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar 
                        DropDownListFrequencia.ClearSelection();
                        DropDownListFrequencia.Visible = false;
                    }
                    else if (tipoResposta == "Resposta Sim/Não")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = true;
                        TextBoxAberta.Visible = false;

                        if (DropDownListSimNao.Text == "")
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '(vazio)')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListSimNao.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListSimNao.ClearSelection();
                        DropDownListSimNao.Visible = false;

                    }
                    else if (tipoResposta == "Resposta Aberta")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = true;

                        if (TextBoxAberta.Text == "")
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '(vazio)')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + TextBoxAberta.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        TextBoxAberta.Text = "";
                        TextBoxAberta.Visible = false;
                    }
                    else if (tipoResposta == "Resposta Satisfação")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = true;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListSatisfacao.Text == "")
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '(vazio)')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListSatisfacao.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListSatisfacao.ClearSelection();
                        DropDownListSatisfacao.Visible = false;
                    }
                    else if (tipoResposta == "Adicionar Manualmente")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = true;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListRespostaManual.Text == "")
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '(vazio)')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "INSERT INTO Respostas_Questionario(ID_Doente, ID_Questionario, Questao, Resposta)VALUES('" + username + "', '" + id_questionario + "', '" + "Questão-" + numero_questao + "', '" + DropDownListRespostaManual.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListRespostaManual.Items.Clear();
                        DropDownListRespostaManual.Visible = false;
                    }

                }
            }
            else
            {
                conexao.Open();
                comando.CommandText = "SELECT Tipo_Resposta from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                tipoResposta = Convert.ToString(comando.ExecuteScalar());

                if (numero_questao == numero_max)
                {
                    if (tipoResposta == "Resposta Frequência")
                    {
                        DropDownListFrequencia.Visible = true;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListFrequencia.Text == "")
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '(vazio)' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '" + DropDownListFrequencia.Text + "' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar 
                        DropDownListFrequencia.ClearSelection();
                        DropDownListFrequencia.Visible = false;
                        button_Seguinte.Visible = false;
                        buttonConcluir.Visible = true;
                        labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";
                    }
                    else if (tipoResposta == "Resposta Sim/Não")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = true;
                        TextBoxAberta.Visible = false;

                        if (DropDownListSimNao.Text == "")
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '(vazio)' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '" + DropDownListSimNao.Text + "' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListSimNao.ClearSelection();
                        DropDownListSimNao.Visible = false;
                        button_Seguinte.Visible = false;
                        buttonConcluir.Visible = true;
                        labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";

                    }
                    else if (tipoResposta == "Resposta Aberta")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = true;

                        if (TextBoxAberta.Text == "")
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '(vazio)' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '" + TextBoxAberta.Text + "' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        TextBoxAberta.Text = "";
                        TextBoxAberta.Visible = false;
                        button_Seguinte.Visible = false;
                        buttonConcluir.Visible = true;
                        labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";
                    }
                    else if (tipoResposta == "Resposta Satisfação")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = true;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListSatisfacao.Text == "")
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '(vazio)' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '" + DropDownListSatisfacao.Text + "' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListSatisfacao.ClearSelection();
                        DropDownListSatisfacao.Visible = false;
                        button_Seguinte.Visible = false;
                        buttonConcluir.Visible = true;
                        labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";
                    }
                    else if (tipoResposta == "Adicionar Manualmente")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = true;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListRespostaManual.Text == "")
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '(vazio)' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '" + DropDownListRespostaManual.Text + "' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListRespostaManual.Items.Clear();
                        DropDownListRespostaManual.Visible = false;
                        button_Seguinte.Visible = false;
                        buttonConcluir.Visible = true;
                        labelQuestao.Text = "Questionário Concluido com Sucesso, Obrigado!";
                    }
                }
                else
                {
                    if (tipoResposta == "Resposta Frequência")
                    {
                        DropDownListFrequencia.Visible = true;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListFrequencia.Text == "")
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '(vazio)' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '" + DropDownListFrequencia.Text + "' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar 
                        DropDownListFrequencia.ClearSelection();
                        DropDownListFrequencia.Visible = false;
                    }
                    else if (tipoResposta == "Resposta Sim/Não")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = true;
                        TextBoxAberta.Visible = false;

                        if (DropDownListSimNao.Text == "")
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '(vazio)' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '" + DropDownListSimNao.Text + "' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListSimNao.ClearSelection();
                        DropDownListSimNao.Visible = false;

                    }
                    else if (tipoResposta == "Resposta Aberta")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = true;

                        if (TextBoxAberta.Text == "")
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '(vazio)' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '" + TextBoxAberta.Text + "' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        TextBoxAberta.Text = "";
                        TextBoxAberta.Visible = false;
                    }
                    else if (tipoResposta == "Resposta Satisfação")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = true;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListSatisfacao.Text == "")
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '(vazio)' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '" + DropDownListSatisfacao.Text + "' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListSatisfacao.ClearSelection();
                        DropDownListSatisfacao.Visible = false;
                    }
                    else if (tipoResposta == "Adicionar Manualmente")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = true;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        if (DropDownListRespostaManual.Text == "")
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '(vazio)' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }
                        else
                        {
                            comando.CommandText = "UPDATE Respostas_Questionario SET Resposta = '" + DropDownListRespostaManual.Text + "' Where ID_Doente = '" + username + "' and ID_Questionario = '" + id_questionario + "' and Questao = '" + "Questão-" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            conexao.Close();
                        }

                        //Limpar
                        DropDownListRespostaManual.Items.Clear();
                        DropDownListRespostaManual.Visible = false;
                    }

                }
            }

            



            // PASSAR PARA A PERGUNTA SEGUINTE //

            //Incrementa o numero da resposta
            numero_questao++;
            ViewState["numero_questao"] = numero_questao;
            labelNome_Questionario.Text = "Nome do Questionário: " + questionario_selectionado;
            panel_procura.Visible = false;
            panel_questionario.Visible = true;

            // Começa a receber as perguntas para serem respondidas
            if (numero_questao <= numero_max)
            {
                string respostaseguinte;
                conexao.Open();
                comando.CommandText = "Select Resposta From Respostas_Questionario Where ID_Questionario = '"+id_questionario+"' and ID_Doente = '"+username+"' and Questao = '" + "Questão-" + numero_questao  +"'";
                respostaseguinte = Convert.ToString(comando.ExecuteScalar());
                conexao.Close();

                if (respostaseguinte == "")
                {
                    string tipoResposta1;
                    conexao.Open();
                    labelNome_Questionario.Text = "Nome do Questionário: " + questionario_selectionado;
                    comando.CommandText = "SELECT QUESTAO from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                    labelQuestao.Text = "Questão " + numero_questao + " de " + numero_max + ": " + Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Tipo_Resposta from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                    tipoResposta1 = Convert.ToString(comando.ExecuteScalar());

                    if (tipoResposta1 == "Resposta Satisfação")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = true;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;
                    }
                    else if (tipoResposta1 == "Resposta Aberta")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = true;
                    }
                    else if (tipoResposta1 == "Resposta Sim/Não")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = true;
                        TextBoxAberta.Visible = false;
                    }
                    else if (tipoResposta1 == "Resposta Frequência")
                    {
                        DropDownListFrequencia.Visible = true;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;
                    }
                    else if (tipoResposta1 == "Adicionar Manualmente")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = true;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        comando.CommandText = "Select Nome_Resposta From TipoResposta Where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                        dataReader = comando.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            DropDownListRespostaManual.Items.Clear();
                            DropDownListRespostaManual.Items.Add("");
                            while (dataReader.Read())
                            {
                                DropDownListRespostaManual.Items.Add(dataReader[0].ToString());
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Não existem valores adicionados!');", true);
                        }
                    }
                    conexao.Close();
                }
                else
                {
                    string tipoResposta1;
                    conexao.Open();
                    labelNome_Questionario.Text = "Nome do Questionário: " + questionario_selectionado;
                    comando.CommandText = "SELECT QUESTAO from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                    labelQuestao.Text = "Questão " + numero_questao + " de " + numero_max + ": " + Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Tipo_Resposta from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                    tipoResposta1 = Convert.ToString(comando.ExecuteScalar());

                    if (tipoResposta1 == "Resposta Satisfação")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = true;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        // Coloca a resposta dada visivel
                        comando.CommandText = "Select Resposta From Respostas_Questionario Where Questao = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '" + username + "'";
                        DropDownListSatisfacao.Text = Convert.ToString(comando.ExecuteScalar());
                    }
                    else if (tipoResposta1 == "Resposta Aberta")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = true;

                        // Coloca a resposta dada visivel
                        comando.CommandText = "Select Resposta From Respostas_Questionario Where Questao = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '" + username + "'";
                        TextBoxAberta.Text = Convert.ToString(comando.ExecuteScalar());
                    }
                    else if (tipoResposta1 == "Resposta Sim/Não")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = true;
                        TextBoxAberta.Visible = false;

                        // Coloca a resposta dada visivel
                        comando.CommandText = "Select Resposta From Respostas_Questionario Where Questao = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '" + username + "'";
                        DropDownListSimNao.Text = Convert.ToString(comando.ExecuteScalar());
                    }
                    else if (tipoResposta1 == "Resposta Frequência")
                    {
                        DropDownListFrequencia.Visible = true;
                        DropDownListRespostaManual.Visible = false;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        // Coloca a resposta dada visivel
                        comando.CommandText = "Select Resposta From Respostas_Questionario Where Questao = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '" + username + "'";
                        DropDownListFrequencia.Text = Convert.ToString(comando.ExecuteScalar());
                    }
                    else if (tipoResposta1 == "Adicionar Manualmente")
                    {
                        DropDownListFrequencia.Visible = false;
                        DropDownListRespostaManual.Visible = true;
                        DropDownListSatisfacao.Visible = false;
                        DropDownListSimNao.Visible = false;
                        TextBoxAberta.Visible = false;

                        comando.CommandText = "Select Nome_Resposta From TipoResposta Where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                        dataReader = comando.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            DropDownListRespostaManual.Items.Clear();
                            DropDownListRespostaManual.Items.Add("");
                            while (dataReader.Read())
                            {
                                DropDownListRespostaManual.Items.Add(dataReader[0].ToString());
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Não existem valores adicionados!');", true);
                        }

                        // Coloca a resposta dada visivel
                        comando.CommandText = "Select Resposta From Respostas_Questionario Where Questao = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '" + username + "'";
                        DropDownListRespostaManual.Text = Convert.ToString(comando.ExecuteScalar());
                    }
                    conexao.Close();
                }
            }

            if (numero_questao == numero_max)
            {
                button_Seguinte.Text = "Concluir Questionário";
            }
            else
            {
                button_Seguinte.Text = "Próxima Questão";
            }
        }

        protected void button_Anterior_Click(object sender, EventArgs e)
        {
            string tipoRespostaAnterior;
            panel_procura.Visible = false;
            panel_questionario.Visible = true;

            numero_questao--;
            ViewState["numero_questao"] = numero_questao;
            conexao.Open();
            comando.CommandText = "SELECT QUESTAO from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
            labelQuestao.Text = "Questão " + numero_questao + " de " + numero_max + ": " + Convert.ToString(comando.ExecuteScalar());
            comando.CommandText = "";

            comando.CommandText = "SELECT Tipo_Resposta from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
            tipoRespostaAnterior = Convert.ToString(comando.ExecuteScalar());

            if (tipoRespostaAnterior == "Resposta Satisfação")
            {
                DropDownListFrequencia.Visible = false;
                DropDownListRespostaManual.Visible = false;
                DropDownListSatisfacao.Visible = true;
                DropDownListSimNao.Visible = false;
                TextBoxAberta.Visible = false;

                // Coloca a resposta dada visivel
                comando.CommandText = "Select Resposta From Respostas_Questionario Where Questao = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '"+username+"'";
                DropDownListSatisfacao.Text = Convert.ToString(comando.ExecuteScalar());
            }
            else if (tipoRespostaAnterior == "Resposta Aberta")
            {
                DropDownListFrequencia.Visible = false;
                DropDownListRespostaManual.Visible = false;
                DropDownListSatisfacao.Visible = false;
                DropDownListSimNao.Visible = false;
                TextBoxAberta.Visible = true;

                // Coloca a resposta dada visivel
                comando.CommandText = "Select Resposta From Respostas_Questionario Where Questao = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '" + username + "'";
                TextBoxAberta.Text = Convert.ToString(comando.ExecuteScalar());
            }
            else if (tipoRespostaAnterior == "Resposta Sim/Não")
            {
                DropDownListFrequencia.Visible = false;
                DropDownListRespostaManual.Visible = false;
                DropDownListSatisfacao.Visible = false;
                DropDownListSimNao.Visible = true;
                TextBoxAberta.Visible = false;

                // Coloca a resposta dada visivel
                comando.CommandText = "Select Resposta From Respostas_Questionario Where Questao = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '" + username + "'";
                DropDownListSimNao.Text = Convert.ToString(comando.ExecuteScalar());
            }
            else if (tipoRespostaAnterior == "Resposta Frequência")
            {
                DropDownListFrequencia.Visible = true;
                DropDownListRespostaManual.Visible = false;
                DropDownListSatisfacao.Visible = false;
                DropDownListSimNao.Visible = false;
                TextBoxAberta.Visible = false;

                // Coloca a resposta dada visivel
                comando.CommandText = "Select Resposta From Respostas_Questionario Where Questao = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '" + username + "'";
                DropDownListFrequencia.Text = Convert.ToString(comando.ExecuteScalar());
            }
            else if (tipoRespostaAnterior == "Adicionar Manualmente")
            {
                DropDownListFrequencia.Visible = false;
                DropDownListRespostaManual.Visible = true;
                DropDownListSatisfacao.Visible = false;
                DropDownListSimNao.Visible = false;
                TextBoxAberta.Visible = false;

                comando.CommandText = "Select Nome_Resposta From TipoResposta Where NUMERO_QUESTAO = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "'";
                dataReader = comando.ExecuteReader();
                if (dataReader.HasRows)
                {
                    DropDownListRespostaManual.Items.Clear();
                    DropDownListRespostaManual.Items.Add("");
                    while (dataReader.Read())
                    {
                        DropDownListRespostaManual.Items.Add(dataReader[0].ToString());
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Não existem valores adicionados!');", true);
                }

                // Coloca a resposta dada visivel
                comando.CommandText = "Select Resposta From Respostas_Questionario Where Questao = '" + "Questão-" + numero_questao + "' and ID_QUESTIONARIO = '" + id_questionario + "' and ID_Doente = '" + username + "'";
                DropDownListRespostaManual.Text = Convert.ToString(comando.ExecuteScalar());
            }
            conexao.Close();

            if (numero_questao == 1)
            {
                button_Anterior.Visible = false;
                button_Seguinte.Visible = true;
            }
            else
            {
                button_Anterior.Visible = true;
                button_Seguinte.Visible = true;
            }

            if (numero_questao == numero_max)
            {
                button_Seguinte.Text = "Concluir Questionário";
            }
            else
            {
                button_Seguinte.Text = "Próxima Questão";
            }
        }

        protected void buttonConcluir_Click(object sender, EventArgs e)
        {
            panel_questionario.Visible = false;
            panel_procura.Visible = false;

            Response.Redirect("~/html/Doente.aspx");
        }
    }
}