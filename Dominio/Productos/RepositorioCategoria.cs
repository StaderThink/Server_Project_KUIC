using System.Collections.Generic;

namespace Dominio.Productos
{
    public sealed class RepositorioCategoria
    {
        public bool Insertar(Categoria entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = "insert into categoria (nombre, descripcion) values (@Nombre, @Descripcion)";

            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }

        public bool Editar(Categoria entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = @"
				update categoria set nombre = @Nombre, descripcion = @Descripcion
				where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Categoria entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = "delete from categoria where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Categoria> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Categoria>("select * from categoria order by nombre desc");
        }
        public Categoria PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from categoria where id = @id";

            return conexion.Obtener<Categoria>(consulta, new { id });
        }
    }
}
