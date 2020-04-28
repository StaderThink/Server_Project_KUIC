using Dapper;
using System;

namespace Centaurus.Modelo {
	public sealed class Entrada: IEntidad { 
		public int Id { get; set; }
		public DateTime Fecha { get; set; }
		public string Observacion { get; set; } 
	}
}
