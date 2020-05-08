using System;
namespace Centaurus.Modelo
{
	public sealed class Pedido : IEntidad
	{
		public int Id { get; set; }
		public DateTime Fecha { get; set; }
		public int Cliente { get; set; }
		public int Asesor { get; set; }
		public int Estado { get; set; }
		public bool Cancelado { get; set; }
		public double Descuento { get; set; }
		public string Observacion { get; set; }
    }
}
