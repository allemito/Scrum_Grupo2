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
    public partial class RegistoMedico1 : System.Web.UI.Page
    {
        OracleConnection conexao = new OracleConnection("DATA SOURCE=localhost:1521/xe;PASSWORD=scrumdatabase;USER ID=SCRUM_GRUPO2_DATABASE");
        OracleCommand comando = new OracleCommand();
        OracleDataReader dataReader;

        protected void Page_Load(object sender, EventArgs e)
        {
            comando.Connection = conexao;
        }

        protected void btn_registar_Click(object sender, EventArgs e)
        {

        }

        protected void txtbox_nome_TextChanged(object sender, EventArgs e)
        {

        }
    }
}