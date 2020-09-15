using System.Collections.Generic;

namespace Dominio.Notificaciones
{
    public sealed class RepositorioNotificacion
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
            return conexion.Seleccionar<Notificacion>("select * from notificacion orden by fecha_inicio desc");
        }
        public Notificacion PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from notificacion where id = @id";
            return conexion.Obtener<Notificacion>(consulta, new { id });
        }

        public IEnumerable<Notificacion> PorAutor (int usuario)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from notificacion where autor = @usuario";
            return conexion.Seleccionar<Notificacion>( consulta, new { usuario } );
        }

        public int PorUltimoId()
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from notificacion order by id desc limit 0, 1";
            Notificacion notificacion = conexion.Obtener<Notificacion>(consulta);

            return notificacion.Id;
        }

        public IEnumerable<Notificacion> PorCargo (int cargo)
        {
            using Conexion conexion = new Conexion();
            string consulta = @"
                select notificacion.* from notificacion
                inner join segmento on segmento.notificacion = notificacion.id
                where segmento.cargo = @cargo
            ";

            return conexion.Seleccionar<Notificacion>(consulta, new { cargo });
        }
    }
}
