using Dominio.Notificaciones;
using System.Collections.Generic;

namespace Aplicacion.Notificaciones
{
    public sealed class FormularioRegistrarNotificacion
    {
        public Notificacion Notificacion { get; set; }
        public IEnumerable<Segmento> Segmentos { get; set; }
    }
}
