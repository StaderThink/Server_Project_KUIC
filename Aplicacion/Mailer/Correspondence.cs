using Aplicacion.Mailer.SendGrid;
using Dominio.Usuarios;

namespace Aplicacion.Mailer
{
    public sealed class Correspondence
    {
        private readonly IMailer mailer;

        public Correspondence()
        {
            this.mailer = new SendGridMailer();
        }

        public void SendPasswordChange(Usuario usuario)
        {
            string content = $"Hola {usuario.Nombre}, hemos registrado el cambio de tu contraseña hace menos de una hora, si no has sido tu, contacta con el administrador del sistema.";

            mailer.Send(usuario.Correo, "Cambio de contraseña", content);
        }
    }
}
