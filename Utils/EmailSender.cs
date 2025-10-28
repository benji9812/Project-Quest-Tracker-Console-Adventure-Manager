using System.Net;
using System.Net.Mail;

namespace Project_Quest_Tracker___Console_Adventure_Manager.Utils
{
    public class EmailSender // Enkel e-postsändare via SMTP (Gmail)
    {
        public static void SendEmail(string toAddress, string subject, string body) // Skicka e-post
        {
            var smtpClient = new SmtpClient("smtp.gmail.com") // Använd din Gmail SMTP-server
            {
                Port = 587,
                Credentials = new NetworkCredential("DIN_GMAIL@gmail.com", "DITT_GMAIL_APP_PASSWORD"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage // Skapa e-postmeddelande
            {
                From = new MailAddress("DIN_GMAIL@gmail.com"), // Avsändaradress
                Subject = subject,
                Body = body,
            };
            mailMessage.To.Add(toAddress); // Lägg till mottagare

            smtpClient.Send(mailMessage); // Skicka e-post
        }
    }
}

