using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.Mail;
using System.Globalization;
using System.Net;

namespace scrum_Grupo2_website
{
    public class Registo
    {
        bool invalid = false;

        //Função para verificar email
        public bool verificarEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid email format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
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

        public void EnviarEmail(string email, string nome, string password)
        {
            var client = new MailMessage();
            var smtp = new SmtpClient("smtp.gmail.com");
            client.From = new MailAddress("scrumgrupo2@gmail.com");
            client.To.Add(email);
            client.Subject = "Confirmação Registo";
            client.Body = "Estimado Sr(a). " + nome + "\nObrigado pelo seu registo, enviamos a sua password: " + password + "\nPor favor na próxima sessão proceda à sua alteração.\n\nCom os melhores cumprimentos Equipa Scrum2";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("scrumgrupo2@gmail.com", "grupo2scrum123");
            smtp.EnableSsl = true;

            smtp.Send(client);
        }
    }
}