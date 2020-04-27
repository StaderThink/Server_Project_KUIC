using Centaurus.Modelo;
using System.Collections.Generic;

namespace Centaurus.Repositorio {
	public sealed class RepoUsuario: IRepo<Usuario> {
		public bool Insertar(Usuario entidad) {
			using var conexion = new Conexion();
			return conexion.Insertar(entidad);
		}

		public bool Editar(Usuario entidad) {
			using var conexion = new Conexion();
			return conexion.Editar(entidad);
		}

		public bool Eliminar(Usuario entidad) {
			using var conexion = new Conexion();
			return conexion.Eliminar(entidad);
		}

		public IEnumerable<Usuario> Listar() {
			using var conexion = new Conexion();
			return conexion.Listar<Usuario>();
		}
	}
}
