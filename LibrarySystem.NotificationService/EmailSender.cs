using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.NotificationService
{
    public class EmailSender
    {
        public SmtpClient Client { get; set; }

        //Send email using mailtrap.io
        public EmailSender()
        {
            Client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("d4ce53a0171ba3", "4edf8f094c2e45"),
                EnableSsl = true
            };
        }

        public void SendMail(string email, string name, string comment)
        {
            Client.Send(email, "infinite.love@abv.bg", name, comment);
        }
    }
}
