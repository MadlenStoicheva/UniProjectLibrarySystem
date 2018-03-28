using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.NotificationServices
{
    public class SendConfirmEmail
    {


        public void SendConfirmationEmail(string email, string name, string comment)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("madlen.stoicheva@gmail.com", "skullhong9723")
                };

                StringBuilder message = new StringBuilder();
                MailAddress from = new MailAddress(email.ToString());
                message.Append("Name: " + name + "\n");
                message.Append("Email: " + email + "\n");
                message.Append("Telephone: " + comment + "\n\n");
                message.Append(message);

                MailMessage mailMessage = new MailMessage
                {
                    From = from
                };
                mailMessage.To.Add("madlen.stoicheva@gmail.com");
                mailMessage.Body = message.ToString();
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Test enquiry from " + name;
                client.EnableSsl = true;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //TODO: Log the exception
                throw new ApplicationException($"Unable to load : '{ex.Message}'.");
            }
        }
    }
}
