using Centaurus.Modelo;
using System.Collections.Generic;

namespace Centaurus.Repositorio {
	public sealed class RepoDetalleEntrada: IRepo<DetalleEntrada> {
		public bool Insertar(DetalleEntrada entidad) {
			using var conexion = new Conexion();
			var consulta = "insert into detalle_entrada (cantidad, entrada, producto) values (@Cantidad, @Entrada, @Producto)";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Editar(DetalleEntrada entidad) {
			using var conexion = new Conexion();
			var consulta = @"
				update detalle_entrada set cantidad = @Cantidad, entrada = @Entrada, producto = @Producto
				where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Eliminar(DetalleEntrada entidad) {
			using var conexion = new Conexion();
			var consulta = "delete from detalle_entrada from id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public IEnumerable<DetalleEntrada> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<DetalleEntrada>("select * from detalle_entrada");
		}
		public DetalleEntrada PorId(int id) {
			using var conexion = new Conexion();
			var consulta = "select * from detalle_entrada where id = @id";
			return conexion.Obtener<DetalleEntrada>(consulta, new { id });
		}
	}
}
