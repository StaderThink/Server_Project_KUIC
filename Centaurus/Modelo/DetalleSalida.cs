using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Modelo {
	public sealed class DetalleSalida: IEntidad {
		public int Id { get; set; }
		public int Cantidad { get; set; }
		public int Salida { get; set; }
		public int Producto { get; set; }
	}
}
