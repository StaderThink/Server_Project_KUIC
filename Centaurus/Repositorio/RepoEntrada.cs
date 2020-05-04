using Centaurus.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Repositorio {
	public sealed class RepoEntrada: IRepo<Entrada> {
		public bool Insertar(Entrada entidad) {
			using var conexion = new Conexion();
			var consulta = "insert into entrada (fecha, observacion) values (@Fecha, @Observacion)";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Editar(Entrada entidad) {
			using var conexion = new Conexion();
			var consulta = @"
				update entrada set fecha = @Fecha, observacion = @Observacion
				where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public bool Eliminar(Entrada entidad) {
			using var conexion = new Conexion();
			var consulta = "delete from entrada from id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}
		public IEnumerable<Entrada> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<Entrada>("select * from entrada");
		}
		public Entrada PorId(int id) {
			using var conexion = new Conexion();
			var consulta = "select * from entrada where id = @id";
			return conexion.Obtener<Entrada>(consulta, new { id });
		}
	}
}
