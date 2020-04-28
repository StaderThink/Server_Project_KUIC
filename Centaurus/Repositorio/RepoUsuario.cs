using Centaurus.Modelo;
using System.Collections.Generic;
using System;

namespace Centaurus.Repositorio {
	public sealed class RepoUsuario: IRepo<Usuario> {
		public bool Insertar(Usuario entidad) {
			throw new NotImplementedException();
		}

		public bool Editar(Usuario entidad) {
			throw new NotImplementedException();
		}

		public bool Eliminar(Usuario entidad) {
			throw new NotImplementedException();
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
