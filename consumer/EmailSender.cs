using System.Net;
using System.Net.Mail;

namespace consumer
{
    static class EmailSender
    {
        public static void Send(string to, string message)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            NetworkCredential credential = new NetworkCredential("dogukanergezer@gmail.com", "xxxxxx");
            smtpClient.Credentials = credential;

            MailAddress sender = new MailAddress("dogukanergezer@gmail.com", "Doğukan Ergezer");
            MailAddress receiver = new MailAddress(to);

            MailMessage mail = new MailMessage(sender, receiver);
            mail.Subject = "Example";
            mail.Body = message;

            smtpClient.Send(mail);
        }

    }
}