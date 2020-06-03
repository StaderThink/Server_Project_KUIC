using Dominio.Modelo;
using System.Collections.Generic;

namespace Dominio.Repositorio {
	public sealed class RepoSalida: IRepo<Salida> {
		public bool Insertar(Salida entidad) {
			using var conexion = new Conexion();
			var consulta = "insert into salida (fecha, observacion, pedido) values (@Fecha, @Observacion, @Pedido)";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Editar(Salida entidad) {
			using var conexion = new Conexion();
			var consulta = @"
				update salida set fecha = @Fecha, observacion = @Observacion, pedido = @Pedido
				where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Eliminar(Salida entidad) {
			using var conexion = new Conexion();
			var consulta = "delete from salida where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public IEnumerable<Salida> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<Salida>("select * from salida");
		}
		public Salida PorId(int id) {
			using var conexion = new Conexion();
			var consulta = "select * from salida where id = @id";
			return conexion.Obtener<Salida>(consulta, new { id });
		}
	}
}
