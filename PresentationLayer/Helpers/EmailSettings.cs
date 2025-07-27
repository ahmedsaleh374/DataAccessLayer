using System.Net;
using System.Net.Mail;

namespace PresentationLayer.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email) 
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ms6803780@gmail.com", "pakrpzxxrcllubtp");
            client.Send("ms6803780@gmail.com",email.to,email.subject,email.body);
        }
    }
}
