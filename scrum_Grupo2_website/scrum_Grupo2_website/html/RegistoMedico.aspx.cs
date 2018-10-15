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
        OracleConnection conexao = new OracleConnection("DATA SOURCE=localhost:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
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
            if (txtbox_nome.Text != "" & txtbox_email.Text != "" & TextBox_morada.Text != "" & TextBox_Contribuinte.Text != "" & DropDownList_Sexo.Text != "" & Calendar_datanascimento.SelectedDate != null & txtbox_cedula.Text != "")
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

                comando.CommandText = "INSERT INTO Medico(ID_Medico, Nome_Medico, Data_Nascimento_Medico, Morada_Medico, Numero_Cedula, Genero_Medico, Contribuinte_Medico, Email_Medico, Password_Medico) VALUES ('" + novoID + "','" + txtbox_nome.Text + "','" + dataNascimento + "', '" + TextBox_morada.Text + "', '" + txtbox_cedula.Text + "', '" + DropDownList_Sexo.Text + "', '" + TextBox_Contribuinte.Text + "', '" + txtbox_email.Text + "', '" + novaPassword + "')";
                comando.ExecuteNonQuery();
                conexao.Close();

                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Senha do Médico: " + novaPassword + "');", true);

                txtbox_nome.Text = "";
                txtbox_email.Text = "";
                TextBox_morada.Text = "";
                TextBox_Contribuinte.Text = "";
                txtbox_cedula.Text = "";
                Calendar_datanascimento.SelectedDates.Clear(); 
                DropDownList_Sexo.ClearSelection();

                }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Preencha todos os campos por favor!');", true);
            }
       }
           
    }
}