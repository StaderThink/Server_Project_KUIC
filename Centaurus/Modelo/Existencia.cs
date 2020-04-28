namespace Centaurus.Modelo {
	public sealed class Existencia: IEntidad {
		public int Id { get; set;  }
		public int Producto { get; set; }
		public int Cantidad { get; set; }
	}
}
