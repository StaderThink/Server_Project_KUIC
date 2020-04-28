using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Modelo {
	public sealed class Segmento: IEntidad {
		public int Id { get; set; }
		public int Notificacion { get; set; }
		public int Cargo { get; set; }
	}
}
