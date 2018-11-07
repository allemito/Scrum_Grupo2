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
        protected void Page_Load(object sender, EventArgs e)
        {
            panelDoente.Visible = false;
            panelMedico.Visible = false;
        }

        protected void ButtonEditar_Click(object sender, EventArgs e)
        {
          if(TextBox_Procurar.Text == "a")
            {
                panelDoente.Visible = true;
                panelMedico.Visible = false;
            }
        }

        protected void ButtonRemover_Click(object sender, EventArgs e)
        {
            
        }
    }
}