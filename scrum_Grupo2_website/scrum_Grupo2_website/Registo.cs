using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;


namespace scrum_Grupo2_website
{
    public class Registo
    {
        
        // Função para validar o email inserido
        public bool verificarEmail(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([.-]?[a-zA-Z0-9]+))@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)).([A-Za-z]{2,})$");

            if (rg.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Função para criar password Aleatoria
        public string CreatePassword(int lenght)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder password = new StringBuilder();
            Random aleatorio = new Random();
            while (0 < lenght--)
            {
                password.Append(valid[aleatorio.Next(valid.Length)]);
            }
            return password.ToString();
        }

        // Função para criar data nascimento para base de dados
        public string CriarNascimentoDataBase (string ano, string mes, string dia)
        {
            string dataNascimento;
            dataNascimento = ano + "." + mes + "." + dia;
            return dataNascimento;
        }

        public void EnviarEmail()
        {

        }
    }
}