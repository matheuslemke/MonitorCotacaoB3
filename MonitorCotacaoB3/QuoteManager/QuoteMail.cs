using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace QuoteManager
{
    public class QuoteMail
    {
        private readonly System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private string emailFrom;
        private string emailTo;
        private SmtpClient client;

        public QuoteMail()
        {
            ReadEmailsFromSettings();
            ReadSMTPFromSettings();
        }

        public void SendMail(string subject, string message)
        {
            try
            {
                client.Send(emailFrom, emailTo, subject, message);
                Console.WriteLine($"Email \"{subject}\" enviado.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ReadEmailsFromSettings()
        {
            emailFrom = appSettings["MailFrom"];
            emailTo = appSettings["MailTo"];
        }

        private void ReadSMTPFromSettings()
        {
            string host = appSettings["SMTPHost"];
            int port = int.Parse(appSettings["SMTPPort"]);
            string user = appSettings["SMTPUser"];
            string password = appSettings["SMTPPassword"];

            client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, password),
                EnableSsl = true
            };
        }
    }
}
