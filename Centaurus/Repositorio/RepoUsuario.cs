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
			return conexion.Seleccionar<Usuario>("select * from usuario");
		}

		public Usuario PorId(int id) {
			using var conexion = new Conexion();

			var consulta = "select * from usuario where id = @id";
			var resultado = conexion.Obtener<Usuario>(consulta, new { id });

			return resultado;
		}

		public Usuario PorDocumento(string documento) {
			using var conexion = new Conexion();

			var consulta = "select * from usuario where documento = @documento";
			var resultado = conexion.Obtener<Usuario>(consulta, new { documento });

			return resultado;
		}
	}
}
