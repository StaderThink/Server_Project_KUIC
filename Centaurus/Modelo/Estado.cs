using System;
namespace Centaurus.Modelo
{
	public sealed class Estado : IEntidad
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int Orden { get; set; }
		public bool Cancelable { get; set; }
	}
}