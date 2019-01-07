using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace scrum_Grupo2_website.html
{
    public partial class Administrador : System.Web.UI.Page
    {
        // Ligação base dados oracle
        OracleConnection conexao = new OracleConnection("DATA SOURCE=25.15.145.193:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;
        Registo registo = new Registo();
        int numero_pergunta = 1;
        int novoID = 0;
        string questionario_selectionado;

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;

            panelDoente.Visible = false;
            panelMedico.Visible = false;
            panelInfo_Socio.Visible = false;
            panelQuestionario.Visible = false;
            panel_QuestionarioOpcoes.Visible = false;

            LabelConfirmacao.Visible = false;
            labelProcurar.Text = "";
            ListBoxOpcoesResposta.Visible = false;
            TextBoxAdicionarResposta.Visible = false;
            ButtonAddResp.Visible = false;
            ButtonRemoveResp.Visible = false;

            labelQuestionario.Text = "Questão-" + numero_pergunta;
            if (IsPostBack)
            {
                if (ViewState["count"] != null)
                {
                    numero_pergunta = (int)ViewState["count"];
                }
                if (ViewState["novoID"] != null)
                {
                    novoID = (int)ViewState["novoID"];
                }
                if (ViewState["nome_questionario"] != null)
                {
                    questionario_selectionado = (string)ViewState["nome_questionario"];
                }
            }
        }

        protected void ButtonProcurar_Click(object sender, EventArgs e)
        {
            if (registo.verificarNumero(TextBox_Procurar.Text) == true)
            {
                conexao.Open();
                comando.CommandText = "SELECT Numero_Utente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                comando.ExecuteNonQuery();
                string IdProcurarDoente = Convert.ToString(comando.ExecuteScalar());

                if (TextBox_Procurar.Text == IdProcurarDoente && TextBox_Procurar.Text != "")
                {
                    panelDoente.Visible = true;
                    panelMedico.Visible = false;
                    panelInfo_Socio.Visible = false;

                    comando.CommandText = "SELECT Nome_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                    txtbox_nome.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Morada FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                    TextBox_morada.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Numero_Utente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                    TextBox_Utente.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Email_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                    txtbox_email.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Data_Nascimento_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                    TextBoxNascimento_Doente.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Genero FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                    DropDownList_Sexo.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.ExecuteNonQuery();
                    conexao.Close();
                }
                else
                {
                    comando.CommandText = "SELECT Numero_Cedula FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                    comando.ExecuteNonQuery();
                    string IdProcurarMedico = Convert.ToString(comando.ExecuteScalar());

                    if (TextBox_Procurar.Text == IdProcurarMedico && TextBox_Procurar.Text != "")
                    {
                        panelDoente.Visible = false;
                        panelMedico.Visible = true;
                        panelInfo_Socio.Visible = false;

                        comando.CommandText = "SELECT Nome_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        TextBox_Nome_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Morada_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        TextBox_Morada_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Numero_Cedula FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        txtbox_cedula.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Email_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        TextBox_Email_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Contribuinte_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        TextBox_Contribuinte_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Genero_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        DropDownList_Sexo_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Data_Nascimento_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        TextBoxNascimento_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.ExecuteNonQuery();
                        conexao.Close();
                    }
                    else
                    {
                        labelProcurar.Text = "Não encontrado!";
                    }
                }
            }
            else
            {
                labelProcurar.Text = "Introduza um número válido!";
            }
        }

        protected void ButtonEditar_Medico_Click(object sender, EventArgs e)
        {
            if (TextBox_Nome_Medico.Text != "" & TextBox_Morada_Medico.Text != "" & TextBox_Contribuinte_Medico.Text != "" & DropDownList_Sexo_Medico.Text != "" & txtbox_cedula.Text != "")
            {
                if (registo.verificarEmail(TextBox_Email_Medico.Text) == true)
                {
                    conexao.Open();
                    comando.CommandText = "UPDATE Medico set Nome_Medico = '" + TextBox_Nome_Medico.Text + "', Morada_Medico = '" + TextBox_Morada_Medico.Text + "', Contribuinte_Medico = '" + TextBox_Contribuinte_Medico.Text + "', Genero_Medico = '" + DropDownList_Sexo_Medico.Text + "', Email_Medico = '" + TextBox_Email_Medico.Text + "',  Numero_Cedula = '" + txtbox_cedula.Text + "' WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Registo alterado com sucesso!');", true);
                    TextBox_Procurar.Text = "";

                    panelDoente.Visible = false;
                    panelMedico.Visible = true;
                    panelInfo_Socio.Visible = false;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Email inválido, por favor verifique novamente!');", true);
                    panelDoente.Visible = false;
                    panelMedico.Visible = true;
                    panelInfo_Socio.Visible = false;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
                panelDoente.Visible = false;
                panelMedico.Visible = true;
                panelInfo_Socio.Visible = false;
            }
        }

        protected void ButtonRemover_Medico_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "DELETE FROM Medico where Numero_Cedula = '"+ TextBox_Procurar.Text +"'";
            comando.ExecuteNonQuery();
            conexao.Close();
            TextBox_Procurar.Text = "";
        }

        protected void ButtonEditar_Doente_Click(object sender, EventArgs e)
        {
            if (txtbox_nome.Text != "" & TextBox_morada.Text != "" & TextBox_Utente.Text != "" & DropDownList_Sexo.Text != "")
            {
                if (registo.verificarEmail(txtbox_email.Text) == true)
                {
                    panelDoente.Visible = true;
                    panelMedico.Visible = false;
                    panelInfo_Socio.Visible = false;
                    conexao.Open();
                    comando.CommandText = "UPDATE Doente set Nome_Doente = '"+txtbox_nome.Text+"', Morada = '"+TextBox_morada.Text+"', Numero_Utente = '"+TextBox_Utente.Text+"', Genero = '"+DropDownList_Sexo.Text+"', Email_Doente = '"+txtbox_email.Text+"' WHERE Numero_Utente = '"+TextBox_Procurar.Text+"'";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Registo alterado com sucesso!');", true);
                    TextBox_Procurar.Text = "";
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Email inválido, por favor verifique novamente!');", true);
                    panelDoente.Visible = true;
                    panelMedico.Visible = false;
                    panelInfo_Socio.Visible = false;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
                panelDoente.Visible = true;
                panelMedico.Visible = false;
                panelInfo_Socio.Visible = false;
            }
        }

        protected void ButtonRemover_Doente_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "DELETE FROM Doente where Numero_Utente = '" + TextBox_Procurar.Text + "'";
            comando.ExecuteNonQuery();
            conexao.Close();
            TextBox_Procurar.Text = "";
        }

        protected void ButtonInfo_Socio_Click(object sender, EventArgs e)
        {
            panelInfo_Socio.Visible = true;
            conexao.Open();
            comando.CommandText = "SELECT Nome_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
            TextBox_Nome_Socio.Text = Convert.ToString(comando.ExecuteScalar());
            comando.CommandText = "SELECT Morada FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
            TextBox_Morada_Socio.Text = Convert.ToString(comando.ExecuteScalar());
            comando.CommandText = "SELECT Data_Nascimento_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
            TextBox_DataNascimento_Socio.Text = Convert.ToString(comando.ExecuteScalar());
            comando.ExecuteNonQuery();
            conexao.Close();

        }

        protected void Button_Guardar_Socio_Click(object sender, EventArgs e)
        {
            if(TextBox_Nome_Socio.Text != "" && TextBox_DataNascimento_Socio.Text != "" && TextBox_Morada_Socio.Text != "" && TextBox_Nacionalidade_Socio.Text != "" && TextBox_Naturalidade_Socio.Text != "" && DropDownList_EstadoCivil_Socio.Text != "" && Textbox_NumeroFilhos_Socio.Text != "" && Textbox_Agregado_Socio.Text != "" && TextBox_Profissão_Socio.Text != "" && DropDownList_Escolaridade_Socio.Text != "")
            {
                panelInfo_Socio.Visible = true;
                panelMedico.Visible = false;
                panelDoente.Visible = false;
                try
                {
                    conexao.Open();
                    comando.CommandText = "INSERT INTO INFOSOCIO(ID_Doente, Nacionalidade, Naturalidade, Estado_Civil, Numero_Filhos, Agregado_Familiar, Profissao, Escolaridade) VALUES ((SELECT ID_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'), '" + TextBox_Nacionalidade_Socio.Text + "', '" + TextBox_Naturalidade_Socio.Text + "', '" + DropDownList_EstadoCivil_Socio.Text + "', '" + Textbox_NumeroFilhos_Socio.Text + "', '" + Textbox_Agregado_Socio.Text + "', '" + TextBox_Profissão_Socio.Text + "', '" + DropDownList_Escolaridade_Socio.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Informação Registada com sucesso!');", true);

                    //Limpar dados
                    TextBox_Nome_Socio.Text = "";
                    TextBox_DataNascimento_Socio.Text = "";
                    TextBox_Morada_Socio.Text = "";
                    TextBox_Nacionalidade_Socio.Text = "";
                    TextBox_Naturalidade_Socio.Text = "";
                    DropDownList_EstadoCivil_Socio.ClearSelection();
                    Textbox_NumeroFilhos_Socio.Text = "";
                    Textbox_Agregado_Socio.Text = "";
                    TextBox_Profissão_Socio.Text = "";
                    DropDownList_Escolaridade_Socio.ClearSelection();
                    TextBox_Procurar.Text = "";
                }
                catch (Oracle.ManagedDataAccess.Client.OracleException)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Utente com dados já inseridos!');", true);
                }      
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
            }
        }

        protected void btn_Questionario_Click(object sender, EventArgs e)
        {
            panelDoente.Visible = false;
            panelMedico.Visible = false;
            panelInfo_Socio.Visible = false;
            panelQuestionario.Visible = false;
            panel_QuestionarioOpcoes.Visible = true;

            conexao.Open();
            comando.CommandText = "Select nome_questionario from questionario order by nome_questionario asc";
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

        protected void ButtonAddQuestionario_Click(object sender, EventArgs e)
        {
            panelDoente.Visible = false;
            panelMedico.Visible = false;
            panelInfo_Socio.Visible = false;
            panelQuestionario.Visible = true;
            panel_QuestionarioOpcoes.Visible = false;
            ButtonEditarQuestao.Visible = false;
            TextBoxNomeQuestionario.Text = "";
            TextBoxPergunta.Text = "";
            DropDownListTipoPergunta.ClearSelection();

            // Dar Reset ao numero das questoes
            numero_pergunta = 1;
            ViewState["count"] = numero_pergunta;
            try
            {
                conexao.Open();
                comando.CommandText = "SELECT MAX(ID_Questionario)+1 from Questionario";
                comando.ExecuteNonQuery();
                novoID = Convert.ToInt32(comando.ExecuteScalar());
                conexao.Close();
                ViewState["novoID"] = novoID;
            }
            catch (System.InvalidCastException)
            {
                novoID = 1;
                ViewState["novoID"] = novoID;
            }
        }

        protected void ButtonRemoverQuestionario_Click(object sender, EventArgs e)
        {
            panelDoente.Visible = false;
            panelMedico.Visible = false;
            panelInfo_Socio.Visible = false;
            panelQuestionario.Visible = false;
            panel_QuestionarioOpcoes.Visible = true;

            try
            {
                questionario_selectionado = ListBox_Questionarios.SelectedItem.Value.ToString();
                ViewState["nome_questionario"] = questionario_selectionado;

                //Apaga o Questionario que estiver selecionado
                conexao.Open();
                comando.CommandText = "Delete from TipoResposta Where ID_QUESTIONARIO = (Select id_questionario from questionario where nome_questionario = '" + questionario_selectionado + "')";
                comando.ExecuteNonQuery();
                comando.CommandText = "Delete from Respostas_Questionario Where ID_Questionario = (Select ID_Questionario from Questionario Where nome_questionario = '" + questionario_selectionado + "')";
                comando.ExecuteNonQuery();
                comando.CommandText = "Delete from Questionario_Pergunta Where ID_Questionario = (Select ID_Questionario from Questionario Where nome_questionario = '" + questionario_selectionado + "')";
                comando.ExecuteNonQuery();
                comando.CommandText = "Delete from Questionario Where Nome_Questionario = '" + questionario_selectionado + "'";
                comando.ExecuteNonQuery();

                //Dá Refresh na ListBox para que nao apareça o questionario que foi apagado (Trocar por metodo mais simples se for encontrado)
                comando.CommandText = "Select nome_questionario from questionario order by nome_questionario asc";
                dataReader = comando.ExecuteReader();
                ListBox_Questionarios.Items.Clear();

                if (dataReader.HasRows)
                {                 
                    while (dataReader.Read())
                    {

                        ListBox_Questionarios.Items.Add(dataReader[0].ToString());
                    }
                }
                conexao.Close();
            }
            catch (System.NullReferenceException)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, selecione um Questionário para remover!');", true);
            }
        }

        protected void ButtonAdicionarQuestionario_Click(object sender, EventArgs e)
        {
            //Colocar Panel visiveis e invisiveis
            panelDoente.Visible = false;
            panelMedico.Visible = false;
            panelInfo_Socio.Visible = false;
            panelQuestionario.Visible = true;
            ButtonEditarQuestao.Visible = true;

            string verificarquestao = "";
            string questao_editar = ("Questão-" + numero_pergunta).ToString();
            conexao.Open();
            comando.CommandText = "select questao from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '"+questao_editar+"' and ID_QUESTIONARIO = '"+novoID+"'";
            verificarquestao = Convert.ToString(comando.ExecuteScalar());
            conexao.Close();
            
            if (verificarquestao == "")
            {
                if (numero_pergunta == 1)
                {
                    ButtonEditarQuestao.Visible = true;
                    if (TextBoxNomeQuestionario.Text != "" && TextBoxPergunta.Text != "")
                    {
                        string questionarioexistente;

                        conexao.Open();
                        comando.CommandText = "Select Nome_Questionario From Questionario Where Nome_Questionario = '" + TextBoxNomeQuestionario.Text + "'";
                        questionarioexistente = Convert.ToString(comando.ExecuteScalar());
                        conexao.Close();

                        if (questionarioexistente == "")
                        {
                            string numero_questao = ("Questão-" + numero_pergunta).ToString();

                            if (DropDownListTipoPergunta.Text == "Adicionar Manualmente")
                            {
                                conexao.Open();
                                comando.CommandText = "INSERT INTO Questionario(ID_Questionario, Nome_Questionario)VALUES('" + novoID + "', '" + TextBoxNomeQuestionario.Text + "')";
                                comando.ExecuteNonQuery();
                                comando.CommandText = "INSERT INTO Questionario_Pergunta(ID_Questionario, Numero_Questao, Questao, Tipo_Resposta)VALUES('" + novoID + "', '" + numero_questao + "', '" + TextBoxPergunta.Text + "', '" + DropDownListTipoPergunta.Text + "')";
                                comando.ExecuteNonQuery();
                                if (ListBoxOpcoesResposta.Items.Count >= 2)
                                {
                                    for (int i = 0; i < ListBoxOpcoesResposta.Items.Count; i++)
                                    {
                                        comando.CommandText = "INSERT INTO TipoResposta(ID_Questionario, Numero_Questao, Nome_Resposta)VALUES('" + novoID + "', '" + numero_questao + "', '" + ListBoxOpcoesResposta.Items[i].Text + "')";
                                        comando.ExecuteNonQuery();
                                    }
                                    conexao.Close();

                                    //Limpar Pergunta anterior para passar para a proxima
                                    TextBoxPergunta.Text = "";
                                    DropDownListTipoPergunta.ClearSelection();
                                    ListBoxOpcoesResposta.Items.Clear();
                                    TextBoxAdicionarResposta.Text = "";

                                    //Incrementar o numero da pergunta
                                    numero_pergunta++;
                                    ViewState["count"] = numero_pergunta;
                                    labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, adicione mais opções de resposta!');", true);
                                    ViewState["count"] = numero_pergunta;
                                    labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                                    ListBoxOpcoesResposta.Visible = true;
                                    ButtonAddResp.Visible = true;
                                    ButtonRemoveResp.Visible = true;
                                    TextBoxAdicionarResposta.Visible = true;
                                }

                            }
                            else
                            {
                                conexao.Open();
                                comando.CommandText = "INSERT INTO Questionario(ID_Questionario, Nome_Questionario)VALUES('" + novoID + "', '" + TextBoxNomeQuestionario.Text + "')";
                                comando.ExecuteNonQuery();
                                comando.CommandText = "INSERT INTO Questionario_Pergunta(ID_Questionario, Numero_Questao, Questao, Tipo_Resposta)VALUES('" + novoID + "', '" + numero_questao + "', '" + TextBoxPergunta.Text + "', '" + DropDownListTipoPergunta.Text + "')";
                                comando.ExecuteNonQuery();
                                conexao.Close();

                                //Limpar Pergunta anterior para passar para a proxima
                                TextBoxPergunta.Text = "";
                                DropDownListTipoPergunta.ClearSelection();

                                //Incrementar o numero da pergunta
                                numero_pergunta++;
                                ViewState["count"] = numero_pergunta;
                                labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Nome de Questionario já existente, por favor escolha outro nome!');", true);
                            ViewState["count"] = numero_pergunta;
                            labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
                        ViewState["count"] = numero_pergunta;
                        labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                    }

                }
                else
                {
                    ButtonEditarQuestao.Visible = true;
                    if (TextBoxNomeQuestionario.Text != "" && TextBoxPergunta.Text != "")
                    {
                        string numero_questao = ("Questão-" + numero_pergunta).ToString();

                        if (DropDownListTipoPergunta.Text == "Adicionar Manualmente")
                        {
                            conexao.Open();
                            comando.CommandText = "INSERT INTO Questionario_Pergunta(ID_Questionario, Numero_Questao, Questao, Tipo_Resposta)VALUES('" + novoID + "', '" + numero_questao + "', '" + TextBoxPergunta.Text + "', '" + DropDownListTipoPergunta.Text + "')";
                            comando.ExecuteNonQuery();
                            
                            if(ListBoxOpcoesResposta.Items.Count >= 2)
                            {
                                for (int i = 0; i < ListBoxOpcoesResposta.Items.Count; i++)
                                {
                                    comando.CommandText = "INSERT INTO TipoResposta(ID_Questionario, Numero_Questao, Nome_Resposta)VALUES('" + novoID + "', '" + numero_questao + "', '" + ListBoxOpcoesResposta.Items[i].Text + "')";
                                    comando.ExecuteNonQuery();
                                }
                                conexao.Close();

                                //Limpar Pergunta anterior para passar para a proxima
                                TextBoxPergunta.Text = "";
                                DropDownListTipoPergunta.ClearSelection();
                                ListBoxOpcoesResposta.Items.Clear();
                                TextBoxAdicionarResposta.Text = "";

                                //Incrementar o numero da pergunta
                                numero_pergunta++;
                                ViewState["count"] = numero_pergunta;
                                labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, adicione mais opções de resposta!');", true);
                                ViewState["count"] = numero_pergunta;
                                labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                                ListBoxOpcoesResposta.Visible = true;
                                ButtonAddResp.Visible = true;
                                ButtonRemoveResp.Visible = true;
                                TextBoxAdicionarResposta.Visible = true;
                            }
                        }
                        else
                        {
                            conexao.Open();
                            comando.CommandText = "INSERT INTO Questionario_Pergunta(ID_Questionario, Numero_Questao, Questao, Tipo_Resposta)VALUES('" + novoID + "', '" + numero_questao + "', '" + TextBoxPergunta.Text + "', '" + DropDownListTipoPergunta.Text + "')";
                            comando.ExecuteNonQuery();
                            conexao.Close();

                            //Limpar Pergunta anterior para passar para a proxima
                            TextBoxPergunta.Text = "";
                            DropDownListTipoPergunta.ClearSelection();

                            //Incrementar o numero da pergunta
                            numero_pergunta++;
                            ViewState["count"] = numero_pergunta;
                            labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
                        ViewState["count"] = numero_pergunta;
                        labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                    }
                }

                LabelConfirmacao.Visible = true;
                LabelConfirmacao.Text = "Questão-" + (numero_pergunta - 1) + " submetida com sucesso, para terminar o questionário apenas saia deste!";
            }
            else
            {
                if (TextBoxNomeQuestionario.Text != "" && TextBoxPergunta.Text != "")
                {
                    string numero_questao = ("Questão-" + numero_pergunta).ToString();

                    if (DropDownListTipoPergunta.Text == "Adicionar Manualmente")
                    {
                        conexao.Open();
                        comando.CommandText = "UPDATE QUESTIONARIO_PERGUNTA SET QUESTAO = '" + TextBoxPergunta.Text + "', TIPO_RESPOSTA = '" + DropDownListTipoPergunta.Text + "' where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao + "'";
                        comando.ExecuteNonQuery();
                        
                        if(ListBoxOpcoesResposta.Items.Count >= 2)
                        {
                            comando.CommandText = "Delete from TipoResposta Where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao + "'";
                            comando.ExecuteNonQuery();
                            for (int i = 0; i < ListBoxOpcoesResposta.Items.Count; i++)
                            {
                                comando.CommandText = "INSERT INTO TipoResposta(ID_Questionario, Numero_Questao, Nome_Resposta)VALUES('" + novoID + "', '" + numero_questao + "', '" + ListBoxOpcoesResposta.Items[i].Text + "')";
                                comando.ExecuteNonQuery();
                            }
                            conexao.Close();

                            //Limpar Pergunta anterior para passar para a proxima
                            TextBoxPergunta.Text = "";
                            DropDownListTipoPergunta.ClearSelection();
                            ListBoxOpcoesResposta.Items.Clear();
                            TextBoxAdicionarResposta.Text = "";

                            //Incrementar o numero da pergunta
                            numero_pergunta++;
                            ViewState["count"] = numero_pergunta;
                            labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();

                            string verificarquestao1;
                            string proximaquestao = ("Questão-" + numero_pergunta).ToString();

                            conexao.Open();
                            comando.CommandText = "select questao from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + proximaquestao + "' and ID_QUESTIONARIO = '" + novoID + "'";
                            verificarquestao1 = Convert.ToString(comando.ExecuteScalar());
                            conexao.Close();
                            if (verificarquestao1 == "")
                            {

                            }
                            else
                            {
                                string numero_questao1 = ("Questão-" + numero_pergunta).ToString();
                                conexao.Open();
                                comando.CommandText = "Select Questao From Questionario_Pergunta where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao1 + "'";
                                TextBoxPergunta.Text = Convert.ToString(comando.ExecuteScalar());
                                comando.CommandText = "Select Tipo_Resposta From Questionario_Pergunta where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao1 + "'";
                                DropDownListTipoPergunta.Text = Convert.ToString(comando.ExecuteScalar());
                                conexao.Close();

                                if (DropDownListTipoPergunta.Text == "Adicionar Manualmente")
                                {

                                    ListBoxOpcoesResposta.Visible = true;
                                    TextBoxAdicionarResposta.Visible = true;
                                    ButtonAddResp.Visible = true;
                                    ButtonRemoveResp.Visible = true;

                                    conexao.Open();
                                    comando.CommandText = "Select Nome_Resposta from TipoResposta Where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao1 + "'";
                                    dataReader = comando.ExecuteReader();

                                    if (dataReader.HasRows)
                                    {
                                        ListBoxOpcoesResposta.Items.Clear();
                                        while (dataReader.Read())
                                        {
                                            ListBoxOpcoesResposta.Items.Add(dataReader[0].ToString());
                                        }
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Não existem valores adicionados!');", true);
                                    }

                                    conexao.Close();
                                }
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, adicione mais opções de resposta!');", true);
                            ViewState["count"] = numero_pergunta;
                            labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                            ListBoxOpcoesResposta.Visible = true;
                            ButtonAddResp.Visible = true;
                            ButtonRemoveResp.Visible = true;
                            TextBoxAdicionarResposta.Visible = true;
                        }

                    }
                    else
                    {
                        conexao.Open();
                        comando.CommandText = "UPDATE QUESTIONARIO_PERGUNTA SET QUESTAO = '" + TextBoxPergunta.Text + "', TIPO_RESPOSTA = '" + DropDownListTipoPergunta.Text + "' where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao + "'";
                        comando.ExecuteNonQuery();
                        comando.CommandText = "Delete from TipoResposta Where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao + "'";
                        comando.ExecuteNonQuery();
                        conexao.Close();

                        //Limpar Pergunta anterior para passar para a proxima
                        TextBoxPergunta.Text = "";
                        DropDownListTipoPergunta.ClearSelection();


                        // PASSAR À PERGUNTA SEGUINTE


                        //Incrementar o numero da pergunta
                        numero_pergunta++;
                        ViewState["count"] = numero_pergunta;
                        labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();

                        string verificarquestao1;
                        string proximaquestao = ("Questão-" + numero_pergunta).ToString();

                        conexao.Open();
                        comando.CommandText = "select questao from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + proximaquestao + "' and ID_QUESTIONARIO = '" + novoID + "'";
                        verificarquestao1 = Convert.ToString(comando.ExecuteScalar());
                        conexao.Close();
                        if (verificarquestao1 == "")
                        {

                        }
                        else
                        {
                            string numero_questao1 = ("Questão-" + numero_pergunta).ToString();
                            conexao.Open();
                            comando.CommandText = "Select Questao From Questionario_Pergunta where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao1 + "'";
                            TextBoxPergunta.Text = Convert.ToString(comando.ExecuteScalar());
                            comando.CommandText = "Select Tipo_Resposta From Questionario_Pergunta where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao1 + "'";
                            DropDownListTipoPergunta.Text = Convert.ToString(comando.ExecuteScalar());
                            conexao.Close();

                            if (DropDownListTipoPergunta.Text == "Adicionar Manualmente")
                            {

                                ListBoxOpcoesResposta.Visible = true;
                                TextBoxAdicionarResposta.Visible = true;
                                ButtonAddResp.Visible = true;
                                ButtonRemoveResp.Visible = true;

                                conexao.Open();
                                comando.CommandText = "Select Nome_Resposta from TipoResposta Where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao1 + "'";
                                dataReader = comando.ExecuteReader();

                                if (dataReader.HasRows)
                                {
                                    ListBoxOpcoesResposta.Items.Clear();
                                    while (dataReader.Read())
                                    {
                                        ListBoxOpcoesResposta.Items.Add(dataReader[0].ToString());
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Não existem valores adicionados!');", true);
                                }

                                conexao.Close();
                            }
                        }
                    }              
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
                    ViewState["count"] = numero_pergunta;
                    labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();
                }

                LabelConfirmacao.Visible = true;
                LabelConfirmacao.Text = "Questão-" + (numero_pergunta - 1) + " alterada e submetida com sucesso, para terminar o questionário apenas saia deste!";
            }        
        }

        protected void ButtonEditarQuestao_Click(object sender, EventArgs e)
        {
            //Colocar Panel visiveis e invisiveis
            panelDoente.Visible = false;
            panelMedico.Visible = false;
            panelInfo_Socio.Visible = false;
            panelQuestionario.Visible = true;
         
            numero_pergunta--;
            ViewState["count"] = numero_pergunta;
            labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();

            string numero_questao = ("Questão-" + numero_pergunta).ToString();
            conexao.Open();
            comando.CommandText = "Select Questao From Questionario_Pergunta where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao + "'";
            TextBoxPergunta.Text = Convert.ToString(comando.ExecuteScalar());
            comando.CommandText = "Select Tipo_Resposta From Questionario_Pergunta where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao + "'";
            DropDownListTipoPergunta.Text = Convert.ToString(comando.ExecuteScalar());
            conexao.Close();

            if (DropDownListTipoPergunta.Text == "Adicionar Manualmente")
            {

                ListBoxOpcoesResposta.Visible = true;
                TextBoxAdicionarResposta.Visible = true;
                ButtonAddResp.Visible = true;
                ButtonRemoveResp.Visible = true;

                conexao.Open();
                comando.CommandText = "Select Nome_Resposta from TipoResposta Where ID_QUESTIONARIO = '" + novoID + "' and NUMERO_QUESTAO = '" + numero_questao + "'";
                dataReader = comando.ExecuteReader();

                if (dataReader.HasRows)
                {
                    ListBoxOpcoesResposta.Items.Clear();
                    while (dataReader.Read())
                    {
                        ListBoxOpcoesResposta.Items.Add(dataReader[0].ToString());
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Não existem valores adicionados!');", true);
                }

                conexao.Close();
            }
            else
            {
                ListBoxOpcoesResposta.Items.Clear();
            }

            if (numero_pergunta == 1)
            {
                ButtonEditarQuestao.Visible = false;
            }
            else
            {
                ButtonEditarQuestao.Visible = true;
            }


            //Tentar fazer com que nao passe para antes da questao1
        }

        protected void DropDownListTipoPergunta_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(DropDownListTipoPergunta.SelectedIndex == 4)
            {
                //Acrescentar as coisas à drop aqui
                panelDoente.Visible = false;
                panelMedico.Visible = false;
                panelInfo_Socio.Visible = false;
                panelQuestionario.Visible = true;
                ViewState["count"] = numero_pergunta;
                labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();

                ListBoxOpcoesResposta.Visible = true;
                TextBoxAdicionarResposta.Visible = true;
                ButtonAddResp.Visible = true;
                ButtonRemoveResp.Visible = true;
            }
            else
            {
                panelDoente.Visible = false;
                panelMedico.Visible = false;
                panelInfo_Socio.Visible = false;
                panelQuestionario.Visible = true;
                ViewState["count"] = numero_pergunta;
                labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();

                ListBoxOpcoesResposta.Visible = false;
                TextBoxAdicionarResposta.Visible = false;
                ButtonAddResp.Visible = false;
                ButtonRemoveResp.Visible = false;
                ListBoxOpcoesResposta.Items.Clear();
                TextBoxAdicionarResposta.Text = "";
            }
        }

        protected void ButtonAddResp_Click(object sender, EventArgs e)
        {
            panelQuestionario.Visible = true;
            ListBoxOpcoesResposta.Visible = true;
            TextBoxAdicionarResposta.Visible = true;
            ButtonAddResp.Visible = true;
            ButtonRemoveResp.Visible = true;

            ListBoxOpcoesResposta.Items.Add(TextBoxAdicionarResposta.Text);
            ViewState["count"] = numero_pergunta;
            labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();

            //Limpar textbox
            TextBoxAdicionarResposta.Text = "";
        }

        protected void ButtonRemoveResp_Click(object sender, EventArgs e)
        {
            panelQuestionario.Visible = true;
            ListBoxOpcoesResposta.Visible = true;
            TextBoxAdicionarResposta.Visible = true;
            ButtonAddResp.Visible = true;
            ButtonRemoveResp.Visible = true;
            ViewState["count"] = numero_pergunta;
            labelQuestionario.Text = ("Questão-" + numero_pergunta).ToString();

            try
            {
                int index;
                index = ListBoxOpcoesResposta.SelectedIndex;
                ListBoxOpcoesResposta.Items.RemoveAt(index);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, selecione um item para remover');", true);
            }
        }
    }
}