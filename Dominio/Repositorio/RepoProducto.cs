using Dominio.Modelo;
using System;
using System.Collections.Generic;

namespace Dominio.Repositorio {
	public sealed class RepoProducto: IRepo<Producto> {
		public bool Insertar(Producto entidad) {
			using var conexion = new Conexion();
			var consulta = @"insert into producto (nombre, descripcion, codigo, precio, min_cantidad, min_peso, max_peso, magnitud, presentacion, categoria) 
				values (@Nombre, @Descripcion, @Codigo, @Precio, @MinCantidad, @MinPeso, @MaxPeso, @Magnitud, @Presentacion, @Categoria)";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}

		public bool Editar(Producto entidad) {
			using var conexion = new Conexion();
			var consulta = @"
				update producto set nombre = @Nombre, descripcion = @Descripcion, codigo = @Codigo,
				precio = @Precio, min_cantidad = @MinCantidad, min_peso = @MinPeso, max_peso = @MaxPeso, 
				magnitud = @Magnitud, presentacion = @Presentacion, categoria = @Categoria
				where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}

		public bool Eliminar(Producto entidad) {
			using var conexion = new Conexion();
			var consulta = "delete from producto where id = @Id";
			var filasAfectadas = conexion.Ejecutar(consulta, entidad);
			return filasAfectadas > 0;
		}

		public IEnumerable<Producto> Listar() {
			using var conexion = new Conexion();
			return conexion.Seleccionar<Producto>("select * from producto");
		}

		public Producto PorId(int id) {
			using var conexion = new Conexion();
			var consulta = "select * from producto where id = @id";
			return conexion.Obtener<Producto>(consulta, new { id });
		}

		public Producto PorCodigo(string codigo) {
			using var conexion = new Conexion();
			var consulta = "select * from producto where codigo = @codigo";
			return conexion.Obtener<Producto>(consulta, new { codigo });
		}
	}
}