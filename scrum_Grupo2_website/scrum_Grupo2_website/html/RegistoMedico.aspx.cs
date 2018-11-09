using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Text;

namespace scrum_Grupo2_website.html
{
    public partial class RegistoMedico1 : System.Web.UI.Page
    {
        // Ligação base dados oracle
        OracleConnection conexao = new OracleConnection("DATA SOURCE=25.15.145.193:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;

        // Acdeder à Classe Registo
        Registo registo = new Registo();

        protected void Page_Load(object sender, EventArgs e)
        {       
            comando.Connection = conexao;
        }
 
        protected void btn_registar_Click(object sender, EventArgs e)
        {
            if (txtbox_nome.Text != "" & TextBox_morada.Text != "" & TextBox_Contribuinte.Text != "" & DropDownList_Sexo.Text != "" & Calendar_datanascimento.SelectedDate != null & txtbox_cedula.Text != "")
            {
                if (DateTime.Now > Calendar_datanascimento.SelectedDate)
                {
                    if (registo.verificarEmail(txtbox_email.Text) == true)
                    {
                        // Gerar password Aleatoria 
                        string novaPassword = registo.CreatePassword(10);

                        // Calcular data nascimento para inserir na base de dados
                        string dataNascimento = registo.CriarNascimentoDataBase(Calendar_datanascimento.SelectedDate.Year.ToString(), Calendar_datanascimento.SelectedDate.Month.ToString(), Calendar_datanascimento.SelectedDate.Day.ToString());

                        // Criar ID para medico incremental
                        int novoID;
                        try
                        {
                            conexao.Open();
                            comando.CommandText = "SELECT MAX(ID_Medico)+1 from Medico";
                            comando.ExecuteNonQuery();
                            novoID = Convert.ToInt32(comando.ExecuteScalar());
                        }
                        catch (System.InvalidCastException)
                        {
                            novoID = 1;
                        }

                        // Inserir dados na Base de Dados
                        comando.CommandText = "INSERT INTO Medico(ID_Medico, Nome_Medico, Data_Nascimento_Medico, Morada_Medico, Numero_Cedula, Genero_Medico, Contribuinte_Medico, Email_Medico, Password_Medico) VALUES ('" + novoID + "','" + txtbox_nome.Text + "','" + dataNascimento + "', '" + TextBox_morada.Text + "', '" + txtbox_cedula.Text + "', '" + DropDownList_Sexo.Text + "', '" + TextBox_Contribuinte.Text + "', '" + txtbox_email.Text + "', '" + novaPassword + "')";
                        comando.ExecuteNonQuery();
                        conexao.Close();

                        // Enviar Email de Registo
                        registo.EnviarEmail(txtbox_email.Text, txtbox_nome.Text, novaPassword);

                        // Limpar Textbox apos registo do médico
                        txtbox_nome.Text = "";
                        txtbox_email.Text = "";
                        TextBox_morada.Text = "";
                        TextBox_Contribuinte.Text = "";
                        txtbox_cedula.Text = "";
                        Calendar_datanascimento.SelectedDates.Clear();
                        DropDownList_Sexo.ClearSelection();

                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Registo Concluido com sucesso!');", true);
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
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos');", true);
            }
       }          
    }
}