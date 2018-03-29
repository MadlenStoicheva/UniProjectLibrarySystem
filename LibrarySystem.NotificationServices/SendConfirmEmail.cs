using LibrarySystem.RelationServices.Domain.User;
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
        
        //private string confirmationEmailUrl = "http://localhost:60585/Users/ValidateEmail";
        private string confirmationEmailUrl = "http://newlibraryproject.apphb.com/Users/ValidateEmail";

        public void SendConfirmationEmailAsync(User user)
        {
            string callbackUrl = $"{confirmationEmailUrl}?userId={user.Id}&validationCode={user.ValidationCode}";
            string link = $"<a href='{ callbackUrl}'>here</a>!";
            SendConfirmationEmail(user.Email, "Madlen Library registration request", $"To confirm your account click  -> {link}");
        }

        public void SendConfirmationEmail(string email, string name, string comment)
        {

            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("madlen.stoycheva23@gmail.com", "skullhong23")
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("madlen.stoycheva23@gmail.com")
                };
                mailMessage.To.Add(email);
                mailMessage.Body = comment;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = name;
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
