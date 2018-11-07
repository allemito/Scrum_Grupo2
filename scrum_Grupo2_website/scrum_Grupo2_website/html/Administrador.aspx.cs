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
            ButtonEditar.Visible = false;
            ButtonRemover.Visible = false;
            labelProcurar.Text = "";
        }

        protected void ButtonEditar_Click(object sender, EventArgs e)
        {
         
        }

        protected void ButtonRemover_Click(object sender, EventArgs e)
        {
           
        }

        protected void ButtonProcurar_Click(object sender, EventArgs e)
        {
            if (TextBox_Procurar.Text == "a")
            {
                panelDoente.Visible = true;
                panelMedico.Visible = false;
                ButtonEditar.Visible = true;
                ButtonRemover.Visible = true;
            }
            else
            {
                labelProcurar.Text = "Não encontrado!";
            }
            TextBox_Procurar.Text = "";
        }
    }
}