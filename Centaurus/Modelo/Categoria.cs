using Dapper;

namespace Centaurus.Modelo {
	public sealed class Categoria: IEntidad {
		public int Id { get; set; }
		[Required] public string Nombre { get; set; }
		[Required] public string Descripcion { get; set; }

	}
}
