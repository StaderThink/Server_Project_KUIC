using Centaurus.Modelo;
using System.Collections.Generic;

namespace Centaurus.Repositorio {
	public sealed class RepoCargo: IRepo<Cargo> {
		public bool Insertar(Cargo entidad) {
			using var conexion = new Conexion();
			return conexion.Insertar(entidad);
		}

		public bool Editar(Cargo entidad) {
			using var conexion = new Conexion();
			return conexion.Editar(entidad);
		}

		public bool Eliminar(Cargo entidad) {
			using var conexion = new Conexion();
			return conexion.Eliminar(entidad);
		}

		public IEnumerable<Cargo> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<Cargo>("select * from cargo");
		}

		public Cargo PorId(int id) {
			using var conexion = new Conexion();

			var consulta = "select * from cargo where id = @id";
			var resultado = conexion.Obtener<Cargo>(consulta, new { id });

			return resultado;
		}
	}
}
