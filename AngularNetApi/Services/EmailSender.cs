using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MimeKit.Text;

namespace AngularNetApi.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration configuration)
        {
            _config = configuration;
        }

        public Task SendEmailAsync(string emailAddress, string subject, string htmlMessage)
        {
            try
            {
                var Sender = _config.GetSection("Email")["Sender"];
                var Password = _config.GetSection("Email")["Password"];

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(Sender));
                email.To.Add(MailboxAddress.Parse(emailAddress));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };
                using (var emailClient = new SmtpClient())
                {
                    emailClient.Connect(
                        "smtp.gmail.com",
                        587,
                        MailKit.Security.SecureSocketOptions.StartTls
                    );
                    emailClient.Authenticate(Sender, Password);
                    emailClient.Send(email);
                    emailClient.Disconnect(true);
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
