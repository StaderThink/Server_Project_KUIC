using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Modelo {
	public sealed class DetalleEntrada: IEntidad {
		public int Id { get; set; }
		public int Cantidad { get; set; }
		public int Entrada { get; set; }
		public int Producto { get; set; }
	}
}
