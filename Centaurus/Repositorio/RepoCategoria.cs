using Centaurus.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Repositorio {
	public sealed class RepoCategoria: IRepo<Categoria> {
		public bool Insertar (Categoria entidad) {
			using var conexion = new Conexion();

			var consulta = "insert into categoria (nombre, descripcion) values (@Nombre, @Descripcion)";

			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}

		public bool Editar(Categoria entidad) {
			using var conexion = new Conexion();
			var consulta = @"
				update categoria set nombre = @Nombre, descripcion = @Descripcion
				where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Eliminar(Categoria entidad) {
			using var conexion = new Conexion();

			var consulta = "delete from categoria from id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public IEnumerable<Categoria> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<Categoria>("select * from categoria");
		}
		public Categoria PorId(int id) {
			using var conexion = new Conexion();
			var consulta = "select * from categoria where id = @id";

			return conexion.Obtener<Categoria>(consulta, new { id });
		}
	}
}
