using Dominio.Modelo;
using System.Collections.Generic;

namespace Dominio.Repositorio {
	public sealed class RepoUsuario: IRepo<Usuario> {
		public bool Editar(Usuario entidad) {
			using var conexion = new Conexion();

			var consulta = @$"
				update usuario set
					nombre = @Nombre,
					apellido = @Apellido,
					correo = @Correo,
					clave = @Clave,
					telefono = @Telefono,
					actualizado = curdate(),
					activo = @Activo
				where id = @Id
			";

			var temporal = PorId(entidad.Id);

			if (entidad.Clave == null) {
				entidad.Clave = temporal.Clave;
			}

			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}

		public bool Eliminar(Usuario entidad) {
			using var conexion = new Conexion();

			var consulta = "delete from usuario where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);

			return filasAfectadas > 0;
		}

		public bool Insertar(Usuario entidad) {
			using var conexion = new Conexion();

			var consulta = @$"
				call crear_usuario(
					@Nombre, @Apellido, @Documento, '{entidad.TipoDocumento}',
					@Expedicion, @Correo, @Clave, @Cargo, @Telefono, @Nacimiento
				)
			";

			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}

		public IEnumerable<Usuario> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<Usuario>("select * from usuario");
		}

		public Usuario PorId(int id) {
			using var conexion = new Conexion();
			var consulta = "select * from usuario where id = @id";

			return conexion.Obtener<Usuario>(consulta, new { id });
		}

		public Usuario PorDocumento(string documento) {
			using var conexion = new Conexion();
			var consulta = "select * from usuario where documento = @documento";

			return conexion.Obtener<Usuario>(consulta, new { documento });
		}
	}
}
