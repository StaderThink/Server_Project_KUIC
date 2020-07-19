using System.Collections.Generic;

namespace Dominio.Notificaciones
{
    public sealed class RepositorioSegmento
    {
        public bool Insertar(Segmento entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "insert into segmento (notificacion, cargo) values (@Notificacion, @Cargo)";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Editar(Segmento entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = @"
				update segmento set notificacion = @Notificacion, cargo = @Cargo
				where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Segmento entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "delete from segmento where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Segmento> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Segmento>("select * from segmento");
        }
        public Segmento PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from segmento where id = @id";
            return conexion.Obtener<Segmento>(consulta, new { id });
        }

        public bool EliminarPorNotificacion (Notificacion notificacion)
        {
            using Conexion conexion = new Conexion();
            string consulta = "delete from segmento where notificacion = @id";
            int filasAfectadas = conexion.Ejecutar(consulta, notificacion);
            return filasAfectadas > 0;
        }
    }
}
