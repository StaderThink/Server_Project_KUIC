using System;

namespace Centaurus.Modelo {
	public sealed class Notificacion: IEntidad {
		public int Id { get; set; }
		public string Texto { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public int Autor { get; set; }
	}
}
