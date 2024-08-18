using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MimeKit.Text;

namespace AngularNetApi.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task SendEmailAsync(string emailRecipient, string subject, string htmlMessage)
        {
            try
            {
                var Sender = _config.GetSection("Email")["Sender"];
                var Password = _config.GetSection("Email")["Password"];

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(Sender));
                email.To.Add(MailboxAddress.Parse(emailRecipient));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };
                using (var emailClient = new SmtpClient())
                {
                    await emailClient.ConnectAsync(
                        "smtp.gmail.com",
                        587,
                        MailKit.Security.SecureSocketOptions.StartTls
                    );
                    await emailClient.AuthenticateAsync(Sender, Password);
                    await emailClient.SendAsync(email);
                    await emailClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
