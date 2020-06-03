using System;

namespace Dominio.Modelo {
	public sealed class Entrada: IEntidad {
		public int Id { get; set; }
		public DateTime Fecha { get; set; }
		public string Observacion { get; set; }
	}
}
