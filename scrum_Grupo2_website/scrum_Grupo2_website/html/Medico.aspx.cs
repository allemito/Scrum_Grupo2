using System;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }
}