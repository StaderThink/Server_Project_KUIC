using Dominio.Modelo;

using System.Collections.Generic;

namespace Dominio.Repositorio
{
    public sealed class RepoDetalleEntrada : IRepo<DetalleEntrada>
    {
        public bool Insertar(DetalleEntrada entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "insert into detalle_entrada (cantidad, entrada, producto) values (@Cantidad, @Entrada, @Producto)";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Editar(DetalleEntrada entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = @"
				update detalle_entrada set cantidad = @Cantidad, entrada = @Entrada, producto = @Producto
				where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(DetalleEntrada entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "delete from detalle_entrada where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }

        public bool EliminarPorEntrada(Entrada entrada)
        {
            using Conexion conexion = new Conexion();
            string consulta = "delete from detalle_entrada where entrada = @idEntrada";

            int filasAfectadas = conexion.Ejecutar(consulta, new { idEntrada = entrada.Id });
            return filasAfectadas > 0;
        }

        public IEnumerable<DetalleEntrada> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<DetalleEntrada>("select * from detalle_entrada");
        }

        public IEnumerable<DetalleEntrada> ListarPorEntrada(Entrada entrada)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from detalle_entrada where entrada = @idEntrada";

            return conexion.Seleccionar<DetalleEntrada>(consulta, new { idEntrada = entrada.Id });
        }

        public DetalleEntrada PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from detalle_entrada where id = @id";
            return conexion.Obtener<DetalleEntrada>(consulta, new { id });
        }
    }
}
