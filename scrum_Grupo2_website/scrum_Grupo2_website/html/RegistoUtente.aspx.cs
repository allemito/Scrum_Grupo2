using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.Mail;

namespace scrum_Grupo2_website.html
{
    public partial class RegistoUtente1 : System.Web.UI.Page
    {
        // Ligação Base Dados Oracle
        OracleConnection conexao = new OracleConnection("DATA SOURCE=25.15.145.193:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;

        //Aceder Classe Registo
        Registo registo = new Registo();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
        }

        // Botão de registo de um doente
        protected void btn_registar_Click(object sender, EventArgs e)
        {
            if (txtbox_nome.Text != ""  & TextBox_morada.Text != "" & TextBox_numeroutente.Text != "" & DropDownList_Sexo.Text != "" & Calendar_datanascimento.SelectedDate != null)
            {
                if (DateTime.Now > Calendar_datanascimento.SelectedDate)
                {
                    if(registo.verificarEmail(txtbox_email.Text) == true)
                    {
                        // Criação Password Aleatoria
                        string novaPassword = registo.CreatePassword(10);

                        // Calcular data de nascimento para inserir na base de dados
                        string dataNascimento = registo.CriarNascimentoDataBase(Calendar_datanascimento.SelectedDate.Year.ToString(), Calendar_datanascimento.SelectedDate.Month.ToString(), Calendar_datanascimento.SelectedDate.Day.ToString());

                        // Calcular ID incremental
                        int novoID;
                        try
                        {
                            conexao.Open();
                            comando.CommandText = "SELECT MAX(ID_Doente)+1 from Doente";
                            comando.ExecuteNonQuery();
                            novoID = Convert.ToInt32(comando.ExecuteScalar());
                        }
                        catch (System.InvalidCastException)
                        {
                            novoID = 1;
                        }

                        comando.CommandText = "INSERT INTO Doente(ID_Doente, Nome_Doente, Data_Nascimento_Doente, Morada, Numero_Utente, Genero, Password_Doente, Email_Doente) VALUES ('" + novoID + "','" + txtbox_nome.Text + "','" + dataNascimento + "', '" + TextBox_morada.Text + "', '" + TextBox_numeroutente.Text + "', '" + DropDownList_Sexo.Text + "', '" + novaPassword + "', '" + txtbox_email.Text + "')";
                        comando.ExecuteNonQuery();
                        conexao.Close();

                        // Enviar Email de Registo
                        registo.EnviarEmail(txtbox_email.Text, txtbox_nome.Text, novaPassword);

                        // Limpar as textbox apos registo de doente
                        txtbox_nome.Text = "";
                        txtbox_email.Text = "";
                        TextBox_morada.Text = "";
                        TextBox_numeroutente.Text = "";
                        Calendar_datanascimento.SelectedDates.Clear();
                        DropDownList_Sexo.ClearSelection();
                   
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Registo Conluido com sucesso!');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Email inválido, por favor verifique novamente!');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Data de nascimento inválida, por favor verifique novamente!');", true);
                }                    
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
            }
        }
    }
}