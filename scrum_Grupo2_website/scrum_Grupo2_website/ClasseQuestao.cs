using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace scrum_Grupo2_website
{
    public class ClasseQuestao
    {
        public void QuestaoTextBox(System.Web.UI.WebControls.Panel novoPanel, string questao)
        {
            TextBox novaTextBox = new TextBox();
            novaTextBox.ID = questao;
            novaTextBox.Text = "";
            novoPanel.Controls.Add(novaTextBox);
        }

        public void QuestaoMultiplaSimNao(System.Web.UI.WebControls.Panel novoPanel, string questao)
        {
            DropDownList novaDrop = new DropDownList();
            novaDrop.ID = questao;
            novaDrop.Items.Add("Sim");
            novaDrop.Items.Add("Não");
            novoPanel.Controls.Add(novaDrop);
        }

        public void QuestaoMultiplaFrequencia(System.Web.UI.WebControls.Panel novoPanel, string questao)
        {
            DropDownList novaDrop = new DropDownList();
            novaDrop.ID = questao;
            novaDrop.Items.Add("Nunca");
            novaDrop.Items.Add("Raramente");
            novaDrop.Items.Add("Algumas vezes");
            novaDrop.Items.Add("Quase sempre");
            novaDrop.Items.Add("Sempre");
            novoPanel.Controls.Add(novaDrop);
        }

        public void QuestaoMultiplaSatisfacao(System.Web.UI.WebControls.Panel novoPanel, string questao)
        {
            DropDownList novaDrop = new DropDownList();
            novaDrop.ID = questao;
            novaDrop.Items.Add("Nada Satisfeito");
            novaDrop.Items.Add("Pouco Satisfeito");
            novaDrop.Items.Add("Satisfeito");
            novaDrop.Items.Add("Muito Satisfeito");
            novaDrop.Items.Add("Completamente Satisfeito");
            novoPanel.Controls.Add(novaDrop);
        }
    }
}