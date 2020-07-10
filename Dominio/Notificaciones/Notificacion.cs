using System;

namespace Dominio.Notificaciones
{
    public sealed class Notificacion
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime FechaFin { get; set; } = DateTime.Now.AddDays(10);
        public int Autor { get; set; }
    }
}
