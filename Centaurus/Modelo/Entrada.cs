using Dapper;
using System;

namespace Centaurus.Modelo {
	public sealed class Entrada: IEntidad { 
		public int Id { get; set; }
		[Required] public DateTime Fecha { get; set; }
		public string Observacion { get; set; } 
	}
}
