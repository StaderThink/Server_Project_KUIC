using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Modelo {
	public sealed class DetalleEntrada: IEntidad {
		public int Id { get; set; }
		[Required] public int Cantidad { get; set; }
		[Required] public int Entrada { get; set; }
		[Required] public int Producto { get; set; }
	}
}
