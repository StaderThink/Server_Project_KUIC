using Centaurus.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centaurus.Repositorio {
	public sealed class RepoCargo: IRepo<Cargo> {
		public bool Editar(Cargo entidad) {
			using var conexion = new Conexion();
			return conexion.Editar(entidad);
		}

		public bool Eliminar(Cargo entidad) {
			using var conexion = new Conexion();
			return conexion.Eliminar(entidad);
		}

		public bool Insertar(Cargo entidad) {
			using var conexion = new Conexion();
			return conexion.Insertar(entidad);
		}

		public IEnumerable<Cargo> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<Cargo>("select * from cargo");
		}

		public Cargo PorId(int id) {
			using var conexion = new Conexion();
			return conexion.Obtener<Cargo>($"select * from cargo where id = @id", new { id });
		}
	}
}
