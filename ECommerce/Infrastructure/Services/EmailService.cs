// EmailService.cs
using ECommerce.Application.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ECommerce.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                _settings.SenderName,
                _settings.SenderEmail));

            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _settings.Host,
                _settings.Port,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _settings.SenderEmail,
                _settings.Password);

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}