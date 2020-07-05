using System;

namespace Dominio.Entradas
{
    public sealed class Entrada
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Observacion { get; set; }
    }
}
