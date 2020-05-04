using Centaurus.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Repositorio {
	public sealed class RepoDetalleSalida: IRepo<DetalleSalida> {
		public bool Insertar(DetalleSalida entidad) {
			using var conexion = new Conexion();
			var consulta = "insert into detalle_salida (cantidad, salida, producto) values (@Cantidad, @Salida, @Producto)";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Editar(DetalleSalida entidad) {
			using var conexion = new Conexion();
			var consulta = @"
				update detalle_salida set cantidad = @Cantidad, salida = @Salida, producto = @Producto
				where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Eliminar(DetalleSalida entidad) {
			using var conexion = new Conexion();
			var consulta = "delete from detalle_salida from id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public IEnumerable<DetalleSalida> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<DetalleSalida>("select * from detalle_salida");
		}
		public DetalleSalida PorId(int id) {
			using var conexion = new Conexion();
			var consulta = "select * from detalle_salida where id = @id";
			return conexion.Obtener<DetalleSalida>(consulta, new { id });
		}
	}
}
