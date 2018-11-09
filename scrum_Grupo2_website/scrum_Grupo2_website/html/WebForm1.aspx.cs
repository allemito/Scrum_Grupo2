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
using System.Data.Common;

namespace scrum_Grupo2_website.html
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Ligação Base Dados Oracle
        OracleConnection conexao = new OracleConnection("DATA SOURCE=25.15.145.193:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //int Numer_inserido = Convert.ToInt32(TextBox1.Text);
           // ObterNumero(Numer_inserido);
            //OracleConnection conexao = new OracleConnection("DATA SOURCE=localhost:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");

           
            //string sql = "SELECT ID_Doente FROM Doente";
            //OracleCommand comando = new OracleCommand(sql,conexao);
            //conexao.Open();

            //OracleDataReader dataReader = comando.ExecuteReader();

            //while (dataReader.Read())
            //{
            //    ListBox1.Items.Add(dataReader["ID_Doente"].ToString());
            //}
            //conexao.Close();


            /*

            foreach (Numero_Inserido  in )
            {

            }

            if (Numero_Inserido == Numero_Utente)
            {
                TextBox1 = ;
            }
            */

            conexao.Open();
            //comando.CommandText = "SELECT ID_Doente, Nome_Doente, Data_Nascimento_Doente, Morada, Numero_Utente, Genero, Email_Doente FROM Doente";

            TextBox1.Text = comando.CommandText = "SELECT Nome_Doente FROM Doente ";
            TextBox1.Text = Convert.ToString(comando.ExecuteScalar());
            //Label2.Text = comando.CommandText = "SELECT Nome_Doente FROM Doente";
            //Label3.Text = comando.CommandText = "SELECT  Data_Nascimento_Doente FROM Doente";
            //Label4.Text = comando.CommandText = "SELECT Morada FROM Doente";
            //Label5.Text = comando.CommandText = "SELECT Genero FROM Doente";
            //Label6.Text = comando.CommandText = "SELECT Email_Doente FROM Doente";
            conexao.Close();

        }

        private void ObterNumero(int Numero_Inserido)
        {
            string sql = "SELECT ID_Doente FROM Doente WHERE ID_Doente = @Numero_Inserido";
            OracleCommand comando = new OracleCommand(sql, conexao);
            comando.Parameters.Add("@ID_Doente", Numero_Inserido);
            conexao.Open();

            OracleDataReader dataReader = comando.ExecuteReader();


            while (dataReader.Read())
            {
                ListBox1.Items.Add(dataReader["ID_Doente"].ToString());
                ListBox1.Items.Add(dataReader["Nome_Doente"].ToString());
                ListBox1.Items.Add(dataReader["Morada"].ToString());
            }
            conexao.Close();
        }

    }
}