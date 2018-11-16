using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace scrum_Grupo2_website
{
    public partial class homepage : System.Web.UI.Page
    {
        // Ligação base dados oracle
        OracleConnection conexao = new OracleConnection("DATA SOURCE=25.15.145.193:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;
        Registo registo = new Registo();

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
        }

        protected void btn_registar_Click(object sender, EventArgs e)
        {
            label_evento.Text = Calendar_Evento.SelectedDate.ToShortDateString();

            // Calcular data nascimento para inserir na base de dados
            string diaMarcado = registo.CriarNascimentoDataBase(Calendar_Evento.SelectedDate.Year.ToString(), Calendar_Evento.SelectedDate.Month.ToString(), Calendar_Evento.SelectedDate.Day.ToString());

            conexao.Open();
            comando.CommandText = "INSERT INTO Calendario(Dia) VALUES('"+diaMarcado+"')";
            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}