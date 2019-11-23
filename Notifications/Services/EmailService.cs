using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Notifications.Services
{
    public interface IEmailService
    {
        void Send(string receiver, string subject, string body);
    }
    public class EmailService : IEmailService
    {
        public void Send(string receiverUid, string subject, string body)
        {
            Debug.WriteLine($"Sending email to {receiverUid} with subject {subject}");

            using (var msg = new MailMessage("mwj7@aber.ac.uk", $"{receiverUid}@aber.ac.uk"))
            {
                msg.Subject = "Siarad: " + subject;
                msg.Body = body;
                using (SmtpClient sc = new SmtpClient())
                {
                    sc.EnableSsl = true;
                    sc.Host = "smtp.office365.com";
                    sc.Port = 587;
                    sc.Credentials = new NetworkCredential("mwj7@aber.ac.uk", "qh76T423");
                    sc.Send(msg);
                }
            }
        }
    }
}
