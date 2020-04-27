using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Modelo {
	public sealed class Notificacion: IEntidad {
		public int Id { get; set; }
		[Required] public string Texto { get; set; }
		[Required] public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		[Required] public int Autor { get; set; }
	}
}
