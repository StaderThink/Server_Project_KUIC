using Dominio.Modelo;
using System.Collections.Generic;

namespace Dominio.Repositorio
{
    public sealed class RepoDetallePedido : IRepo<DetallePedido>
    {
        public bool Editar(DetallePedido entidad)
        {
            using var conexion = new Conexion();

            var consulta = $@"
                update detalle_pedido set 
                    pedido = @Pedido,
                    cantidad = @Cantidad,
                    producto = @Producto

                    where id = @Id
           ";
            var filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(DetallePedido entidad)
        {
            using var conexion = new Conexion();

            var consulta = "delete from detalle_pedido where id = @Id";
            var filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Insertar(DetallePedido entidad)
        {
            using var conexion = new Conexion();
            var consulta = $@"insert into (pedido, cantidad, producto) 
                            values (@Pedido, @Cantidad, @Producto)";
            var filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<DetallePedido> Listar()
        {
            using var conexion = new Conexion();
            return conexion.Seleccionar<DetallePedido>("select * from detalle_pedido");
        }

        public DetallePedido PorId(int id)
        {
            using var conexion = new Conexion();
            var consulta = "select * from detalle_pedido where id = @id";

            return conexion.Obtener<DetallePedido>(consulta, new { id });
        }
    }
}
