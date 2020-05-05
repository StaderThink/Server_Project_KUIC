using Centaurus.Modelo;
using System.Collections.Generic;

namespace Centaurus.Repositorio {
	public sealed class RepoCargo: IRepo<Cargo> {
		public bool Editar(Cargo entidad) {
			using var conexion = new Conexion();

			var consulta = @"
				update cargo set
					nombre = @Nombre, pedidos = @Pedidos,
					usuarios = @Usuarios, logistica = @Logistica,
					clientes = @Clientes, solicitar = @Solicitar
				where id = @Id
			";

			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}

		public bool Eliminar(Cargo entidad) {
			using var conexion = new Conexion();

			var consulta = "delete from cargo where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);

			return filasAfectadas > 0;
		}

		public bool Insertar(Cargo entidad) {
			using var conexion = new Conexion();

			var consulta = @"
				insert into cargo(nombre, pedidos, usuarios, logistica, clientes, solicitar)
				values (@Nombre, @Pedidos, @Usuarios, @Logistica, @Clientes, @Solicitar)
			";

			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}

		public IEnumerable<Cargo> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<Cargo>("select * from cargo");
		}

		public Cargo PorId(int id) {
			using var conexion = new Conexion();
			var consulta = "select * from cargo where id = @id";

			return conexion.Obtener<Cargo>(consulta, new { id });
		}
	}
}
