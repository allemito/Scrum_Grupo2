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
    public partial class Login : System.Web.UI.Page
    {
        // Ligação base dados oracle
        OracleConnection conexao = new OracleConnection("DATA SOURCE=25.15.145.193:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
        }
   
        protected void btn_login_account_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "SELECT username_direcao FROM direcao WHERE USERNAME_DIRECAO = '" + txtbox_username.Text + "'";
            comando.ExecuteNonQuery();
            string username = Convert.ToString(comando.ExecuteScalar());

            if (txtbox_username.Text == username && txtbox_username.Text != "")
            {
                comando.CommandText = "SELECT Password_Direcao FROM Direcao where USERNAME_DIRECAO = '"+username+"'";
                comando.ExecuteNonQuery();
                string password = Convert.ToString(comando.ExecuteScalar());
                if (txtbox_password.Text == password && txtbox_password.Text != "")
                {
                    Response.Redirect("~/html/Administrador.aspx");
                    conexao.Close();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Utilizador ou Password Incorretos!');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Utilizador ou Password Incorretos!');", true);
            }
        }
    }
}