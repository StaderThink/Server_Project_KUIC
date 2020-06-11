using System;

namespace Dominio.Modelo {
    public sealed class Salida : IEntidad {
        public int Id { get; set; }
        public DateTime fecha { get; set; }
        public string Observacion { get; set; }
        public int Pedido { get; set; }
    }
}
