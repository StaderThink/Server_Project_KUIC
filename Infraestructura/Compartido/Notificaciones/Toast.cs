using System;

namespace Infraestructura.Compartido.Notificaciones
{
    public sealed class Toast
    {
        public Guid Id { get; set; }
        public ToastType Type { get; set; }
        public string Body { get; set; }
    }
}
