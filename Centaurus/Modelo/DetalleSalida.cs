using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Modelo {
	public sealed class DetalleSalida: IEntidad {
		public int Id { get; set; }
		[Required] public int Cantidad { get; set; }
		[Required] public int Salida { get; set; }
		[Required] public int Producto { get; set; }
	}
}
