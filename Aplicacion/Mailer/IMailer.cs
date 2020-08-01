using System.Threading.Tasks;

namespace Aplicacion.Mailer
{
    public interface IMailer
    {
        Task SendMessage(string receiver, string subjet, string content);
        Task SendHtml(string receiver, string subject, string html);
    }
}
