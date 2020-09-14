using Aplicacion.Mailer.SendGrid;
using Dominio.Clientes;
using Dominio.Usuarios;

namespace Aplicacion.Mailer
{
    public sealed class Correspondence
    {
        private readonly IMailer mailer;

        public Correspondence()
        {
            mailer = new SendGridMailer();
        }

        public void SendPasswordChange(Usuario usuario)
        {
            string content = $"<strong>Hallo {usuario.Nombre}</strong>! Hemos registrado el cambio de tu contraseña hace menos de una hora, si no has sido tu, contacta con el encargado de recursos humanos en el sistema.";

            mailer.SendHtml(usuario.Correo, "Cambio de contraseña", content);
        }

        public void SendDefaultPassword(Usuario usuario)
        {
            string html = $"¡Te damos la bienvenida a tu nueva amiga, <strong>Aurelia</strong>! estamos a la espera de como podemos mejorar tus actividades. Estamos aquí para informarte que has sido registrada satisfactoriamente en nuestro sistema y para poder iniciar sesión debes utilizar tu número de documento (CC o NIT) y tu contraseña es <strong>{usuario.Clave}</strong> (esta clave fue generada por el sistema y puedes cambiarla en cualquier momento).<br>Recuerda, tu contraseña es: <strong>{usuario.Clave}</strong><br><br>Este mensaje fue enviado con amor, por el equipo de desarrollo de Aurelia.";

            mailer.SendHtml(usuario.Correo, "Te damos la bienvenida", html);
        }

        public void SendMessageClient(Cliente cliente)
        {
            string html = $"¡Te damos la bienvenida a tu nueva amiga, <strong>Aurelia.</strong>!<br> <strong>{cliente.Nombre}</strong> estamos aquí para informarte que has sido registrado satisfactoriamente en nuestro sistema, infórmale a tu asesor de ventas para empezar a realizar tus futuros pedidos.<br>Este mensaje fue enviado con amor, por el equipo de desarrollo de Aurelia.";
            mailer.SendHtml(cliente.Correo, "Te damos la bienvenida", html);

        }
    }
}
