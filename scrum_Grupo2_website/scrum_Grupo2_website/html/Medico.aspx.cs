using System;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
            panelDoente.Visible = false;
            panelExcel.Visible = false;
            ButtonAdicionarInfo.Visible = false;
            label_Vacina_Falta.Visible = false;
            Button_Falta.Visible = false;
            ddl_vacine.Visible = false; ;
            label_Falta.Visible = false;
            labelProcurar.Text = "";
        }

        protected void ButtonProcurar_Click(object sender, EventArgs e)
        {
            if(registo.verificarNumero(TextBox_Procurar.Text ) == true)
            {
                //Limpar Dados anteriores
                label_Falta.Text = "";
                labelIMC.Text = "";

                //Executar Query de procura
                conexao.Open();
                comando.CommandText = "SELECT Numero_Utente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                comando.ExecuteNonQuery();
                string IdProcurarDoente = Convert.ToString(comando.ExecuteScalar());

                if (TextBox_Procurar.Text == IdProcurarDoente && TextBox_Procurar.Text != "")
                {
                    panelDoente.Visible = true;

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

        }
    }
}