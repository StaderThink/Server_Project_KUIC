using Centaurus.Modelo;
using System.Collections.Generic;

namespace Centaurus.Repositorio {
	public sealed class RepoSegmento: IRepo<Segmento> {
		public bool Insertar(Segmento entidad) {
			using var conexion = new Conexion();
			var consulta = "insert into segmento (notificacion, cargo) values (@Notificacion, @Cargo)";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Editar(Segmento entidad) {
			using var conexion = new Conexion();
			var consulta = @"
				update segmento set notificacion = @Notificacion, cargo = @Cargo
				where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Eliminar(Segmento entidad) {
			using var conexion = new Conexion();
			var consulta = "delete from segmento from id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public IEnumerable<Segmento> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<Segmento>("select * from segmento");
		}
		public Segmento PorId(int id) {
			using var conexion = new Conexion();
			var consulta = "select * from segmento where id = @id";
			return conexion.Obtener<Segmento>(consulta, new { id });
		}
	}
}
