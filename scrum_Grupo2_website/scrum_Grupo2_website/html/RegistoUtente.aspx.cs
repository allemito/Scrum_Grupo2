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

namespace scrum_Grupo2_website.html
{
    public partial class RegistoUtente1 : System.Web.UI.Page
    {
        OracleConnection conexao = new OracleConnection("DATA SOURCE=localhost:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;
        public string id_number;
        public int i;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
        }

       protected bool verificarEmail(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([.-]?[a-zA-Z0-9]+))@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)).([A-Za-z]{2,})$");

            if (rg.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string CreatePassword(int lenght)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder password = new StringBuilder();
            Random aleatorio = new Random();
            while (0 < lenght--)
            {
                password.Append(valid[aleatorio.Next(valid.Length)]);
            }
            return password.ToString();
        }

        protected void btn_registar_Click(object sender, EventArgs e)
        {
            if (txtbox_nome.Text != ""  & verificarEmail(txtbox_email.Text) == true & TextBox_morada.Text != "" & TextBox_numeroutente.Text != "" & DropDownList_Sexo.Text != "" & Calendar_datanascimento.SelectedDate != null)
            {
                int novoID;
                string ano;
                string mes;
                string dia;
                string dataNascimento;
                string novaPassword = CreatePassword(10);

                ano = Calendar_datanascimento.SelectedDate.Year.ToString();
                mes = Calendar_datanascimento.SelectedDate.Month.ToString();
                dia = Calendar_datanascimento.SelectedDate.Day.ToString();
                dataNascimento = ano + "." + mes + "." + dia;
                
                if (DateTime.Now > Calendar_datanascimento.SelectedDate)
                {
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
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Data mal!');", true);
                }
                       
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Preencha todos os campos por favor!');", true);
            }
        }
    }
}