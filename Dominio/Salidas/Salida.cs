using System;

namespace Dominio.Salidas
{
    public sealed class Salida
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public int Pedido { get; set; }
    }
}
