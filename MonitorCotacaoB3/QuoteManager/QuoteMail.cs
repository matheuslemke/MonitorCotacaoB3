using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace QuoteManager
{
    public class QuoteMail
    {
        private readonly System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private readonly string emailFrom;
        private readonly string emailTo;
        private readonly SmtpClient client;

        public QuoteMail()
        {
            emailFrom = appSettings["MailFrom"];
            emailTo = appSettings["MailTo"];

            string host = appSettings["SMTPHost"];
            int port = Int32.Parse(appSettings["SMTPPort"]);
            string user = appSettings["SMTPUser"];
            string password = appSettings["SMTPPassword"];

            client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, password),
                EnableSsl = true
            };
        }

        public void SendMail(string subject, string message)
        {
            try
            {
                client.Send(emailFrom, emailTo, subject, message);
                Console.WriteLine($"Email {subject} sent");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
