using Dominio.Modelo;

using System.Collections.Generic;

namespace Dominio.Repositorio {
    public sealed class RepoUsuario : IRepo<Usuario> {
        public bool Editar(Usuario entidad) {
            using Conexion conexion = new Conexion();

            string consulta = @$"
				update usuario set
					nombre = @Nombre,
					apellido = @Apellido,
					correo = @Correo,
					clave = @Clave,
					telefono = @Telefono,
					actualizado = curdate(),
                    cargo = @Cargo
					activo = @Activo
				where id = @Id
			";

            Usuario temporal = PorId(entidad.Id);

            if (entidad.Clave == null) {
                entidad.Clave = temporal.Clave;
            }

            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }

        public bool Eliminar(Usuario entidad) {
            using Conexion conexion = new Conexion();

            string consulta = "delete from usuario where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);

            return filasAfectadas > 0;
        }

        public bool Insertar(Usuario entidad) {
            using Conexion conexion = new Conexion();

            string consulta = @$"
				call crear_usuario(
					@Nombre, @Apellido, @Documento, '{entidad.TipoDocumento}',
					@Expedicion, @Correo, @Clave, @Cargo, @Telefono, @Nacimiento
				)
			";

            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }

        public IEnumerable<Usuario> Listar() {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Usuario>("select * from usuario");
        }

        public Usuario PorId(int id) {
            using Conexion conexion = new Conexion();
            string consulta = "select * from usuario where id = @id";

            return conexion.Obtener<Usuario>(consulta, new { id });
        }

        public Usuario PorDocumento(string documento) {
            using Conexion conexion = new Conexion();
            string consulta = "select * from usuario where documento = @documento";

            return conexion.Obtener<Usuario>(consulta, new { documento });
        }
    }
}
