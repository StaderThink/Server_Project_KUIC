using System;
using Dapper;

namespace Centaurus.Modelo {
	public sealed class Existencia: IEntidad {
		public int Id { get; set;  }
		[Required] public int Producto { get; set; }
		[Required] public int Cantidad { get; set; }
	}
}
