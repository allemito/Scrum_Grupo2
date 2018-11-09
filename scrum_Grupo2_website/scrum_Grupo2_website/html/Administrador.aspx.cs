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

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
            panelDoente.Visible = false;
            panelMedico.Visible = false;
        }

        protected void ButtonProcurar_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "SELECT Numero_Utente FROM Doente WHERE Numero_Utente = '"+TextBox_Procurar.Text+"'";
            comando.ExecuteNonQuery();
            string IdProcurarDoente = Convert.ToString(comando.ExecuteScalar());

            if (TextBox_Procurar.Text == IdProcurarDoente && TextBox_Procurar.Text != "")
            {
                panelDoente.Visible = true;
                panelMedico.Visible = false;

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
           
                    comando.CommandText = "SELECT Nome_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                    TextBox_Nome_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Morada_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                    TextBox_Morada_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Numero_Cedula FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                    txtbox_cedula.Text= Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Email_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                    TextBox_Email_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Contribuinte_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                    TextBox_Contribuinte_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Genero_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                    DropDownList_Sexo_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.CommandText = "SELECT Data_Nascimento_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                    TextBoxNascimento_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                    comando.ExecuteNonQuery();
                }
                else
                {
                    labelProcurar.Text = "Não encontrado!";
                }
            }
        }

        protected void ButtonEditar_Medico_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "UPDATE Medico set nome_medico = '"+TextBox_Nome_Medico.Text+"', CONTRIBUINTE_MEDICO = 199111600 where ID_MEDICO";
            comando.ExecuteNonQuery();
            conexao.Close();
            TextBox_Procurar.Text = "";
        }

        protected void ButtonRemover_Medico_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "DELETE FROM Medico where Numero_Cedula = '"+TextBox_Procurar.Text+"';";
            comando.ExecuteNonQuery();
            conexao.Close();
            TextBox_Procurar.Text = "";
        }

        protected void ButtonEditar_Doente_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "";
            comando.ExecuteNonQuery();
            conexao.Close();
            TextBox_Procurar.Text = "";
        }

        protected void ButtonRemover_Doente_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "DELETE FROM Doente where Numero_Utente = '" + TextBox_Procurar.Text + "'";
            comando.ExecuteNonQuery();
            conexao.Close();
            TextBox_Procurar.Text = "";
        }
    }
}