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
            string content = $"Hola <strong>{usuario.Nombre}</strong>, hemos registrado el cambio de tu contraseña hace menos de una hora, si no has sido tu, contacta con el administrador del sistema.";

            mailer.SendHtml(usuario.Correo, "Cambio de contraseña", content);
        }

        public void SendDefaultPassword(Usuario usuario)
        {
            string html = $"¡Te damos la bienvenida al sistema de información <strong>Aurelia</strong>! estamos a la espera de como podemos mejorar tus actividades. Estamos aquí para informarte que has sido registrada satisfactoriamente en nuestro sistema y para poder acceder debes utilizar tu documento y tu contraseña es <strong>{usuario.Clave}</strong> (esta clave fue generada por el sistema y puedes cambiarla en cualquier momento). <i>Te esperamos 🎉</i>.";

            mailer.SendHtml(usuario.Correo, "Te damos la bienvenida", html);
        }
    }
}
