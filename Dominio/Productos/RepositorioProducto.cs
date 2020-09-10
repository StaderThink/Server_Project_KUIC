using System.Collections.Generic;

namespace Dominio.Productos
{
    public sealed class RepositorioProducto
    {
        public bool Insertar(Producto entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = @$"
				insert into producto (nombre, descripcion, codigo, precio, min_cantidad, existencias, min_peso, max_peso, magnitud, presentacion, categoria) 
				values (
					@Nombre, @Descripcion, @Codigo, @Precio, @MinCantidad, 0,
					@MinPeso, @MaxPeso, '{entidad.Magnitud}', '{entidad.Presentacion}', @Categoria
				)
			";

            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }

        public bool Editar(Producto entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = @$"
				update producto set
                nombre = @Nombre, descripcion = @Descripcion, codigo = @Codigo,
				precio = @Precio, min_cantidad = @MinCantidad, min_peso = @MinPeso, max_peso = @MaxPeso, 
				existencias = @Existencias, magnitud = '{entidad.Magnitud}', presentacion = '{entidad.Presentacion}',
                categoria = @Categoria
				where id = @Id
            ";

            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }

        public bool Eliminar(Producto entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "delete from producto where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }

        public IEnumerable<Producto> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Producto>("select * from producto");
        }

        public Producto PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from producto where id = @id";
            return conexion.Obtener<Producto>(consulta, new { id });
        }

        public Producto PorCodigo(string codigo)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from producto where codigo = @codigo";
            return conexion.Obtener<Producto>(consulta, new { codigo });
        }

        public int UltimoPorId()
        {
            using Conexion conexion = new Conexion();

            string consulta = "select * from producto order by id desc limit 0, 1";
            Producto producto = conexion.Obtener<Producto>(consulta);

            return producto.Id;
        }
    }
}