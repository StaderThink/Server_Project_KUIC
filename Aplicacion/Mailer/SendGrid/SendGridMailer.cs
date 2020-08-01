using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Aplicacion.Mailer.SendGrid
{
    internal sealed class SendGridMailer : IMailer
    {
        public async Task SendHtml(string receiver, string subject, string html)
        {
            var key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(key);

            var from = new EmailAddress("aureliaproyecto@gmail.com", "Notificaciones Aurelia");
            var to = new EmailAddress(receiver);

            var message = MailHelper.CreateSingleEmail(from, to, subject, html, html);

            await client.SendEmailAsync(message);
        }

        public Task SendMessage(string receiver, string subject, string content)
        {
            return SendHtml(receiver, subject, content);
        }
    }
}
