using Dominio.Modelo;

using System.Collections.Generic;

namespace Dominio.Repositorio
{
    public sealed class RepoNotificacion : IRepo<Notificacion>
    {
        public bool Insertar(Notificacion entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = @"insert into notificacion (texto, fecha_inicio, fecha_fin, autor) 
				values (@Texto, @FechaInicio, @FechaFin, @Autor)";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Editar(Notificacion entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = @"
				update notificacion set texto = @Texto, fecha_inicio = @FechaInicio, fecha_fin = @FechaFin, autor = @Autor
				where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Notificacion entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "delete from notificacion where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Notificacion> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Notificacion>("select * from notificacion");
        }
        public Notificacion PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from notificacion where id = @id";
            return conexion.Obtener<Notificacion>(consulta, new { id });
        }
    }
}
