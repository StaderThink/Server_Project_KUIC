using Dominio.Modelo;

using System.Collections.Generic;

namespace Dominio.Repositorio {
    public sealed class RepoDetalleSalida : IRepo<DetalleSalida> {
        public bool Insertar(DetalleSalida entidad) {
            using Conexion conexion = new Conexion();
            string consulta = "insert into detalle_salida (cantidad, salida, producto) values (@Cantidad, @Salida, @Producto)";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Editar(DetalleSalida entidad) {
            using Conexion conexion = new Conexion();
            string consulta = @"
				update detalle_salida set cantidad = @Cantidad, salida = @Salida, producto = @Producto
				where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(DetalleSalida entidad) {
            using Conexion conexion = new Conexion();
            string consulta = "delete from detalle_salida where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<DetalleSalida> Listar() {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<DetalleSalida>("select * from detalle_salida");
        }
        public DetalleSalida PorId(int id) {
            using Conexion conexion = new Conexion();
            string consulta = "select * from detalle_salida where id = @id";
            return conexion.Obtener<DetalleSalida>(consulta, new { id });
        }
    }
}
