using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace scrum_Grupo2_website.html
{
    public partial class RegistoUtente1 : System.Web.UI.Page
    {
        //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('A tua prima');", true);
        OracleConnection conexao = new OracleConnection("DATA SOURCE=localhost:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;
        public string id_number;
        public int i;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
        }

        protected void btn_registar_Click(object sender, EventArgs e)
        {
            if (txtbox_pass.Text == txtbox_repass.Text)
            {

                if (txtbox_nome.Text != "" & txtbox_email.Text != "" & TextBox_morada.Text != "" & TextBox_numeroutente.Text != "" & DropDownList_Sexo.Text != "" & Calendar_datanascimento.SelectedDate != null)
                {
                    conexao.Open();
                    comando.CommandText = "INSERT INTO Doente(ID_Doente, Nome_Doente, Morada, Numero_Utente, Genero, Password_Doente) VALUES ('" + 1234 + "', '" + txtbox_nome.Text + "', '" + TextBox_morada.Text + "', '" + TextBox_numeroutente.Text + "', '" + DropDownList_Sexo.Text + "', '" + txtbox_repass.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    txtbox_nome.Text = "";
                    txtbox_email.Text = "";
                    txtbox_pass.Text = "";
                    txtbox_repass.Text = "";
                    TextBox_morada.Text = "";
                    TextBox_numeroutente.Text = "";
                    Calendar_datanascimento.SelectedDates.Clear();
                    DropDownList_Sexo.ClearSelection();
                }
                else
                {
                    
                }
            }
            else
            {

            }
        }
    }
}