using System;

namespace Dominio.Notificaciones
{
    public sealed class Notificacion
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Autor { get; set; }
    }
}
