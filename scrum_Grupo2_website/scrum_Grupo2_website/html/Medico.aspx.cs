﻿using System;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;
//using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Data.SqlClient;

namespace scrum_Grupo2_website
{
    public partial class Medico : System.Web.UI.Page
    {
        // Ligação base dados oracle
        OracleConnection conexao = new OracleConnection("DATA SOURCE=25.15.145.193:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;
        Registo registo = new Registo();
        string iddoente;
        int numero_pergunta = 1;
        int novoID = 0;
        string questionario_selectionado;

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;

            panelDoente.Visible = false;
            panelExcel.Visible = false;
            panelBotoes.Visible = false;
            panelDadosBiometricos.Visible = false;
            panelQuestionario.Visible = false;
            panel_QuestionarioOpcoes.Visible = false;

            LabelConfirmacao.Visible = false;
            ButtonAdicionarInfo.Visible = false;
            label_Vacina_Falta.Visible = false;
            Button_Falta.Visible = false;
            ddl_vacine.Visible = false; ;
            label_Falta.Visible = false;
            labelProcurar.Text = "";
            ListBox_valores.Visible = false;
            Label_verificacao_valores.Visible = false;


            ListBoxOpcoesResposta.Visible = false;
            TextBoxAdicionarResposta.Visible = false;
            ButtonAddResp.Visible = false;
            ButtonRemoveResp.Visible = false;

            labelQuestionario.Text = "Questão-" + numero_pergunta;

            if (IsPostBack)
            {
                if (ViewState["iddoente"] != null)
                {
                    iddoente = (string)ViewState["iddoente"];
                }
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
            if(registo.verificarNumero(TextBox_Procurar.Text ) == true)
            {
                //Executar Query de procura
                conexao.Open();
                comando.CommandText = "SELECT Numero_Utente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                comando.ExecuteNonQuery();
                string IdProcurarDoente = Convert.ToString(comando.ExecuteScalar());

                if (TextBox_Procurar.Text == IdProcurarDoente && TextBox_Procurar.Text != "")
                {
                    panelBotoes.Visible = true;
                    ViewState["iddoente"] = TextBox_Procurar.Text;
                    comando.CommandText = "SELECT Nome_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                    TextBox_Nome.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.ExecuteNonQuery();
                    conexao.Close();
                }
                else
                {
                    labelProcurar.Text = "Não encontrado!";
                }
            }
            else
            {
                labelProcurar.Text = "Introduza um número válido!";
            }
        }

        protected void ButtonIMC_Click(object sender, EventArgs e)
        {
            panelDoente.Visible = true;
            labelIMC.Text = "IMC = " + registo.CalcularIMC(TextBox_Altura.Text, TextBox_Peso.Text);

            if(DropDownListVacinas.Text == "Em Falta")
            {
                label_Vacina_Falta.Visible = true;
                Button_Falta.Visible = true;
                ddl_vacine.Visible = true;
                label_Falta.Visible = true;
            }
            else
            {
                label_Vacina_Falta.Visible = false;
                Button_Falta.Visible = false;
                ddl_vacine.Visible = false; ;
                label_Falta.Visible = false; 
            }
        }

        protected void Button_Falta_Click(object sender, EventArgs e)
        {
            panelDoente.Visible = true;
            if (label_Falta.Text == "")
            {
                label_Falta.Text = ddl_vacine.Text;
                label_Vacina_Falta.Visible = true;
                Button_Falta.Visible = true;
                ddl_vacine.Visible = true;
                label_Falta.Visible = true;
            }
            else
            {
                label_Falta.Text = label_Falta.Text + ", " + ddl_vacine.Text;
                label_Vacina_Falta.Visible = true;
                Button_Falta.Visible = true;
                ddl_vacine.Visible = true;
                label_Falta.Visible = true;         
            }
            
        }

        protected void Button_Adicionar_Click(object sender, EventArgs e)
        {
            if(TextBox_PArterial.Text != "" && TextBox_Altura.Text != "" && TextBox_Peso.Text != "" && labelIMC.Text != "")
            {
                string IMC = registo.CalcularIMC(TextBox_Altura.Text, TextBox_Peso.Text);
                panelDoente.Visible = true;
                try
                {
                    conexao.Open();
                    comando.CommandText = "INSERT INTO INFOCLINICA(ID_Doente, Peso, Altura, IMC, Pressao_Arterial, Vacinas, Vacinas_Falta) VALUES ((SELECT ID_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'), '" + TextBox_Peso.Text + "', '" + TextBox_Altura.Text + "', '" + IMC + "', '" + TextBox_PArterial.Text + "', '" + DropDownListVacinas.Text + "', '" + label_Falta.Text + "')";
                    //comando.CommandText = "INSERT INTO INFOCLINICA(ID_Doente, Peso, Altura, IMC) VALUES (1, '"+TextBox_Peso.Text+"','"+TextBox_Altura.Text+"', '"+IMC+"')";
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    if (label_Falta.Text != "")
                    {
                        label_Vacina_Falta.Visible = true;
                        Button_Falta.Visible = true;
                        ddl_vacine.Visible = true;
                        label_Falta.Visible = true;
                    }
                    else
                    {
                        label_Vacina_Falta.Visible = false;
                        Button_Falta.Visible = false;
                        ddl_vacine.Visible = false;
                        label_Falta.Visible = false;
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Informação Registada com sucesso!');", true);

                    //Limpar dados
                    TextBox_Peso.Text = "";
                    TextBox_Altura.Text = "";
                    TextBox_Nome.Text = "";
                    TextBox_PArterial.Text = "";
                    DropDownListVacinas.ClearSelection();
                    ddl_vacine.ClearSelection();
                    label_Falta.Text = "";
                    labelIMC.Text = "";
                    TextBox_Procurar.Text = "";
                }
                catch (Oracle.ManagedDataAccess.Client.OracleException)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Utente com dados já inseridos!');", true);
                }    
            }
            else
            {
                panelDoente.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
            }
        }

        protected void ButtonRecolha_Click(object sender, EventArgs e)
        {
            panelExcel.Visible = true;
            panelDoente.Visible = false;
            ButtonAdicionarInfo.Visible = false;
            ButtonExcel.Visible = true;

            GridView1.DataSource = null;
            GridView1.Visible = false;

        }

        protected void ButtonExcel_Click(object sender, EventArgs e)
        {
            panelExcel.Visible = true;
            panelDoente.Visible = false;

            //apanhar o path que se pos no web.config
            string FilePath = ConfigurationManager.AppSettings["FilePath"].ToString();
            string FileName = string.Empty;

            //verificar se o ficheiro esta selecionado ou nao 
            if (FileUpload1.HasFile)
            {
                try
                {
                    string[] allowdFile = { ".xlsx" };

                    //verificar se e outro tipo de ficheiro 
                    string FileExt = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

                    //verificar se o ficheiro selecionado tem uma extensao valida
                    bool isValidFile = allowdFile.Contains(FileExt);

                    if (!isValidFile)
                    {
                        LabelTexto.ForeColor = System.Drawing.Color.Red;
                        LabelTexto.Text = "Por Favor selecione um ficheiro excel!";
                    }
                    else
                    {

                        //descobrir o tamanho do ficheiro selecionado 
                        int FileSize = FileUpload1.PostedFile.ContentLength;
                        if (FileSize <= 1048576) // 1048576 byte = 1MB
                        {
                            //apanhar o nome do ficheiro selecionado
                            FileName = Path.GetFileName(Server.MapPath(FileUpload1.FileName));

                            //salvar o ficheiro selecionado no servidor local
                            FileUpload1.SaveAs(Server.MapPath(FilePath) + FileName);

                            //apanhar o filepath
                            string filePath = Server.MapPath(FilePath) + FileName;

                            //abrir a conexao com o excel
                            OleDbConnection con = null;

                            if (FileExt == ".xlsx")
                            {
                                con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties= Excel 8.0;");
                            }

                            con.Open();
                            //extrair a lista do sheet do excel
                            System.Data.DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                            //extrair o nome do primeiro sheet
                            string getExcelSheetName = dt.Rows[0]["Table_Name"].ToString();

                            //selecionar as rows do primeiro sheet do excel e mandar para o dataset
                            OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT * FROM [" + getExcelSheetName + @"]", con);
                            OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                            DataSet ExcelDataSet = new DataSet();

                            ExcelAdapter.Fill(ExcelDataSet);
                            con.Close();

                            //por o dataset na tabela 
                            GridView1.DataSource = ExcelDataSet;
                            GridView1.DataBind();

                            //Esconder buttons
                            ButtonAdicionarInfo.Visible = true;
                            ButtonExcel.Visible = false;
                            LabelTexto.Text = "";
                            GridView1.Visible = true;

                        }
                        else
                        {
                            LabelTexto.ForeColor = System.Drawing.Color.Red;
                            LabelTexto.Text = "	Tamanho do ficheiro selecionado superior a 1MB!";
                        }


                    }
                }
                catch (Exception ex)
                {
                    LabelTexto.Text = "Erro: " + ex.Message;
                }
            }
            else
            {
                LabelTexto.Text = "Por favor selecione um ficheiro!";
            }
        }

        protected void ButtonAdicionarInfo_Click(object sender, EventArgs e)
        {
            panelExcel.Visible = true;
            panelDoente.Visible = false;
            ButtonExcel.Visible = true;
            ButtonAdicionarInfo.Visible = false;
            for (int i = 0; i < GridView1.Rows.Count - 1; i++)
            {
                conexao.Open();
                comando.CommandText = "INSERT INTO Dados_Biometricos(ID_Doente, Valor_Biometrico, ID_Dado)VALUES('" + Convert.ToInt32(GridView1.Rows[i].Cells[0].Text) + "', '" + Convert.ToDecimal(GridView1.Rows[i].Cells[1].Text) + "', '"+(i + 1)+"')";
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        protected void btn_Questionario_Click(object sender, EventArgs e)
        {
            panelDoente.Visible = false;
            panelBotoes.Visible = false;
            panelDadosBiometricos.Visible = false;
            panelExcel.Visible = false;
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

        protected void Button_dados_Click(object sender, EventArgs e)
        {
            panelDadosBiometricos.Visible = true;
            conexao.Open();
            comando.CommandText = "SELECT Nome_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
            TextBoxNome1.Text = Convert.ToString(comando.ExecuteScalar());
            comando.ExecuteNonQuery();
            comando.CommandText = "select VALOR_BIOMETRICO from DADOS_BIOMETRICOS inner join DOENTE on doente.id_doente = dados_biometricos.id_doente where doente.numero_utente = '" + iddoente + "'";
            dataReader = comando.ExecuteReader();

            ListBox_valores.Visible = true;
            if (dataReader.HasRows)
            {
                ListBox_valores.Items.Clear();
                while (dataReader.Read())
                {
                    ListBox_valores.Items.Add(dataReader[0].ToString());
                }
            }
            else
            {
                Label_verificacao_valores.Visible = true;
                Label_verificacao_valores.Text = "Não existem valores";
            }
            conexao.Close();
        }

        protected void ButtonInformacaoCli_Click(object sender, EventArgs e)
        {
            panelDoente.Visible = true;
            conexao.Open();
            comando.CommandText = "SELECT Nome_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
            TextBox_Nome.Text = Convert.ToString(comando.ExecuteScalar());
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        protected void ButtonVoltar1_Click(object sender, EventArgs e)
        {
            panelBotoes.Visible = true;

            //Limpar Dados anteriores
            label_Falta.Text = "";
            labelIMC.Text = "";
            TextBox_Altura.Text = "";
            TextBox_Nome.Text = "";
            TextBox_Peso.Text = "";
            TextBox_PArterial.Text = "";
            DropDownListVacinas.ClearSelection();
            ddl_vacine.ClearSelection();
        }

        protected void ButtonVoltar2_Click(object sender, EventArgs e)
        {
            panelBotoes.Visible = true;

            //Limpar Dados anteriores
            TextBoxNome1.Text = "";
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

        protected void ButtonEditarQuestao_Click(object sender, EventArgs e)
        {
            //Colocar Panel visiveis e invisiveis
            panelDoente.Visible = false;
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
            if (DropDownListTipoPergunta.SelectedIndex == 4)
            {
                //Acrescentar as coisas à drop aqui
                panelDoente.Visible = false;
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

        protected void ButtonAdicionarQuestionario_Click(object sender, EventArgs e)
        {
            //Colocar Panel visiveis e invisiveis
            panelDoente.Visible = false;
            panelQuestionario.Visible = true;
            ButtonEditarQuestao.Visible = true;

            string verificarquestao = "";
            string questao_editar = ("Questão-" + numero_pergunta).ToString();
            conexao.Open();
            comando.CommandText = "select questao from QUESTIONARIO_PERGUNTA where NUMERO_QUESTAO = '" + questao_editar + "' and ID_QUESTIONARIO = '" + novoID + "'";
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
                        comando.CommandText = "Select Nome_Questionario From Questionario Where Nome_Questionario = '"+TextBoxNomeQuestionario.Text+"'";
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

                        if (ListBoxOpcoesResposta.Items.Count >= 2)
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

        protected void ButtonAddQuestionario_Click(object sender, EventArgs e)
        {
            panelDoente.Visible = false;
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
    }
}