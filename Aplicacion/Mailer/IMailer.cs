using System.Threading.Tasks;

namespace Aplicacion.Mailer
{
    public interface IMailer
    {
        Task Send(string receiver, string subjet, string content);
    }
}
