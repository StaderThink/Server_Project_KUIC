using Dominio.Modelo;

using System.Collections.Generic;

namespace Dominio.Repositorio
{
    public sealed class RepoExistencia : IRepo<Existencia>
    {
        public bool Insertar(Existencia entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "insert into existencia (producto, cantidad) values (@Producto, @Cantidad)";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Editar(Existencia entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = @"
				update existencia set producto = @Producto, cantidad = @Cantidad
				where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Existencia entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "delete from existencia where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Existencia> Listar()
        {
            using Conexion conexion = new Conexion();
            var listado = conexion.Seleccionar<Existencia>("select * from existencia");
            return listado;
        }
        public Existencia PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from existencia where id = @id";
            return conexion.Obtener<Existencia>(consulta, new { id });
        }

        public Existencia PorProducto(int producto)
        {
            using var conexion = new Conexion();
            string consulta = "select * from existencia where producto = @producto";

            return conexion.Obtener<Existencia>(consulta, new { producto });
        }
    }
}
