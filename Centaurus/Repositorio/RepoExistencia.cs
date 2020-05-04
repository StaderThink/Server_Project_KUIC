using Centaurus.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Repositorio {
	public sealed class RepoExistencia: IRepo<Existencia> {
		public bool Insertar(Existencia entidad) {
			using var conexion = new Conexion();
			var consulta = "insert into existencia (producto, cantidad) values (@Producto, @Cantidad)";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Editar(Existencia entidad) {
			using var conexion = new Conexion();
			var consulta = @"
				update existencia set producto = @Producto, cantidad = @Cantidad
				where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Eliminar(Existencia entidad) {
			using var conexion = new Conexion();
			var consulta = "delete from existencia from id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public IEnumerable<Existencia> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<Existencia>("select * from existencia");
		}
		public Existencia PorId(int id) {
			using var conexion = new Conexion();
			var consulta = "select * from existencia where id = @id";
			return conexion.Obtener<Existencia>(consulta, new { id });
		}
	}
}
