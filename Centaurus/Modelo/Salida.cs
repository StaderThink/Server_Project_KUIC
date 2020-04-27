using System;
using Dapper;

namespace Centaurus.Modelo {
	public sealed class Salida:IEntidad {
		public int Id { get; set; }
		[Required] public DateTime fecha { get; set; }
		public string Observacion { get; set; }
		[Required] public int Pedido { get; set; }
	}
}
