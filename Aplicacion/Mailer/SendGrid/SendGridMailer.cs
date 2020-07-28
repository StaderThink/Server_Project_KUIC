using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Aplicacion.Mailer.SendGrid
{
    internal sealed class SendGridMailer : IMailer
    {
        public async Task Send(string receiver, string subject, string content)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("aureliaproyecto@gmail.com", "Notificaciones Aurelia");
            var to = new EmailAddress(receiver);
            var message = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            await client.SendEmailAsync(message);
        }
    }
}
