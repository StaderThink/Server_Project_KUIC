using Centaurus.Modelo;
using System;
using System.Collections.Generic;

namespace Centaurus.Repositorio {
	public sealed class RepoCargo: IRepo<Cargo> {
		public bool Editar(Cargo entidad) {
			throw new NotImplementedException();
		}

		public bool Eliminar(Cargo entidad) {
			throw new NotImplementedException();
		}

		public bool Insertar(Cargo entidad) {
			throw new NotImplementedException();
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
