using System;
namespace Dominio.Pedidos
{
    public sealed class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Cliente { get; set; }
        public int Asesor { get; set; }
        public Estado Estado { get; set; }
        public bool Activo { get; set; }
        public double Descuento { get; set; }
        public string Observacion { get; set; }
    }
}
