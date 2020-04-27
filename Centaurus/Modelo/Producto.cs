using Dapper;
using System;
using System.Text.Json.Serialization;

namespace Centaurus.Modelo {
	public sealed class Producto: IEntidad {
		public int Id { get; set; }
		[Required] public string Nombre { get; set; }
		[Required] public string Descripcion { get; set; }
		[Required] public int Codigo { get; set; }
		[Required] public double Precio { get; set; }
		[Required] public string MinCantidad { get; set; }
		[Required] public double MinPeso { get; set; }
		[Required] public double MaxPeso { get; set; }
		[Required] public Magnitud Magnitud { get; set; }
		[Required] public Presentacion Presentacion { get; set; }
		[Required] public int Categoria { get; set; }
	}
}
