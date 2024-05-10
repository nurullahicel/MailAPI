using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    static public class Emailsender
    {
        public static void Send(string to ,string message)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            NetworkCredential credential = new NetworkCredential("nurullahiceltest@gmail.com", "ejok awkc bqdm acsv");
            smtpClient.Credentials = credential;

            MailAddress sender = new MailAddress("nurullahiceltest@gmail.com","Nurullah İÇEL");
            MailAddress receiver = new MailAddress(to);

            MailMessage mail=new MailMessage(sender,receiver);
            mail.Subject = "example";
            mail.Body = message;
            
            smtpClient.Send(mail);

        }
    }
}
