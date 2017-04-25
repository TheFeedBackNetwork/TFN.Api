using System.Net;
using System.Net.Mail;

namespace TFN.Infrastructure.Modules.Email
{
    public class EmailServiceOptions
    {
        public MailAddress Sender { get; set; }
        public NetworkCredential Credentials { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
    }
}