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
        Registo registo = new Registo();

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
            panelDoente.Visible = false;
            panelMedico.Visible = false;
            panelInfo_Socio.Visible = false;
            labelProcurar.Text = "";
        }

        protected void ButtonProcurar_Click(object sender, EventArgs e)
        {
            if (registo.verificarNumero(TextBox_Procurar.Text) == true)
            {
                conexao.Open();
                comando.CommandText = "SELECT Numero_Utente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
                comando.ExecuteNonQuery();
                string IdProcurarDoente = Convert.ToString(comando.ExecuteScalar());

                if (TextBox_Procurar.Text == IdProcurarDoente && TextBox_Procurar.Text != "")
                {
                    panelDoente.Visible = true;
                    panelMedico.Visible = false;
                    panelInfo_Socio.Visible = false;

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
                    conexao.Close();
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
                        panelInfo_Socio.Visible = false;

                        comando.CommandText = "SELECT Nome_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        TextBox_Nome_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Morada_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        TextBox_Morada_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Numero_Cedula FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        txtbox_cedula.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Email_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        TextBox_Email_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Contribuinte_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        TextBox_Contribuinte_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Genero_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        DropDownList_Sexo_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.CommandText = "SELECT Data_Nascimento_Medico FROM Medico WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                        TextBoxNascimento_Medico.Text = Convert.ToString(comando.ExecuteScalar());
                        comando.ExecuteNonQuery();
                        conexao.Close();
                    }
                    else
                    {
                        labelProcurar.Text = "Não encontrado!";
                    }
                }
            }
            else
            {
                labelProcurar.Text = "Introduza um número válido!";
            }
        }

        protected void ButtonEditar_Medico_Click(object sender, EventArgs e)
        {
            if (TextBox_Nome_Medico.Text != "" & TextBox_Morada_Medico.Text != "" & TextBox_Contribuinte_Medico.Text != "" & DropDownList_Sexo_Medico.Text != "" & txtbox_cedula.Text != "")
            {
                if (registo.verificarEmail(TextBox_Email_Medico.Text) == true)
                {
                    conexao.Open();
                    comando.CommandText = "UPDATE Medico set Nome_Medico = '" + TextBox_Nome_Medico.Text + "', Morada_Medico = '" + TextBox_Morada_Medico.Text + "', Contribuinte_Medico = '" + TextBox_Contribuinte_Medico.Text + "', Genero_Medico = '" + DropDownList_Sexo_Medico.Text + "', Email_Medico = '" + TextBox_Email_Medico.Text + "',  Numero_Cedula = '" + txtbox_cedula.Text + "' WHERE Numero_Cedula = '" + TextBox_Procurar.Text + "'";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Registo alterado com sucesso!');", true);
                    TextBox_Procurar.Text = "";

                    panelDoente.Visible = false;
                    panelMedico.Visible = true;
                    panelInfo_Socio.Visible = false;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Email inválido, por favor verifique novamente!');", true);
                    panelDoente.Visible = false;
                    panelMedico.Visible = true;
                    panelInfo_Socio.Visible = false;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
                panelDoente.Visible = false;
                panelMedico.Visible = true;
                panelInfo_Socio.Visible = false;
            }
        }

        protected void ButtonRemover_Medico_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "DELETE FROM Medico where Numero_Cedula = '"+ TextBox_Procurar.Text +"'";
            comando.ExecuteNonQuery();
            conexao.Close();
            TextBox_Procurar.Text = "";
        }

        protected void ButtonEditar_Doente_Click(object sender, EventArgs e)
        {
            if (txtbox_nome.Text != "" & TextBox_morada.Text != "" & TextBox_Utente.Text != "" & DropDownList_Sexo.Text != "")
            {
                if (registo.verificarEmail(txtbox_email.Text) == true)
                {
                    panelDoente.Visible = true;
                    panelMedico.Visible = false;
                    panelInfo_Socio.Visible = false;
                    conexao.Open();
                    comando.CommandText = "UPDATE Doente set Nome_Doente = '"+txtbox_nome.Text+"', Morada = '"+TextBox_morada.Text+"', Numero_Utente = '"+TextBox_Utente.Text+"', Genero = '"+DropDownList_Sexo.Text+"', Email_Doente = '"+txtbox_email.Text+"' WHERE Numero_Utente = '"+TextBox_Procurar.Text+"'";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Registo alterado com sucesso!');", true);
                    TextBox_Procurar.Text = "";
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Email inválido, por favor verifique novamente!');", true);
                    panelDoente.Visible = true;
                    panelMedico.Visible = false;
                    panelInfo_Socio.Visible = false;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
                panelDoente.Visible = true;
                panelMedico.Visible = false;
                panelInfo_Socio.Visible = false;
            }
        }

        protected void ButtonRemover_Doente_Click(object sender, EventArgs e)
        {
            conexao.Open();
            comando.CommandText = "DELETE FROM Doente where Numero_Utente = '" + TextBox_Procurar.Text + "'";
            comando.ExecuteNonQuery();
            conexao.Close();
            TextBox_Procurar.Text = "";
        }

        protected void ButtonInfo_Socio_Click(object sender, EventArgs e)
        {
            panelInfo_Socio.Visible = true;
            conexao.Open();
            comando.CommandText = "SELECT Nome_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
            TextBox_Nome_Socio.Text = Convert.ToString(comando.ExecuteScalar());
            comando.CommandText = "SELECT Morada FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
            TextBox_Morada_Socio.Text = Convert.ToString(comando.ExecuteScalar());
            comando.CommandText = "SELECT Data_Nascimento_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'";
            TextBox_DataNascimento_Socio.Text = Convert.ToString(comando.ExecuteScalar());
            comando.ExecuteNonQuery();
            conexao.Close();

        }

        protected void Button_Guardar_Socio_Click(object sender, EventArgs e)
        {
            if(TextBox_Nome_Socio.Text != "" && TextBox_DataNascimento_Socio.Text != "" && TextBox_Morada_Socio.Text != "" && TextBox_Nacionalidade_Socio.Text != "" && TextBox_Naturalidade_Socio.Text != "" && DropDownList_EstadoCivil_Socio.Text != "" && Textbox_NumeroFilhos_Socio.Text != "" && Textbox_Agregado_Socio.Text != "" && TextBox_Profissão_Socio.Text != "" && DropDownList_Escolaridade_Socio.Text != "")
            {
                panelInfo_Socio.Visible = true;
                panelMedico.Visible = false;
                panelDoente.Visible = false;
                try
                {
                    conexao.Open();
                    comando.CommandText = "INSERT INTO INFOSOCIO(ID_Doente, Nacionalidade, Naturalidade, Estado_Civil, Numero_Filhos, Agregado_Familiar, Profissao, Escolaridade) VALUES ((SELECT ID_Doente FROM Doente WHERE Numero_Utente = '" + TextBox_Procurar.Text + "'), '" + TextBox_Nacionalidade_Socio.Text + "', '" + TextBox_Naturalidade_Socio.Text + "', '" + DropDownList_EstadoCivil_Socio.Text + "', '" + Textbox_NumeroFilhos_Socio.Text + "', '" + Textbox_Agregado_Socio.Text + "', '" + TextBox_Profissão_Socio.Text + "', '" + DropDownList_Escolaridade_Socio.Text + "')";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Informação Registada com sucesso!');", true);

                    //Limpar dados
                    TextBox_Nome_Socio.Text = "";
                    TextBox_DataNascimento_Socio.Text = "";
                    TextBox_Morada_Socio.Text = "";
                    TextBox_Nacionalidade_Socio.Text = "";
                    TextBox_Naturalidade_Socio.Text = "";
                    DropDownList_EstadoCivil_Socio.ClearSelection();
                    Textbox_NumeroFilhos_Socio.Text = "";
                    Textbox_Agregado_Socio.Text = "";
                    TextBox_Profissão_Socio.Text = "";
                    DropDownList_Escolaridade_Socio.ClearSelection();
                    TextBox_Procurar.Text = "";
                }
                catch (Oracle.ManagedDataAccess.Client.OracleException)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Utente com dados já inseridos!');", true);
                }      
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Por favor, preencha todos os campos!');", true);
            }
        }
    }
}