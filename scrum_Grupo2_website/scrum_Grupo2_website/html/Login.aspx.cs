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
        string username;

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
        }

        protected void btn_login_account_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "SELECT username_direcao FROM direcao WHERE USERNAME_DIRECAO = '" + txtbox_username.Text + "'";
            comando.ExecuteNonQuery();
            string usernameAdministrador = Convert.ToString(comando.ExecuteScalar());

            if(txtbox_username.Text == usernameAdministrador && txtbox_username.Text != "")
            {
                comando.CommandText = "SELECT Password_Direcao FROM Direcao where USERNAME_DIRECAO = '" + usernameAdministrador + "'";
                comando.ExecuteNonQuery();
                string passwordAdministrador = Convert.ToString(comando.ExecuteScalar());
                if (txtbox_password.Text == passwordAdministrador && txtbox_password.Text != "")
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
                comando.CommandText = "SELECT Numero_Cedula FROM Medico WHERE Numero_Cedula = '" + txtbox_username.Text + "'";
                comando.ExecuteNonQuery();
                string userMedico = Convert.ToString(comando.ExecuteScalar());

                if (txtbox_username.Text == userMedico && txtbox_username.Text != "")
                {
                    comando.CommandText = "SELECT Password_Medico FROM Medico where Numero_Cedula = '" + userMedico + "'";
                    comando.ExecuteNonQuery();
                    string passwordMedico = Convert.ToString(comando.ExecuteScalar());
                    if (txtbox_password.Text == passwordMedico && txtbox_password.Text != "")
                    {
                        Response.Redirect("~/html/Medico.aspx");
                        conexao.Close();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Utilizador ou Password Incorretos!');", true);
                    }           
                }
                else
                {
                    comando.CommandText = "SELECT Numero_Utente FROM Doente WHERE Numero_Utente = '" + txtbox_username.Text + "'";
                    comando.ExecuteNonQuery();
                    string userDoente = Convert.ToString(comando.ExecuteScalar());

                    if (txtbox_username.Text == userDoente && txtbox_username.Text != "")
                    {
                        comando.CommandText = "SELECT Password_Doente FROM Doente where Numero_Utente = '" + userDoente + "'";
                        comando.ExecuteNonQuery();
                        string passwordDoente = Convert.ToString(comando.ExecuteScalar());
                        if (txtbox_password.Text == passwordDoente && txtbox_password.Text != "")
                        {
                            comando.CommandText = "Select ID_Doente from Doente WHERE Numero_Utente = '"+userDoente+"'";
                            username = Convert.ToString(comando.ExecuteScalar());
                            Session["username"] = username;
                            Response.Redirect("~/html/Doente.aspx");
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
                        conexao.Close();
                    }
                }            
            } 
        }
    }
}